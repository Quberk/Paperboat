using UnityEngine;

public class IkanSpawner : MonoBehaviour
{
    private GameObject myCam;
    private GameObject perahu;
    [SerializeField]
    private GameObject ikan;

    // Start is called before the first frame update
    void Start()
    {
        myCam = GameObject.FindGameObjectWithTag("MainCamera");
        perahu = GameObject.FindGameObjectWithTag("Perahu");
    }

    // Update is called once per frame
    void Update()
    {
        if (perahu.transform.position.z <= transform.position.z)
        {
            Instantiate(ikan, 
                new Vector3(transform.position.x, transform.position.y, myCam.transform.position.z + 6.73f), 
                Quaternion.Euler(0f, 0f, 0f));

            Destroy(gameObject);
        }
    }
}
