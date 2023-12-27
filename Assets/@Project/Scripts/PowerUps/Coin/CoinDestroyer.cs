using UnityEngine;

public class CoinDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ObstacleBerat") &&
            !other.CompareTag("ObstacleSlow") &&
            !other.CompareTag("ObstacleRingan"))
        {
            return;
        }

        parentObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("ObstacleBerat") &&
            !other.CompareTag("ObstacleSlow") &&
            !other.CompareTag("ObstacleRingan"))
        {
            return;
        }

        parentObject.SetActive(false);

    }
}
