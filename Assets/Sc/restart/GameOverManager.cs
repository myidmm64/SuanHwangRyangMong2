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
        highScoreText.text = string.Format("������ �ܿ�\n{0}�ۿ� �ȵ�?",
           PlayerPrefs.GetInt("HIGHSCORE", 10));
    }
    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
