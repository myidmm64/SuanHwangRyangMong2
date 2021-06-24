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
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private GameObject gameobob = null;
    void Start()
    {
        StartCoroutine(Fire());
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
    private IEnumerator Fire()
    {
        GameObject bullet;
        while (true)
        {
            if (isDead) yield break;
            bullet = Instantiate(bulletPrefab, gameobob.transform);
            bullet.transform.SetParent(null);
            yield return new WaitForSeconds(1.5f);
        }
    }
    private void CheckLimit()
    {
        if (transform.localPosition.y < gameManager.MinPosition.y)
        {
            Destroy(gameObject);
        }
        if (transform.localPosition.y > gameManager.MaxPosition.y + 4f)
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
        if (collision.CompareTag("BoomImage"))
        {
            gameManager.AddScore(score);
        }
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (hp > 1)
            {
                Damaged();
                return;
            }

            if (isDead) return;
            isDead = true;
            gameManager.AddScore(score);
            StartCoroutine(Dead());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("PilSalBullet"))
        {
            if (hp > 1)
            {
                Damaged();
                return;
            }

            if (isDead) return;
            isDead = true;
            gameManager.AddScore(score);
            StartCoroutine(Dead());
        }
    }
    private void Damaged()
    {
        hp--;
        StartCoroutine(ColorChange());
    }
    private IEnumerator ColorChange()
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
        animator.Play("enemyDead");
        //animator.Play("Explosion");  �״� �ִϸ��̼� �ֱ�
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