using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.localPosition.y < gameManager.MinPosition.y-1.2f)
        {
            Destroy(gameObject);
        }
    }
}
