using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadController : MonoBehaviour
{
    MoveController moveController;
    Rigidbody rb;
    GameController gameController;
    CapsuleCollider capsuleColl;
    BoxCollider boxColl;

    CurrencyTransactionManager currencyTransactionManager;

    [Header("Layar Perahu")]
    [SerializeField] private ObstacleDestroyer obstacleDestroyer;
    [SerializeField] private ParticleSystem layarDestroyedFx;

    [Header("Dead Score")]
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text finalCoinText;
    [SerializeField] private ReviveTimer reviveTimer;


    private bool imDead = false;

    [Header("Dead Effect")]
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject deadSfx;

    [SerializeField] private GameObject mainObject;
    [SerializeField] private Animator perahuGfx;
    [SerializeField] private Bouyancy bouyancy;
    [SerializeField] private GameObject perahuFx;
    [SerializeField] private Image waterScreenFx;
    [SerializeField] private Animator flashFx;

    [Header("Half Dead")]
    private int halfDeadCount;
    [SerializeField]
    private float halfDeadTimer = 10f;
    private float halfDeadTimeCounter = 0f;
    private Camera myCam;

    [Header("Revive Effect")]
    [SerializeField] private GameObject reviveFx;
    [SerializeField] private GameObject revivePanelFx;
    [SerializeField] private Animator reviveFxAnim;
    

    [Header("Non Activated Things when Dead")]
    private WallDieTime[] wallDieTimes;
    private FollowTarget[] followTargets;
    private ActivateWater[] activateWaters;

    private bool beingRevived = false;

    [SerializeField] private GameObject layarPerahuBtn;

    // Start is called before the first frame update
    void Start()
    {
        currencyTransactionManager = new CurrencyTransactionManager();

        moveController = mainObject.GetComponent<MoveController>();
        rb = mainObject.GetComponent<Rigidbody>();
        gameController = FindObjectOfType<GameController>();
        boxColl = GetComponent<BoxCollider>();
        capsuleColl = mainObject.GetComponent<CapsuleCollider>();
        myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        deadPanel.SetActive(false);

        layarDestroyedFx.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (beingRevived == true)
        {
            #region Reset Forces
            rb.freezeRotation = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            #endregion
            
            #region Reset Position and Rotation
            mainObject.transform.position = new Vector3(7.52f, 1.877789f, (myCam.transform.position.z - 5.43602f));
            mainObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            #endregion
        }

        //Jika waktu Half Dead selesai maka posisi Perahu kembali ke semula
        if (halfDeadTimeCounter >= halfDeadTimer)
        {
            moveController.BackIntoRightPositionFromCamera();
            moveController.setForwardSpeed(moveController.getForwardSpeed() - 0.8f);
            halfDeadCount = 0;
            halfDeadTimeCounter = 0f;

            return;
        }

        if (halfDeadCount > 0) halfDeadTimeCounter += Time.deltaTime;

        //Jika perahu sudah berada di belakang Kamera
        if ((myCam.transform.position.z - mainObject.transform.position.z) <= 2.8f && beingRevived == false && imDead == false)
        {
            Dead(false);
        }

        //Fungsi untuk membuat efek air pada layar menyala sesuai dengan jarak perahu dengan kamera
        WaterScreenMeter();


    }

    public void Dead(bool movingBackwards)
    {
        //Jika player mati saat menggunakan layar perahu maka tidak jadi mati
        if (gameController.GetLayarPerahuStatus() == true)
        {
            layarDestroyedFx.Play();
            layarDestroyedFx.gameObject.GetComponent<AudioSource>().Play();
            gameController.DeactivateLayarPerahu();
            obstacleDestroyer.SetResetPlayerPosition(true);
            obstacleDestroyer.ActivateObstacleDestroyer();

            if ((myCam.transform.position.z - mainObject.transform.position.z) <= 2.8f)
                mainObject.transform.position = new Vector3(mainObject.transform.position.x,
                                                            mainObject.transform.position.y,
                                                            mainObject.transform.position.z - 1f);
            return;
        }

        //Jika magnet Aktif maka Non aktifkan Magnet saat mati
        if (gameController.GetMagnetStatus())
            gameController.DeactivateMagnet();

        //Menonaktifkan Button untuk menggunakan LayarPerahu ketika mati.
        layarPerahuBtn.SetActive(false);


        //Jika Ditabrak Oleh kecoak maka Terlempar ke belakang dan Ke atas
        if (movingBackwards)
        {
            moveController.setStopMovingForward(false, 50f);
            moveController.DeadPush(-2000f);
        }
        else
        {
            moveController.setStopMovingForward(false, -50f);
            moveController.DeadPush(1000f);
        }

        imDead = true;



        #region Effects
        Instantiate(deadSfx, transform.position, Quaternion.identity);
        flashFx.gameObject.SetActive(true);
        flashFx.Play("Idle", -1, 0f);
        #endregion

        #region Physics Control
        rb.freezeRotation = false; 
        gameController.StopAllThings();
        boxColl.enabled = false;
        bouyancy.DecreasingWaterHeight(1f);
        #endregion

        #region Score Input

        finalScoreText.text = gameController.GetScore().ToString() + "m";
        finalCoinText.text = gameController.GetCoinCount().ToString();

        //Menambah koin ke saving data
        //PlayerPrefs.SetInt("Coins", (int)PlayerPrefs.GetInt("Coins") + gameController.GetCoinCount());
        currencyTransactionManager.EarnCurrency(gameController.GetCoinCount(), TransactionCurrencyTypes.Coins);

        #endregion

        #region Deactivating Things
        //Deactivating The Self Destroying of Everything
        WallDieTime[] allThings = FindObjectsOfType<WallDieTime>();
        if (allThings.Length > 0)
        {
            wallDieTimes = new WallDieTime[allThings.Length];
            for (int i = 0; i < allThings.Length; i++)
            {
                wallDieTimes[i] = allThings[i];
                wallDieTimes[i].enabled = false;
            }
        }


        //Deactivating The Follow Target of Everything
        FollowTarget[] allOfIt = FindObjectsOfType<FollowTarget>();
        if (allOfIt.Length > 0)
        {
            followTargets = new FollowTarget[allOfIt.Length];
            for (int i = 0; i < allOfIt.Length; i++)
            {
                followTargets[i] = allOfIt[i];
                followTargets[i].enabled = false;
            }
        }


        //Deactivating The ActivateWater of Everything
        ActivateWater[] allOfIt1 = FindObjectsOfType<ActivateWater>();
        if (allOfIt1.Length > 0)
        {
            activateWaters = new ActivateWater[allOfIt1.Length];
            for (int i = 0; i < allOfIt1.Length; i++)
            {
                activateWaters[i] = allOfIt1[i];
                activateWaters[i].enabled = false;
            }

        }

        #endregion

        perahuFx.SetActive(false);

        #region Couroutine
        StartCoroutine(TimeForDeadPanelActivate(2f));
        StartCoroutine(OpenTheRevivePanel(2f));
        #endregion
    }

    IEnumerator OpenTheRevivePanel(float time)
    {
        yield return new WaitForSeconds(time);

        reviveTimer.StartCounting();
    }

    public void Revive()
    {
        mainObject.tag = "Perahu";

        imDead = false;

        #region Reset Forces
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        #endregion

        #region Reset Position and Rotation
        mainObject.transform.position = new Vector3(7.52f, 1.877789f, (myCam.transform.position.z - 5.43602f));
        mainObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        #endregion

        #region FX Play
        layarDestroyedFx.Play();
        Instantiate(reviveFx, new Vector3(mainObject.transform.position.x, mainObject.transform.position.y, mainObject.transform.position.z), 
                    Quaternion.Euler(0f,0f,0f));
        revivePanelFx.SetActive(true);
        reviveFxAnim.Play("Idle", -1, 0f);
        flashFx.gameObject.SetActive(false);
        #endregion

        #region Set the MoveController
        moveController.setForwardSpeed(0f);
        moveController.setStopMovingForward(false, 0f);
        #endregion

        beingRevived = true;

        #region Set The Obstacle Destroyer
        obstacleDestroyer.SetResetPlayerPosition(false);
        obstacleDestroyer.ActivateObstacleDestroyer();
        #endregion

        StartCoroutine(StartPlayingAgain(1f));

        perahuFx.SetActive(true);

        
        #region Activating Back Deactivated Things

        //Activaing The Self Destroying of Everything
        if (wallDieTimes.Length > 0)
        {
            for (int i = 0; i < wallDieTimes.Length; i++)
            {
                if (wallDieTimes[i].gameObject.activeSelf)
                    wallDieTimes[i].enabled = true;
            }
        }


        //Activaing The Follow Target of Everything
        if (followTargets.Length > 0)
        {
            for (int i = 0; i < followTargets.Length; i++)
            {
                if (followTargets[i] != null)
                    if (followTargets[i].gameObject.activeSelf)
                        followTargets[i].enabled = true;
            }
        }

        mainObject.GetComponent<FollowTarget>().enabled = false;

        //Activaing The ActivateWater of Everything
        if (activateWaters.Length > 0)
        {
            for (int i = 0; i < activateWaters.Length; i++)
            {
                if (activateWaters[i].gameObject.activeSelf)
                    activateWaters[i].enabled = true;
            }
        }
        
        #endregion
        
    }

    IEnumerator StartPlayingAgain(float time)
    {
        #region Reset Position and Rotation
        mainObject.transform.position = new Vector3(7.52f, 1.877789f, (myCam.transform.position.z - 5.43602f));
        mainObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        #endregion

        yield return new WaitForSeconds(time);

        #region Reset Position and Rotation
        mainObject.transform.position = new Vector3(7.52f, 1.877789f, (myCam.transform.position.z - 5.43602f));
        mainObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        #endregion

        #region Reset Forces
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        #endregion

        #region StartMoving
        gameController.StartAllThings();
        moveController.setForwardSpeed(gameController.GetTheGlobalSpeed());

        moveController.setStopMovingForward(true, 0f);
        #endregion

        #region Set The Fx
        layarDestroyedFx.Play();
        revivePanelFx.SetActive(false);
        #endregion

        #region Initiating some Stat
        beingRevived = false;
        imDead = false;
        mainObject.tag = "Perahu";
        boxColl.enabled = true;
        #endregion

        bouyancy.DecreasingWaterHeight(-1f);
        
        //Mengaktifkan Button untuk menggunakan LayarPerahu ketika mati.
        layarPerahuBtn.SetActive(true);
    }

    IEnumerator TimeForDeadPanelActivate(float time)
    {
        yield return new WaitForSeconds(time);

        deadPanel.SetActive(true);
        
    }

    public void HalfDead()
    {
        halfDeadCount++;

        if (halfDeadCount >= 2)
        {
            halfDeadCount = 0;
            halfDeadTimeCounter = 0f;
            Dead(false);
        }
    }

    public bool GetDeadData()
    {
        return imDead;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleBerat") && imDead == false)
        {
            Dead(false);
            return;
        }

        if (other.CompareTag("Gerbang") && imDead == false)
        {
            Dead(false);
            return;
        }

        if (other.CompareTag("ObstacleRingan") && imDead == false)
        {
            perahuGfx.SetTrigger("Impacted");
            HalfDead();
            //Pushing Aside
            MoveController mc = mainObject.GetComponent<MoveController>();
            return;
        }

        if (other.CompareTag("Cockroach") && imDead == false)
        {
            Dead(true);
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Jika masuk ke obstacle Ringan maka kecepatan akan melambat
        if (other.CompareTag("ObstacleRingan"))
        {
            moveController.setForwardSpeed(moveController.getForwardSpeed() + 2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Jika keluar dri obstacle Ringan maka kecepatan kembali seperti semula
        if (other.CompareTag("ObstacleRingan"))
        {
            moveController.setForwardSpeed(moveController.getForwardSpeed());
        }
    }

    private void WaterScreenMeter()
    {
        float distance = myCam.transform.position.z - transform.position.z;
        float opacity = (5f - distance) * 122.6f;

        if (opacity > 255f) opacity = 255f;
        else if (opacity < 0) opacity = 0f;

        waterScreenFx.color = new Color32(255, 255, 255, (byte)opacity);
    }

    public void ResetWaterScreenMeter()
    {
        waterScreenFx.color = new Color32(255, 255, 255, 0);
    }
}
