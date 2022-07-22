using UnityEngine;
using EzySlice;
public class Sword : Singleton<Sword>, IInteractible
{
    [Header("Scriptable Objects Reference")] [SerializeField]
    private PlayerSettings playerSettings;

    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;
    
    [Header("Components")]
    public BoxCollider boxCollider;
    
    [Header("Settings")] public bool interacted = false;

    
    [Header("Slice Settings")] [Space] public Material materialSlicedSide;
    public float explosionForce;
    public float explosionRadius;
    public bool gravity, kinematic;
    
    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        // INVEvents.OnSwordInteract += OnSwordInteractWithEnemy;
    }
    private void OnTriggerEnter(Collider other)
    {
        // FindObjectOfType<INVEvents>().OnSwordInteractWithEnemy(other);
        OnSwordInteractWithEnemy(other);
        
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    public void OnSwordInteractWithEnemy(Collider collider)
    {
        OnEnter(collider);
        
    }

    
    #region Implement
    
    public void OnEnter(Collider other)
    {
        if (interacted == true) return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Cuttable"))
        {
            print("ttT");
            other.transform.parent = null;
            Cut.Instance._material = other.GetComponent<MeshRenderer>().material;
            Cut.Instance.willCutObj = other.gameObject;
            Cut.Instance.Slice();

            interacted = true;
        }
    }

    public void OnStay(Collider other)
    {
        
    }

    public void OnExit(Collider other)
    {
        Cut.Instance.cuttedd = false;
    }
    
    #endregion
    
    
}