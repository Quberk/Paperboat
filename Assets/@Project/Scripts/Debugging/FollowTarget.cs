using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private bool usingTag;
    [SerializeField]
    private string theTag;

    [Header("Offsets")]
    [SerializeField]
    private float xOffset;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float zOffset;

    [Header("Axis Follow Target")]
    [SerializeField]
    private bool xTarget;
    [SerializeField]
    private bool yTarget;
    [SerializeField]
    private bool zTarget;
    [SerializeField]
    private bool xyTarget;
    [SerializeField]
    private bool xzTarget;
    [SerializeField]
    private bool yzTarget;
    [SerializeField]
    private bool posTarget;


    // Start is called before the first frame update
    void Start()
    {
        if (usingTag == true) target = GameObject.FindGameObjectWithTag(theTag);
    }

    // Update is called once per frame
    void Update()
    {
        if (xTarget)
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                transform.position.y,
                                transform.position.z);

            return;
        }

        if (yTarget)
        {
            transform.position = new Vector3(transform.position.x,
                                target.transform.position.y + yOffset,
                                transform.position.z);

            return;
        }

        if (zTarget)
        {
            transform.position = new Vector3(transform.position.x,
                                transform.position.y,
                                target.transform.position.z + zOffset);

            return;
        }

        if (xyTarget)
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                target.transform.position.y + yOffset,
                                transform.position.z);

            return;
        }

        if (xzTarget)
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                transform.position.y,
                                target.transform.position.z + zOffset);

            return;
        }

        if (yzTarget)
        {
            transform.position = new Vector3(transform.position.x,
                                target.transform.position.y + yOffset,
                                target.transform.position.z + zOffset);

            return;
        }
    }
}
