using UnityEngine;

public class ComberanArea : MonoBehaviour
{
    [SerializeField]
    private float pushingPower = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            MoveController mc = other.GetComponent<MoveController>();
            mc.addOutsideForce(pushingPower);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            MoveController mc = other.GetComponent<MoveController>();
            mc.resetOutsideForce();
        }
    }
}
