using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(transform.localPosition.y > gameManager.MaxPosition.y+1.2f)
        {
            Destroy(gameObject);
        }
    }
}
