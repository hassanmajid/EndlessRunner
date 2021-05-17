using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{

    [System.Serializable]

    public class Pool
    {
        public GameObject prefab;
        public int size;
        public string type;
    }
    GameObject objectToSpawn;

    public static objectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {

            Queue<GameObject> objectpool = new Queue<GameObject>();

            for(int i=0; i<pool.size;i++)
            {


                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }

            poolDictionary.Add(pool.type, objectpool);



        }


    }

    public GameObject SpawnFromPool(string type,Vector3 position,Quaternion rotation)
    {

        if(!poolDictionary.ContainsKey(type))
        {

            Debug.LogWarning("Pool with type" + type + " Doesnt exists.");
            return null;

        }

        objectToSpawn = poolDictionary[type].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;

    }



}
