using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    ObjectPooler objectPooler;
    CurrencyTransactionManager currencyTransactionManager;

    private bool gameStart = false;

    [SerializeField]
    private float boatSpeed = 0.5f;
    private float tempBoatSpeed;

    [Header("Water Based on Region")]
    [SerializeField] Color cityShallowWater;
    [SerializeField] Color cityDeepWater;
    [SerializeField] Color villageShallowWater;
    [SerializeField] Color villageDeepWater;
    [SerializeField] float transitionRate;

    [Header("PowerUps")]
    [SerializeField] private GameObject[] powerUpsUiPoses;

    [Header("Layar Perahu")]
    [SerializeField] private GameObject layarPerahu;
    private bool layarPerahuActive = false;
    private bool layarPerahuPause = false;
    [SerializeField] private float layarPerahuTime;
    private float layarPerahuCounter = 0f;
    [SerializeField] private GameObject layarPerahuBtn;
    [SerializeField] private ObstacleDestroyer obstacleDestroyer;
    [SerializeField] private GameObject layarPerahuUi;
    private GameObject layarPerahuUiInCanvas;
    private Image layarPerahuUiMeter;

    [Header("Boost Up")]
    [SerializeField] private GameObject parasutGfx;
    [SerializeField] private int boostUpDistanceGoal;
    [SerializeField] private GameObject boostUpUi;
    private GameObject boostUpUiInCanvas;
    private Image boostUpUiMeter;
    private int boostUpFinishPoint;
    private bool boostUpActive = false;
    private bool boostUpPause = false;
    
    [Header("Pesawat Terbang")]
    [SerializeField] private GameObject pesawatKertas;
    [SerializeField] private Animator pesawatKertasGfx;
    [SerializeField] private GameObject perahuGfx;
    [SerializeField] private GameObject obstacleDetector;
    [SerializeField] private CapsuleCollider perahuCapsuleCollider;
    [SerializeField] private float pesawatKertasDurationTime;
    [SerializeField] private FollowTarget perahuFollowTarget;
    [SerializeField] private MoveController perahuMoveController;
    [SerializeField] private GameObject shadowPerahuKertas;
    private float pesawatKertasDurationCounter = 0f;
    private bool pesawatKertasActive = false;
    [SerializeField] private GameObject paperPlaneUi;
    private GameObject paperPlaneUiInCanvas;
    private Image paperPlaneUiMeter;

    [Header("Magnet")]
    [SerializeField] private GameObject magnet;
    [SerializeField] private float magnetDurationTime;
    [SerializeField] private GameObject magnetUi;
    private GameObject magnetUiInCanvas;
    private Image magnetUiMeter;
    private float magnetDurationCounter = 0f;
    private bool magnetActive = false;
    private bool magnetPause = false;


    [Header("General")]
    [SerializeField] private GameObject perahu;
    private GameObject myCam;

    [Header("Score")]
    [SerializeField] private CurrencyController currencyController;
    [SerializeField]
    private Text scoreText;
    private int meterScore;
    private int highScore;

    [Header("Wave")]
    private int wave = 1;
    private string region = "Village";
    private float randomNumForWaveTransition;
    private float randomNumForTunnelToActivate;
    private float randomNumForObstacleWallsToActivate;

    [Header("Coin")]
    [SerializeField]
    private GameObject[] coinPatterns;
    private int coinScore;

    [Header("New Wall Instantiate")]
    [SerializeField]
    private GameObject[] leftWalls;
    [SerializeField]
    private GameObject[] rightWalls;
    private float leftXWallPos = 9.763262f;
    private float rightXWallPos = 5.543262f;
    private float wallYPos = 1.953593f;
    private float wallZPos = -52.979f;
    private int ordinaryWallLimit = 6;
    private int ordinaryWallCounter = 0;

    [Header("Tunnel Wall")]
    [SerializeField]
    private GameObject tunnelWall;
    private int tunnelLimit;
    private int tunnelCounter = 0;

    [Header("Water")]
    [SerializeField]
    private GameObject[] waters;

    [Header("Obstacles")]
    [SerializeField] private GameObject[] obstacleBeratVillage;
    [SerializeField] private GameObject[] obstacleRinganVillage;
    [SerializeField] private GameObject[] slowDownAreaVillage;
    [SerializeField] private GameObject[] obstacleBeratCity;
    [SerializeField] private GameObject[] obstacleRinganCity;
    [SerializeField] private GameObject[] slowDownAreaCity;
    [SerializeField] private GameObject cockroachSpawner;
    [SerializeField] private GameObject ikanSpawner;
    [SerializeField] private GameObject fence;
    [SerializeField] private GameObject villageRoofObstacle;
    [SerializeField] private GameObject cityRoofObstacle;
    private int obstaclePreventor = 3;

    private float startXPointInstantiate = 6.57f;
    private float finishXPointInstantiate = 8.74f;
    private float startZPointObstacleRingan = -32.76f;
    private float finishZPointObstacleRingan = -40.84f;

    [Header("Ikan")]
    private float ikanXPosStart = 8.77f;
    private float ikanXPosFinish = 6.3f;
    private float ikanYPos = 1.47f;
    private float ikanZPos;

    [Header("Bacground")]
    [SerializeField] private GameObject[] backGroundVillage;
    [SerializeField] private GameObject[] backGroundCity;
    private float backGroundXPos = 7.2f;
    private float backGroundYPos = -0.4f;
    private float backGroundZPos = 4.7f;
    private int backgroundWallCount = 1;
    private string backgroundRegion = "Village";

    [Header("Power Up VFX and SFX")]
    [SerializeField] private GameObject perahuWaterFx;
    [SerializeField] private ParticleSystem cloudVfx;
    [SerializeField] private ParticleSystem airFx;
    [SerializeField] private ParticleSystem splashFx;
    [SerializeField] private ParticleSystem smallSplashFx;
    [SerializeField] private GameObject layarTransitionVfx;
    [SerializeField] private GameObject powerUpStopVx;
    [SerializeField] private GameObject pesawatKertasSfx;

    [Header("Instantiated Container")]
    [HideInInspector] public int cockroaches;
    [HideInInspector] public int fishes;
    [HideInInspector] public int dangerSignals;
    [HideInInspector] public int skyCoins;
    [HideInInspector] public int coins;

    private AdjustController adjustController;

    // Start is called before the first frame update
    void Start()
    {
        currencyTransactionManager = new CurrencyTransactionManager();

        myCam = GameObject.FindGameObjectWithTag("MainCamera");
        adjustController = AdjustController.Instance;

        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);

        objectPooler = ObjectPooler.Instance;

        airFx.Stop();
        splashFx.Stop();
        smallSplashFx.Stop();

        pesawatKertas.SetActive(false);
        parasutGfx.SetActive(false);
        perahuFollowTarget.enabled = false;

        //if (!PlayerPrefs.HasKey("Highscore"))
           // PlayerPrefs.SetInt("Highscore", meterScore);



    }

    public void GameStart()
    {
        gameStart = true;
    }

    public bool GetGameStartStatus()
    {
        return gameStart;
    }

    // Update is called once per frame
    void Update()
    {
        LayarPerahuController();

        PaperPlaneController();

        BoostUpController();

        MagnetController();

        ChangingWaterColor();

        HighScoreController();

    }

    private void FixedUpdate()
    {   if (gameStart)
        myCam.transform.position = new Vector3(myCam.transform.position.x,
                                myCam.transform.position.y,
                                myCam.transform.position.z - boatSpeed * Time.deltaTime);
    }

    #region Scores
    public void ScoreIncrement()
    {
        meterScore++;
        scoreText.text = meterScore.ToString() + "m";
    }

    public int GetScore()
    {
        return meterScore;
    }

    void HighScoreController()
    {
        if (meterScore > currencyTransactionManager.GetHighScore()){
            highScore = meterScore;

            currencyController.UpdateHighScore(highScore);
        }
    }

    public int GetHighScore()
    {
        return currencyTransactionManager.GetHighScore();
    }

    void WaveIncrement()
    {
        wave++;

        //Jika Wave mencapai kelipatan 2 maka mengganti region
        if (wave % 4 == 0)
        {
            if (region == "Village") region = "City";
            else region = "Village";
        }

        if (wave >= 6) {
            return;
        }

        boatSpeed += 0.5f;
        tempBoatSpeed += 0.5f;
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);
    }
    #endregion

    //Fungsi mengganti warna air sesuai dengan Region
    void ChangingWaterColor()
    {
        if (region == "City")
        {
            //Water LOD0
            GameObject myWater = GameObject.FindGameObjectWithTag("Water_Gfx");
            Renderer theWater = myWater.GetComponent<Renderer>();

            theWater.material.SetColor("Color_Shallow", Color.Lerp(theWater.material.GetColor("Color_Shallow"), cityShallowWater, Time.deltaTime * transitionRate));
            theWater.material.SetColor("Color_Deep", Color.Lerp(theWater.material.GetColor("Color_Deep"), cityDeepWater, Time.deltaTime * transitionRate));

            //Water LOD1
            GameObject yourWater = GameObject.FindGameObjectWithTag("Water_GFX1");
            Renderer theYourWater = null;
            if (yourWater != null)
            {
                theYourWater = yourWater.GetComponent<Renderer>();

                theYourWater.material.SetColor("Color_Shallow", Color.Lerp(theYourWater.material.GetColor("Color_Shallow"), cityShallowWater,
                                                                                                                Time.deltaTime * transitionRate));
                theYourWater.material.SetColor("Color_Deep", Color.Lerp(theYourWater.material.GetColor("Color_Deep"), cityDeepWater,
                                                                                                                Time.deltaTime * transitionRate));
            }
        }

        else if (region == "Village")
        {
            //Water LOD0
            GameObject myWater = GameObject.FindGameObjectWithTag("Water_Gfx");
            Renderer theWater = myWater.GetComponent<Renderer>();

            theWater.material.SetColor("Color_Shallow", Color.Lerp(theWater.material.GetColor("Color_Shallow"), villageShallowWater, Time.deltaTime * transitionRate));
            theWater.material.SetColor("Color_Deep", Color.Lerp(theWater.material.GetColor("Color_Deep"), villageDeepWater, Time.deltaTime * transitionRate));

            //Water LOD1
            GameObject yourWater = GameObject.FindGameObjectWithTag("Water_GFX1");
            Renderer theYourWater = null;
            if (yourWater != null)
            {
                theYourWater = yourWater.GetComponent<Renderer>();

                theYourWater.material.SetColor("Color_Shallow", Color.Lerp(theYourWater.material.GetColor("Color_Shallow"), villageShallowWater, 
                                                                                                                Time.deltaTime * transitionRate));
                theYourWater.material.SetColor("Color_Deep", Color.Lerp(theYourWater.material.GetColor("Color_Deep"), villageDeepWater, 
                                                                                                                Time.deltaTime * transitionRate));
            }
        }
                
    }


    //----------------------------------------------------------------------------------------------------------------------------------------


    //Coin
    #region Coins
    public void CoinCounter()
    {
        coinScore++;
    }

    public int GetCoinCount()
    {
        return coinScore;
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------------
    #region Procedural
    public float GetTheGlobalSpeed()
    {
        float speed = -boatSpeed;
        if (boatSpeed == 0) speed = -tempBoatSpeed;
        return speed;
    }

    public void InstantiatingWall()
    {
        float randomNum = Random.Range(0f, leftWalls.Length * 100f);
        float randomNum1 = Random.Range(0f, leftWalls.Length * 100f);

        ordinaryWallCounter += 1;
        backgroundWallCount++;

        wallZPos += -14.623f;
        startZPointObstacleRingan += -14.44f;
        finishZPointObstacleRingan += -14.44f;

        //Air
        objectPooler.SpawnFromThePool("Water1", new Vector3(7.64f, 1.499f, wallZPos), Quaternion.Euler(0f, 0f, 0f));

        //Background, background hanya dipanggil setiap melewati 2 wall
        float angkaRandom = Random.Range(0, 300f);

        if (backgroundWallCount % 3 == 0)
        {
            backGroundZPos += -92.5f;

            if (backgroundWallCount % 12 == 0)
            {
                if (backgroundRegion == "Village") backgroundRegion = "City";
                else backgroundRegion = "Village";
            }

            //Region Village
            if (backgroundRegion == "Village")
            {
                if (angkaRandom <= 300)
                    Instantiate(backGroundVillage[0], new Vector3(backGroundXPos, backGroundYPos, backGroundZPos), Quaternion.Euler(0f, 0f, 0f));
            }

            //Region City
            else if (backgroundRegion == "City")
            {
                if (angkaRandom <= 100)
                    Instantiate(backGroundCity[0], new Vector3(backGroundXPos, backGroundYPos, backGroundZPos), Quaternion.Euler(0f, 0f, 0f));
                if (angkaRandom <= 200)
                    Instantiate(backGroundCity[1], new Vector3(backGroundXPos, backGroundYPos, backGroundZPos), Quaternion.Euler(0f, 0f, 0f));
                if (angkaRandom <= 300)
                    Instantiate(backGroundCity[2], new Vector3(backGroundXPos, backGroundYPos, backGroundZPos), Quaternion.Euler(0f, 0f, 0f));
            }
        }


         //----------------------------------------------------------//

        //Wall biasa, obstacle, dll akan di Instantiate dalam jumlah tertentu saja hingga wall Tunnel yang di Instantiate
        if (ordinaryWallCounter <= ordinaryWallLimit)
        {
            //Region Village
            if (region == "Village")
            {
                //Dinding Kiri
                if (randomNum <= 100f)
                {
                    objectPooler.SpawnFromThePool("LeftWallVillage1", new Vector3(leftXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }

                else
                {
                    objectPooler.SpawnFromThePool("LeftWallVillage2", new Vector3(leftXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }

                //Dinding Kanan

                if (randomNum1 <= 100f)
                {
                    objectPooler.SpawnFromThePool("RightWallVillage1", new Vector3(rightXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }

                else
                {
                    objectPooler.SpawnFromThePool("RightWallVillage2", new Vector3(rightXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }
            }

            //Region City
            if (region == "City")
            {
                //Dinding Kiri
                if (randomNum <= 100f)
                {
                    objectPooler.SpawnFromThePool("LeftWallCity1", new Vector3(leftXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }

                else
                {
                    objectPooler.SpawnFromThePool("LeftWallCity2", new Vector3(leftXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }

                //Dinding Kanan

                if (randomNum1 <= 100f)
                {
                    objectPooler.SpawnFromThePool("RightWallCity1", new Vector3(rightXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }

                else
                {
                    objectPooler.SpawnFromThePool("RightWallCity2", new Vector3(rightXWallPos, wallYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
                }
            }

            //Obstacles
            InstantiatingObstacle();

            //Coins
            InstantiatingCoinSpawner();

            //Data yang dibutuhkan jika ingin memasuki pergantian wave
            tunnelLimit = Random.Range(1, 3);
            
            //Jika Wave dibawah 4 maka hanya dapat memanggil PowerUp Wall
            if (wave < 4)
                randomNumForWaveTransition = Random.Range(0f, 100f);
            //Jika Wave dibawah 4 maka sudah dapat memanggil Obstacle Wall dan Tunnel wall
            else
                randomNumForWaveTransition = Random.Range(0f, 300f);

            randomNumForTunnelToActivate = Random.Range(0f, 200f);
            randomNumForObstacleWallsToActivate = Random.Range(0f, 200f);

            return;
        }

        //Memanggil PowerUps Wall
        if (randomNumForWaveTransition <= 100f)
        {
            //Region Village
            if (region == "Village")
                objectPooler.SpawnFromThePool("PowerUpsWallVillage",
                    new Vector3(8.740047f, -2.225456f, wallZPos), Quaternion.Euler(0f, 0f, 0f));

            //Region City
            else if (region == "City")
                objectPooler.SpawnFromThePool("PowerUpsWallCity",
                    new Vector3(8.740047f, -2.225456f, wallZPos), Quaternion.Euler(0f, 0f, 0f));

            tunnelCounter = 0;
            ordinaryWallCounter = 0;
            WaveIncrement(); //Transisi ke wave selanjutnya

            return;
        }

        //Memanggil Tunnel Wall
        else if (randomNumForWaveTransition <= 200f)
            InstantiateTunnelWall();

        //Memanggil Obstacle Wall
        else
            InstantiateObstacleChain();
    }

    void InstantiateTunnelWall()
    {
        float tunnelXPos = 8.740047f;
        float tunnelYPos = -2.225456f;

        GameObject theTunnel = null;

        if (region == "Village")
        {
            if (randomNumForTunnelToActivate <= 100f)
            {
                theTunnel = objectPooler.SpawnFromThePool("TireTunnel",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
            }

            else
            {
                objectPooler.SpawnFromThePool("BridgeWood",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
            }
        }

        else if (region == "City")
        {
            if (randomNumForTunnelToActivate <= 100f)
            {
                theTunnel = objectPooler.SpawnFromThePool("ConcreteTunnel",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
            }

            else
            {
                objectPooler.SpawnFromThePool("BridgeConcrete",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
            }
        }

        tunnelCounter++;

        //Jika sudah mencapai maksimum jumlah TUnnel wall maka akan kembali ke Ordinary Wall
        if (tunnelCounter >= tunnelLimit)
        {
            tunnelCounter = 0;
            ordinaryWallCounter = 0;
            if (theTunnel != null)theTunnel.GetComponent<LastTunnelCheck>().ItsTheLastTunnel(); //JIka last Tunnel maka mereset Kamera
            WaveIncrement(); //Transisi ke wave selanjutnya
        }
        return;
    }

    void InstantiateObstacleChain()
    {
        float tunnelXPos = 8.740047f;
        float tunnelYPos = -2.225456f;

        //Region Village
        if (region == "Village")
        {
            if (randomNumForObstacleWallsToActivate <= 100f)
                objectPooler.SpawnFromThePool("ObstacleWallVillage",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));

            else if (randomNumForObstacleWallsToActivate <= 200f)
                objectPooler.SpawnFromThePool("FishAttackWallVillage",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
        }

        //Region City
        else if (region == "City")
        {
            if (randomNumForObstacleWallsToActivate <= 100f)
                objectPooler.SpawnFromThePool("ObstacleWallCity",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));

            else if (randomNumForObstacleWallsToActivate <= 200f)
                objectPooler.SpawnFromThePool("FishAttackWallCity",
                new Vector3(tunnelXPos, tunnelYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));
        }

        tunnelCounter++;

        //Jika sudah mencapai maksimum jumlah TUnnel wall maka akan kembali ke Ordinary Wall
        if (tunnelCounter >= tunnelLimit)
        {
            tunnelCounter = 0;
            ordinaryWallCounter = 0;
            WaveIncrement(); //Transisi ke wave selanjutnya
        }
        return;
    }

    void InstantiatingObstacle()
    {
        float randomNum1 = Random.Range(0f, obstacleBeratVillage.Length * 100f);
        float randomNum3 = Random.Range(0f, slowDownAreaVillage.Length * 100f);
        int randomChoice = 0;

        //Jika Wave dibawah 2 maka Obstacle hanya dapat berupa Obstacle Berat atau Muddy Area
        if (wave < 3)
            randomChoice = Random.Range(0, 200);
        //Jika wave diatas 2 maka variasi Obstacle bertambah dengan ikan, kecoak dan roof obs
        else
            randomChoice = Random.Range(0, 400);


        //Pencegah pemanggilan obstacle jika ingin spawn ikan
        if (obstaclePreventor < 2)
        {
            obstaclePreventor++;
            return;
        }

        //Obstacle Berat
        if (randomChoice <= 100)
        {
            for (int i = 1; i <= obstacleBeratVillage.Length; i++)
            {
                if (randomNum1 <= i * 100f)
                {
                    float xPos = Random.Range(startXPointInstantiate, finishXPointInstantiate);
                    float zPos = wallZPos;
                    if (region == "Village")
                    {
                        Instantiate(obstacleBeratVillage[i - 1],
                                    new Vector3(xPos, 3.120314f, zPos), Quaternion.Euler(0f, 0f, 0f));
                        break;
                    }

                    else if (region == "City")
                    {
                        Instantiate(obstacleBeratCity[i - 1],
                                    new Vector3(xPos, 3.120314f, zPos), Quaternion.Euler(0f, 0f, 0f));
                        break;
                    }

                }
            }
            return;
        }

        //Muddy Area
        if (randomChoice <= 200)
        {
            for (int i = 1; i <= slowDownAreaVillage.Length; i++)
            {
                if (randomNum3 <= i * 100f)
                {
                    float xPos = Random.Range(startXPointInstantiate, finishXPointInstantiate);
                    float zPos = wallZPos;

                    if (region == "Village")
                    {
                        Instantiate(slowDownAreaVillage[i - 1],
                                    new Vector3(xPos, 1.537f, zPos), Quaternion.Euler(0f, 0f, 0f));



                        break;
                    }

                    else if (region == "City")
                    {
                        Instantiate(slowDownAreaCity[i - 1],
                                    new Vector3(xPos, 1.537f, zPos), Quaternion.Euler(0f, 0f, 0f));
                        break;
                    }

                }
            }
            return;
        }

        //Cockroach
        if (randomChoice <= 250)
        {
            float xPos = Random.Range(startXPointInstantiate, finishXPointInstantiate);
            float zPos = wallZPos;

            Instantiate(cockroachSpawner,
                        new Vector3(xPos, 1.48f, zPos), Quaternion.Euler(0f, 0f, 0f));
            return;
        }

        //Ikan
        if (randomChoice <= 300)
        {
            ikanZPos = myCam.transform.position.z + 6.73f;
            float xPos = Random.Range(ikanXPosStart, ikanXPosFinish);
            Instantiate(ikanSpawner,
                        new Vector3(xPos, ikanYPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));

            obstaclePreventor = 1;

            return;
        }

        //Roof Obstacle
        if (randomChoice <= 400)
        {
            if (region == "Village")
            Instantiate(villageRoofObstacle,
                new Vector3(7.199894f, 2.040314f, wallZPos), Quaternion.Euler(0f, 0f, 0f));

            else
            Instantiate(cityRoofObstacle,
                new Vector3(7.199894f, 2.040314f, wallZPos), Quaternion.Euler(0f, 0f, 0f));

            return;
        }

    }

    void InstantiatingCoinSpawner()
    {
        float xPos = 7.622f;
        float yPos = 2.191f;
        float myrand = Random.Range(0f, coinPatterns.Length * 100);

        for (int i = 1; i <= coinPatterns.Length; i++)
        {
            if (myrand <= i * 100f)
            {
                Instantiate(coinPatterns[i - 1],
                    new Vector3(xPos, yPos, wallZPos), Quaternion.Euler(0f, 0f, 0f));

                break;
            }
        }

        return;
    }

    public void StopAllThings()
    {
        tempBoatSpeed = boatSpeed;
        boatSpeed = 0;
    }

    public void StartAllThings()
    {
        boatSpeed = tempBoatSpeed;
    }
    #endregion


    //-------------------------------------------------------------POWER UPS-----------------------------------------------------------------------------------------------



    //Layar Perahu
    #region Layar Perahu
    void LayarPerahuController()
    {
        if (layarPerahuActive && layarPerahuPause == false)
        {
            //Mengupdate nilai dari UI Meter
            layarPerahuUiMeter.fillAmount = (layarPerahuTime - layarPerahuCounter) / layarPerahuTime;

            //Mengupdate Warna UI Meter menjadi hijau jika berada di atas 75%, orens di atas 20% dan dibawah itu merah
            if (layarPerahuUiMeter.fillAmount <= 0.2f)
                layarPerahuUiMeter.color = new Color32(248, 77, 32, 255);
            else if (layarPerahuUiMeter.fillAmount <= 0.7f)
                layarPerahuUiMeter.color = new Color32(248, 154, 32, 255);
            else
                layarPerahuUiMeter.color = new Color32(11, 255, 22, 255);

            layarPerahuCounter += Time.deltaTime;
            if (layarPerahuCounter >= layarPerahuTime)
            {
                DeactivateLayarPerahu();
            }
        }
    }

    public bool GetLayarPerahuStatus()
    {
        return layarPerahuActive;
    }

    public void ActivateLayarPerahu()
    {
        Instantiate(layarTransitionVfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));

        //Mengembalikan Posisi perahu ke posisi semula relatif terhadap posisi kamera
        perahuMoveController.setForwardSpeed(perahuMoveController.getForwardSpeed() - 0.8f);
        perahuMoveController.BackIntoRightPositionFromCamera();

        tempBoatSpeed = boatSpeed;
        layarPerahuActive = true;
        layarPerahu.SetActive(true);
        airFx.Play();
        airFx.gameObject.GetComponent<AudioSource>().Play();
        smallSplashFx.Play();

        boatSpeed = 12f;
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);
        layarPerahuBtn.SetActive(false);

        //Instantiating Layar Perahu UI pada Canvas
        layarPerahuUiInCanvas = PowerUpUIInstantiate(layarPerahuUi);
        layarPerahuUiMeter = layarPerahuUiInCanvas.transform.Find("Meter").GetComponent<Image>();

        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(layarPerahuUiInCanvas);

        //Adjust Event
        adjustController.CallingAdjustEvent("icknxa");
    }

    public void DeactivateLayarPerahu()
    {
        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(layarPerahuUiInCanvas);

        Instantiate(powerUpStopVx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));

        layarPerahuCounter = 0f;
        layarPerahuActive = false;
        layarPerahu.SetActive(false);
        airFx.Stop();
        airFx.gameObject.GetComponent<AudioSource>().Stop();
        smallSplashFx.Stop();

        boatSpeed = tempBoatSpeed;
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);
        layarPerahuBtn.SetActive(true);

        //Menghancurkan UI Layar Perahu
        Destroy(layarPerahuUiInCanvas);
    }

    void PauseLayarPerahu()
    {
        layarPerahuPause = true;
        smallSplashFx.Stop();
        //boatSpeed = tempBoatSpeed;
        //perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);

    }

    void ResumeLayarPerahu()
    {
        Instantiate(layarTransitionVfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));
        airFx.Play();
        smallSplashFx.Play();
        airFx.gameObject.GetComponent<AudioSource>().Play();

        //Mengembalikan Posisi perahu ke posisi semula relatif terhadap posisi kamera
        perahuMoveController.setForwardSpeed(perahuMoveController.getForwardSpeed() - 0.8f);
        perahuMoveController.BackIntoRightPositionFromCamera();

        layarPerahuPause = false;

        boatSpeed = 12f;
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);
    }

    public void DeactivateLayarPerahuWhenPowerUpActive()
    {
        //Menonaktifkan Tombol Layar Perahu
        layarPerahuBtn.SetActive(false);
    }

    public void ActivateLayarButtonWhenPowerUpFinish()
    {
        layarPerahuBtn.SetActive(true);
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------------


    //Boost Up
    #region Boost Up
    private void BoostUpController()
    {
        if (boostUpActive  && boostUpPause == false)
        {
            //Menghancurkan semua Obstacle dan Coin dan PowerUp lainnya
            GameObject[] obstacleBerat = GameObject.FindGameObjectsWithTag("ObstacleBerat");
            GameObject[] slowObs = GameObject.FindGameObjectsWithTag("ObstacleSlow");
            GameObject[] gerbang = GameObject.FindGameObjectsWithTag("Gerbang");
            GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");


            if (obstacleBerat.Length > 0)
                foreach (GameObject berat in obstacleBerat)
            {
                //Jika obstacle berat adalah Obstacle Wall
                if (berat.GetComponent<ObstacleWall>()) 
                    berat.SetActive(false);
                else
                    Destroy(berat);
            }

            if (slowObs.Length > 0)
                foreach (GameObject slow in slowObs)
                {
                    Destroy(slow);
                }

            if (gerbang.Length > 0)
                foreach (GameObject gerbangs in gerbang)
            {
                if (gerbangs.GetComponent<GerbangDestroyed>())
                    gerbangs.GetComponent<GerbangDestroyed>().DestroyTheParentObject();

                else
                    gerbangs.SetActive(false);
            }

            if (cockroaches > 0)
            {
                GameObject[] cockroach = GameObject.FindGameObjectsWithTag("Cockroach");
                foreach (GameObject cockroaches in cockroach)
                {
                    Destroy(cockroaches);
                }
            }
 

            if (fishes > 0)
            {
                GameObject[] ikan = GameObject.FindGameObjectsWithTag("Ikan");
                foreach (GameObject ikans in ikan)
                {
                    Destroy(ikans);
                }
            }


            if (powerUps.Length > 0)
                foreach (GameObject powerUp in powerUps)
            {
                Destroy(powerUp);
            }

            if (dangerSignals > 0)
            {
                GameObject[] dangerSignals = GameObject.FindGameObjectsWithTag("DangerSignal");
                foreach (GameObject myDangerSignal in dangerSignals)
                {
                    Destroy(myDangerSignal);
                }
            }


            //----------------------------------------------------------------------------
            //Mengupdate nilai dari UI Meter
            float fill = ((float)boostUpFinishPoint - (float)meterScore) / (float)boostUpDistanceGoal;
            boostUpUiMeter.fillAmount = fill;


            //Mengupdate Warna UI Meter menjadi hijau jika berada di atas 75%, orens di atas 20% dan dibawah itu merah
            if (boostUpUiMeter.fillAmount <= 0.2f) 
                boostUpUiMeter.color = new Color32(248, 77, 32, 255);
            else if (boostUpUiMeter.fillAmount <= 0.7f)
                boostUpUiMeter.color = new Color32(248, 154, 32, 255);
            else
                boostUpUiMeter.color = new Color32(11, 255, 22, 255);

            if (meterScore >= boostUpFinishPoint)
                DeactivateBoostUp();
        }
    }
    public void ActivateBoostUp()
    {
        //Pause Layar Perahu jika BoostUp Aktif
        if (layarPerahuActive == true) {
            PauseLayarPerahu();
        }

        //Jika Layar Perahu sedang tidak aktif maka atur ulang tempBoatSpeed
        else
        {
            tempBoatSpeed = boatSpeed;
        }

        //Aktifkan GFX parasut
        parasutGfx.SetActive(true);

        //Play Cloud FX
        cloudVfx.Play();

        //Menginisiasi Goal Meter
        boostUpFinishPoint = meterScore + boostUpDistanceGoal;

        Instantiate(layarTransitionVfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));
        boostUpActive = true;
        airFx.Play();
        airFx.gameObject.GetComponent<AudioSource>().Play();
        splashFx.Play();

        //Mengembalikan Posisi perahu ke posisi semula relatif terhadap posisi kamera
        perahuMoveController.setForwardSpeed(perahuMoveController.getForwardSpeed() - 0.8f);
        perahuMoveController.BackIntoRightPositionFromCamera();

        //Menonaktifkan Layar Perahu ketika BoostUp Aktif
        DeactivateLayarPerahuWhenPowerUpActive();

        boatSpeed = 20f;
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);

        //Instantiating BoostUp UI pada Canvas
        boostUpUiInCanvas = PowerUpUIInstantiate(boostUpUi);
        boostUpUiMeter = boostUpUiInCanvas.transform.Find("Meter").GetComponent<Image>();

        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(boostUpUiInCanvas);
    }
    public void DeactivateBoostUp()
    {
        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(boostUpUiInCanvas);

        Instantiate(powerUpStopVx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));
        airFx.Stop();
        airFx.gameObject.GetComponent<AudioSource>().Stop();
        splashFx.Stop();

        //Nonaktifkan GFX parasut
        parasutGfx.SetActive(false);

        //Resume Layar Perahu jika BoostUp Aktif
        if (layarPerahuActive == true) ResumeLayarPerahu();
        //Mengaktifkan Kembali Button Layar Perahu ketika Boost Up selesai
        else {
            ActivateLayarButtonWhenPowerUpFinish();

            boatSpeed = tempBoatSpeed;
            perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);
        }

        Instantiate(layarTransitionVfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));

        boostUpActive = false;


        //Menghancurkan UI BoostUp
        Destroy(boostUpUiInCanvas);
    }

    void PauseBoostUp()
    {
        boostUpPause = true;
    }

    void ResumeBoostUp()
    {
        airFx.Play();
        airFx.gameObject.GetComponent<AudioSource>().Play();
        boatSpeed = 20f;
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);

        boostUpPause = false;
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------------


    //Paper Plane
    #region Paper Plane
    public void ActivatePaperPlane()
    {
        DeactivateLayarPerahuWhenPowerUpActive();
        Instantiate(layarTransitionVfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));
        Instantiate(pesawatKertasSfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));

        //Mengembalikan posisi relatif pesawat dengan kamera
        pesawatKertas.GetComponent<PaperPlaneController>().BackIntoRightPositionFromCamera();

        //Reset Water Screen Effect
        perahu.transform.Find("Obstacle_Detector").GetComponent<DeadController>().ResetWaterScreenMeter();

        //Pause Layar Perahu jika BoostUp Aktif
        if (layarPerahuActive == true) {
            PauseLayarPerahu();
        }

        if (boostUpActive == true)
        {
            PauseBoostUp(); 
        }

        if (magnetActive == true)
        {
            PauseMagnet();
        }

        if (!layarPerahuActive && !boostUpActive)
        {
            tempBoatSpeed = boatSpeed;
        }

        //Play Cloud FX
        cloudVfx.Play();

        //Mereset Animator Pesawat
        pesawatKertasGfx.Play("Takeoff", -1, 0f);

        //Menonaktifkan Tombol Layar Perahu
        layarPerahuBtn.SetActive(false);

        //Mengubah Field of View Camera
        myCam.GetComponent<Camera>().fieldOfView = 80f;

        //Mengembalikan Posisi perahu ke posisi semula relatif terhadap posisi kamera
        perahuMoveController.setForwardSpeed(perahuMoveController.getForwardSpeed() - 0.8f);
        perahuMoveController.BackIntoRightPositionFromCamera();

        pesawatKertasActive = true;
        pesawatKertas.SetActive(true);

        //Memposisikan pesawat kertas pada posisi awalnya yang sesuai dengan perahu
        pesawatKertas.transform.position = new Vector3(perahu.transform.position.x, 4.17f, perahu.transform.position.z + 1.546f);

        boatSpeed = 11f;
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);
        airFx.Play();
        airFx.gameObject.GetComponent<AudioSource>().Play();

        //Menonaktifkan komponen perahu kertas
        perahu.tag = "Untagged";
        perahuGfx.SetActive(false);
        obstacleDetector.SetActive(false);
        perahuCapsuleCollider.enabled = false;
        perahuMoveController.enabled = false;
        shadowPerahuKertas.SetActive(false);
        perahuWaterFx.SetActive(false);

        perahuFollowTarget.enabled = true;

        //Instantiating PaperPlane UI pada Canvas
        paperPlaneUiInCanvas = PowerUpUIInstantiate(paperPlaneUi);
        paperPlaneUiMeter = paperPlaneUiInCanvas.transform.Find("Meter").GetComponent<Image>();

        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(paperPlaneUiInCanvas);
    }

    public void DeactivatePaperPlane()
    {
        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(paperPlaneUiInCanvas);

        Instantiate(powerUpStopVx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));
        airFx.Stop();
        airFx.gameObject.GetComponent<AudioSource>().Stop();

        //Resume Layar Perahu jika BoostUp Aktif
        if (layarPerahuActive == true) ResumeLayarPerahu();
        if (boostUpActive == true) ResumeBoostUp();
        if (magnetActive == true) ResumeMagnet();
        //Jika tidak ada PowerUp lain yg aktif
        if (!layarPerahuActive && !boostUpActive)
        {
            boatSpeed = tempBoatSpeed;
            perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);
            ActivateLayarButtonWhenPowerUpFinish();
        }

        //Play Cloud FX
        cloudVfx.Play();

        myCam.GetComponent<CameraSwithController>().ResetPosToNormal();

        pesawatKertasActive = false;
        pesawatKertas.SetActive(false);
        perahu.GetComponent<MoveController>().setForwardSpeed(-boatSpeed);

        //Mengubah Field of View Camera
        myCam.GetComponent<Camera>().fieldOfView = 62f;

        //Mengaktifkan komponen perahu kertas
        perahu.tag = "Perahu";
        perahuGfx.SetActive(true);
        obstacleDetector.SetActive(true);
        perahuCapsuleCollider.enabled = true;
        perahuMoveController.enabled = true;
        shadowPerahuKertas.SetActive(true);
        perahuWaterFx.SetActive(true);

        //Reset Cool Down Paper Plane
        pesawatKertasDurationCounter = 0f;

        //Mereset Wall Counter ketika selesai menggunakan paper plane
        ordinaryWallCounter = ordinaryWallLimit / 2;

        perahuFollowTarget.enabled = false;

        //Mengaktifkan Sky Coin
        GameObject[] skyCoins = GameObject.FindGameObjectsWithTag("SkyCoin");

        if (skyCoins.Length > 0)
            foreach (GameObject skyCoin in skyCoins)
            {
                skyCoin.GetComponent<SkyCoin>().DeactivateSkyCoinObject();
            }

        //Menghancurkan Coin Sky
        GameObject[] coinSkies = GameObject.FindGameObjectsWithTag("CoinSky");

        if (coinSkies.Length > 0)
            foreach(GameObject coinSky in coinSkies)
            {
                coinSky.SetActive(false);
            }

        //Menghancurkan semua Tunnel
        GameObject[] tunnels = GameObject.FindGameObjectsWithTag("Tunnel");
        if (tunnels.Length > 0)
            foreach (GameObject myTunnel in tunnels)
            {
                Destroy(myTunnel);
            }

        /*Mengaktifkan Obstacle Destroyer ketika selesai menggunakan paper plane agar nantinya jika player mengenai
          Obstacle player tidak langsung mati melainkan menghancurkan obstacle tersebut*/
        obstacleDestroyer.SetResetPlayerPosition(true);
        obstacleDestroyer.ActivateObstacleDestroyer();


        //Menghancurkan UI PaperPlane
        Destroy(paperPlaneUiInCanvas);
    }

    void PaperPlaneController()
    {
        if (pesawatKertasActive)
        {
            //Mengupdate nilai dari UI Meter
            float fill = (pesawatKertasDurationTime - pesawatKertasDurationCounter) / pesawatKertasDurationTime;
            paperPlaneUiMeter.fillAmount = fill;

            //Mengupdate Warna UI Meter menjadi hijau jika berada di atas 75%, orens di atas 20% dan dibawah itu merah
            if (paperPlaneUiMeter.fillAmount <= 0.2f)
                paperPlaneUiMeter.color = new Color32(248, 77, 32, 255);
            else if (paperPlaneUiMeter.fillAmount <= 0.7f)
                paperPlaneUiMeter.color = new Color32(248, 154, 32, 255);
            else
                paperPlaneUiMeter.color = new Color32(11, 255, 22, 255);


            //Menonaktifkan Cockroach Obstacle agar Pesawat dapat lewat
            if (cockroaches > 0)
            {
                GameObject[] roaches = GameObject.FindGameObjectsWithTag("Cockroach");
                foreach (GameObject roach in roaches)
                {
                    Destroy(roach);
                }
            }

            //Menonaktifkan Ikan Obstacle agar Pesawat dapat lewat
            if (fishes > 0)
            {
                GameObject[] fishes = GameObject.FindGameObjectsWithTag("Ikan");
                foreach (GameObject fish in fishes)
                {
                    Destroy(fish);
                }
            }

            //Menonaktifkan Danger Signal ketika PEsawat aktif
            if (dangerSignals > 0)
            {
                GameObject[] dangerSignals = GameObject.FindGameObjectsWithTag("DangerSignal");

                foreach (GameObject dangerSignal in dangerSignals)
                {
                    Destroy(dangerSignal);
                }
            }


            //Menghitung durasi Pesawat kertas
            pesawatKertasDurationCounter += Time.deltaTime;
            if (pesawatKertasDurationCounter >= pesawatKertasDurationTime) DeactivatePaperPlane();
            //Play Landing Animation dari Pesawat
            else if (pesawatKertasDurationCounter >= (pesawatKertasDurationTime * 97f / 100f)) pesawatKertasGfx.SetTrigger("Dead");


            //Mengaktifkan Sky Coin Spawner
            GameObject[] skyCoins = GameObject.FindGameObjectsWithTag("SkyCoin");
            if (skyCoins.Length > 0)
            {
                foreach (GameObject skyCoin in skyCoins)
                {
                    skyCoin.GetComponent<SkyCoin>().ActivateSkyCoinObject();
                }
            }

            //Menghancurkan Coin yang berada di bawah
            if (coins > 0)
            {
                GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
                foreach (GameObject coinSky in coins)
                {
                    coinSky.SetActive(false);
                }
            }


            return;
        }

    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------------

    //Magnet
    #region Magnet
    public void MagnetController()
    {
        if (magnetActive && magnetPause == false)
        {
            //Mengupdate nilai dari UI Meter
            float fill = (magnetDurationTime - magnetDurationCounter) / magnetDurationTime;
            magnetUiMeter.fillAmount = fill;


            //Mengupdate Warna UI Meter menjadi hijau jika berada di atas 75%, orens di atas 20% dan dibawah itu merah
            if (magnetUiMeter.fillAmount <= 0.2f)   
                magnetUiMeter.color = new Color32(248, 77, 32, 255);
            else if (magnetUiMeter.fillAmount <= 0.7f)
                magnetUiMeter.color = new Color32(248, 154, 32, 255);
            else
                magnetUiMeter.color = new Color32(11, 255, 22, 255);


            magnetDurationCounter += Time.deltaTime;
            if (magnetDurationCounter >= magnetDurationTime)
            {
                DeactivateMagnet();
                magnetDurationCounter = 0f;
            }
        }
    }

    public void ActivateMagnet()
    {
        //Jika Magnet sedang aktif kemudian mendapatkan Magnet lagi maka aktifkan dlu yg lama
        if (magnetActive) DeactivateMagnet();

        magnetDurationCounter = 0f;

        //VFX
        Instantiate(layarTransitionVfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));

        magnet.SetActive(true);
        magnetActive = true;

        //Instantiating Magnet UI pada Canvas
        magnetUiInCanvas = PowerUpUIInstantiate(magnetUi);
        magnetUiMeter = magnetUiInCanvas.transform.Find("Meter").GetComponent<Image>();

        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(magnetUiInCanvas);
    }

    public void DeactivateMagnet()
    {
        //Mengupdate Posisi UI pada Canvas
        PowerUpUiPositioningInCanvas(magnetUiInCanvas);

        Instantiate(powerUpStopVx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));

        magnet.SetActive(false);
        magnetActive = false;

        //Menghancurkan UI Magnet
        Destroy(magnetUiInCanvas);
    }

    void PauseMagnet()
    {
        magnetPause = true;
        magnet.SetActive(false);
    }

    void ResumeMagnet()
    {
        Instantiate(layarTransitionVfx, perahu.transform.position, Quaternion.Euler(0f, 0f, 0f));
        magnetPause = false;
        magnet.SetActive(true);
    }

    public bool GetMagnetStatus()
    {
        return magnetActive;
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------------

    //PowerUps UI
    #region Power Ups UI
    GameObject PowerUpUIInstantiate(GameObject UI)
    {
        GameObject myUI = null;
        for (int i = 0; i < powerUpsUiPoses.Length; i++)
        {
            if (powerUpsUiPoses[i].GetComponent<RectTransform>().transform.childCount <= 0)
            {
                myUI = Instantiate(UI, transform.position, Quaternion.identity);
                myUI.transform.SetParent(GameObject.Find("PowerUp_UI_Poses").transform.Find("Point " + (i + 1).ToString()));
                myUI.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f,0f);
                myUI.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                break;
            }
        }

        return myUI;
    }

    //Fungsi untuk mengupdate posisi UI PowerUp
    void PowerUpUiPositioningInCanvas(GameObject UI)
    {
        for (int i = 0; i < powerUpsUiPoses.Length; i++)
        {
            if (powerUpsUiPoses[i].GetComponent<RectTransform>().transform.Find(UI.name))
            {
                UI.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            }

            if (powerUpsUiPoses[i].GetComponent<RectTransform>().transform.childCount <= 0)
            {
                UI.transform.SetParent(GameObject.Find("PowerUp_UI_Poses").transform.Find("Point " + (i + 1).ToString()));
                UI.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                UI.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                UI.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);

                break;
            }
        }    
    }
    #endregion
}
