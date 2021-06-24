using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMove : MonoBehaviour
{
    private float randomX = 0f;
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(RandomX());
    }
    private IEnumerator RandomX()
    {
        while (true) { 
        randomX = Random.Range(gameManager.MinPosition.x, gameManager.MaxPosition.x);
        yield return new WaitForSeconds(1.2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(randomX, 5.5f);
    }
}
