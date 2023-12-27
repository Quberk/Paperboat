using UnityEngine;

public class DieWithinTime : MonoBehaviour
{
    [SerializeField]
    private float dieTime;
    private float dieCounter = 0f;

    [SerializeField] private bool isDangerSignal = false;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        dieCounter += Time.deltaTime;

        if (dieCounter >= dieTime) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (isDangerSignal)
            gameController.dangerSignals--;
    }
}
