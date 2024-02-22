using System.Collections;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    Vector3 originalPos;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        originalPos = transform.position;
    }
    
    public IEnumerator Shake(float duration, float magnitude)
    {
        Debug.Log("Screenshake!");
        Vector3 originalPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(0, yOffset, originalPos.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}