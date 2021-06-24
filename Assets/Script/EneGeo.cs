using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneGeo : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private float speed = 2.5f;
    int oneShoting = 10;
    private GameManager gameManager = null;
    [SerializeField]
    private long score = 2000;
    [SerializeField]
    private int hp = 20;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer = null;
    private Collider2D col = null;
    private Animator ani = null;
    private bool isdead = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ani = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(shot());
    }
    private IEnumerator shot()
    {
        float angle = 360 / oneShoting;
        GameObject obj;
        while (true)
        {
            if (isDead) yield break;
            for (int i = 0; i < oneShoting; i++)
            {
                obj = Instantiate(bullet, boss.transform.position, Quaternion.identity);

                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Mathf.Cos(Mathf.PI * 2 * i / oneShoting), speed * Mathf.Sin(Mathf.PI * i * 2 / oneShoting)));


                obj.transform.Rotate(new Vector3(0f, 0f, 360 * i /oneShoting - 90));
            }
            yield return new WaitForSeconds(2f);
        }
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
        isdead = true;
        spriteRenderer.material.color = Color.white;
        col.enabled = false;
        ani.Play("GeDead");
        //animator.Play("Explosion");  죽는 애니메이션 넣기
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (isdead == false){
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (gameObject.transform.localPosition.x < gameManager.MinPosition.x)
            {
                Destroy(gameObject);
            } }
    }
}

