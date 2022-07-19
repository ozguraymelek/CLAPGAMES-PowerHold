using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Reference")] [Space] [SerializeField]
    private INVBehaviour invBehaviour;
    
    [Header("Settings")] [Space] [SerializeField]
    private CinemachineVirtualCamera activeCam;
    [SerializeField] private float changeCameraDelayTime;
    [SerializeField] private float changeFirstCamera2GameCameraDelayTime;

    [Header("Component")] [SerializeField]
    private CinemachineVirtualCamera firstCamera, gameCamera, successCamera, failCamera, jumpCamera;

    private void Start()
    {
        activeCam = firstCamera;

        ChangeCamera(firstCamera);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        INVEvents.OnStart += OnStart;
        INVEvents.OnSuccess += SuccessCamera;
        INVEvents.OnFail += FailCamera;
        INVEvents.OnJump += JumpCamera;
    }

    //TODO: Zıplarken kamerayı geriye doğru çek ve platforma değdiğinde eski haline çevir.
    private void JumpCamera()
    {
        
    }

    private void FailCamera()
    {
        StartCoroutine(Delay(failCamera));
    }

    private void SuccessCamera()
    {
        StartCoroutine(Delay(successCamera));
    }

    IEnumerator Delay(CinemachineVirtualCamera virtualCamera)
    {
        yield return new WaitForSeconds(changeCameraDelayTime);
        
        ChangeCamera(virtualCamera);
    }

    /// <summary>
    ///     Play butonuna tıklanırsa aktif kamerayı gameCamera yap.
    /// </summary>
    private void OnStart()
    {
        ActivateGameCamera();
    }

    private void ActivateGameCamera()
    {
        ChangeCamera_FirstCamera2GameCamera(gameCamera);
    }

    private void ChangeCamera(CinemachineVirtualCamera virtualCamera)
    {
        if (activeCam == virtualCamera) return;

        activeCam.Priority = 0;
        
        activeCam = virtualCamera;

        activeCam.Priority = 10;
    }

    private void ChangeCamera_FirstCamera2GameCamera(CinemachineVirtualCamera virtualCamera)
    {
        if (activeCam == virtualCamera) return;

        StartCoroutine(DelayFirstCamera2GameCamera(virtualCamera));


    }

    private IEnumerator DelayFirstCamera2GameCamera(CinemachineVirtualCamera virtualCamera)
    {
        activeCam.Follow = null;
        activeCam.LookAt = null;
        activeCam.Priority = 0;
        
        invBehaviour.playerSpeed = 0;
        invBehaviour.PlayerTurnToForwardAxis();
        yield return new WaitForSeconds(changeFirstCamera2GameCameraDelayTime);
        
        activeCam = virtualCamera;
        activeCam.Priority = 10;
        
        invBehaviour.playerSpeed = 5;
        
    }
}
