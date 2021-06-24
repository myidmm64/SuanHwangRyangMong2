using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Collider2D col = null;
    private SpriteRenderer sprite = null;
     // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        sprite.material.color = Color.red;
        col.enabled = false;
        Invoke("Ing", 1f);
    }

    private void Ing()
    {
        col.enabled = true;
        sprite.material.color = Color.white;
        Invoke("Dead", 0.5f);
    }
    private void Dead()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
