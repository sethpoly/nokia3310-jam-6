using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    [SerializeField] private bool isSpinning = false;
    private Animator animator;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        animator = GetComponent<Animator>();
        isSpinning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpinning)
        {
            Spin();
        }
    }

    public void OnCollect()
    {
        // Play animation then destroy
        if(animator != null)
        {
            isSpinning = false;
            transform.rotation = startRotation;
            animator.Play("Coin_Collect");
            Destroy(gameObject, .4f);
        }
    }

    private void Spin()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f); 
    }
}
