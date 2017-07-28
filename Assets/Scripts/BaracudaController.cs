using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaracudaController : MonoBehaviour {

    public float speed = 5;
    public float slowSpeed = 1;
    public float bulletSpeed = 20;
    public int points = 10;
    public Animation move;

    public ParticleSystem eyeExplosion;

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
            gameObject.SetActive(false);

            AudioManager.PlayVariedEffect("Explosion7");

            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            shake.Shake();

            GameManager.instance.ScoreUp(points);

            ParticleSystem explosion = Instantiate(eyeExplosion); ;

            explosion.Stop();
            explosion.transform.position = transform.position;
            explosion.Play();
        }
        if (c.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Enemy2Coroutine()
    {
        body.velocity = Vector2.left * speed;
        yield return new WaitForSeconds(1);
        body.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        body.velocity = Vector2.left * slowSpeed;
    }

    IEnumerator shootLeft()
    {
        //While loop to continuously shoot while the game object is active...
        //while()
        GameObject spore = null;

        while (spore == null)
        {
            yield return new WaitForSeconds(2);
            GameObject Spore = Spawner.Spawn("Spore");
            Spore.transform.position = transform.position;
            Spore.GetComponent<EnemyProjectileController>().Fire(Vector2.left);
            AudioManager.PlayVariedEffect("EyeLaserShot1");
        }
    }

    private void OnEnable()
    {
        StartCoroutine("Enemy2Coroutine");
        Debug.Log("blah!");
        StartCoroutine("shootLeft");

    }

}
