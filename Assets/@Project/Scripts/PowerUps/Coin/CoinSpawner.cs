using UnityEngine;

public class CoinSpawner : MonoBehaviour, IPooledObject
{
    ObjectPooler objectPooler;

    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject[] childObject;
    [SerializeField] private bool coinSky;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        for (int i = 0; i < childObject.Length; i++)
        {
            //GameObject myCoin = Instantiate(coin, childObject[i].transform.position, Quaternion.identity);
            GameObject myCoin = objectPooler.SpawnFromThePool("Coins", childObject[i].transform.position, Quaternion.identity);

            if (coinSky) myCoin.tag = "CoinSky";
            else myCoin.tag = "Coin";
        }

        if (coinSky == false) {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        objectPooler = ObjectPooler.Instance;

        for (int i = 0; i < childObject.Length; i++)
        {
            //GameObject myCoin = Instantiate(coin, childObject[i].transform.position, Quaternion.identity);
            GameObject myCoin = objectPooler.SpawnFromThePool("Coins", childObject[i].transform.position, Quaternion.identity);

            if (coinSky) myCoin.tag = "CoinSky";
            else myCoin.tag = "Coin";
        }

        if (coinSky == false)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(false);
    }
}
