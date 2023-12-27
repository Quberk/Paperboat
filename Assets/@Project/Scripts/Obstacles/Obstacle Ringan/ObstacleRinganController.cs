using UnityEngine;

public class ObstacleRinganController : MonoBehaviour
{
    Rigidbody myRb;
    private float random;

    MoveController moveController;
    ObstacleDestroyer obstacleDestroyer;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();

        moveController = FindObjectOfType<MoveController>();
        obstacleDestroyer = FindObjectOfType<ObstacleDestroyer>();
    }

    void DestroyedByPlayer()
    {
        if (obstacleDestroyer.GetActivateObstacleDestroyerStatus() == true) {
            moveController.BackIntoRightPositionFromCamera();
            moveController.setForwardSpeed(moveController.getForwardSpeed() - 0.8f);
            obstacleDestroyer.DeactivateObstacleDestroyer();
        }
    }

    private void OnDestroy()
    {
        DestroyedByPlayer();
    }


}
