using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public static BossController instance;

    // Set Spawner Script to Inactive upon reaching a specified score or the Boss is Active on a bool;

    // Spawn Boss on its own spawner

    public int health = 100;
    public int points = 1000;
    public int miniEyes = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start() {  
        StartCoroutine("SpawnLaser");
    }

    void Update()
    {
        deathCheck();
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
      // while (instance.miniEyes <= 0)
        // {
            if (c.gameObject.tag == "Bullet")
            {
                instance.health -= 1;
                AudioManager.PlayVariedEffect("StingerHit1");
            }
       // }
    }

    public void MiniEyeDown ()
    {
        instance.miniEyes -= 1;
        Debug.Log("Oh no I'm dead!");
    }

    public void deathCheck ()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.ScoreUp(points);

            GameManager.instance.winText.gameObject.SetActive(true);

            ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController>();
            shake.Shake();

            AudioManager.instance.StartCoroutine("ChangeMusicBack");

        }
    }

    IEnumerator SpawnLaser()
    {
        while (this != null)
        {
            yield return new WaitForSeconds(5);

            // Charge Animation Here!

            GameObject laser = Spawner.Spawn("MajorLaser");
            laser.SetActive(true);
            laser.transform.position = transform.position;
            AudioManager.PlayVariedEffect("GiantLaser(5.5)");
            yield return new WaitForSeconds(2);
            LaserController.StopLaser();
        }
        while (this == null)
        {
            LaserController.StopLaser();
        }
    }
}
