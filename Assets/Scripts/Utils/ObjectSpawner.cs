using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float minSpawnRate = 0.4f; // Minimum spawn rate
    public float maxSpawnRate = 0.8f; // Maximum spawn rate
    public float objectSpeed = 10f; // Adjust as needed

    private float spawnTimer;
    private float currentSpawnRate;

    void Start()
    {
        // Initialize spawn rate with a random value within the specified range
        currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void Update()
    {
        // Timer to spawn objects
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnRate)
        {
            SpawnObject();

            // Reset timer and update spawn rate with a slightly randomized value
            spawnTimer = 0f;
            currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void SpawnObject()
    {
        // Calculate random spawn position below the camera's view
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x);
        Vector3 spawnPosition = new Vector3(spawnX, Camera.main.transform.position.y - Camera.main.orthographicSize - 1f, 0); // Offset by 1 unit below the camera's view

        // Instantiate object at the calculated position
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Set the initial velocity for moving upwards
        ObjectMovement movement = spawnedObject.AddComponent<ObjectMovement>();
        movement.Init(objectSpeed);

        // Destroy the object after a set time
        if(spawnedObject.TryGetComponent<Balloon>(out var balloon))
        {
            StartCoroutine(PopBalloon(balloon, Random.Range(1f, 5f)));
        }
    }
    
    private IEnumerator PopBalloon(Balloon balloon, float delay)
    {
        yield return new WaitForSeconds(delay);
        balloon.Pop();
    }
}

public class ObjectMovement : MonoBehaviour
{
    private float speed;

    public void Init(float _speed)
    {
        speed = _speed;
    }

    void Update()
    {
        // Move the object upwards
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
