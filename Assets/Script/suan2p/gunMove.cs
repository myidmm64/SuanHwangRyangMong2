using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunMove : MonoBehaviour
{
    private GameManager gameManager = null;
    private Vector2 diff = Vector2.zero;
    private float rotationZ = 0f;
    [SerializeField]
    private GameObject bulletPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Shot());
        Invoke("Dead", 2f);
    }
    private IEnumerator Shot()
    {
        GameObject bullet;
        while(true){ 
        bullet = Instantiate(bulletPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.25f), Quaternion.identity);
        bullet.transform.SetParent(null);
        yield return new WaitForSeconds(0.3f);
        }
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
    private void turn()
    {
        diff = gameManager.Player.transform.position - transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);
    }
    // Update is called once per frame
    void Update()
    {

        turn();
    }
}
