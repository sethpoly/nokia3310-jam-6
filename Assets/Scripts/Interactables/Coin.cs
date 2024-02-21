using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    [SerializeField] private bool isSpinning = false;

    // Start is called before the first frame update
    void Start()
    {
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

    private void Spin()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f); 
    }
}
