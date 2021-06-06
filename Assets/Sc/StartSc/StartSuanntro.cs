using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSuanntro : MonoBehaviour
{
    [SerializeField]
    private GameObject Image = null;
    [SerializeField]
    private Image isImage = null;
    [SerializeField]
    private Sprite Image1 = null;
    [SerializeField]
    private Sprite Image2 = null;
    [SerializeField]
    private Sprite Image3 = null;
    private int click = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("suanStart").GetComponent<StartManager>().suanClick == true)
        {
            Image.SetActive(true);
        }
    }
    public void Click0()
    {
        isImage.sprite = Image1;
        click += 1;
        if (click == 2)
            Click1();
        if(click == 3)
        {
            Click2();
        }
        if (click == 4)
        {
            GameStart();
        }
    }
    private void GameSpeedStart()
    {
        click = 4;
    }
    public void Click1()
    {
        isImage.sprite = Image2;
    }
    public void Click2()
    {
        isImage.sprite = Image3;
    }
    private void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
