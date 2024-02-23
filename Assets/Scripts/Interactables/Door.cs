using UnityEngine;

public class Door: MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    public bool isOpen = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.Play("Door_Open");
        isOpen = true;
        Debug.Log("Opening door");
    }
}