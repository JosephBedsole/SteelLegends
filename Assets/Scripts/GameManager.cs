using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Text winText;
    public Text loseText;
    public Text warningText;
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
        Cursor.visible = false;       
        instance.shieldText.text = "OverShield: " + overShield;

        StartCoroutine("WavesChanger");

        //StartCoroutine("SpawnEnemiesCoroutine");
        //StartCoroutine("SpawnVFormCoroutine");
        //StartCoroutine("SpawnFollowFormCoroutine");
        //StartCoroutine("SpawnBlockadeFormCoroutine");
        //StartCoroutine("SpawnTheBossMan");      
    }

    IEnumerator WavesChanger()
    {
        // Add && "Level1" to the while loop
        // Need to find a way to have the same wave changer for multiple levels
        while (enabled)
        {
            yield return StartCoroutine("Wave1");
            yield return StartCoroutine("Wave2");
            yield return StartCoroutine("Wave3");
            yield return StartCoroutine("SpawnTheBossMan");

            break;
        }
    }

    IEnumerator Wave1()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(5); // 5 seconds after start
            SpawnStinger(0.9f);
            SpawnStinger(0.1f);
            SpawnVForm();
            yield return new WaitForSeconds(3);
            SpawnBlockadeForm();
            
            break;
        }
    }

    IEnumerator Wave2()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(5); // 5 seconds after wave 1 ends
            Debug.Log("Wave2 has spawned... or has it! Bwahahahahahah!");
            break;
        }
    }

    IEnumerator Wave3()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(5);
            Debug.Log("Wave3 has spawned... or has it! Bwahahahahahah!");



            yield return new WaitForSeconds(10);
            break;
        }
    }

    void SpawnStinger(float spawnPoint) // Check to see if "LaserEye is right!
    {
        GameObject enemy = Spawner.Spawn("Stinger"); // Could put a function in main function head that says "String prefabName" then put it in Spawn(prefabName).s

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, spawnPoint, -Camera.main.transform.position.z));
        enemy.transform.position = pos;
        enemy.SetActive(true);
    }

    void SpawnFollowForm (float spawnPoint) // Check to see if "LaserEye is right!
    {
        GameObject enemy = Spawner.Spawn("Follow Form");

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, spawnPoint, -Camera.main.transform.position.z));
        enemy.transform.position = pos;
        enemy.SetActive(true);
    }

    void SpawnEyeLaser(float spawnPoint) // Check to see if "LaserEye is right!
    {
        GameObject enemy = Spawner.Spawn("EyeLaser");

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, spawnPoint, -Camera.main.transform.position.z));
        enemy.transform.position = pos;
        enemy.SetActive(true);
    }

    void SpawnVForm()
    {
        GameObject enemy = Spawner.Spawn("V Form");

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, -Camera.main.transform.position.z));
        enemy.transform.position = pos;
        enemy.SetActive(true);
    }

    void SpawnBlockadeForm()
    {
        GameObject enemy = Spawner.Spawn("Blockade Form");

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, -Camera.main.transform.position.z));
        enemy.transform.position = pos;
        enemy.SetActive(true);
    }

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


    /*IEnumerator SpawnFollowFormCoroutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(7);
            GameObject enemy = Spawner.Spawn("Follow Form");

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, Random.value, -Camera.main.transform.position.z));
            enemy.transform.position = pos;
            enemy.SetActive(true);

        }
    }*/

    /*IEnumerator SpawnVFormCoroutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(10);
            GameObject enemy = Spawner.Spawn("V Form");

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, -Camera.main.transform.position.z));
            enemy.transform.position = pos;
            enemy.SetActive(true);

        }
    }*/

    /*IEnumerator SpawnBlockadeFormCoroutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(20);
            GameObject enemy = Spawner.Spawn("Blockade Form");

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, -Camera.main.transform.position.z));
            enemy.transform.position = pos;
            enemy.SetActive(true);

        }
    }*/


    IEnumerator SpawnTheBossMan()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(10);
            AudioManager.instance.StartCoroutine("ChangeMusic");
            instance.warningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(10);
            instance.warningText.gameObject.SetActive(false);
            GameObject enemy = Spawner.Spawn("Final Boss");

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, -Camera.main.transform.position.z));
            enemy.transform.position = pos;
            enemy.SetActive(true);

            instance.StopCoroutine("SpawnTheBossMan");

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
