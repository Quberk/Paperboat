using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private BoxCollider myCollider;
    private bool active = false;

    [SerializeField] private float durationTime = 0.1f;
    private float durationCounter = 0f;

    private MoveController playerMoveController;

    private bool resetPlayerPosition = true;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        myCollider.enabled = false;

        playerMoveController = GameObject.FindGameObjectWithTag("Perahu").GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true){
            durationCounter += Time.deltaTime;
            if (durationCounter >= durationTime)
            {
                DeactivateObstacleDestroyer();
                durationCounter = 0f;
            }
        }
    }

    public void ActivateObstacleDestroyer()
    {
        ResetPlayerPosition();
        myCollider.enabled = true;
        active = true;
    }

    public void DeactivateObstacleDestroyer()
    {
        myCollider.enabled = false;
        active = false;

        durationCounter = 0f;
    }

    public bool GetActivateObstacleDestroyerStatus()
    {
        return active;
    }

    public void SetResetPlayerPosition(bool resettingPlayer)
    {
        resetPlayerPosition = resettingPlayer;
    }

    //Fungsi agar player tidak stuck di posisi yang tidak seharusnya relatif terhadap posisi kamera
    void ResetPlayerPosition()
    {
        if (resetPlayerPosition)
        {
            playerMoveController.setForwardSpeed((playerMoveController.getForwardSpeed()) - 0.8f);
            playerMoveController.BackIntoRightPositionFromCamera();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleBerat"))
        {
            ResetPlayerPosition();

            //Jika obstacle berat adalah Obstacle Wall
            if (other.GetComponent<ObstacleWall>())
                other.gameObject.SetActive(false);

            else
                Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Gerbang"))
        {
            ResetPlayerPosition();

            if (other.GetComponent<GerbangDestroyed>())
            {
                other.GetComponent<GerbangDestroyed>().DestroyTheParentObject();
                return;
            }

            other.gameObject.SetActive(false);
            return;
        }

        if (other.CompareTag("ObstacleSlow"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("ObstacleRingan"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Cockroach"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Ikan"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ObstacleBerat"))
        {
            ResetPlayerPosition();

            //Jika obstacle berat adalah Obstacle Wall
            if (other.GetComponent<ObstacleWall>())
                other.gameObject.SetActive(false);

            else
                Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Gerbang"))
        {
            ResetPlayerPosition();

            if (other.GetComponent<GerbangDestroyed>())
            {
                other.GetComponent<GerbangDestroyed>().DestroyTheParentObject();
                return;
            }

            other.gameObject.SetActive(false);
            return;
        }

        if (other.CompareTag("ObstacleSlow"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("ObstacleRingan"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Cockroach"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Ikan"))
        {
            ResetPlayerPosition();

            Destroy(other.gameObject);
            return;
        }
    }
}
