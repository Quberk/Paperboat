using UnityEngine;

public class MeterCounter : MonoBehaviour, IPooledObject
{
    GameController gameController;
    public static int currentMeterCount;

    [SerializeField] private bool firstWall;

    [SerializeField] private GameObject highScoreFinishLine;

    [SerializeField] private GameObject highScoreMetFx;

    private bool imTheHighScore = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (firstWall) { currentMeterCount = 9;
            return;
        }
    }

    public void OnObjectSpawn()
    {
        gameController = FindObjectOfType<GameController>();

        if (firstWall)
        {
            currentMeterCount = 9;
            return;
        }

        currentMeterCount++;
        highScoreFinishLine.SetActive(false);
        imTheHighScore = false;

        if (currentMeterCount == gameController.GetHighScore())
        {
            highScoreFinishLine.SetActive(true);
            imTheHighScore = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            gameController.ScoreIncrement();

            if (imTheHighScore)
            {
                imTheHighScore = false;
                Instantiate(highScoreMetFx,
                            new Vector3(highScoreFinishLine.transform.position.x, highScoreFinishLine.transform.position.y + 1f, highScoreFinishLine.transform.position.z),
                            Quaternion.Euler(0f,0f,0f));
                highScoreFinishLine.SetActive(false);
            }
        }
    }
}
