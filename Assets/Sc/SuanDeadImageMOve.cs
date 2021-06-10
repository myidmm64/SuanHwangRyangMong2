using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuanDeadImageMOve : MonoBehaviour
{
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.suandest)
            Destroy(gameObject);
    }
}
