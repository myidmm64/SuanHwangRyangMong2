using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuanImageMove : MonoBehaviour
{
    [SerializeField]
    private Text SuanTalkText = null;
    private GameManager gameManager = null;
    private int touch = 0;
    private bool textstart = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if(gameManager.bossDeadChack == true)
        {
            Touch();
            if (textstart) return;
            textstart = true;
            Invoke("TextStart", 1.5f);


        }
    }
    private void TextStart()
    {

        SuanTalkText.text = string.Format("크윽... 좀 하는데..?");
    }
    private void Touch()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            touch+=1;
        }
        if (touch == 1)
            text2();
        if (touch == 2)
            text3();
        if (touch == 3)
            text4();
    }
    private void text2()
    {
        SuanTalkText.text = string.Format("감히 나를 중독에서 벗어나게 하려 하다니..");
    }
    private void text3()
    {
        SuanTalkText.text = string.Format("난 영원한 중독이다!");
    }
    private void text4()
    {
        gameManager.suandest = true;
        SuanTalkText.text = string.Format(" ");
    }
}
