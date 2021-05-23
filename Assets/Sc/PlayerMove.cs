using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float wasdSpeed = 5f;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;
    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    [SerializeField]
    private float bulletDelay = 0.2f;
    private bool isDamaged = false;
    private SpriteRenderer spriteRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Fire());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private IEnumerator Fire()
    {
        GameObject bullet;
        while (true)
        {
            bullet = Instantiate(bulletPrefab, bulletPosition);
            bullet.transform.SetParent(null);
            yield return new WaitForSeconds(bulletDelay);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        isDamaged = true;
        StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {

        gameManager.Dead();
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDamaged = false;
    }

// Update is called once per frame
void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.MinPosition.x, gameManager.MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.MinPosition.y - 0.85f, gameManager.MaxPosition.y + 0.85f);
            transform.localPosition =
            Vector2.MoveTowards(transform.localPosition,
            targetPosition, speed * Time.deltaTime);
        }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * wasdSpeed * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * wasdSpeed * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * wasdSpeed * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * wasdSpeed * Time.deltaTime);

        }
        if (transform.localPosition.x > gameManager.MaxPosition.x)
        {
            transform.Translate(Vector2.left * wasdSpeed * Time.deltaTime);
        }
        if (transform.localPosition.x < gameManager.MinPosition.x)
        {
            transform.Translate(Vector2.right * wasdSpeed * Time.deltaTime);
        }
        if (transform.localPosition.y < gameManager.MinPosition.y-0.85)
        {
            transform.Translate(Vector2.up * wasdSpeed * Time.deltaTime);
        }
        if (transform.localPosition.y > gameManager.MaxPosition.y+0.85)
        {
            transform.Translate(Vector2.down * wasdSpeed * Time.deltaTime);
        }

    }
}
