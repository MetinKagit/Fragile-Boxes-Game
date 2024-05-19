using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private Rigidbody lootRb;
    private GameManager gameManager;
    //private GameManager gameManager;
    public ParticleSystem explosionParticle;
    public AudioClip chickenSound; // Assign the chicken.mp3 clip in the Unity Editor
    private AudioSource audioSource;
    public float chickenSoundVolume = 0.45f;

    private float minSpeed = 3;
    private float maxSpeed = 3;
    private float maxTorque = 5;


    // Start is called before the first frame update
    void Start()
    {
        lootRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        lootRb.AddForce(RandomForce(minSpeed, maxSpeed), ForceMode.Impulse);
        lootRb.AddTorque(RandomTorque(maxTorque), RandomTorque(maxTorque), RandomTorque(maxTorque), ForceMode.Impulse);

        // Ensure audioSource is assigned to the component if it exists
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = chickenSound;
        audioSource.volume = chickenSoundVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomForce(float minSpeed, float maxSpeed)
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque(float maxTorque)
    {
        return Random.Range(-maxTorque, maxTorque);
    }


    public void DestroyTarget()
{
    if (gameManager.isGameActive)
    {
        if (gameObject.CompareTag("Heal") && gameManager.currentHealth < 3)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.TakeDamage(-1);
        }
        else
        {
            if (chickenSound != null && chickenSound.length >= 2.0f)
            {
                AudioClip shortenedClip = AudioClip.Create("ShortenedClip", (int)(chickenSound.frequency * 2.0f), chickenSound.channels, chickenSound.frequency, false);
                float[] data = new float[(int)(chickenSound.frequency * 2.0f)];
                chickenSound.GetData(data, 0);
                shortenedClip.SetData(data, 0);
                audioSource.PlayOneShot(shortenedClip);
            }
            lootRb.AddForce(RandomForce(5, 5), ForceMode.Impulse);
        }
    }
}

}
