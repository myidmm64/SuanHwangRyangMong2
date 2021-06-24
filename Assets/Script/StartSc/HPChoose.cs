using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPChoose : MonoBehaviour
{
    [SerializeField]
    private GameObject butten1 = null;
    [SerializeField]
    private GameObject butten2 = null;
    [SerializeField]
    private GameObject butten3 = null;
    private int PlayerHP = 0;

    // Start is called before the first frame update

    public void Hard()
    {
        PlayerHP = 1;
        PlayerPrefs.SetInt("PlayerHP", PlayerHP);
        SceneManager.LoadScene("SampleScene");
    }
    public void Middle()
    {
        PlayerHP = 20;
        PlayerPrefs.SetInt("PlayerHP", PlayerHP);
        SceneManager.LoadScene("SampleScene");

    }
    public void Easy()
    {
        PlayerHP = 40;
        PlayerPrefs.SetInt("PlayerHP", PlayerHP);
        SceneManager.LoadScene("SampleScene");
    }
    void Start()
    {
        PlayerHP = PlayerPrefs.GetInt("PlayerHP", 0);
    }

   public void See()
    {
        
        butten1.SetActive(true);
        butten2.SetActive(true);
        butten3.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
