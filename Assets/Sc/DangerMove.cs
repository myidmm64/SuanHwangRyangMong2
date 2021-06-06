using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerMove : MonoBehaviour
{
   // private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dead());
    }
    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
