using UnityEngine;

public class Portal : MonoBehaviour 
{
    [SerializeField] private Portal linkedPortal;
    public bool used = false;

    public GameObject TeleportTo()
    {
        if(used) return null;
        used = true;
        linkedPortal.used = true;
        return linkedPortal.gameObject;
    }
}