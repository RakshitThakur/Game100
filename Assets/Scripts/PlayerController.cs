using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    bool touchStart = false;
    Rigidbody2D rb;
    float moveMultiplier = 100f;
    Vector2 direction;
    int leftTouch = 99;
    Vector3 touchPos;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform muzzle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void MyInput()
    {
        int x = 0;
        while(x < Input.touchCount)
        {
            Touch t = Input.GetTouch(x);
            if(t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    Shoot();
                }
                else
                {
                    leftTouch = t.fingerId;
                    touchPos = Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, transform.position.z));
                }
            }
            else if(t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                touchStart = true;
                direction = Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, transform.position.z)) - touchPos;
                direction = Vector2.ClampMagnitude(direction, 1.0f);
            }
            else if(t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                touchStart = false;
                leftTouch = 99;
            }
            x++;
        }
    }
    void MovePlayer()
    {
       if (touchStart == true)
       {
            rb.velocity = new Vector2(direction.x * moveSpeed * moveMultiplier * Time.fixedDeltaTime, direction.y * moveSpeed * moveMultiplier * Time.fixedDeltaTime); 
       }
    }
    public void Shoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if(bullet != null)
        {
            bullet.transform.position = muzzle.transform.position;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f);
        }
    }
}
