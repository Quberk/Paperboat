using UnityEngine;

public class KodokController : MonoBehaviour
{
    private float playerXDistance;
    [SerializeField]
    private Animator kodokAnim;

    [SerializeField]
    private float pushPower = 100f;

    private GameObject[] pushPoints;

    // Start is called before the first frame update
    void Start()
    {
        pushPoints = GameObject.FindGameObjectsWithTag("PushPoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            playerXDistance = other.transform.position.x - transform.position.x;

            kodokAnim.SetTrigger("attacking");

            //Player berada di sebelah kiri Kodok
            if (playerXDistance >= 0)
            {
                MoveController mc = other.GetComponent<MoveController>();
                mc.addOutsideForce(5f);
                return;
            }

            //Player berada di sebelah kanan kodok
            if (playerXDistance < 0)
            {
                MoveController mc = other.GetComponent<MoveController>();
                mc.addOutsideForce(-5f);
                return;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            float randomNum = Random.Range(0, pushPoints.Length * 100f);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            for (int i = 1; i <= pushPoints.Length; i++)
            {
                if (randomNum <= i * 100f)
                {
                    rb.AddForceAtPosition(Vector3.down * pushPower, pushPoints[i].transform.position);
                    break;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            MoveController mc = other.GetComponent<MoveController>();
            mc.resetOutsideForce();
        }
    }
}
