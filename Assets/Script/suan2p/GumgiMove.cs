using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumgiMove : MonoBehaviour
{
    private float speed = 2f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        Speed();
    }
    private void Speed()
    {
        speed = 14f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
