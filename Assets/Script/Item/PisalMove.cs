using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisalMove : MonoBehaviour
{
    [SerializeField]
    private GameObject pilsalBullet = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pilsal());
    }

    private IEnumerator Pilsal()
    {
        GameObject bullet;
        bullet = Instantiate(pilsalBullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 5f), Quaternion.identity);
        bullet.transform.SetParent(gameObject.transform);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
