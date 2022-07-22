using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleHead : MonoBehaviour
{
    public RagdollDismembermentVisual _dismemberment;
    public CharacterJoint _joint;

    private void OnEnable()
    {
        _dismemberment.Dismember("Forearm");
    }
}
