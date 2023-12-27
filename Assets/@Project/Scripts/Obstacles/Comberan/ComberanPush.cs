using UnityEngine;

public class ComberanPush : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float pushPower = 100f;

    private GameObject[] pushPoints;

    float randomNum;

    // Start is called before the first frame update
    void Start()
    {
        pushPoints = GameObject.FindGameObjectsWithTag("PushPoint");
        randomNum = Random.Range(0, pushPoints.Length * 100f);
    }

    public void OnObjectSpawn()
    {
        pushPoints = GameObject.FindGameObjectsWithTag("PushPoint");
        randomNum = Random.Range(0, pushPoints.Length * 100f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            for (int i = 1; i <= pushPoints.Length; i++)
            {
                if (randomNum <= i * 100f)
                {
                    rb.AddForceAtPosition(Vector3.down * pushPower, pushPoints[i - 1].transform.position);
                    break;
                }
            }
            
        }
    }
}
