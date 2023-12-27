using UnityEngine;

public class BridgeController : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        float randomNum = Random.Range(0f, 300f);

        if (randomNum <= 100)
        {
            obstacles[0].SetActive(true);
            obstacles[1].SetActive(false);
        }

        else if (randomNum <= 200)
        {
            obstacles[1].SetActive(true);
            obstacles[0].SetActive(false);
        }

        else
        {
            obstacles[1].SetActive(false);
            obstacles[0].SetActive(false);
        }
    }

    public void OnObjectSpawn()
    {
        float randomNum = Random.Range(0f, 300f);

        if (randomNum <= 100)
        {
            obstacles[0].SetActive(true);
            obstacles[1].SetActive(false);
        }

        else if (randomNum <= 200)
        {
            obstacles[1].SetActive(true);
            obstacles[0].SetActive(false);
        }

        else
        {
            obstacles[1].SetActive(false);
            obstacles[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
