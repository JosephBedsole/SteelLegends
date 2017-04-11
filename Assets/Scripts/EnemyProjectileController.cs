using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour {

    public float initialSpeed = 20;
    public float lifeSpan = 2;

    private Rigidbody2D body;

    void Awake()
    {
        Debug.Log("awake");
    }

    // Keep this here;
    public void Fire(Vector2 direction)
    {
        gameObject.SetActive(true);
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = direction * initialSpeed;
        StartCoroutine("LifeCycleCoroutine");
    }

    // ProtoType
    public void FireAt ()
    {
        Vector3 target = PlayerController.player.transform.position;
        Vector3 direction = -(target - transform.position);
        transform.right = direction;

        gameObject.SetActive(true);
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = transform.right;
        StartCoroutine("LifeCycleCoroutine");
    }

    IEnumerator LifeCycleCoroutine()
    {
        yield return new WaitForSeconds(lifeSpan);
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start");
        body = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
        //if (c.gameObject.tag == "Bullet")
        //{
        //    gameObject.SetActive(false);
        //}
    }
}
