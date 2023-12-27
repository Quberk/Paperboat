using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bouyancy : MonoBehaviour
{
    [SerializeField]
    private Transform[] floaters;

    [SerializeField]
    private float underWaterDrag = 3f;
    [SerializeField]
    private float underWaterAngularDrag = 1f;
    [SerializeField]
    private float airDrag = 0f;
    [SerializeField]
    private float airAngularDrag = 0.0f;
    [SerializeField]
    private float floatingPower = 15f;
    [SerializeField]
    private float waterHeight = 0f;

    MoveController moveController;

    [Header("Periodic Push")]
    [SerializeField] private GameObject pushPoint;
    [SerializeField] private float pushPower;

    private Rigidbody rb;
    bool underWater = false;

    int floatersUnderWater;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveController = GetComponent<MoveController>();
    }

    private void FixedUpdate()
    {
        floatersUnderWater = 0;
        for (int i = 0; i < floaters.Length; i++)
        {
            float difference = floaters[i].position.y - waterHeight;

            if (difference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floaters[i].transform.position);
                floatersUnderWater++;
                if (!underWater)
                {
                    underWater = true;
                    SwitchState(true);
                }
            }
        }


        if (underWater && floatersUnderWater == 0)
        {
            underWater = false;
            SwitchState(false);
        }

    }

    private void Update()
    {
        //PeriodicForce();
    }

    void SwitchState(bool underwater)
    {
        if (underwater)
        {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;

        }

        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
            
        }
    }

    public void DecreasingWaterHeight(float minusHeight)
    {
        waterHeight -= minusHeight;
    }

    /*
    //Memberikan Push Power setiap waktu tertentu
    void PeriodicForce()
    {
        pushCounter += Time.deltaTime;

        if (pushCounter >= 1f)
        {
            pushCounter = 0f;
            rb.AddForceAtPosition(Vector3.down * pushPower, pushPoint.transform.position);
        }
    }*/
}
