using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
