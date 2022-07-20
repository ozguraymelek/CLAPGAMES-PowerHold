using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer;
using BzKovSoft.ObjectSlicer.Samples;
using UnityEngine;

public class GM : Singleton<GM>
{
   public Transform _box;
   public Transform _katana;

   public void Move(float x)
   {
      var pos = _box.position;
      pos.x = x;
      _box.position = pos;
   }

   public void MoveKatana(float y)
   {
      var pos = _katana.position;
      pos.y = y;
      _katana.position = pos;
   }
   public void Cut(GameObject target)
   {
      var sliceable = target.GetComponent<ObjectSlicerSample>();
      if (sliceable == null)
      {
         return;
      }

      Plane plane = new Plane(Vector3.right, 0f);
      sliceable.Slice(plane,null);
      print(sliceable.name);
      sliceable.GetComponent<Rigidbody>().isKinematic = false;
   }
}
