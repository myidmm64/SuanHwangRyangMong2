using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class suankill : MonoBehaviour
{
    [SerializeField]
    private Text SuanKillCountText = null;
    // Start is called before the first frame update
    void Start()
    {

        SuanKillCountText.text = string.Format("����� ������� �����߸� Ƚ�� {0}",
           PlayerPrefs.GetInt("SUANKILLCOUNT", 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
