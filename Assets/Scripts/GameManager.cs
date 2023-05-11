using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject gameOverPanel;
    private int score = 0;

    public bool isGameOver = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public void SetGameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;

            Panda panda = FindObjectOfType<Panda>();
            if (panda != null)
            {
                panda.SetGameOver();
            }

            ObjectSpawner spawner = FindObjectOfType<ObjectSpawner>();
            if (spawner != null)
            {
                spawner.StopObjectSpawning();
            }

            gameOverPanel.SetActive(true);

        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
