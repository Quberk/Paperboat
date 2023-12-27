using UnityEngine;
using UnityEngine.UI;

public class CheckingTheMaterialValue : MonoBehaviour
{
    [SerializeField]
    private Text waterDepth;
    [SerializeField]
    private Text airDangkal;
    [SerializeField]
    private Text airDalam;
    [SerializeField]
    private Text refractionSpeed;
    [SerializeField]
    private Text refractionScale;
    [SerializeField]
    private Text refractionStrength;
    [SerializeField]
    private Text foamAmount;
    [SerializeField]
    private Text foamCutOff;
    [SerializeField]
    private Text foamSpeed;
    [SerializeField]
    private Text foamScale;
    [SerializeField]
    private Text foamColor;


    [SerializeField]
    private GameObject water;

    private float waterDepthValue;
    private Color airDangkalValue;
    private Color airDalamValue;
    private float refractionSpeedValue;
    private float refractionScaleValue;
    private float refractionStrengthValue;
    private float foamAmountValue;
    private float foamCutOffValue;
    private float foamSpeedValue;
    private float foamScaleValue;
    private Color foamColorValue;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        waterDepthValue = water.GetComponent<Renderer>().material.GetFloat("Water_Depth");
        airDangkalValue = water.GetComponent<Renderer>().material.GetColor("Color_Shallow");
        airDalamValue = water.GetComponent<Renderer>().material.GetColor("Color_Deep");
        refractionSpeedValue = water.GetComponent<Renderer>().material.GetFloat("Refraction_Speed");
        refractionScaleValue = water.GetComponent<Renderer>().material.GetFloat("Refraction_Scale");
        refractionStrengthValue = water.GetComponent<Renderer>().material.GetFloat("Refraction_Strength");
        foamAmountValue = water.GetComponent<Renderer>().material.GetFloat("Foam_Amount");
        foamCutOffValue = water.GetComponent<Renderer>().material.GetFloat("Foam_Cut_Off");
        foamSpeedValue = water.GetComponent<Renderer>().material.GetFloat("Foam_Speed");
        foamScaleValue = water.GetComponent<Renderer>().material.GetFloat("Foam_Scale");
        foamColorValue = water.GetComponent<Renderer>().material.GetColor("Foam_Color");

        waterDepth.text = "Water Depth : " + waterDepthValue.ToString();
        airDangkal.text = "Air Dangkal : " + airDangkalValue.ToString();
        airDalam.text = "Water Depth : " + airDalamValue.ToString();
        refractionSpeed.text = "Refraction Speed : " + refractionSpeedValue.ToString();
        refractionScale.text = "Refraction Scale : " + refractionScaleValue.ToString();
        refractionStrength.text = "Refraction Strength : " + refractionStrengthValue.ToString();
        foamAmount.text = "Foam Amoun : " + foamAmountValue.ToString();
        foamCutOff.text = "Foam Cut Off : " + foamCutOffValue.ToString();
        foamSpeed.text = "Foam Speed : " + foamSpeedValue.ToString();
        foamScale.text = "Foam Scale : " + foamScaleValue.ToString();
        foamColor.text = "Foam Color : " + foamColorValue.ToString();
    }
}
