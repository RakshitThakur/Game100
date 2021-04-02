using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweening : MonoBehaviour
{
    [SerializeField] GameObject deathCanvas;
    [SerializeField] GameObject scoreCanvas;
    public static Tweening tween;
    // Start is called before the first frame update
    void Start()
    {
        tween = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TweenCanvas()
    {
        LeanTween.cancel(deathCanvas);
       
        LeanTween.scale(deathCanvas, deathCanvas.transform.localScale * 2 , 5f).setEasePunch();
    }
}
