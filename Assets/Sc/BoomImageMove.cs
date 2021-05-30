using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomImageMove : MonoBehaviour
{
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Boom());
    }
    private IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        gameManager.booming = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeakEnemy"))
        {
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
