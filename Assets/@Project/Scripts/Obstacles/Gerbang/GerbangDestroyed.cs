using UnityEngine;

public class GerbangDestroyed : MonoBehaviour
{
    [SerializeField] private GameObject parentToDestroy;
    private CameraSwithController myCamera;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSwithController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyTheParentObject()
    {
        parentToDestroy.SetActive(false);
        myCamera.ResetPosToNormal();
    }
}
