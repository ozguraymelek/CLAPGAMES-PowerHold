using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleHead : Singleton<ExampleHead>
{
    public RagdollDismembermentVisual _dismemberment;
    public CharacterJoint[] _joints;

    public void DismemberAllRig()
    {
        _dismemberment.Dismember("R_Thigh");
        _dismemberment.Dismember("R_Calf");

        foreach (var joint in _joints)
        {
            joint.breakForce = 0;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        
        foreach (var joint in _joints)
        {
            joint.breakForce = 0;
        }
    }
}
