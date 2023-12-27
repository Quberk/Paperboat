using UnityEngine;

public class BirdController : MonoBehaviour
{
    private GameObject perahu;
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    private float attackingPos;
    private bool dead;
    private bool killPlayer = false;

    [SerializeField]
    private Animator birdAnim;

    [SerializeField]
    private GameObject dangerSignal;

    private DeadController deadController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        perahu = GameObject.FindGameObjectWithTag("Perahu");
        deadController = FindObjectOfType<DeadController>();

        Instantiate(dangerSignal, new Vector3(7.416f, 4.649f, perahu.transform.position.z), Quaternion.identity);
    }

    private void Update()
    {
        perahu = GameObject.FindGameObjectWithTag("Perahu");
    }

    private void FixedUpdate()
    {        

        if (transform.position.y <= 1.02f) Destroy(gameObject);
        else if (transform.position.y >= 10f) Destroy(gameObject);

        if (killPlayer == true)
        {
            Vector3 target = new Vector3(perahu.transform.position.x, 20f, perahu.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            return;
        }

        if (dead == false)
        {
            if (transform.position.z <= attackingPos)
            {
                Vector3 target = new Vector3(perahu.transform.position.x, perahu.transform.position.y, perahu.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * 2 * Time.deltaTime);
                return;
            }

            else
            {
                Vector3 target = new Vector3(perahu.transform.position.x, transform.position.y, perahu.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }

        // Jika Player Mati maka burung tidak lagi mengejar player tetapi terbang ke angkasa raya, dadah burung
        if (deadController.GetDeadData() == true) killPlayer = true;

    }

    public void SetTheAttackingPos(float pos)
    {
        attackingPos = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObstacleBerat"))
        {
            rb.useGravity = true;
            dead = true;
            birdAnim.SetTrigger("dead");
        }

        if (collision.gameObject.CompareTag("Perahu"))
        {
            killPlayer = true;
        }
    }

}
