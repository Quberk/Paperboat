﻿using UnityEngine;

public class NewWallTrigger : MonoBehaviour, IPooledObject
{
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void OnObjectSpawn()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Perahu"))
        {
            gameController.InstantiatingWall();
        }
    }
}
