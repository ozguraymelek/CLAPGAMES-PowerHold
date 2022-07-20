using System;
using UnityEngine;

public class INVInputs : MonoBehaviour
{
    public static event Action<Vector3> PointerPressed;
    public static event Action<Vector3> PointerMoved;
    public static event Action<Vector3> PointerRemoved;

    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;
    
    [Header("Settings")] private Vector3 lastMousePosition;

    private void Start()
    {
        INVEvents.OnStart += OnStart;
    }

    private void OnStart()
    {
        INVEvents.OnUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0) && invBehaviour.isPlayerDead == false)
        {
            lastMousePosition = Input.mousePosition;
            PointerPressed?.Invoke(lastMousePosition);
        }
        
        if (Input.GetMouseButton(0))
        {
            var currentMousePosition = Input.mousePosition;

            if (lastMousePosition != currentMousePosition)
            {
                PointerMoved?.Invoke(currentMousePosition - lastMousePosition);
                lastMousePosition = currentMousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            PointerRemoved?.Invoke(Input.mousePosition);
        }
    }
}
