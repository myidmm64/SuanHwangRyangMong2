using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnStart()
    {
        SceneManager.LoadScene("Start");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
