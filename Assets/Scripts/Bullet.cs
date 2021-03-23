using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   // CameraShake cameraShake;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // cameraShake = FindObjectOfType<CameraShake>().GetComponent<CameraShake>();
    }
    void Update()
    {
       // StartCoroutine(cameraShake.Shake(0.01f, 0.9f));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(10f, 0f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<Explosion>().GetComponent<Explosion>().death.gameObject.transform.position = transform.position;
            FindObjectOfType<Explosion>().GetComponent<Explosion>().death.Play();
            FindObjectOfType<GameManager>().GetComponent<GameManager>().Scored();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    
}
