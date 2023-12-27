using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private GameObject cameraPos;
    [SerializeField]
    private float distanceTresshold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraPos.transform.position.z - transform.position.z <= -distanceTresshold)
            Destroy(gameObject);
    }
}
