using UnityEngine;

public class Door: MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OpenDoor()
    {
        animator.Play("Door_Open");
    }
}