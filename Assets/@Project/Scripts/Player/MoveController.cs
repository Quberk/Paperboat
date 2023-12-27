using UnityEngine;

public class MoveController : MonoBehaviour
{
    private string controlMode = "Drag";

    Rigidbody rb;
    float dirX;
    float dirForAnim;
    float outsiderForce;

    [SerializeField]
    private Animator perahuGfxAnim;

    [SerializeField]
    private float moveSpeed;

    private float deadMoveSpeed;

    [Header("Starting The Game")]
    private bool gameStart = false;

    [Header("Swipe Control")]
    private float lastFingerPos;
    private float touchXPos;

    [Header("Forward Move")]
    private float forwardSpeed;
    private bool forwardMove = true;
    private bool backToInitialPos = false;

    [Header("Dead Thing")]
    [SerializeField]
    private GameObject pushFrontPoint;
    [SerializeField]
    private GameObject pushBackPoint;
    [SerializeField]
    private GameObject pushMiddlePoint;
    [SerializeField]
    private GameObject shadowObject;

    private GameObject myCamera;

    GameController gameController;

    private bool alreadyStatingTheRigidbodyProp = false;

    [Header("Random Push Periodicaly")]
    private float coolDownTime = 2f;
    private float coolDownCounter = 0f;

    [Header("Sound")]
    [SerializeField] private AudioSource playerMoveSfx;
    private bool activateMoveSound = false;
    private bool lastMoveSoundState = false;
    private bool movingRight = false;
    private bool isLastMoveRight = false;
    private float lastXPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        gameController = FindObjectOfType<GameController>();

        rb.useGravity = false;

        playerMoveSfx.Stop();
    }

    public void GameStart()
    {
        gameStart = true;
    }

    public bool GetGameStartState()
    {
        return gameStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            perahuGfxAnim.SetFloat("RightXValue", dirX / -moveSpeed);

            if (forwardMove == false) perahuGfxAnim.SetBool("SideWall", true);

            PeriodiclyPush();

            //Jika perahu sampai pada sumbu Z yang diharuskan maka kamera mulai berjalan beriringan dengan perahu
            if (transform.position.z <= 4.85f)
            {
                gameController.GameStart();
            }


            //Pakai Gravity
            rb.useGravity = true;
            return;
        }

    }

    private void FixedUpdate()
    {
        if (gameStart)
        {

            #region Drag Control

            if (controlMode == "Drag")
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    touchXPos = (touch.position.x - (Screen.width / 2)) / (Screen.width / 2);

                    dirX = (touchXPos * -moveSpeed * 1.5f) + outsiderForce;

                    lastFingerPos = touchXPos;
                }

                else dirX = outsiderForce;
            }

            #endregion

            #region Gyro Control

            if (controlMode == "Gyro")
                dirX = (Input.acceleration.x * -moveSpeed) + outsiderForce;

            #endregion

            
            #region KeyBoard Control
            /*
            touchXPos = Input.GetAxis("Horizontal");
            touchXPos /= 2;
            if (touchXPos != 0)
                dirX = (touchXPos * -moveSpeed) + outsiderForce;
            */
            #endregion

            #region Moving Sound Effect
            if ((transform.position.x - lastXPos) >= 0.003f)
            {
                activateMoveSound = true;
                movingRight = true;
            }
                
            else if ((transform.position.x - lastXPos) <= -0.003f)
            {
                activateMoveSound = true;
                movingRight = false;
            }
            else
                activateMoveSound = false;

            if (activateMoveSound && !lastMoveSoundState) playerMoveSfx.Play();
            else if (isLastMoveRight != movingRight) playerMoveSfx.Play();
            else if (!activateMoveSound) playerMoveSfx.Stop();

            lastMoveSoundState = activateMoveSound;
            lastXPos = transform.position.x;
            isLastMoveRight = movingRight;
            #endregion

            //Membuat posisi Player pada sumbu Y dapat berubah
            if (alreadyStatingTheRigidbodyProp == false)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                alreadyStatingTheRigidbodyProp = true;
            }

            if (forwardMove == true)
            {

                //Move Right and Left
                rb.velocity = new Vector3(dirX, 0f, 0f);

                //Move Forward Normal
                if (transform.position.z < 4.85f)
                    transform.position = new Vector3(transform.position.x,
                                        transform.position.y,
                                        transform.position.z + forwardSpeed * Time.deltaTime);

                //Move Forward ketika awal permainan
                else
                    transform.position = new Vector3(transform.position.x,
                                        transform.position.y - 10f * Time.deltaTime,
                                        transform.position.z + forwardSpeed * Time.deltaTime) ;


                //Jika Perahu mau maju ke tempat semula
                if (backToInitialPos && ((myCamera.transform.position.z - transform.position.z) >= 5.45f))
                {
                    backToInitialPos = false;
                    forwardSpeed = gameController.GetTheGlobalSpeed();
                }

                return;
            }

            else if (forwardMove == false)
            {
                //Move Forward
                rb.AddForceAtPosition(Vector3.forward * deadMoveSpeed, pushMiddlePoint.transform.position);
                gameObject.tag = "Untagged";
                shadowObject.SetActive(false);

                return;
            }
        }
    }

    public void setForwardSpeed(float speed)
    {
        forwardSpeed = speed;

    }

    public void setStopMovingForward(bool stopping, float speed)
    {
        forwardMove = stopping;
        deadMoveSpeed = speed;
        coolDownCounter = 0f;
        coolDownTime = 1f;
        if (stopping) perahuGfxAnim.SetBool("SideWall", false);
    }

    public float getForwardSpeed()
    {
        return (gameController.GetTheGlobalSpeed());
    }

    public void BackIntoRightPositionFromCamera()
    {
        backToInitialPos = true;
    }

    public void addOutsideForce(float power)
    {
        outsiderForce += power;
    }

    public void resetOutsideForce()
    {
        outsiderForce = 0f;
    }

    public void DeadPush(float power)
    {
        rb.AddForceAtPosition(Vector3.down * power, pushFrontPoint.transform.position);

        float randomNum = Random.Range(0, 200f);

        if (randomNum <= 100)
        {
            rb.AddForceAtPosition(Vector3.right * power, pushBackPoint.transform.position);

            return;
        }

        rb.AddForceAtPosition(Vector3.left * power, pushBackPoint.transform.position);
    }

    //Fungsi untuk membuat Pushing dalam waktu random
    void PeriodiclyPush()
    {
        coolDownCounter += Time.deltaTime;

        if (coolDownCounter >= coolDownTime)
        {
            coolDownCounter = 0f;
            coolDownTime = Random.Range(3f, 6f);

            float randomNum = Random.Range(0f, 200f);

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, 0f);

            if (randomNum <= 100f)
            {
                rb.AddForceAtPosition(Vector3.down * 30f, pushFrontPoint.transform.position);
            }

            else
            {
                rb.AddForceAtPosition(Vector3.down * 30f, pushBackPoint.transform.position);
            }
        }
    }

    public void SetTheControlMode(string mode)
    {
        controlMode = mode;
    }


}
