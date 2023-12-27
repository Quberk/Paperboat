using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private GameObject[] poses;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnObjectSpawn()
    {
        float randomNum = Random.Range(0, poses.Length * 100f);
        float randomNum1 = Random.Range(0, powerUps.Length * 100f);

        //Pos of the PowerUps
        for (int i = 0; i < poses.Length; i++)
        {
            if (randomNum <= (i + 1) * 100f)
            {
                //Power Ups selected
                for (int j = 0; j < powerUps.Length; j++)
                {
                    if (randomNum1 <= (j + 1) * 100f)
                    {
                        Instantiate(powerUps[j], poses[i].transform.position, Quaternion.Euler(0f, 0f, 0f));
                        break;
                    }
                }

                break;
            }
        }
    }
}
