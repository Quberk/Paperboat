using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var m in materials)
        {
            m.SetVector(Shader.PropertyToID("Camera_Pos"), transform.position);
        }
    }
}
