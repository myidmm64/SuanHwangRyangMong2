using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFPilsalMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Dead());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") || collision.CompareTag("Player") || collision.CompareTag("Bullet"))
            return;
        Destroy(collision.gameObject);
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
