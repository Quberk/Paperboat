using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    private MoveController moveController;

    // Start is called before the first frame update
    void Start()
    {
        moveController = FindObjectOfType<MoveController>();
        tutorialPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerPrefs.HasKey("FirstTime") && moveController.GetGameStartState() == true)
        {
            Time.timeScale = 0f;
            tutorialPanel.SetActive(true);

            PlayerPrefs.SetInt("FirstTime", 1);
            Destroy(gameObject);
        }
    }

    public void ActivatePanel(GameObject myPanel)
    {
        myPanel.SetActive(true);
    }

    public void DeactivatePanel(GameObject myPanel)
    {
        myPanel.SetActive(false);
    }


    public void ResumeTheGame()
    {
        Time.timeScale = 1f;
        tutorialPanel.SetActive(false);
    }
}
