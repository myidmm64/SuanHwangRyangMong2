using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtenManager : MonoBehaviour
{
    private bool charge = false;
    private float pilsalTimer = 0f;
    [SerializeField]
    private GameObject pilsalPrefeb = null;
    [SerializeField]
    private GameObject bulletPosition = null;
    private bool pilsaling = false;
    private bool slow = false;
    private GameManager gameManager = null;
    // Start is called before the first f

    public void Oncl()
    {
        slow = true;
           charge = true;
    }
    public void OnExit()
    {
        slow = false;
        charge = false;
    }
    public void Pilsal()
    {
            if (charge)
        {
            pilsalTimer += Time.deltaTime;
        }
        if (pilsalTimer >= 2f)
        {
            if (pilsaling) return;
            StartCoroutine(PilsalBoom());
            pilsalTimer = 0f;
        }
    }
    private IEnumerator PilsalBoom()
    {
        GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().StopBullet();
        //pilsaling = false;
        GameObject pilsalgi;
        pilsalgi = Instantiate(pilsalPrefeb, new Vector2(bulletPosition.transform.position.x,
                bulletPosition.transform.position.y + 1f), Quaternion.identity);
        pilsalgi.transform.SetParent(bulletPosition.transform);
        yield return new WaitForSeconds(3f);
        FireAndStop();
        pilsalTimer = 0f;
    }
    private void FireAndStop()
    {
        if (GameObject.Find("TextManager").GetComponent<SuanImageMove>().wer==true)
        {
            return;
        }
        GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().StartBullet();
        pilsaling = false;
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slow)
            GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().bulletDelay = 0.1f;
        else
            GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().bulletDelay = 0.05f;

                    Pilsal();
        if (GameObject.Find("sangbinplane_0").GetComponent<PlayerMove>().deading == true)
            pilsalTimer = 0f;
    }
}
