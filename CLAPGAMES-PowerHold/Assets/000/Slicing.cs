using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Slicing : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;
    
    [Header("Slice Settings")] [Space] public Material materialSlicedSide;
    public float explosionForce;
    public float explosionRadius;
    public bool gravity, kinematic;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carrot"))
        {
            print("XtSs");
            Slicee(other);
        }
    }
    
    #region Slice Functions
    
    public void Slicee(Collider other)
    {
        other.transform.parent = null;
        
        SlicedHull sliceObject = Slice(other.gameObject, materialSlicedSide);
        
        GameObject slicedObjectUpper = sliceObject.CreateUpperHull(other.gameObject, materialSlicedSide);
        GameObject slicedObjectLower = sliceObject.CreateLowerHull(other.gameObject, materialSlicedSide);
        
        Destroy(other.gameObject);
            
        AddComponent(slicedObjectUpper);
        AddComponent(slicedObjectLower);
    }
    
    public SlicedHull Slice(GameObject _object, Material material)
    {
        return _object.Slice(transform.position, transform.up, material);
    }
    
    private void AddComponent(GameObject _object)
    {
        var bc = _object.AddComponent<BoxCollider>();
        var rb = _object.AddComponent<Rigidbody>();

        rb.useGravity = gravity;
        rb.isKinematic = kinematic;

        bc.isTrigger = true;
        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
    }

    #endregion
    
}