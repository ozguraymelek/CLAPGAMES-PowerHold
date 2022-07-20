using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;

    [Header("Settings")] [SerializeField] private float timeScale;
    [SerializeField] private float activatePanelDelayTime;

    private void Start()
    {
        SetTimeScale();
        SubscribeEvents();
    }

    #region Game Settings

    private void SetTimeScale()
    {
        Time.timeScale = timeScale;
    }

    private void SubscribeEvents()
    {
        INVEvents.OnSuccess += OnSuccess;
        INVEvents.OnFail += OnFail;
    }

    private void OnSuccess()
    {
        StartCoroutine(DelayPanelActivate(UIManager.Instance.successPanel));
    }

    private void OnFail()
    {
        // Handheld.Vibrate();
        
        invBehaviour.SetPlayer();
        invBehaviour.DeactivateAllWorldSpaceCanvas();
        invBehaviour.UnsubscribeMoveForward();
        invBehaviour.DisableJSwordCollider();
        invBehaviour.DisablePlayerCollider();
        
        StartCoroutine(DelayPanelActivate(UIManager.Instance.failPanel));
    }

    private IEnumerator DelayPanelActivate(GameObject requestedPanel)
    {
        yield return new WaitForSeconds(activatePanelDelayTime);
        requestedPanel.SetActive(true);
    }

    #endregion
}
