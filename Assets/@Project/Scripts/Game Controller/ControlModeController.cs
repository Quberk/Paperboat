using UnityEngine;

public class ControlModeController : MonoBehaviour
{
    [SerializeField] private MoveController perahuMoveController;
    [SerializeField] private PaperPlaneController paperPlaneMoveController;

    [Header("Setting Buttons")]
    [SerializeField] private GameObject gyroBtn;
    [SerializeField] private GameObject dragBtn;

    [Header("Pause Buttons")]
    [SerializeField] private GameObject gyroBtn1;
    [SerializeField] private GameObject dragBtn1;

    [Header("Control Tutorial Panel")]
    [SerializeField] private GameObject[] gyroPanel;
    [SerializeField] private GameObject[] dragPanel;

    private string controlMode = "Drag";

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("ControlMode"))
            PlayerPrefs.SetString("ControlMode", controlMode);

        gyroBtn.SetActive(false);
        gyroBtn1.SetActive(false);
        dragBtn.SetActive(false);
        dragBtn1.SetActive(false);

        if (PlayerPrefs.GetString("ControlMode") == "Drag")
        {
            dragBtn.SetActive(true);
            dragBtn1.SetActive(true);

            controlMode = "Drag";

            perahuMoveController.SetTheControlMode("Drag");
            paperPlaneMoveController.SetTheControlMode("Drag");


            return;
        }

        gyroBtn.SetActive(true);
        gyroBtn1.SetActive(true);

        controlMode = "Gyro";

        perahuMoveController.SetTheControlMode("Gyro");
        paperPlaneMoveController.SetTheControlMode("Gyro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTheControlMode(string mode)
    {
        controlMode = mode;

        gyroBtn.SetActive(false);
        gyroBtn1.SetActive(false);
        dragBtn.SetActive(false);
        dragBtn1.SetActive(false);

        if (controlMode == "Drag")
        {
            dragBtn.SetActive(true);
            dragBtn1.SetActive(true);

            perahuMoveController.SetTheControlMode("Drag");
            paperPlaneMoveController.SetTheControlMode("Drag");

            PlayerPrefs.SetString("ControlMode", controlMode);

            for (int i = 0; i < gyroPanel.Length; i++)
            {
                gyroPanel[i].SetActive(false);
                dragPanel[i].SetActive(true);
            }


            return;
        }

        gyroBtn.SetActive(true);
        gyroBtn1.SetActive(true);

        perahuMoveController.SetTheControlMode("Gyro");
        paperPlaneMoveController.SetTheControlMode("Gyro");

        PlayerPrefs.SetString("ControlMode", controlMode);

        for (int i = 0; i < gyroPanel.Length; i++)
        {
            gyroPanel[i].SetActive(true);
            dragPanel[i].SetActive(false);
        }
    }

    public string GetTheControlMode()
    {
        return controlMode;
    }

    public void TutorialControlMode()
    {
        if (controlMode == "Drag")
        {
            for (int i = 0; i < gyroPanel.Length; i++)
            {
                gyroPanel[i].SetActive(false);
                dragPanel[i].SetActive(true);
            }
            return;
        }

        for (int i = 0; i < gyroPanel.Length; i++)
        {
            gyroPanel[i].SetActive(true);
            dragPanel[i].SetActive(false);
        }
    }
}
