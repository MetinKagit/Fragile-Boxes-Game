using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button pauseButton;
    public GameObject titleSccreen;
    public GameObject healthCanvas;
    public GameObject volumeSlider;

    public bool isGameActive;
    private int score;
    private float spawnRate = 1.0f;

    [SerializeField] private float startingHealth;
    public float currentHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = currentHealth / 3;
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive) {

            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        currentHealth = startingHealth;
        totalHealthBar.fillAmount = currentHealth;
        isGameActive = true;
        scoreText.gameObject.SetActive(true);
        healthCanvas.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        volumeSlider.gameObject.SetActive(false);
        score = 0;

        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleSccreen.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            
        }
        else
        {
            GameOver();
        }
    }

}
