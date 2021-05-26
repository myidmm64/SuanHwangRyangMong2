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
        if (bossLive) return;
        if(score >= 250)
        {
            GameObject suan;
            suan = Instantiate(bossSuanPrefeb, new Vector2(0,3),Quaternion.identity);
            bossLive = true;
        }
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
    }
    private IEnumerator EnemySpawn()
    {
        float spawnDelay = 0f;
        float randomX = 0f;

        while (bossLive == false)
        {
            spawnDelay = Random.Range(1f, 3f);
            randomX = Random.Range(MinPosition.x, MaxPosition.x);

            for (int i = 0; i < 4; i++)
            {
                if (bossLive) break;
                Instantiate(disineyPrefeb, new Vector2(randomX, 5.2f), Quaternion.identity);
                yield return new WaitForSeconds(0.3f);
            }

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
