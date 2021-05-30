using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private long score = 10;
    [SerializeField]
    private int hp = 2;
    [SerializeField]
    protected float speed = 15f;
    [SerializeField]
    private GameObject boomPointPrefeb = null;

    protected GameManager gameManager = null;
    private Animator animator = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;
    private bool isDead = false;
    private int random = 0;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDead) return;
        Move();
        CheckLimit();
    }

    private void CheckLimit()
    {
        if (transform.localPosition.y < gameManager.MinPosition.y - 2f)
        {
            Destroy(gameObject);
        }
        if (transform.localPosition.y > gameManager.MaxPosition.y + 2f)
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

    protected virtual void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (hp > 1)
            {
                StartCoroutine(Damaged());
                return;
            }

            if (isDead) return;
            isDead = true;
            gameManager.AddScore(score);
            StartCoroutine(Dead());
        }
    }

    private IEnumerator Damaged()
    {
        hp--;
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.01f);
        spriteRenderer.material.color = Color.white;
    }


    private IEnumerator Dead()
    {
        spriteRenderer.material.color = Color.white;
        col.enabled = false;
        //animator.Play("Explosion");  죽는 애니메이션 넣기
        random = Random.Range(1, 100);
        if (random <= 40)
        {
            GameObject boomPoint;
            boomPoint = Instantiate(boomPointPrefeb, gameObject.transform);
            boomPoint.transform.SetParent(null);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}