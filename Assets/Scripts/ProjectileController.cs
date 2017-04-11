using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float initialSpeed = 20;
    public float lifeSpan = 2;

    private Rigidbody2D body;
   
    void Awake()
    {
        Debug.Log("awake");
    }
    public void Fire (Vector2 direction)
    {
        gameObject.SetActive(true);
        Rigidbody2D body = GetComponent<Rigidbody2D>(); 
        body.velocity = direction * initialSpeed;
        StartCoroutine("LifeCycleCoroutine");
    }

    IEnumerator LifeCycleCoroutine ()
    {
        yield return new WaitForSeconds(lifeSpan);
        gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Start");
        body = GetComponent<Rigidbody2D>();
	}

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Enemy1")
        {
            gameObject.SetActive(false);
        }
        if (c.gameObject.tag == "Enemy2")
        {
            gameObject.SetActive(false);
        }
        if (c.gameObject.tag == "MiniEye")
        {
            gameObject.SetActive(false);
        }
        if (c.gameObject.tag == "Shield")
        {
            gameObject.SetActive(false);
        }
        //if (c.gameObject.tag == "Spore")
        //{
        //    gameObject.SetActive(false);
        //}
    }
}
