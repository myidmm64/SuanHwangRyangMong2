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
    public bool textstart = false;
    private bool qwe = false;
    public bool wer;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if(gameManager.bossDeadChack == true)
        {
            Touch();
            textstart = false;
            if (textstart == true) return;
            textstart = true;
            Invoke("TextStart",1.5f);



        }
    }
    private void TextStart()
    {
        if (qwe == true) return;
        qwe = true;
        SuanTalkText.text = string.Format("ũ��... �� �ϴ°�");
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
        SuanTalkText.text = string.Format("���� ���� �ߵ����� ����� �Ϸ� �ϴٴ�..");
    }
    private void text3()
    {
        SuanTalkText.text = string.Format("�� ������ �ߵ��̴�!");
    }
    private void text4()
    {
        wer = false;
        gameManager.suandest = true;
        textstart = false;
        SuanTalkText.text = string.Format(" ");
    }
}
