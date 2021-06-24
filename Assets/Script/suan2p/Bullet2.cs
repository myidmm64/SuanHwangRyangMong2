using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private GameManager gameManager = null;
    private float speed = 5f;
    // Start is called before the first frame update
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
            Despawn();
        }
        if (transform.localPosition.y > gameManager.MaxPosition.y + 0.5f)
        {
            Despawn();
        }
        if (transform.localPosition.x < gameManager.MinPosition.x)
        {
            Despawn();
        }
        if (transform.localPosition.x > gameManager.MaxPosition.x)
        {
            Despawn();
        }
    }
    public void Despawn()
    {
        transform.SetParent(gameManager.enemyPoolManager.transform, false);
        gameObject.SetActive(false);
    }
}

