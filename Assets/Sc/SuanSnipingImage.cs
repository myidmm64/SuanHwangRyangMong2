using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuanSnipingImage : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private GameObject Player2;
    private Vector2 PlayerPos;
    private float timer = 0f;
    private GameManager gameManager = null;
    private bool playerDead = false;
    [SerializeField]
    private GameObject mainCamara;
    private Collider2D col = null;
    Vector3 cameraPos;
    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField] [Range(0.1f, 1f)] float duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Player2 = GameObject.Find("Player");
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    public void Shake()
    {
        cameraPos = mainCamara.transform.position;
        InvokeRepeating("StartShake", 0.01f, 0.02f);
        Invoke("StopShake",duration);
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
    // Update is called once per frame
    void Update()
    {
        StartCoroutine (TimeCheck());
        Move();
    }
    private void Move()
    {
        PlayerPos = Player2.transform.position;
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, PlayerPos, speed * Time.deltaTime);
    }
    private IEnumerator TimeCheck()
    {

        timer += Time.deltaTime;
        if (timer >= 2)
        {
            Shake();
            col.enabled = true;
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerDead) return;
            gameManager.Dead();
            gameManager.Dead();
            gameManager.Dead();
            playerDead = true;
        }
    }
}
