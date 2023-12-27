using UnityEngine;

public class WallDieTime : MonoBehaviour, IPooledObject
{
    private GameObject perahuPos;
    [SerializeField]
    private float distanceTresshold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        perahuPos = GameObject.FindGameObjectWithTag("Perahu");
    }

    public void OnObjectSpawn()
    {
        perahuPos = GameObject.FindGameObjectWithTag("Perahu");
    }

    // Update is called once per frame
    void Update()
    {
        perahuPos = GameObject.FindGameObjectWithTag("Perahu");
        if (perahuPos != null)
            if (perahuPos.transform.position.z - transform.position.z <= -distanceTresshold)
                gameObject.SetActive(false);
    }
}
