using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayImmediate : MonoBehaviour
{

    private UIController uIController;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameController = FindObjectOfType<GameController>();

        if (gameController.GetGameStartStatus() == false)
        {
            uIController = FindObjectOfType<UIController>();

            uIController.StartLevel();

            Destroy(gameObject);
        }

    }
}
