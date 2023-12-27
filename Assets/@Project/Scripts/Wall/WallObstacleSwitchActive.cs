using UnityEngine;

public class WallObstacleSwitchActive : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        float randomNum = Random.Range(0, 200f);

        if (randomNum <= 100f)
        {
            obstacles[0].SetActive(true);
            obstacles[1].SetActive(false);
            return;
        }
        obstacles[0].SetActive(false);
        obstacles[1].SetActive(true);

    }

    public void OnObjectSpawn()
    {
        float randomNum = Random.Range(0, 200f);

        if (randomNum <= 100f)
        {
            obstacles[0].SetActive(true);
            obstacles[1].SetActive(false);
            return;
        }
        obstacles[0].SetActive(false);
        obstacles[1].SetActive(true);
    }

}
