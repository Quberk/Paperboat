using UnityEngine;

public class CocroachController : MonoBehaviour
{
    private GameObject perahu;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distanceTresshold;
    [SerializeField]
    private float firstYPos;

    [SerializeField]
    private GameObject dangerSignal;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        perahu = GameObject.FindGameObjectWithTag("Perahu");
        transform.position = new Vector3(transform.position.x, firstYPos, transform.position.z);

        Instantiate(dangerSignal, new Vector3(transform.position.x - 1.5f, 3.69f,
                                               GameObject.FindGameObjectWithTag("MainCamera").transform.position.z - 21f), Quaternion.identity);

        gameController = FindObjectOfType<GameController>();

        //Menambah kecoak dalam Scene
        gameController.cockroaches++;

        //Menambah Danger Signal dalam Scene
        gameController.dangerSignals++;
    }

    // Update is called once per frame
    void Update()
    {
        float kecepatanPerahu = 0f;

        if (perahu.gameObject.activeSelf == true)
            if (perahu.GetComponent<MoveController>())
                kecepatanPerahu = perahu.GetComponent<MoveController>().getForwardSpeed();

        speed = 27f + kecepatanPerahu;

        if ((perahu.transform.position.z - transform.position.z) <= distanceTresshold)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (speed * Time.deltaTime));
            return;
        }

        Vector3 pos = new Vector3(transform.position.x, perahu.transform.position.y, perahu.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        gameController.cockroaches--;
    }
}
