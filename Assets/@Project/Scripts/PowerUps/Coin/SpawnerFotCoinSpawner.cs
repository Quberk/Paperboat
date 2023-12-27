using UnityEngine;

public class SpawnerFotCoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] coinSpawners;
    [SerializeField] private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        float randomNum = Random.Range(0f, coinSpawners.Length * 100);
        
        for (int i = 0; i < coinSpawners.Length; i++)
        {
            if (randomNum <= (i + 1) * 100f)
            {
                Instantiate(coinSpawners[i], 
                    new Vector3(7.622f, 2.191f, parent.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
                break;
            }
        }
    }
}
