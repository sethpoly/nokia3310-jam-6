using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint; // Starting point of the platform
    public Transform endPoint; // Ending point of the platform
    public float moveSpeed = 200f; // Speed of movement
    public float delay = 0f;

    private bool movementReady = false;

    private Vector3 nextPoint; // The next point towards which the platform will move

    void Start()
    {
        nextPoint = endPoint.position; // Start by moving towards the end point
        StartCoroutine(DelayStart());
    }

    void Update()
    {
        if(movementReady) MovePlatform();
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(delay);
        movementReady = true;
    }

    void MovePlatform()
    {
        // Move the platform towards the next point
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);

        // Check if the platform has reached the next point
        if (Vector3.Distance(transform.position, nextPoint) < 0.01f)
        {
            // If it reached the end point, set the next point to the start point
            if (nextPoint == endPoint.position)
            {
                nextPoint = startPoint.position;
            }
            // If it reached the start point, set the next point to the end point
            else
            {
                nextPoint = endPoint.position;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Reverse the direction of the platform upon collision with an object
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Coin"))
        {
            if (nextPoint == endPoint.position)
            {
                nextPoint = startPoint.position;
            }
            else
            {
                nextPoint = endPoint.position;
            }
        }
    }
}
