using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject beforePlayPanel;
    [SerializeField] private GameObject runningOutSailBoatPanel;

    [Header("Sub Grub")]
    [SerializeField] private GameObject uiSkin;
    [SerializeField] private GameObject postDeadPerahu;

    [Header("Controllers")]
    [SerializeField] private MoveController moveController;
    [SerializeField] private GameController gameController;
    [SerializeField] private DeadController deadController;
    [SerializeField] private AccessoriesController accessoriesController;
    [SerializeField] private MusicSoundController musicSoundController;
    [SerializeField] private ControlModeController controlModeController;
    [SerializeField] private ReviveTimer reviveTimer;

    [SerializeField] private GameObject playImmediateObject;

    [Header("Layar Perahu Button")]
    private float delayTime = 0.5f;
    private float delayCounter = 0;
    private int layarBtnCount = 0;

    [Header("Revive")]
    [SerializeField] private GameObject reviveBtn;
    private int reviveCounter = 0;
    private int reviveLimit = 2;

    [SerializeField] private AudioSource musicGameplay;

    [SerializeField] private HomePageAnimationController homePageAnimationController;

    [SerializeField] private CurrencyController currencyController;

    LocalStorageManager localStorageManager = new LocalStorageManager();

    // Start is called before the first frame update
    void Start()
    {
        homePanel.SetActive(true);
        beforePlayPanel.SetActive(true);

        uiSkin.SetActive(false);
        postDeadPerahu.SetActive(false);

        settingPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameplayPanel.SetActive(false);
        musicGameplay.volume = 0.07f;
    }

    // Update is called once per frame
    void Update()
    {
        //Menghitung jumlah Click pada Layar Perahu Btn
        LayarPerahuCounterController();

        ReviveLimitController();
    }

    public void GenericOpenButton(GameObject uiToOpen)
    {
        if (uiToOpen == null)
            return;

        uiToOpen.SetActive(true);

        Animator[] uiAnims = uiToOpen.GetComponentsInChildren<Animator>();

        foreach (Animator myAnim in uiAnims)
        {
            myAnim.Play("Idle", -1, 0f);
        }
    }

    public void GenericCloseButton(GameObject uiToClose)
    {
        if (uiToClose == null)
            return;

        uiToClose.SetActive(false);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
    }

    public void CloseSettingButton()
    {
        settingPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    #region Accessories
    public void OpenAccessoriesController()
    {
        accessoriesController.ResetTheAccessoriesPanel();
        uiSkin.SetActive(true);
    }

    public void CloseAccessories()
    {
        uiSkin.SetActive(false);
    }
    #endregion

    #region Post Dead
    public void PostDeadOpen()
    {
        postDeadPerahu.SetActive(true);
    }

    public void PostDeadClose()
    {
        postDeadPerahu.SetActive(false);
    }
    #endregion

    //Hapus ini 2 fungsi dibawah
    public void OpenButton(GameObject target)
    {
        target.SetActive(true);
    }

    public void CloseButton(GameObject target)
    {
        target.SetActive(false);
    }

    #region Settings
    public void MusicBtn(bool status)
    {
        musicSoundController.SetTheMusicStatus(status);
    }

    public void SfxBtn(bool status)
    {
        musicSoundController.SetTheSfxStatus(status);
    }

    public void ControlModeBtn(string mode)
    {
        controlModeController.SetTheControlMode(mode);
    }
    #endregion

    #region Restart, Play Again
    public void StartLevel()
    {
        musicGameplay.volume = 0.41f;

        homePageAnimationController.StartFallingAnim();

        gameplayPanel.SetActive(true);
        beforePlayPanel.SetActive(false);
        homePanel.SetActive(false);
    }

    //Restart Main Menu
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Restart langsung Play
    public void PlayAgain()
    {
        GameObject immediatePlay = Instantiate(playImmediateObject, transform.position, Quaternion.identity);
        DontDestroyOnLoad(immediatePlay);
    }

    public void ReviveButton(int coinsPrive)
    {
        if (localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().coins < coinsPrive) {
            return;
        }

        reviveCounter++;

        deadPanel.SetActive(false);
        deadController.Revive();

        //Mengurangi Jumlah Paper Clip
        currencyController.SubtractCoins(coinsPrive);
    }

    public void StopRevivingTimer()
    {
        reviveTimer.StopCounting();
    }

    void ReviveLimitController()
    {
        if (reviveCounter >= reviveLimit)
        {
            //reviveBtn.SetActive(false);
        }
    }
    #endregion

    #region Layar Perahu
    public void LayarPerahuBtnClick()
    {
        layarBtnCount++;
        delayCounter = 0f;
    }

    void LayarPerahuCounterController()
    {
        delayCounter += Time.deltaTime;

        if (layarBtnCount == 2)
        {
            if (localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().layar > 0)
            {
                gameController.ActivateLayarPerahu();
                layarBtnCount = 0;
                currencyController.SubtractLayar(1);

                return;
            }

            runningOutSailBoatPanel.SetActive(true);
            layarBtnCount = 0;
            PauseButton();

        }

        if (delayCounter >= delayTime)
        {
            layarBtnCount = 0;
        }
    }

    public void FreeLayarPerahuBtn()
    {

    }
    #endregion

}
