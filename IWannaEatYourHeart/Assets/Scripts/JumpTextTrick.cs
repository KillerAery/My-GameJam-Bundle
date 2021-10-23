using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTextTrick : MonoBehaviour
{
    public float force = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
       var body = GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(Random.Range(-1.0f,1.0f), Random.Range(0, 1.0f)).normalized * force);
        StartCoroutine(Suicide());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
