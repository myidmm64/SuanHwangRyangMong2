using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Suan2pText : MonoBehaviour
{
    [SerializeField]
    private Text SuanTalkText = null;
    private GameManager gameManager = null;
    private int touch = 0;
    private bool qwe = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.suan2pDead == true)
        {
            Touch();
          //  textstart = false;
            //if (textstart == true) return;
            //textstart = true;
            Invoke("TextStart", 1.5f);



        }
    }
    private void TextStart()
    {
        if (qwe == true) return;
        qwe = true;
        SuanTalkText.text = string.Format("안돼..... 안돼...!!!");
    }
    private void Touch()
    {

        if (Input.GetMouseButtonDown(0))
        {
            touch += 1;
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
        SuanTalkText.text = string.Format("여기서 이렇게 죽을 수는 없다고!!");
    }
    private void text3()
    {
        SuanTalkText.text = string.Format("죽고싶지 않아...");
    }
    private void text4()
    {
        //wer = false;
        //gameManager.suandest = true;
        //textstart = false;
        gameManager.suanLastDead = true;
        SuanTalkText.text = string.Format("으아아아아아아ㅏ아ㅏ!!!");
    }
}
