using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suanit : MonoBehaviour
{
    private StartSuanntro st = null;
    [SerializeField]
    private GameObject intr = null;
    // Start is called before the first frame update
    void Start()
    {
        st = GetComponent<StartSuanntro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(st.istrue == true)
        {
            intr.SetActive(false);
        }
    }
}
