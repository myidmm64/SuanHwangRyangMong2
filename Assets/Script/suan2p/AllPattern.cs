using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPattern : MonoBehaviour
{
    private Animator ani = null;
    private Collider2D col = null;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        col.enabled = false;
        StartCoroutine(jae());
    }

    private IEnumerator jae()
    {
        yield return new WaitForSeconds(1f);
        col.enabled = true;
        ani.Play("fulling");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
