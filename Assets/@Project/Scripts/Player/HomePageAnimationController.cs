using UnityEngine;

public class HomePageAnimationController : MonoBehaviour
{
    private Animator thePerahuAnim;
    MoveController moveController;

    [Header("UI")]
    [SerializeField] private GameObject waterScreenFx;
    [SerializeField] private GameObject layarPerahuBtn;

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<MoveController>();
        thePerahuAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFallingAnim()
    {
        thePerahuAnim.SetTrigger("falling");
    }

    public void FallingAnimFinished()
    {
        thePerahuAnim.enabled = false;
        moveController.GameStart();
        waterScreenFx.SetActive(true);
        layarPerahuBtn.SetActive(true);
    }
}
