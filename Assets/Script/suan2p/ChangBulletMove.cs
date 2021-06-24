using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangBulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 12f;
    private GameManager gameManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        CheckLimit();
    }
    private void CheckLimit()
    {
        if (transform.localPosition.y < gameManager.MinPosition.y - 2f)
        {
            Destroy(gameObject);
        }
        if (transform.localPosition.x < gameManager.MinPosition.x)
        {
            Destroy(gameObject);
        }
        if (transform.localPosition.x > gameManager.MaxPosition.x)
        {
            Destroy(gameObject);
        }
    }
}
