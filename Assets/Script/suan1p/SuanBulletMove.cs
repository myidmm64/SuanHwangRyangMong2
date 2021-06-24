using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuanBulletMove : MonoBehaviour
{
    [SerializeField]
    private float suanBulletSpeed = 30f;
    private GameManager gameManager = null;
    private Vector2 diff = Vector2.zero;
    private float rotationZ = 0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        turn();
    }

    // Update is called once per frame
    void Update()
    {
        
        Shot();
    }
    private void turn()
    {
        diff = gameManager.Player.transform.position - transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);
        Shot();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void Shot()
    {
        transform.Translate(Vector2.down * suanBulletSpeed * Time.deltaTime);
    }
}
