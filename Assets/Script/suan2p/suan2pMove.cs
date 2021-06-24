using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class suan2pMove : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private GameObject boss = null;
    private int oneShoting = 10;
    private float boosSpeed = 5f;
    private float randomx = 0f;
    private float randomy = 0f;
    private GameManager gameManager = null;
    [SerializeField]
    private GameObject chang = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;
    private Animator ani = null;
    Coroutine tanMak = null;
    private bool tanMakIng = false;
    private int gak = 360;
    [SerializeField]
    private int BossHP = 300;
    private bool isDead = false;
    [SerializeField]
    private int score = 400000;
    private int randomPatten = 0;
    private bool lasering = false;
    private bool allPatterning = false;
    private bool guning = false;

    private bool gumgiing = false;
    [SerializeField]
    private GameObject gunPrefab = null;
    [SerializeField]
    private GameObject gumgiPrefab = null;
    [SerializeField]
    private GameObject laserPrefab = null;
    [SerializeField]
    private GameObject SuanPosition = null;
    [SerializeField]
    private GameObject allPatternPrefab = null;
    private int SuanKillCount = 0;
    private bool dead = false;

    private bool wer = false;
    [SerializeField]
    private GameObject Explo = null;

    private bool changing = false;

    [SerializeField]
    private GameObject mainCamara;
    Vector3 cameraPos;
    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField] [Range(0.1f, 5f)] float duration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SuanKillCount = PlayerPrefs.GetInt("SUANKILLCOUNT", 0);
        ani = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        tanMak = StartCoroutine(SpellStart());
        gameManager.Suan2PUPdateUI();
        gameManager.suan2p = true;
       // GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().StartBullet();
        StartCoroutine(RandomPos());
        StartCoroutine(SpellStart());
        Invoke("Chang", 9f);
        Invoke("Col", 9f);
        Invoke("Deagi", 10f);
    }
    public void Shake()
    {
        cameraPos = mainCamara.transform.position;
        InvokeRepeating("StartShake", 0.01f, 0.02f);
        Invoke("StopShake", duration);
    }
    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamara.transform.position;
        cameraPos.x = cameraPosX + transform.localPosition.x;
        cameraPos.y = cameraPosY + transform.localPosition.y;
        mainCamara.transform.position = cameraPos;
    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamara.transform.position = cameraPos;
    }
    private void Deagi()
    {
        StartCoroutine(RandomPatten());
    }


    private IEnumerator RandomPatten()
    {
        while (true)
        {
            randomPatten = Random.Range(0, 100);
            yield return new WaitForSeconds(0.1f);
            randomPatten = 0;
            yield return new WaitForSeconds(3f);
        }
    }
    private void Chang()
    {
        GameObject changbullet;
        for(int i=0; i<7; i++)
        {
            changbullet = Instantiate(chang, new Vector2(gameManager.MinPosition.x + (1f*i), gameManager.MaxPosition.y+5f), Quaternion.identity);
            changbullet.transform.SetParent(null);
        }
        Invoke("Chang2", 1f);
    }
    private void Chang2()
    {
        GameObject changbullet;
        for (int i = 0; i < 7; i++)
        {
            changbullet = Instantiate(chang, new Vector2(gameManager.MaxPosition.x - (1f*i), gameManager.MaxPosition.y + 5f), Quaternion.identity);
            changbullet.transform.SetParent(null);
        }
        changing = false;
    }
    IEnumerator SpellStart()
    {
        col.enabled = false;
        tanMakIng = true;
        float angle = 360 / oneShoting;
        for (int i = 0; i < 450; i++) { 
        instorpool();
            gak -= 30;
            if (i > 35)
                tanMakIng = false;
            yield return new WaitForSeconds(0.2f);
        }
        tanMakIng = false;
    }
    private void Col()
    {
        col.enabled = true;
    }
    private float randomZ = 0f;
    private void Reset(GameObject obj)
    {
        obj.transform.SetParent(SuanPosition.transform, false);
        obj.transform.localPosition = Vector2.zero;
        obj.transform.localRotation = Quaternion.identity;
        //obj.transform.localRotation = Quaternion.Euler(0, 0, randomZ);
        obj.SetActive(true);
        obj.transform.SetParent(null);
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void instorpool()
    {
        randomZ = Random.Range(-70f, 70f);
        GameObject obj;

        if (gameManager.enemyPoolManager.transform.childCount > 2) {
            obj = gameManager.enemyPoolManager.transform.GetChild(0).gameObject;
            Reset(obj);
        }
        else {
            obj = Instantiate(bullet, boss.transform.position, Quaternion.identity);
        }

            //obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Mathf.Cos(Mathf.PI * 2 * i / oneShoting), speed * Mathf.Sin(Mathf.PI * i * 2 / oneShoting)));

            // obj.transform.Rotate(new Vector3(0f, 0f, gak * i / oneShoting - 90));
            obj.transform.Rotate(new Vector3(0f, 0f, randomZ));
        
    }

    IEnumerator RandomPos()
    {
        while (true) {
        randomx = Random.Range(-2.3f, 2.3f);
        randomy = Random.Range(2f, 4f);
            yield return new WaitForSeconds(2f);
        }
    }
    private void GumGi()
    {
        GameObject gumgi;
        gumgi = Instantiate(gumgiPrefab, new Vector2(SuanPosition.transform.position.x, SuanPosition.transform.position.y - 0.25f), Quaternion.identity);
        gumgi.transform.SetParent(null);
        Invoke("Gumgiing", 2f);
    }
    private void Gumgiing()
    {
        gumgiing = false;
    }
    private void Gun()
    {
        GameObject gun;
        gun = Instantiate(gunPrefab, new Vector2(SuanPosition.transform.position.x - 0.25f, SuanPosition.transform.position.y - 0.25f),Quaternion.identity);
        gun.transform.SetParent(SuanPosition.transform);
        Invoke("Guning", 2f);
    }
    private void Guning()
    {
        guning = false;
    }
    private void Laser()
    {
        GameObject laser;
        laser= Instantiate(laserPrefab,new Vector2(SuanPosition.transform.position.x,0f),Quaternion.identity);
        laser.transform.SetParent(null);
        laserPrefab.SetActive(true);
        Invoke("Lasering", 2f);
    }
    private void Lasering()
    {
        lasering = false;
    }
    private void AllPattern()
    {
        GameObject all;
        all = Instantiate(allPatternPrefab, new Vector2(0f, 0f), Quaternion.identity);
        all.transform.SetParent(null);
        Invoke("Alling", 2f);
    }
    private void Alling()
    {
        allPatterning = false;
    }
    void Update()
    {
        if(gameManager.suanLastDead == true)
        {if (wer == true) return;
            wer = true;
            Shake();
            StartCoroutine(ExPLOSION());

        }
        if (dead == false)
        {
            if (randomPatten >= 1 && randomPatten < 20)
            {
                if (guning) return;
                guning = true;
                Gun();
            }
            else if (randomPatten > 21 && randomPatten < 40)
            {
                if (lasering) return;
                lasering = true;
                Laser();
            }
            else if (randomPatten > 41 && randomPatten < 60)
            {
                if (gumgiing) return;
                gumgiing = true;
                GumGi();
            }
            else if (randomPatten > 61 && randomPatten < 80)
            {
                if (allPatterning) return;
                allPatterning = true;
                AllPattern();
            }
            else if (randomPatten > 81 && randomPatten < 101)
            {
                if (changing) return;
                changing = true;
                Chang();
            }
            Invoke("Move", 6f);
        }
    }
    private IEnumerator ExPLOSION()
    {
        GameObject expl;
        expl = Instantiate(Explo, gameObject.transform);
        yield return new WaitForSeconds(5f);
        if(SuanKillCount <=1)
        {
            SceneManager.LoadScene("Event");
        }
        if (SuanKillCount > 1) { SceneManager.LoadScene("Start"); }
    }
    private void Move()
    {
        if (dead) return;
        if (tanMakIng) return;
        transform.position = Vector2.MoveTowards(
            transform.localPosition, new Vector2(randomx, randomy), boosSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (BossHP > 0)
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
            if (BossHP > 0)
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
    private void Damaged()
    {
        BossHP--;
        gameManager.Suan2pDamaged();
        gameManager.Suan2PUPdateUI();
        StartCoroutine(ColorChange());
    }
    private IEnumerator ColorChange()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.01f);
        spriteRenderer.material.color = Color.white;
    }
    private void Dead()
    {
        gameManager.suan2pDead = true;
        col.enabled = false;
        GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().StopBullet();
        spriteRenderer.material.color = Color.white;
        dead = true;
        SuanKillCount++;
        PlayerPrefs.SetInt("SUANKILLCOUNT", (int)SuanKillCount);
        
        StopAllCoroutines();
        transform.localPosition = new Vector2(0, 3);
        ani.Play("suan2pDead");
        transform.localPosition = new Vector2(0, 3);
    }
}
