using UnityEngine;

public class SideBodyDetector : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    DeadController deadController;

    // Start is called before the first frame update
    void Start()
    {
        deadController = FindObjectOfType<DeadController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if (other.CompareTag("ObstacleBerat"))
        {
            deadController.HalfDead();
            anim.SetTrigger("Impacted");
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WallKanan") || other.CompareTag("WallKiri"))
        {
            anim.SetBool("SideWall", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallKanan") || other.CompareTag("WallKiri"))
        {
            anim.SetBool("SideWall", false);
        }
    }
}
