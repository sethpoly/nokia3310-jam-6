using UnityEngine;

public class Door: MonoBehaviour
{
    private Animator animator;
    public bool isOpen = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool OpenDoor()
    {
        if(isOpen) return false;
        animator.Play("Door_Open");
        isOpen = true;
        Debug.Log("Opening door");
        return true;
    }
}