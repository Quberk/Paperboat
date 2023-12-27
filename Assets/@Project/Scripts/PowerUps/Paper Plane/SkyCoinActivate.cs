using UnityEngine;

public class SkyCoinActivate : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject[] skyCoins;

    // Start is called before the first frame update
    void Start()
    {
        float randomNum = Random.Range(0, skyCoins.Length * 100f);

        for (int i = 0; i < skyCoins.Length; i++)
        {
            skyCoins[i].SetActive(false);
        }

        for (int i = 0; i < skyCoins.Length; i++)
        {
            if (randomNum <= ((i + 1) * 100f)) { 
                skyCoins[i].SetActive(true);
                break;
            }
        }
    }

    public void OnObjectSpawn()
    {
        float randomNum = Random.Range(0, skyCoins.Length * 100f);

        for (int i = 0; i < skyCoins.Length; i++)
        {
            skyCoins[i].SetActive(false);
        }

        for (int i = 0; i < skyCoins.Length; i++)
        {
            if (randomNum <= ((i + 1) * 100f))
            {
                skyCoins[i].SetActive(true);
                break;
            }
        }
    }
}
