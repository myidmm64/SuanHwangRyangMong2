using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PoolManager poolManager { get; private set; }
    public EnemyPoolManager enemyPoolManager { get; private set; }
    public PlayerMove Player { get; private set; }
    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textHighScore = null;
    [SerializeField]
    private Text textLife = null;
    [SerializeField]
    private Text textBoomPoint = null;
    [SerializeField]
    private Text suanLifeText = null;
    [SerializeField]
    private GameObject bossSuanPrefeb = null;
    private long score = 0;
    private long highScore = 0;
    private bool bossLive = false;
    [SerializeField]
    private float playerLife = 3f;
    [SerializeField]
    private float suanLife = 250f;
    [SerializeField]
    private int suan2pLife = 300;
    [SerializeField]
    private GameObject disineyPrefeb = null;
    [SerializeField]
    private GameObject bossStartDangerPrefeb = null;
    [SerializeField]
    private GameObject BoomPrefeb = null;
    private float randomX = 0f;
    private float randomY = 0f;
   // private float dangerTime = 0f;
    public bool bossDeadChack = false;
    //private bool bossStart = false;
    private int boomPoint = 0;
    public bool booming = false;
    public bool suandest = false;
    public bool suan2p = false;
    [SerializeField]
    private GameObject geobook = null;
    private int DeadCount = 0;
   
    public bool suan2pDead = false;
    public bool suanLastDead = false;

    // Start is called before the first frame update
    void Awake()
    {
        enemyPoolManager = FindObjectOfType<EnemyPoolManager>();
        poolManager = FindObjectOfType<PoolManager>();
        MinPosition = new Vector2(-2.3f, -4f);
        MaxPosition = new Vector2(2.3f, 4f);
        DeadCount = PlayerPrefs.GetInt("DEADCOUNT", 0);
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        StartCoroutine(EnemySpawn());
        StartCoroutine(EnemySpawn2());
        Player = FindObjectOfType<PlayerMove>();

        playerLife = PlayerPrefs.GetInt("PlayerHP", 0);
        UpdateUI();
    }
    public void BossStart()
    {
       // bossStart = true;
    }
    public void BossDead()
    {
        bossDeadChack = true;
    }
    public void BoomPointUP()
    {
        boomPoint++;
        if (bossDeadChack == false)
            UpdateUI();
        else
            Suan2PUPdateUI();
    }
    public void BoomPointDOWN()
    {
        if (boomPoint <= 0) return;
        if (booming) return;
        boomPoint--;
        if (bossDeadChack == false)
            UpdateUI();
        else
            Suan2PUPdateUI();
        Boom();
        booming = true;
    }
    private void Boom()
    {
        GameObject boom;
        boom = Instantiate(BoomPrefeb, new Vector2(Player.transform.position.x,Player.transform.position.y +1f),Quaternion.identity);
    }
    public void AddScore(long addScore)
    {
        score += addScore;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", (int)highScore);
        }
        if (bossDeadChack == false)
            UpdateUI();
        else
            Suan2PUPdateUI();
    }
    private void BossSuanCheck()
    {
        if(score >= 50000)
        {
            //dangerTime += Time.deltaTime;
            BossSuanSummon();
        }
    }
    private void BossSuanSummon()
    {
        if (bossLive) return;
        StartCoroutine(DangerSummon());
        bossLive = true;
    }
    private IEnumerator DangerSummon()
    {
        GameObject danger;
        danger = Instantiate(bossStartDangerPrefeb, new Vector2(0f, 2f), Quaternion.identity);
        yield return new WaitForSeconds(4f);
        SuanSummon();

    }
    private void SuanSummon()
    {
        GameObject suan;
        suan = Instantiate(bossSuanPrefeb, new Vector2(0, 3), Quaternion.identity);

    }
    public void SuanDamaged()
    {
        suanLife--;
    }
    public void Suan2pDamaged()
    {
        suan2pLife--;
    }
    public void Suan2PUPdateUI()
    {
        textScore.text = string.Format("SCORE\n{0}", score);
        textHighScore.text = string.Format("HIGHSCORE\n{0}", highScore);
        textLife.text = string.Format("LIFE\n{0}", playerLife);
        suanLifeText.text = string.Format("BOSS HP\n{0}", suan2pLife);
        textBoomPoint.text = string.Format("BOOM\n{0}", boomPoint);
    }
    public void UpdateUI()
    {
        textScore.text = string.Format("SCORE\n{0}", score);
        textHighScore.text = string.Format("HIGHSCORE\n{0}", highScore);
        textLife.text = string.Format("LIFE\n{0}", playerLife);
        suanLifeText.text = string.Format("BOSS HP\n{0}", suanLife);
        textBoomPoint.text = string.Format("BOOM\n{0}", boomPoint);
    }
    private IEnumerator EnemySpawn()
    {
        float spawnDelay = 0f;

        while (bossLive == false)
        {
            spawnDelay = Random.Range(1.5f, 2f);
            randomX = Random.Range(MinPosition.x,MaxPosition.x);
            if (bossLive) break;
            GameObject enemy;
            enemy = Instantiate(disineyPrefeb, new Vector2(randomX,5.5f),Quaternion.identity);
            enemy.transform.SetParent(null);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private IEnumerator EnemySpawn2()
    {
        float spawnDelay = 0f;

        while (bossLive == false)
        {
            spawnDelay = Random.Range(4f, 7f);
            randomY = Random.Range(MinPosition.y + 3.5f, MaxPosition.y);
            if (bossLive) break;
            GameObject enemy;
            enemy = Instantiate(geobook, new Vector2(MaxPosition.x, randomY), Quaternion.identity);
            enemy.transform.SetParent(null);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    public void Dead()
    {
        playerLife--;
        if (bossDeadChack == false)
            UpdateUI();
        else
            Suan2PUPdateUI();
        if (playerLife <= 0)
        {
            DeadCount++;
            PlayerPrefs.SetInt("DEADCOUNT", (int)DeadCount);
            SceneManager.LoadScene("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        BossSuanCheck();
    }
}
