using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour 
{
    [SerializeField] private Portal linkedPortal;
    private Animator animator;
    public bool used = false;

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Portal_Anim");
    }

    public GameObject TeleportTo()
    {
        if(used) return null;
        used = true;
        linkedPortal.used = true;
        return linkedPortal.gameObject;
    }
}