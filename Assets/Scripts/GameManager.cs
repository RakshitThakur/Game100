using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform spawnPosition;
    public Canvas deathCanvas;
    int score = 0;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        deathCanvas.enabled = false;
        Invoke("SpawnEnemy", 2f);
        score = PlayerPrefs.GetInt("FinalScore");
        scoreText.text = "SCORE : " + score.ToString();
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
            enemy.transform.position = spawnPosition.position + new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-4f, 4f));
            enemy.transform.rotation = spawnPosition.rotation;
            enemy.SetActive(true);
            Invoke("SpawnEnemy", 1f);
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
        PlayerPrefs.SetInt("FinalScore", score);
        scoreText.text = "SCORE : " + score.ToString();
    }

   
   

   
}
