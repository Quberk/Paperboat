using UnityEngine;
using UnityEngine.UI;

public class RunningOutOfSailController : MonoBehaviour
{
    [SerializeField] private Text boatSailAmount;
    [SerializeField] private int boatSailPrice;
    [SerializeField] private GameObject buyBtn;

    LocalStorageManager localStorageManager = new LocalStorageManager();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boatSailAmount.text = "x" + localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().highScore.ToString();

        if (localStorageManager.LoadCurrenciesAndHighscoreFromLocalStorage().coins < boatSailPrice)
        {
            buyBtn.SetActive(false);
            return;
        }

        buyBtn.SetActive(true);
    }
}
