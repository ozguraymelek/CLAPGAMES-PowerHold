using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
        Handheld.Vibrate();
        StartCoroutine(DelayPanelActivate(UIManager.Instance.failPanel));
    }

    private IEnumerator DelayPanelActivate(GameObject requestedPanel)
    {
        yield return new WaitForSeconds(activatePanelDelayTime);
        requestedPanel.SetActive(true);
    }

    #endregion
}
