using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed = 10;
    public float blastOff = 50;
    private int healthCount;
    public int maxHealth = 2;
    public int points = 20;

    public ParticleSystem enemyExplosion;

    //public int bulletSpeed 30;
    // Change to Rigidbody2D
    Rigidbody2D _body;
    public Rigidbody2D body
    {
        get
        {
            if (_body == null)
            {
                _body = GetComponent<Rigidbody2D>();
            }
            return _body;
        }
    }

    // Change OnTriggerEnter2D (Collider2D c)
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            healthCount -= 1;
            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            AudioManager.PlayVariedEffect("StingerHit1");
            shake.Shake();
        }
        if (c.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

    private void ResetHealth()
    {
        healthCount = maxHealth;
    }
    
    void OnEnable()
    {
        ResetHealth();
        StartCoroutine("Enemy1Coroutine");
    }

    IEnumerator Enemy1Coroutine()
    {
        body.velocity = Vector2.left * speed;
        yield return new WaitForSeconds(1);
        body.velocity = Vector2.zero;
        AudioManager.PlayVariedEffect("StingerRevUp1");

        yield return new WaitForSeconds(1);
        body.velocity = Vector2.left * blastOff;
        AudioManager.PlayVariedEffect("StingerBlastOff3");

    }
    void Update()
    {
        if (healthCount == 0)
        {
            AudioManager.PlayVariedEffect("Explosion7");
            gameObject.SetActive(false);
            GameManager.instance.ScoreUp(points);

            // Instantiates a new particle system for each new spawned enemy;
            // Setting the new ParticleSystem explosion to the instance will allow the enemyExplosion prefab to be instantiated for each new enemy spawned.
            ParticleSystem explosion = Instantiate(enemyExplosion);
            explosion.Stop();
            explosion.transform.position = transform.position;
            explosion.Play();
        }
    }
}
