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
    private Transform playerPosition;
    [SerializeField]
    private GameObject machineGunBulletPrefeb = null;
    private float machineGunTimer = 0;
    private float randomx = 0;
    private float randomy = 0;
    private Collider2D col = null;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private bool isDead = false;
    private long score = 100000;
    private float timer = 0f;
    private bool timerCheck = false;
    public GameObject Player;
    private bool snipinging = false;
    private bool machineGuning = false;
    Coroutine my_Coroutine;
    Coroutine MachineGun;
    void Start()
    {
        my_Coroutine = StartCoroutine(NeoMoHaeShot());
        StartCoroutine(RandomMove());
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    private IEnumerator MachineGunShot()
    {
        if (snipinging) { 
            yield break; }
        machineGuning = true;
        GameObject machineGunBullet;
        for (int i = 0; i < 6; i++)
        {
            if (snipinging) {
                machineGuning = false;
                yield break; }
            machineGunBullet = Instantiate(machineGunBulletPrefeb, Position);
            yield return new WaitForSeconds(0.25f);
        }
        StopCoroutine(MachineGun);
        machineGuning = false;
        machineGunTimer = 0f;
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
        Move();
        Timer();
        MachineGunTimer();
    }
    private void Move()
    {
        if (snipinging) return;
        if (machineGuning) return;
        transform.position = Vector2.MoveTowards(
            transform.localPosition, new Vector2(randomx, randomy), speed * Time.deltaTime);
    }
    private void Timer()
    {
        if (timerCheck) return;
        timer += Time.deltaTime;
        if(timer >= 7)
        {
            timerCheck = true;
            StartCoroutine(SuanSniping());
        }
    }
    private void MachineGunTimer()
    {
        if (machineGuning) return;
        machineGunTimer += Time.deltaTime;
        if (machineGunTimer >= 4)
        {
            MachineGun = StartCoroutine(MachineGunShot());
        }
    }
    private IEnumerator SuanSniping()
    {
        StopCoroutine(my_Coroutine);
        snipinging = true;
            GameObject suansnipe;
        suansnipe = Instantiate(suanSnipePrefeb, new Vector2(0,0),Quaternion.identity);
        suansnipe.transform.SetParent(null);
        //우수안 스나이핑 애니 불러오기
        yield return new WaitForSeconds(3f);
        my_Coroutine = StartCoroutine(NeoMoHaeShot());
        timer = 0f;
        timerCheck = false;
        snipinging = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (bossUsuanHP > 0)
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
        bossUsuanHP--;
        gameManager.SuanDamaged();
        gameManager.UpdateUI();
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.01f);
        spriteRenderer.material.color = Color.white; 
    }

    private IEnumerator Dead()
    {
        col.enabled = false;
        //죽는 애니메이션 실행 -> animator.play~~
        Destroy(gameObject);
        yield return new WaitForSeconds(0.01f);
    }
}
