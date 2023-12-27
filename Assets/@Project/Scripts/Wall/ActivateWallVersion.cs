using UnityEngine;

public class ActivateWallVersion : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject[] wallVersion;

    // Start is called before the first frame update
    void Start()
    {
        float randomNum = Random.Range(0, 300f);

        if (randomNum <= 100)
        {
            wallVersion[0].SetActive(true);
            wallVersion[1].SetActive(false);
            wallVersion[2].SetActive(false);
            return;
        }

        else if (randomNum <= 200)
        {
            wallVersion[0].SetActive(false);
            wallVersion[1].SetActive(true);
            wallVersion[2].SetActive(false);
            return;
        }

        else
        {
            wallVersion[0].SetActive(false);
            wallVersion[1].SetActive(false);
            wallVersion[2].SetActive(true);
            return;
        }
    }

    public void OnObjectSpawn()
    {
        float randomNum = Random.Range(0, 300f);

        if (randomNum <= 100)
        {
            wallVersion[0].SetActive(true);
            wallVersion[1].SetActive(false);
            wallVersion[2].SetActive(false);
            return;
        }

        else if (randomNum <= 200)
        {
            wallVersion[0].SetActive(false);
            wallVersion[1].SetActive(true);
            wallVersion[2].SetActive(false);
            return;
        }

        else
        {
            wallVersion[0].SetActive(false);
            wallVersion[1].SetActive(false);
            wallVersion[2].SetActive(true);
            return;
        }
    }
}
