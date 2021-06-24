using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleColor : MonoBehaviour
{
    private int randomColor = 0;
    [SerializeField]
    private Text text = null;
    private int r = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(di());
    }
    private IEnumerator di()
    {
        while (randomColor <= 150) { 
        randomColor++;
        yield return new WaitForSeconds(0.01f);
        }
        randomColor = 0;
        r++;
        if (r == 4)
            r = 1;
        StartCoroutine(di());
    }

    // Update is called once per frame
    void Update()
    {
        if (r == 1)
            blue();
        if (r == 2)
            green();
        if (r == 3)
            red();
    }
    private void blue()
    {
        text.color = new Color(randomColor / 255f, randomColor / 255f, 1f);
    }
    private void green()
    {
        text.color = new Color(randomColor / 255f, 1f, randomColor / 255f);
    }
    private void red()
    {
        text.color = new Color(1f, randomColor / 255f, randomColor / 255f);
    }
}
