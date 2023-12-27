using UnityEngine;

public class IkanController : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float drowningSpeed;

    private GameObject obstacleDetector;
    private GameObject myCam;

    [SerializeField]
    private GameObject dangerSignal;

    private GameObject perahu;

    private float zAxisWait;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        obstacleDetector = GameObject.Find("Obstacle_Detector");
        myCam = GameObject.FindGameObjectWithTag("MainCamera");
        perahu = GameObject.FindGameObjectWithTag("Perahu");
        gameController = FindObjectOfType<GameController>();

        Instantiate(dangerSignal,
                    new Vector3(transform.position.x, 1.89f, transform.position.z), Quaternion.identity);

        //Menambah jumlah ikan yg terpanggil di Scene
        gameController.fishes += 1;

        //Menambah Danger Signal dalam Scene
        gameController.dangerSignals++;
    }


    private void FixedUpdate()
    {
        float kecepatanPerahu = 0f;
        if (perahu.GetComponent<MoveController>() && perahu != null) kecepatanPerahu = perahu.GetComponent<MoveController>().getForwardSpeed();
        forwardSpeed = 11f - kecepatanPerahu;

        if (transform.position.y <= -3f) Destroy(gameObject);

        if ((myCam.transform.position.z - transform.position.z) >= 10)
        {
            transform.position = new Vector3(transform.position.x,
                                            transform.position.y - (drowningSpeed * Time.deltaTime),
                                            transform.position.z - (forwardSpeed * Time.deltaTime));
            return;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (forwardSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Perahu") && obstacleDetector.GetComponent<DeadController>().GetDeadData() == false)
        {
            obstacleDetector.GetComponent<DeadController>().Dead(false);
            return;
        }
    }

    private void OnDestroy()
    {
        //Mengurangi jumlah ikan yg ada di Scene
        gameController.fishes -= 1;
    }
}
