using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsLevelController : MonoBehaviour
{
    [SerializeField] private Slider magnetLevelSlider;
    [SerializeField] private Slider paperPlaneLevelSlider;
    [SerializeField] private Slider boostUpLevelSlider;
    [SerializeField] private Slider layarLevelSlider;

    private int maxLevel = 5;

    LocalStorageManager localStorageManager = new LocalStorageManager();

    // Start is called before the first frame update
    void Start()
    {
        magnetLevelSlider.value = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelMagnet;
        paperPlaneLevelSlider.value = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelPesawat;
        boostUpLevelSlider.value = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelParasut;
        layarLevelSlider.value = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelLayar;
    }

    public void AddingLevel(ShopController.ItemType itemType)
    {
        PowerUpsLevel powerUpsLevel = new PowerUpsLevel(0, 0, 0, 0);
        powerUpsLevel.levelMagnet = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelMagnet;
        powerUpsLevel.levelPesawat = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelPesawat;
        powerUpsLevel.levelParasut = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelParasut;
        powerUpsLevel.levelLayar = localStorageManager.LoadPowerUpsLevelFromLocalStorage().levelLayar;

        switch (itemType) {

            case ShopController.ItemType.magnetLvl:
                powerUpsLevel.levelMagnet += 1;

                if (powerUpsLevel.levelMagnet > maxLevel)
                    return;
                localStorageManager.SavePowerUpsLevelIntoLocalStorage(0, 0, powerUpsLevel.levelMagnet, 0);
                break;

            case ShopController.ItemType.boostUpLvl:
                powerUpsLevel.levelParasut += 1;

                if (powerUpsLevel.levelParasut > maxLevel)
                    return;

                localStorageManager.SavePowerUpsLevelIntoLocalStorage(0, powerUpsLevel.levelParasut, 0, 0);
                break;

            case ShopController.ItemType.paperPlaneLvl:
                powerUpsLevel.levelPesawat += 1;

                if (powerUpsLevel.levelPesawat > maxLevel)
                    return;

                localStorageManager.SavePowerUpsLevelIntoLocalStorage(powerUpsLevel.levelPesawat, 0, 0, 0);
                break;

            case ShopController.ItemType.layarLvl:
                powerUpsLevel.levelLayar += 1;

                if (powerUpsLevel.levelLayar > maxLevel)
                    return;

                localStorageManager.SavePowerUpsLevelIntoLocalStorage(0, 0, 0, powerUpsLevel.levelLayar);
                break;

            default:
                break;
        }



        //Update in SHop the Level
        magnetLevelSlider.value = powerUpsLevel.levelMagnet;
        paperPlaneLevelSlider.value = powerUpsLevel.levelPesawat;
        boostUpLevelSlider.value = powerUpsLevel.levelParasut;
        layarLevelSlider.value = powerUpsLevel.levelLayar;

    }
}
