using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public List<GameObject> loots;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPosition = -3;
    public int pointValue;

    public ParticleSystem explosianParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(RandomForce(minSpeed, maxSpeed), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(maxTorque), RandomTorque(maxTorque), RandomTorque(maxTorque), ForceMode.Impulse);

        if (!gameObject.CompareTag("Bad")) {
           transform.position = RandomSpawnPos(xRange, ySpawnPosition, 0);
        }
        else
        {
           transform.position = RandomSpawnPos(xRange, ySpawnPosition, 1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.TakeDamage(1);
        }
        
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosianParticle, transform.position, explosianParticle.transform.rotation);
            if (gameObject.CompareTag("Loot"))
            {
                int index = Random.Range(0, loots.Count);
                Instantiate(loots[index], transform.position, Quaternion.identity);
            }

            gameManager.UpdateScore(pointValue);
        }
    }

    Vector3 RandomForce(float minSpeed, float maxSpeed)
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque(float maxTorque)
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos(float xRange, float ySpawnPosition, float zValue)
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPosition, zValue); ;
    }
}
