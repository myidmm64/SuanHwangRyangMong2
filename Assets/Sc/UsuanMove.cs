using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsuanMove : MonoBehaviour
{
    [SerializeField]
    private GameObject NeoMoHaePrefab = null;
    [SerializeField]
    private Transform Position = null;
    [SerializeField]
    private float speed = 30f;
    [SerializeField]
    private float bossUsuanHP = 250f;
    [SerializeField]
    private GameObject suanSnipePrefeb = null;
    [SerializeField]
    private Transform playerPosition = null;
    private float randomx = 0;
    private float randomy = 0;
    private Collider2D col = null;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private bool isDead = false;
    private bool isDamaged = false;
    private long score = 100000;
    private float timer = 0f;
    private bool timerCheck = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(NeoMoHaeShot());
        StartCoroutine(RandomMove());
    }

    private IEnumerator NeoMoHaeShot()
    {
        GameObject bullet;
        while (true)
        {
            bullet = Instantiate(NeoMoHaePrefab, Position);
            bullet.transform.SetParent(null);
            yield return new WaitForSeconds(1f); 
        }
    }
    private IEnumerator RandomMove()
    {
        while (true)
        {
            randomx = Random.Range(-2.3f, 2.3f);
            randomy = Random.Range(2f, 4f);
            yield return new WaitForSeconds(2f);
        }
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.localPosition, new Vector2(randomx, randomy), speed * Time.deltaTime);
        Timer();
    }
    private void Timer()
    {
        if (timerCheck) return;
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            timerCheck = true;
            StartCoroutine(SuanSniping());
        }
    }
    private IEnumerator SuanSniping()
    {
            GameObject suansnipe;
        suansnipe = Instantiate(suanSnipePrefeb, playerPosition);
        suansnipe.transform.SetParent(null);
        yield return new WaitForSeconds(1f);
            timerCheck = false;
            timer = 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (bossUsuanHP > 0)
            {
                if (isDamaged) return;
                isDamaged = true;
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
        bossUsuanHP--;
        gameManager.SuanDamaged();
        gameManager.UpdateUI();
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.color = Color.white; 
        isDamaged = false;
    }

    private IEnumerator Dead()
    {
        col.enabled = false;
        //죽는 애니메이션 실행 -> animator.play~~
        Destroy(gameObject);
        yield return new WaitForSeconds(0.01f);
    }
}
