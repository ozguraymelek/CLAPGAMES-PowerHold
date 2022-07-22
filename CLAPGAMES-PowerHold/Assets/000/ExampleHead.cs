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
        #region Lower Body
        
        _dismemberment.Dismember("R_Thigh");
        _dismemberment.Dismember("R_Calf");
        
        _dismemberment.Dismember("L_Thigh");
        _dismemberment.Dismember("L_Calf");
        
        #endregion

        #region Upper Body

        _dismemberment.Dismember("Spine");
        _dismemberment.Dismember("Head");
        
        _dismemberment.Dismember("L_Upperarm");
        _dismemberment.Dismember("L_Forearm");
        
        _dismemberment.Dismember("R_Upperarm");
        _dismemberment.Dismember("R_Forearm");

        #endregion
        

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
