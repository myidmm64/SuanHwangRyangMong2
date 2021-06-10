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
    private bool neoMoHaeing = false;
    Coroutine my_Coroutine;
    Coroutine MachineGun;
    public bool suanDead = false;
    private Animator animator = null;
    [SerializeField]
    private GameObject suanImage1 = null;
    void Start()
    {
        animator = GetComponent<Animator>();
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
            if (suanDead) yield break;
            neoMoHaeing = true;
            if (machineGuning==true || snipinging==true)
            {
                StopCoroutine(my_Coroutine);
            }
            bullet = Instantiate(NeoMoHaePrefab, Position);
            bullet.transform.SetParent(null);
            yield return new WaitForSeconds(1f);
            neoMoHaeing = false;
        }
    }
    private IEnumerator MachineGunShot()
    {
        if (snipinging) {
            yield break;
        }
        machineGuning = true;

        GameObject machineGunBullet;
        for (int i = 0; i < 6; i++)
        {
            if (suanDead) yield break;
            if (neoMoHaeing)
                StopCoroutine(my_Coroutine);
            if (snipinging) {
                machineGuning = false;
                yield break; }
            machineGunBullet = Instantiate(machineGunBulletPrefeb, Position);
            machineGunBullet.transform.SetParent(null);
            yield return new WaitForSeconds(0.25f);
        }
        StopCoroutine(MachineGun);
        my_Coroutine = StartCoroutine(NeoMoHaeShot());
        machineGuning = false;
        machineGunTimer = 0f;
    }
    private IEnumerator RandomMove()
    {
        if (suanDead) yield break;
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
        Desd();
    }
    private void Move()
    {
        if (suanDead) return;
        if (snipinging) return;
        if (machineGuning) return;
        transform.position = Vector2.MoveTowards(
            transform.localPosition, new Vector2(randomx, randomy), speed * Time.deltaTime);
    }
    private void Timer()
    {
        if (suanDead) return;
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
        if (suanDead) return;
        if (machineGuning) return;
        machineGunTimer += Time.deltaTime;
        if (machineGunTimer >= 3.4)
        {
            MachineGun = StartCoroutine(MachineGunShot());
        }
    }
    private IEnumerator SuanSniping()
    {
        if (suanDead) yield break;
        if (neoMoHaeing) { 
        StopCoroutine(my_Coroutine);
    }
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
                Damaged();
                return;
            }

            if (isDead) return;
            isDead = true;
            gameManager.AddScore(score);
            Dead();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("PilSalBullet"))
        {
            if (bossUsuanHP > 0)
            {
                Damaged();
                Damaged();
                return;
            }

            if (isDead) return;
            isDead = true;
            gameManager.AddScore(score);
            Dead();
        }
    }
    public void Damaged()
    {
        bossUsuanHP--;
        StartCoroutine(ColorChange());
    }
    private IEnumerator ColorChange()
    {
        gameManager.SuanDamaged();
        gameManager.UpdateUI();
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.01f);
        spriteRenderer.material.color = Color.white; 
    }
    private void Dead()
    {
        gameManager.BossDead();
        suanDead = true;
        transform.localPosition = new Vector2(0, 3);
        col.enabled = false;
        animator.Play("suanDead");
        Invoke("SuanImageSummon", 1.5f);
        GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().StopBullet();
        //죽는 애니메이션 실행 -> animator.play~~
    }
    private void SuanImageSummon()
    {
        GameObject suanImage;
        suanImage = Instantiate(suanImage1, new Vector2(-1.5f, 1f), Quaternion.identity);
    }
    private void Desd()
    {
        if (gameManager.suandest)
            Destroy(gameObject);
    }
}
