using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    private float wasdSpeed = 5f;
    [SerializeField]
    private GameObject bulletPosition = null;
    [SerializeField]
    private GameObject smallBulletPrefab = null;
    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    public float bulletDelay = 0.05f;
    [SerializeField]
    private GameObject pilsalPrefeb = null;
    private bool isDamaged = false;
    private SpriteRenderer spriteRenderer = null;
    //private float cooldown = 0f;
    public Coroutine bullet1;
    private float pilsalTimer = 0f;
    private bool pilsaling = false;
    public bool deading = false;

    public bool[] joyControl;
    public bool isControl;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bullet1 = StartCoroutine(Fire());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public IEnumerator Fire()
    {
        while (true)
        {
            SpawnOrInstantiate();
            yield return new WaitForSeconds(bulletDelay);
        }
    }
    private void SpawnOrInstantiate()
    {
        GameObject bullet = null;

        if(gameManager.poolManager.transform.childCount > 6)
        {
            for (int i = 0; i < 3; i++)
            {
                
                bullet = gameManager.poolManager.transform.GetChild(i).gameObject;
                bullet.transform.SetParent(bulletPosition.transform, false);
                bullet.transform.position = bulletPosition.transform.position+new Vector3(0.2f*(i-1),0f,0f);
                if (i == 1)
                    bullet.transform.localScale = Vector3.one*1.5f;
                else
                    bullet.transform.localScale = Vector3.one;
                bullet.SetActive(true);
                bullet.transform.SetParent(null);
            }
        }
        else
        {
            bullet = Instantiate(smallBulletPrefab, new Vector2(bulletPosition.transform.position.x,
                bulletPosition.transform.position.y + 0.7f), Quaternion.identity);
            bullet.transform.localScale = Vector3.one * 1.5f;
            bullet = Instantiate(smallBulletPrefab, new Vector2(bulletPosition.transform.position.x + 0.2f,
                bulletPosition.transform.position.y + 0.3f), Quaternion.identity);
            bullet = Instantiate(smallBulletPrefab, new Vector2(bulletPosition.transform.position.x - 0.2f,
                bulletPosition.transform.position.y + 0.3f), Quaternion.identity);
            bullet.transform.SetParent(null);
            bullet.transform.localScale = Vector3.one;

        }
        if (bullet != null) { bullet.transform.SetParent(null); }

    }
 
    public void Pilsal()
    {
        if (Input.GetKey(KeyCode.R))
        {
            wasdSpeed = 1f;
            if (deading)
                pilsalTimer = 0f;
            pilsalTimer += Time.deltaTime;
            if (pilsalTimer >= 2f)
            {
                if (pilsaling) return;
                pilsalTimer = 0f;
                StartCoroutine(pilsalBoom());
            }
        }
        else
        {
            wasdSpeed = 5f;
            pilsalTimer = 0f;
        }
    }
    public void Slow()
    {
        wasdSpeed = 1f;
    }
    public void NoSlow()
    {
        wasdSpeed = 5f;
    }
    private IEnumerator pilsalBoom()
    {
        StopCoroutine(bullet1);
        pilsaling = true;
        GameObject pilsalgi;
        pilsalgi = Instantiate(pilsalPrefeb, new Vector2(bulletPosition.transform.position.x,
                bulletPosition.transform.position.y + 0.9f), Quaternion.identity);
        pilsalgi.transform.SetParent(gameObject.transform);
        yield return new WaitForSeconds(3f);
            FireAndStop();
    }
    private void FireAndStop()
    {
        pilsaling = false;
        bullet1 = StartCoroutine(Fire());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isDamaged) return;
        if (collision.CompareTag("NeoMoHae")){
            gameManager.Dead();
            gameManager.Dead();
        }
        if (collision.CompareTag("SuanBullet"))
        {
            gameManager.Dead();
        }
        if (collision.CompareTag("GoodItem")|| collision.CompareTag("BoomImage")) return;
        if (collision.CompareTag("Bullet")) return;
        isDamaged = true;
        StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {
        deading = true;
        gameManager.Dead();
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDamaged = false;
        deading = false;
    }
    public void StopBullet()
    {
        StopCoroutine(bullet1);
    }
    public void StartBullet()
    {
        bullet1 = StartCoroutine(Fire());
    }
// Update is called once per frame
void Update()
    {
        Pilsal();
        TouchMove();
        //WASDMove();
       // Move();
        LimitCheck();

    }
    public void JoyPannel(int type)
    {
            for(int index=0; index<9; index++)
        {
            joyControl[index] = index == type; 
        }
    }
    public void JoyDown()
    {
        isControl = true;
    }
    public  void JoyUp()
    {
        isControl = false;
    }
    private bool isTouchRight;
    private bool isTouchTop ;
    private bool isTouchLeft ;
    private bool isTouchBottom ;
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if(joyControl[0]) { h = -1;v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }


        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl)
            h = 0;
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }
    private void TouchMove() { 
    if (Input.GetMouseButton(0))
        {
            targetPosition =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.MinPosition.x, gameManager.MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.MinPosition.y - 0.85f, gameManager.MaxPosition.y + 0.85f);
            transform.localPosition =
            Vector2.MoveTowards(transform.localPosition,
            targetPosition, speed* Time.deltaTime);
        }
}
    private void WASDMove() {
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
    }
    private void LimitCheck() {
        if (Input.GetKey(KeyCode.Space))
        {
            gameManager.BoomPointDOWN();
        }
        if (transform.localPosition.x > gameManager.MaxPosition.x)
        {
            transform.Translate(Vector2.left * wasdSpeed * Time.deltaTime);
        }
        if (transform.localPosition.x < gameManager.MinPosition.x)
        {
            transform.Translate(Vector2.right * wasdSpeed * Time.deltaTime);
        }
        if (transform.localPosition.y < gameManager.MinPosition.y - 0.85)
        {
            transform.Translate(Vector2.up * wasdSpeed * Time.deltaTime);
        }
        if (transform.localPosition.y > gameManager.MaxPosition.y + 0.85)
        {
            transform.Translate(Vector2.down * wasdSpeed * Time.deltaTime);
        }
    }
}
