using UnityEngine;

public class BodyCollider : MonoBehaviour
{
    [SerializeField]
    private DeadController deadController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cockroach") && deadController.GetDeadData() == false)
        {
            deadController.Dead(false);
        }
    }
}
