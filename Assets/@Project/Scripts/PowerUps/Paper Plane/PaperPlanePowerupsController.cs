using UnityEngine;

public class PaperPlanePowerupsController : MonoBehaviour
{
    [SerializeField] private GameObject impactGfx;
    private CameraSwithController mainCamera;
    private GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSwithController>();
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
            mainCamera.ActivateHighCamera();
            gameController.ActivatePaperPlane();

            Destroy(gameObject);
            return;
        }
    }
}
