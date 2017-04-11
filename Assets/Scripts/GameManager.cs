using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Text loseText;
    public Text chapterText;
    public Text chapterSub;

    public int score = 0;
    public Text scoreText;

    public int overShield = 100;
    private int maxOverShield = 100;
    public Text shieldText;

    public string[] enemyPrefabNames;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine("SpawnEnemiesCoroutine");
        instance.shieldText.text = "OverShield: " + overShield;
        StartCoroutine("SpawnTheBossMan");
    }
    //IEnumerator WavesChanger()
    //{
    //    while (enabled)
    //    {
    //        StartCoroutine("wave1");
    //        yield return 
    //        StartCoroutine("Wave2");
    //    }
    //}
    IEnumerator SpawnEnemiesCoroutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1);
            string enemyPrefabName = enemyPrefabNames[Random.Range(0, enemyPrefabNames.Length)];
            GameObject enemy = Spawner.Spawn(enemyPrefabName);

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, Random.value, -Camera.main.transform.position.z));
            enemy.transform.position = pos;
            enemy.SetActive(true);

        }
    }
    IEnumerator SpawnTheBossMan()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(30);
            GameObject enemy = Spawner.Spawn("Final Boss");

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, -Camera.main.transform.position.z));
            enemy.transform.position = pos;
            enemy.SetActive(true);

        }
    }
    
    public void ShieldDown(int damage)
    {
        instance.overShield -= damage;
        instance.shieldText.text = "OverShield: " + instance.overShield.ToString();

    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "ShieldUp")
        {
            overShield = maxOverShield;
        }
    }
    public void ScoreUp(int points)
    {
        instance.score += points;
        instance.scoreText.text = "Score: " + score;
    }
}
