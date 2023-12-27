using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Physics.IgnoreLayerCollision(14, 13);
        Physics.IgnoreLayerCollision(14, 17);
    }
}
