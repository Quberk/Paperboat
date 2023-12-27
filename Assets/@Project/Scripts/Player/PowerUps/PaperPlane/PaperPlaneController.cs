using UnityEngine;

public class PaperPlaneController : MonoBehaviour
{
    private string controlMode = "Drag";

    Rigidbody rb;
    float dirX;
    private float forwardSpeed;
    GameController gameController;

    [SerializeField]
    private float moveSpeed = 20f;

    private bool backToInitialPos = false;

    [SerializeField] private Animator pesawatGfxAnim;

    private GameObject myCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        forwardSpeed = gameController.GetTheGlobalSpeed();

        pesawatGfxAnim.SetBool("SideWall", false);

        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        pesawatGfxAnim.SetFloat("RightXValue", dirX / -moveSpeed);
    }

    private void FixedUpdate()
    {
        //Move Right and Left
        rb.velocity = new Vector3(dirX, 0f, 0f);

        //Move Forward
        transform.position = new Vector3(transform.position.x,
                                transform.position.y,
                                transform.position.z + forwardSpeed * Time.deltaTime);

        if (backToInitialPos && ((myCamera.transform.position.z - transform.position.z) >= 3.9f))
        {
            backToInitialPos = false;
            forwardSpeed = gameController.GetTheGlobalSpeed();
        }

        else if (backToInitialPos)
            transform.position = new Vector3(transform.position.x,
                                    transform.position.y,
                                    transform.position.z + (forwardSpeed + 0.7f) * Time.deltaTime);

        forwardSpeed = gameController.GetTheGlobalSpeed();

        #region Drag Control

        if (controlMode == "Drag")
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                float touchXPos = (touch.position.x - (Screen.width / 2)) / (Screen.width / 2);

                dirX = (touchXPos * -moveSpeed * 1.5f);
            }

            else dirX = 0f;
        }

        #endregion

        #region Gyro Control

        if (controlMode == "Gyro")
            dirX = (Input.acceleration.x * -moveSpeed);

        #endregion

        
        #region KeyBoard Control
        /*
        float touchXPosKeyboard = Input.GetAxis("Horizontal");
        touchXPosKeyboard /= 2;
        if (touchXPosKeyboard != 0)
            dirX = (touchXPosKeyboard * -moveSpeed);
        */
        #endregion
        
    }

    public void BackIntoRightPositionFromCamera()
    {
        backToInitialPos = true;

    }

    public void SetTheControlMode(string mode)
    {
        controlMode = mode;
    }
}
