using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public static PlayerController player;

    public float speed = 1;
    public float shootSpeed = 10;
    public ParticleSystem explosionParticle;
    public Rigidbody2D body;

    new Collider2D collider;

    void Start()
    {
        player.body = GetComponent<Rigidbody2D>();
        StartCoroutine("ChapterIntro");
        collider = GetComponent<Collider2D>(); 
    }

    IEnumerator ChapterIntro ()
    {
        GameManager.instance.chapterText.gameObject.SetActive(true);
        GameManager.instance.chapterSub.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        GameManager.instance.chapterText.gameObject.SetActive(false);
        GameManager.instance.chapterSub.gameObject.SetActive(false);
    }

    private void Awake ()
    {
        if (player == null)
        {
            player = this;
        }
    }

    void Update()
    {
        LoseCheck();

        float y = Input.GetAxis("Vertical");
        player.body.velocity = Vector2.up * y * player.speed;

        if (Input.GetButtonDown("Jump"))
        {
            GameObject bullet = Spawner.Spawn("Bullet");
            bullet.transform.position = transform.position;
            bullet.GetComponent<ProjectileController>().Fire(Vector2.right);
            AudioManager.PlayVariedEffect("EnergyWeapon");
        }

        ClampToScreen(collider.bounds.extents.y);
        ClampToScreen(-collider.bounds.extents.y);

    }

    void ClampToScreen (float yOffset)
    {
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * yOffset);
        v.y = Mathf.Clamp01(v.y);
        transform.position = Camera.main.ViewportToWorldPoint(v) - Vector3.up * yOffset;

    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Spore")
        {
            GameManager.instance.ShieldDown(10);

            AudioManager.PlayVariedEffect("StingerHit1");

            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            shake.Shake();
        }
        else if (c.gameObject.tag == "Enemy1")
        {
            GameManager.instance.ShieldDown(20);

            AudioManager.PlayVariedEffect("StingerHit1");

            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            shake.Shake();
        }
        else if (c.gameObject.tag == "Explosion")
        {
            GameManager.instance.ShieldDown(50);

            AudioManager.PlayVariedEffect("StingerHit1");

            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            shake.Shake();
        }
        else if (c.gameObject.tag == "MajorLaser")
        {
            GameManager.instance.ShieldDown(50);

            AudioManager.PlayVariedEffect("StingerHit1");

            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            shake.Shake();
        }
    }

    public void LoseCheck()
    {
        if (GameManager.instance.overShield <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.loseText.gameObject.SetActive(true);

            AudioManager.PlayEffect("Explosion2");

            player.explosionParticle.Stop();
            player.explosionParticle.transform.position = transform.position;
            player.explosionParticle.Play();
        }
    }
}
