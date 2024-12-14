using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] public float playerHealth = 100f;
    float startHealth = 100;
    public int score = 0;
    private void Awake()
    {
        int numGameSession = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        playerHealth = startHealth;
        Debug.Log("Player Lives is " + playerLives);
    }
    public void ProcessPlayerDamage(int damage)
    {
        if (damage == 1)
        {
            playerHealth -= 30;
        }
        else if (damage == 2)
        {
            playerHealth -= 10;
        }
        else if (damage == 3)
        {
            playerHealth -= 50;
        }
        else if (damage == 4)
        {
            playerHealth -= 20;
        }
        if (playerHealth<=0)
        {
            TakeLife();
        }
    }
    public void UpdateScore(int points)
    {
        if (points == 1)
        {
            score += 10;
        }
        else if (points == 2)
        {
            score += 100;
        }
        Debug.Log(score);
    }
    void TakeLife()
    {
        if (playerLives > 1)
        {
            playerLives--;
            Debug.Log("Player Lives is " + playerLives);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            playerHealth = startHealth;
        }
        else
        {
            ResetGameSession();
        }
    }
    void ResetGameSession()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindFirstObjectByType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }
        else
        {
            FindFirstObjectByType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
