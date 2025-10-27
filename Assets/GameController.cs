using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("timer")]
    public float timer = 60f;
    public TMP_Text timerText;
    
    [Header("Game Over Screen")]
    public TMP_Text resultText;
    
    public List<GameObject> levelEnemies = new List<GameObject>();
    public Image gameOverScreen;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;

        UpdateTimer();

        var isVictory = levelEnemies.All(enemy => enemy == null);

        if (isVictory)
        {
            Victory();
        }else if (timer <= 0)
        {
            TimerExpired();
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        Time.timeScale = 1;
    }
    
    

    private void UpdateTimer()
    {
        timerText.text = $"{Mathf.Round(timer)}";
    }


    public void GameOver()
    {
        // activa screen
        resultText.text = "Has muerto, una lastima";
        gameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void TimerExpired()
    {
        // activa screen
        resultText.text = "Se ha acabado el tiempo, una lastima";
        Time.timeScale = 0;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void Victory()
    {
        // activa screen
        resultText.text = "En buena hora! has ganado";
        Time.timeScale = 0;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/GameScene");
        gameOverScreen.gameObject.SetActive(false);
        
    }
}
