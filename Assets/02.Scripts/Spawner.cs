using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Enemy_0,
    Enemy_1,
    Enemy_2,
    Enemy_3,
    Enemy_4
}

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    float timer;
    int level;

    //public float levelTime;

    bool isBossTage;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        isBossTage = false;
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;
        level = GameManager.Instance.level;
        //Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f), spawnData.Length - 1);

        if (isBossTage == false)
        {
            if (timer > spawnData[level].spawnTime)
            {
                Spawn();
                timer = 0;
            }
            if (level == 9)
            {
                isBossTage = true;
                GameObject enemy;
                enemy = GameManager.Instance.pool.GetEnemies(4);
                enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            }
        }
        else // isBossTage == true
        {

        }




    }

    void Spawn()
    {
        GameObject enemy;
        if (level == 0)
            enemy = GameManager.Instance.pool.GetEnemies(0);
        else if (level == 1)
            enemy = GameManager.Instance.pool.GetEnemies(1);
        else if (level == 2)
            enemy = GameManager.Instance.pool.GetEnemies(0);
        else if (level == 3)
            enemy = GameManager.Instance.pool.GetEnemies(2);
        else if (level == 4)
            enemy = GameManager.Instance.pool.GetEnemies(0);
        else if (level == 5)
            enemy = GameManager.Instance.pool.GetEnemies(1);
        else if (level == 6)
            enemy = GameManager.Instance.pool.GetEnemies(0);
        else if (level == 7)
            enemy = GameManager.Instance.pool.GetEnemies(2);
        else if (level == 8)
            enemy = GameManager.Instance.pool.GetEnemies(0);
        else
            enemy = GameManager.Instance.pool.GetEnemies(4);

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;


        try
        {
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Log(enemy + " , " + enemy.gameObject.name);
            throw;
        }

    }
}


[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}