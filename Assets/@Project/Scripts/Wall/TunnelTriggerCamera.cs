using UnityEngine;

public class TunnelTriggerCamera : MonoBehaviour, IPooledObject
{
    private CameraSwithController mainCamera;

    [SerializeField]
    private bool rightTunnel;
    [SerializeField]
    private bool leftTunnel;
    [SerializeField]
    private bool underTunnel;
    [SerializeField]
    private bool lastTunnel;

    private float startPoint;
    private float finishPoint;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSwithController>();

        startPoint = transform.position.z + 8.84607f;
        finishPoint = transform.position.z - 11.514f;
    }

    public void OnObjectSpawn()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSwithController>();

        startPoint = transform.position.z + 8.84607f;
        finishPoint = transform.position.z - 11.514f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] obstacleBerat = GameObject.FindGameObjectsWithTag("ObstacleBerat");
        GameObject[] obstacleRingan = GameObject.FindGameObjectsWithTag("ObstacleRingan");
        GameObject[] obstacleSlow = GameObject.FindGameObjectsWithTag("ObstacleSlow");
        GameObject[] cockroaches = GameObject.FindGameObjectsWithTag("Cockroach");

        //Obs Berat
        for (int i = 0; i < obstacleBerat.Length; i++)
        {
            if (obstacleBerat[i].transform.position.z >= finishPoint && obstacleBerat[i].transform.position.z <= startPoint)
            {
                Destroy(obstacleBerat[i]);
            }
        }

        //Obs Ringan
        for (int i = 0; i < obstacleRingan.Length; i++)
        {
            if (obstacleRingan[i].transform.position.z >= finishPoint && obstacleRingan[i].transform.position.z <= startPoint)
            {
                Destroy(obstacleRingan[i]);
            }
        }

        //Obs Slowdown
        for (int i = 0; i < obstacleSlow.Length; i++)
        {
            if (obstacleSlow[i].transform.position.z >= finishPoint && obstacleSlow[i].transform.position.z <= startPoint)
            {
                Destroy(obstacleSlow[i]);
            }
        }

        //Obs Kecoak
        for (int i = 0; i < cockroaches.Length; i++)
        {
            if (cockroaches[i].transform.position.z >= finishPoint && cockroaches[i].transform.position.z <= startPoint)
            {
                Destroy(cockroaches[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perahu") && rightTunnel)
        {
            mainCamera.ActivateRightCamera();
        }

        if (other.CompareTag("Perahu") && leftTunnel)
        {
            mainCamera.ActivateLeftCamera();
        }

        if (other.CompareTag("Perahu") && underTunnel)
        {
            mainCamera.ActivateLowCamera();
        }

        if (other.CompareTag("Perahu") && lastTunnel)
        {
            mainCamera.ResetPosToNormal();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Perahu") && !underTunnel)
        {
            mainCamera.ResetPosToNormal();
        }
    }
}
