using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct touchInf
{
    public Vector3 touchPos;
    public int touchID;
    public touchInf(int touchID,Vector3 touchPos)
    {
        this.touchID = touchID;
        this.touchPos = touchPos;
    }
};

public class PlayerController : MonoBehaviour
{
    bool touchStart = false;
    Rigidbody2D rb;
    float moveMultiplier = 100f;
    List<touchInf> touchData = new List<touchInf>();
    Vector2 direction;
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
                if(t.position.x > Screen.width/2)
                {
                    Shoot();
                }
                else
                {
                    touchData.Add(new touchInf(t.fingerId, Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, transform.position.z))));
                }
            }
            else if(t.phase == TouchPhase.Moved && touchData[x].touchID == t.fingerId)
            {
                touchStart = true;
                direction = (Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, transform.position.z))) - touchData[x].touchPos;
                direction = Vector2.ClampMagnitude(direction, 1.0f);
            }else if(t.phase == TouchPhase.Ended && t.position.x < Screen.width/2)
            {
                touchStart = false;
                touchInf thisTouch = touchData.Find(touchInf => touchInf.touchID == t.fingerId);
                touchData.RemoveAt(touchData.IndexOf(thisTouch));
            }
            ++x;
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
