using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrGameManager : Singleton<ScrGameManager>
{
    public static ScrGameManager instance;

    public static int score = 0;
    public static string playerName = "";
    public static int timesPlayed = 0;
    public GameObject enemy;

    public static float spawnTime = 3f;
    public static float spawnTick = 0f;

    public static bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            spawnTick += Time.deltaTime;
            if (spawnTick >= spawnTime)
            {
                bool left = false;
                if (Random.value >= 0.5)
                {
                    left = true;
                }
                Vector2 spawnLocation;

                if (left)
                {
                    spawnLocation = new Vector2(-13, -3);
                }
                else
                {
                    spawnLocation = new Vector2(13, -3);
                }

                if (spawnTime > 0.7)
                {
                    spawnTime -= 0.1f;
                }

                GameObject newEnemy = GameObject.Instantiate(enemy, transform.position, transform.rotation);
                newEnemy.transform.position = spawnLocation;
                spawnTick = 0;
            }
        }

    }

    public void ResetStats()
    {
        spawnTime = 3f;
        spawnTick = 0f;
        score = 0;
        playerName = "";
        timesPlayed = 0;
    }

    public static void GameEnd()
    {
        playing = false;
    }

    public static void GameStart()
    {
        spawnTime = 3f;
        spawnTick = 0f;
        score = 0;
        playing = true;
    }
}
