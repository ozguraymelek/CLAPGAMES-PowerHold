using System;
using Unity.VisualScripting;
using UnityEngine;

public class INVEvents : MonoBehaviour
{
    /// <summary>
    ///     OnSuccess --> Arenalar 5 bölümden oluşuyor. Arenanın son bölümündeki boss'un levelinden yüksekse yeni arenaya geçilir.
    ///     OnFail --> Engellere çarparsa bölüm yeniden başlar.
    ///     OnHold --> Karakterin saldırı mekaniği ve animasyonu
    ///     OnRelease --> Normal koşu
    ///     OnJump --> Sıradaki platforma zıplamak.
    ///     OnInteractWithEnemy --> Karakterin leveli yükselecek ve düşmanlar ölecek.
    /// </summary>
    public static event Action OnStart,
        OnUpdate,
        OnFixedUpdate,
        OnSuccess,
        OnFail,
        OnHold,
        OnRelease,
        OnJump,
        OnInteractWithEnemy;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        OnStart = null;
        OnUpdate = null;
        OnFixedUpdate = null;
        OnSuccess = null;
        OnFail = null;
        OnHold = null;
        OnRelease = null;
        OnJump = null;
        OnInteractWithEnemy = null;
    }

    public void OnStartButtonClicked()
    {
        OnStart?.Invoke();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    public void OnPlayerSuccess()
    {
        OnSuccess?.Invoke();
    }

    public void OnPlayerFail()
    {
        OnFail?.Invoke();
    }

    public void OnPlayerHold()
    {
        OnHold?.Invoke();
    }

    public void OnPlayerRelease()
    {
        OnRelease?.Invoke();
    }

    public void OnPlayerJump()
    {
        OnJump?.Invoke();
    }

    public void OnPlayerInteractWithEnemy()
    {
        OnInteractWithEnemy?.Invoke();
    }
}

