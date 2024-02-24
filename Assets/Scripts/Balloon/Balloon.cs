using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Balloon : MonoBehaviour
{
    private Animator animator;
    public Vector3 startingScale;
    public readonly float minScale = .01f;
    private bool isDetached = false;
    [SerializeField] private float detachFloatUpSpeed;
    [SerializeField] private GameObject stringPrefab;

    void Awake()
    {
        animator = GetComponent<Animator>();
        startingScale = transform.localScale;
    }

    void Update()
    {
        
        if(isDetached)
        {
            transform.Translate(detachFloatUpSpeed * Time.deltaTime * Vector3.up);
        }
    }

    public void Shrink(float targetScale)
    {
        if(transform.localScale.x >= minScale) 
        {
            transform.localScale = new(targetScale, targetScale, targetScale);
        }
        else {
            Debug.LogError("Cannot shrink balloon any more. It should POP!");
        }
    }

    public void Inflate(float targetScale)
    {
        if(transform.localScale.x < startingScale.x) 
        {
            transform.localScale = new(targetScale, targetScale, targetScale);
        }
        else {
            transform.localScale = startingScale;
        }
    }

    public void Pop()
    {
        Debug.Log("Balloon POPPED!");
        StartCoroutine(PopAnimation());
    }

    public void Detach()
    {
        transform.parent = null;

        // Add string
        var spawnPos = transform.position;
        spawnPos.y -= 6;
        Instantiate(stringPrefab, spawnPos, transform.rotation, gameObject.transform);

        isDetached = true;
    }

    private IEnumerator PopAnimation()
    {
        transform.localScale = startingScale * 1.2f;
        animator.Play("ExplodeBalloon");
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
