using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEyes : MonoBehaviour {

    public float bulletSpeed = 20;
    public int points = 50;
    public int healthCount = 30;

    public Animation move;

    public ParticleSystem eyeExplosion;

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

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            healthCount -= 1;
            AudioManager.PlayVariedEffect("StingerHit1");
        }

    }
    void Update()
    {

        Follow();

        if (healthCount <= 0)
        {
            AudioManager.PlayVariedEffect("Explosion7");
            gameObject.SetActive(false);
            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            shake.Shake();
            GameManager.instance.ScoreUp(points);
            BossController.instance.MiniEyeDown();
           


            ParticleSystem explosion = Instantiate(eyeExplosion);
            explosion.Stop();
            explosion.transform.position = transform.position;
            explosion.Play();

           
        }
    }

    public void Follow ()
    {
        Vector3 target = PlayerController.player.transform.position;
        transform.right = -(target - transform.position);
    }

    IEnumerator shootLeft()
    {
        
        GameObject spore = null;

        while (spore == null)
        {
            yield return new WaitForSeconds(1);
            GameObject Spore = Spawner.Spawn("Spore");
            Spore.transform.position = transform.position;


            Spore.GetComponent<EnemyProjectileController>().Fire(Vector2.left);
            AudioManager.PlayVariedEffect("EyeLaserShot1");
        }
    }

    private void OnEnable()
    {
        Debug.Log("blah!");
        StartCoroutine("shootLeft");

    }
}
