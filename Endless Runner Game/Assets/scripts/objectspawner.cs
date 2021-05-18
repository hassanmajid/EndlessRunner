using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectspawner : MonoBehaviour
{

    private bool spawningobject = false;
    public static objectspawner Instance;
    public float GroundSpawnDistance;
    


    [System.Serializable]
    public struct Spawnable
    {
        public string type;
        public float weight;
    }
    [System.Serializable]
    public struct SpawnSettings
    {
        public string type;
        public float minwait;
        public float maxwait;
        public int maxObjects;

    }
    private float totalWeight;

    public List<Spawnable> enemySpawns = new List<Spawnable>();
    public List<SpawnSettings> enemySettings = new List<SpawnSettings>();

    private void Awake()
    {
        Instance = this;
        totalWeight = 0;
        foreach(Spawnable spawn in enemySpawns)
        {
            totalWeight += spawn.weight;
        }

    }

    public void SpawnGround()
    {

        objectPooler.instance.SpawnFromPool("ground", new Vector3(0,0,GroundSpawnDistance), Quaternion.identity);    

    }

    private IEnumerator  spawnObject(string type,float time)
    {
        yield return new WaitForSeconds(time);
        objectPooler.instance.SpawnFromPool(type, new Vector3(Random.Range(-8.9f, 8.9f), 1.41f, 4f), Quaternion.identity);
        spawningobject = false;
        GameController.EnemyCount++;
    }



    private void Update()
    {
        if (!spawningobject && GameController.EnemyCount < enemySettings[0].maxObjects && !GameController.GamePaused;)
        {
            spawningobject = true;
            float pick = Random.value * totalWeight;
            int chosenIndex = 0;
            float cumulativeWeight = enemySpawns[0].weight;

            while (pick> cumulativeWeight && chosenIndex<enemySpawns.Count-1)
            {
                chosenIndex++;
                cumulativeWeight += enemySpawns[chosenIndex].weight;
            }

            StartCoroutine(spawnObject(enemySpawns[chosenIndex].type, Random.Range(enemySettings[0].minwait / GameController.DifficultyMultiplier, enemySettings[0].maxwait / GameController.DifficultyMultiplier)));
        }
    }



}
