using UnityEngine;

public class BirdAttackAreaController : MonoBehaviour,IPooledObject
{
    [SerializeField]
    private GameObject bird;
    [SerializeField]
    private GameObject pointToSpawnBird;
    [SerializeField]
    private GameObject pointBirdAttack;
    private GameObject perahu;
    [SerializeField]
    private GameObject[] obstacles;

    private bool alreadyInstantiate = false;


    // Start is called before the first frame update
    void Start()
    {
        perahu = GameObject.FindGameObjectWithTag("Perahu");
        alreadyInstantiate = false;

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }

            float randomNum = Random.Range(0f, obstacles.Length * 100f);
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (randomNum <= (i+1) * 100f)
            {
                obstacles[i].SetActive(true);
                break;
            }
        }
    }

    public void OnObjectSpawn()
    {
        perahu = GameObject.FindGameObjectWithTag("Perahu");
        alreadyInstantiate = false;

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }

        float randomNum = Random.Range(0f, obstacles.Length * 100f);
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (randomNum <= (i + 1) * 100f)
            {
                obstacles[i].SetActive(true);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (perahu.transform.position.z <= pointToSpawnBird.transform.position.z && alreadyInstantiate == false)
        {
            GameObject myBird = Instantiate(bird, new Vector3(5.3f, 4.224f, perahu.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
            myBird.GetComponent<BirdController>().SetTheAttackingPos(pointBirdAttack.transform.position.z);
            alreadyInstantiate = true;
        }
    }
}
