using UnityEngine;
using EzySlice;
public class Sword : MonoBehaviour, IInteractible
{
    [Header("Scriptable Objects Reference")] [SerializeField]
    private PlayerSettings playerSettings;

    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;
    
    [Header("Components")]
    public BoxCollider boxCollider;
    
    [Header("Settings")] public bool interacted = false;


    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        INVEvents.OnSwordInteract += OnSwordInteractWithEnemy;
    }
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<INVEvents>().OnSwordInteractWithEnemy(other);
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void OnSwordInteractWithEnemy(Collider collider)
    {
        OnEnter(collider);
    }

    
    #region Implement
    
    public void OnEnter(Collider other)
    {
        if (interacted == true) return;

        if (other.GetComponent<IEnemy>() != null)
        {
            print("Enemy!");
            invBehaviour.ExploitEnemyLevel(other.GetComponent<Enemy>());
            interacted = true;
        }
    }

    public void OnStay(Collider other)
    {
        
    }

    public void OnExit(Collider collider)
    {
        
    }
    
    #endregion
}