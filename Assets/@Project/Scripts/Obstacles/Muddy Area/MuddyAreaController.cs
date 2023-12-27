using UnityEngine;

public class MuddyAreaController : MonoBehaviour
{
    [SerializeField]
    private float minusSpeed;

    private GameController gameController;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            player = other.gameObject;
            other.GetComponent<MoveController>().setForwardSpeed((other.GetComponent<MoveController>().getForwardSpeed()) + minusSpeed);
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            player = null;
            other.GetComponent<MoveController>().setForwardSpeed((other.GetComponent<MoveController>().getForwardSpeed()) - 0.8f);
            other.GetComponent<MoveController>().BackIntoRightPositionFromCamera();
            return;
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.GetComponent<MoveController>().setForwardSpeed((player.GetComponent<MoveController>().getForwardSpeed()) - 0.8f);
            player.GetComponent<MoveController>().BackIntoRightPositionFromCamera();
        }

    }
}
