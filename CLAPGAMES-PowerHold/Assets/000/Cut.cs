using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cut : Singleton<Cut>
{
    [Header("Components")] internal Material _material;
    internal GameObject willCutObj;

    [Header("Settings")] public bool cuttedd = false;
    public void Slice()
    {
        SlicedHull cutted = SlicedH(willCutObj, _material);
        
        GameObject cuttedUp = cutted.CreateUpperHull(willCutObj, _material);
        cuttedUp.layer = 7;
        cuttedUp.AddComponent<BoxCollider>();
        var rbCU = cuttedUp.AddComponent<Rigidbody>();
        
        GameObject cuttedLow = cutted.CreateLowerHull(willCutObj, _material);
        cuttedLow.layer = 7;
        cuttedLow.AddComponent<BoxCollider>();
        var rbCL = cuttedLow.AddComponent<Rigidbody>();

        rbCL.AddExplosionForce(500f, transform.up, 10f);
        rbCU.AddExplosionForce(500f, transform.up, 10f);
        
        Destroy(willCutObj);
        Destroy(cuttedUp,1.5f);
        Destroy(cuttedLow,1.5f);
        cuttedd = true;

    }

    public SlicedHull SlicedH(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
