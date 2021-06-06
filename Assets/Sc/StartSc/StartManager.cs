using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private GameObject WayImage = null;
    [SerializeField]
    private GameObject wayUI = null;
    public bool suanClick = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void OnClickStart() {
        suanClick = true;
        //SceneManager.LoadScene("SampleScene");
    }
    public void WayCliick()
    {
        WayImage.SetActive(true);
        wayUI.SetActive(false);
    }
    public void Wayclose()
    {
        wayUI.SetActive(true);
        WayImage.SetActive(false);

    }
}
