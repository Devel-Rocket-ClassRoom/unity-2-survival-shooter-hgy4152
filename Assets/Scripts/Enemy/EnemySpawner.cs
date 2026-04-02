using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] Zombie;
    public Transform[] spawnPoint;

    private List<Enemy> enemyList = new List<Enemy>(); 

    private float spawnTime = 3f;
    private float lastSpawnTime = 0f;

    private bool isStart = false;
    private int wave = 1;
    private int maxZombie = 1;


    public void Update()
    {
        WaveCheck();

        if (Time.time > spawnTime + lastSpawnTime && maxZombie > 0)
        {
            lastSpawnTime = Time.time;

            int index = 0;
            float spawnChance = Random.value;

            if (spawnChance < 0.4f)
            {
                index = 0;
            }
            else if (spawnChance < 0.8f)
            {
                index = 1;
            }
            else if(spawnChance <= 1.0f)
            {
                index = 2;
            }

            var pos = spawnPoint[Random.Range(0, spawnPoint.Length)];


        

            if (NavMesh.SamplePosition(pos.position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                Vector3 spawnPosition = hit.position + Vector3.up * 0.5f;
                var zom = Instantiate(Zombie[index], spawnPosition, pos.rotation);

                zom.gameObject.GetComponent<Enemy>().SetUp();

                enemyList.Add(zom);

                maxZombie--;
            }

 


        }

        enemyList.RemoveAll((x) => x.health <= 0);
    }

    private void WaveCheck()
    {
        if (!isStart)
        {
            isStart = true;
            wave++;
            maxZombie = (int)(wave * 1.5);

        }
        else if (maxZombie <= 0 && enemyList.Count == 0)
        {
            isStart = false;
        }



    }
}
