using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAI : MonoBehaviour
{
    public float speed = 1.0f;
    public float dir = 1.0f;
    public float minx = 0.0f;
    public float maxx = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = pos.x + dir * Time.deltaTime * speed;
        if(pos.x < minx)
        {
            pos.x = maxx;
        }
        else if(pos.x > maxx)
        {
            pos.x = minx;
        }
        transform.position = pos;
    }
}
