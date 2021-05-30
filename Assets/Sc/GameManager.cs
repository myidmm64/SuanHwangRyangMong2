using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    private GameObject disineyPrefeb = null;
    [SerializeField]
    private GameObject bossStartDangerPrefeb = null;
    [SerializeField]
    private GameObject BoomPrefeb = null;
    private float dangerTime = 0f;
    private bool bossDeadChack = false;
    private bool bossStart = false;
    private int boomPoint = 0;
    public bool booming = false;
    // Start is called before the first frame update
    void Start()
    {
        MinPosition = new Vector2(-2.3f, -4f);
        MaxPosition = new Vector2(2.3f, 4f);
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        UpdateUI();
        StartCoroutine(EnemySpawn());
        Player = FindObjectOfType<PlayerMove>();
    }
    public void BossStart()
    {
        bossStart = true;
    }
    public void BossDead()
    {
        bossDeadChack = true;
    }
    public void BoomPointUP()
    {
        boomPoint++;
        UpdateUI();
    }
    public void BoomPointDOWN()
    {
        if (boomPoint <= 0) return;
        if (booming) return;
        boomPoint--;
        UpdateUI();
        Boom();
        booming = true;
    }
    private void Boom()
    {
        GameObject boom;
        boom = Instantiate(BoomPrefeb, new Vector2(0, 0), Quaternion.identity);
    }
    public void AddScore(long addScore)
    {
        score += addScore;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", (int)highScore);
        }
         UpdateUI();
    }
    private void BossSuanCheck()
    {
        if(score >= 50000)
        {
            dangerTime += Time.deltaTime;
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
        danger = Instantiate(bossStartDangerPrefeb, new Vector2(-2f, 1.7f), Quaternion.identity);
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
        float randomX = 0f;

        while (bossLive == false)
        {
            spawnDelay = Random.Range(0.3f, 0.6f);
            randomX = Random.Range(MinPosition.x, MaxPosition.x);

                if (bossLive) break;
                Instantiate(disineyPrefeb, new Vector2(randomX, 5.2f), Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    public void Dead()
    {
        playerLife--;
        UpdateUI();
        if (playerLife <= 0)
        {
            return;
        //    SceneManager.LoadScene("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        BossSuanCheck();
    }
}
