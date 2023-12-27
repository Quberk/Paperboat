using UnityEngine;

public class CocroachSpawner : MonoBehaviour
{
    private GameObject myCam;
    private GameObject perahu;
    [SerializeField]
    private GameObject kecoak;

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
            Instantiate(kecoak,
                new Vector3(transform.position.x, transform.position.y, perahu.transform.position.z - 30f),
                Quaternion.Euler(0f, 0f, 0f));
            Destroy(gameObject);
        }
    }
}
