using UnityEngine;

public class ActivateWater : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float distanceFromPlayer = 13.959999f;
    [SerializeField]
    private GameObject[] targetToActivate;
    private GameObject perahu;

    // Start is called before the first frame update
    void Start()
    {
        perahu = GameObject.FindGameObjectWithTag("Perahu");
        
        for (int i = 0; i < targetToActivate.Length; i++)
        {
            targetToActivate[i].SetActive(false);
        }
    }

    public void OnObjectSpawn()
    {
        perahu = GameObject.FindGameObjectWithTag("Perahu");

        for (int i = 0; i < targetToActivate.Length; i++)
        {
            targetToActivate[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        perahu = GameObject.FindGameObjectWithTag("Perahu");
        if (perahu != null)
            for (int i = 0; i < targetToActivate.Length; i++)
            {
                if (Mathf.Abs(transform.position.z - perahu.transform.position.z) <= distanceFromPlayer) targetToActivate[i].SetActive(true);
            }
        
    }
}
