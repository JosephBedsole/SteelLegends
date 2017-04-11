using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxMineController : MonoBehaviour {

    public int speed = 1;
    public ParticleSystem explosionParticles;

    public string[] explosionPrefab;

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

    void OnEnable()
    {
        StartCoroutine("MoveLeftAndExplode");
    }

    IEnumerator MoveLeftAndExplode()
    {
        body.velocity = Vector2.left * speed;
        // Play a looped Mine Sound;
        AudioManager.PlayVariedEffect("MineWarningSound1");

        yield return new WaitForSeconds(1);

        AudioManager.PlayVariedEffect("MineWarningSound1");

        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
        explosionParticles.Stop();

        explosionParticles.Play();
        AudioManager.PlayVariedEffect("Explosion2");
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);

            AudioManager.PlayVariedEffect("Explosion2");
        }
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1);
            string explosionPrefabName = explosionPrefab[Random.Range(0, explosionPrefab.Length)];
            GameObject explosion = Spawner.Spawn(explosionPrefabName);

            explosion.transform.position = this.transform.position;
            
            explosion.SetActive(true);
        }
    }

}
