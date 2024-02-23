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
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            var yOffset = -.5f * magnitude;

            transform.localPosition = new Vector3(originalPos.x, yOffset, originalPos.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}