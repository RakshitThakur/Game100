using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem death;
    // Start is called before the first frame update
    void Start()
    {
        death = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void dead()
    {
        death.Play();
    }

}
