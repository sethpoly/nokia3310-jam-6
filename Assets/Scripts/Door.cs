using UnityEngine;

public class Door: MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private bool isOpen = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        boxCollider.enabled = isOpen;
    }

    public void OpenDoor()
    {
        animator.Play("Door_Open");
        isOpen = true;
    }
}