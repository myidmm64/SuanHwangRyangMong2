using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomButten : MonoBehaviour
{
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Click()
    {
        gameManager.BoomPointDOWN();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
