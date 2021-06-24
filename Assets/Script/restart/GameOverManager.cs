using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText = null;
    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = string.Format("나한테 벌써 {0}번이나 죽었네?",
           PlayerPrefs.GetInt("DEADCOUNT", 1));
    }
    public void restart()
    {
        SceneManager.LoadScene("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
