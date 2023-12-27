using UnityEngine;

public class CameraSwithController : MonoBehaviour
{

    private Vector3 targetPos;
    [SerializeField]
    private float speed = 4f;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        targetPos = new Vector3(7.6f, 5.382f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float x = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
        float y = Mathf.MoveTowards(transform.position.y, targetPos.y, speed * Time.deltaTime);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void ActivateRightCamera()
    {
        targetPos = new Vector3(6.86f, 3.9f, transform.position.z);
    }

    public void ActivateLeftCamera()
    {
        targetPos = new Vector3(8.57f, 3.9f, transform.position.z);
    }

    public void ActivateLowCamera()
    {
        targetPos = new Vector3(7.6f, 3.57f, transform.position.z);
    }

    public void ActivateHighCamera()
    {
        targetPos = new Vector3(7.6f, 6.404f, transform.position.z);
    }

    public void ResetPosToNormal()
    {
        targetPos = new Vector3(7.6f, 5.382f, transform.position.z);
    }
    
}
