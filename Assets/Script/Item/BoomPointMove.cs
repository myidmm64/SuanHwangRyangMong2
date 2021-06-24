using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPointMove : MonoBehaviour
{
    private GameManager gameManager = null;
    [SerializeField]
    private float speed = 10f;
    private bool upCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (upCheck) return;
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.BoomPointUP();
            upCheck = true;
        }
    }
    private void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.localPosition.y < gameManager.MinPosition.y - 0.85f)
        {
            Destroy(gameObject);
        }

    }
}
