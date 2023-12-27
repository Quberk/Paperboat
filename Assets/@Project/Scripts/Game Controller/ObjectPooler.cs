using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private List<Pool> pools;
    [SerializeField] private Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject SpawnFromThePool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject[] pooledObj = objectToSpawn.GetComponents<IPooledObject>();
        IPooledObject[] pooledObjChild = objectToSpawn.GetComponentsInChildren<IPooledObject>();

        if (pooledObj.Length != 0)
        {
            //Memanggil Fungsi pengganti start ketika memanggil Object dari Pool
            foreach (IPooledObject pooledObjects in pooledObj)
            {
                pooledObjects.OnObjectSpawn();
            }

        }

        if (pooledObjChild.Length != 0)
        {
            //Memanggil Fungsi pengganti start ketika memanggil Object dari Pool
            foreach (IPooledObject pooledObjects in pooledObjChild)
            {
                pooledObjects.OnObjectSpawn();
            }

        }

        poolDictionary[tag].Enqueue(objectToSpawn);


        return objectToSpawn;

    }
}
