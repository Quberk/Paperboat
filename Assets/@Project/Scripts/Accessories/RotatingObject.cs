using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private bool randomizeStartRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        float random = Random.Range(0f, 360f);
        if (randomizeStartRotation)
            transform.localRotation = Quaternion.Euler(0f, random, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (0f, rotatingSpeed, 0f) * Time.deltaTime);
    }
}
