using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform spawnPositionEnemy;
    public Transform spawnPositionSpike;
    public Canvas deathCanvas;
    int score = 0;
    int highScore = 0;
    public Text highscoreText;
    public Text scoreText;
    

    // Start is called before the first frame update
    void Start()
    {
       
        deathCanvas.enabled = false;
        Invoke("SpawnEnemy", 2f);
        Invoke("SpawnSpike", 2f);
        highScore = PlayerPrefs.GetInt("FinalScore");
        highscoreText.text = "HIGHSCORE : " + highScore.ToString();
    }
        // Update is called once per frame
        void Update()
    {
        
    }
    void SpawnEnemy()
    {
        GameObject enemy = ObjectPool.SharedInstance.GetPooledEnemy();
        if (enemy != null)
        {
            enemy.transform.position = spawnPositionEnemy.position + new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-4f, 4f));
            enemy.transform.rotation = spawnPositionEnemy.rotation;
            enemy.SetActive(true);
            Invoke("SpawnEnemy", 1f);
        }
    }
    void SpawnSpike()
    {
        GameObject spike = ObjectPool.SharedInstance.GetPooledSpike();
        if (spike != null)
        {
            spike.transform.position = spawnPositionSpike.position + new Vector3(Random.Range(-8f, 8f), 0);
            spike.transform.rotation = spawnPositionSpike.rotation;
            spike.SetActive(true);
            Invoke("SpawnSpike", 1f);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }
    public void GameOver()
    {
        deathCanvas.enabled = true;
        Time.timeScale = 0;
    }
    public void Scored()
    {
        score++;
        scoreText.text = "SCORE : " + score.ToString();
        if (score > highScore)
        {
            highScore = score;
            highscoreText.text = "HIGHSCORE : " + highScore.ToString();
            PlayerPrefs.SetInt("FinalScore", highScore);
        }
    }
}
