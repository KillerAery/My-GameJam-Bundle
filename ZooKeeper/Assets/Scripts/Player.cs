using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float speedup = 2.0f;
    public float speedupTime = 3.0f;
    float speedupRemainTime;
    float speedupNeedTime;

    public GameObject body;
    public Animator player1;
    public Animator player2;

    // Start is called before the first frame update
    void Start()
    {
        speedupRemainTime = speedupTime;
        speedupNeedTime = speedupTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float v = speed;

        // 加速
        if (Input.GetKey(KeyCode.LeftShift) && speedupRemainTime > 0f)
        {
            speedupRemainTime -= Time.deltaTime;
            v = speedup;
            speedupNeedTime = speedupTime;
        }
        else
        {
            speedupNeedTime -= Time.deltaTime;
        }

        if(speedupNeedTime <= 0f)
        {
            speedupRemainTime = speedupTime;
        }

        // 左右移动
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= v * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += v * Time.deltaTime;
        }

        //转身
        float d = Mathf.Abs(transform.position.x - pos.x);
        if (d > 0.001f)
        {
            float bodydir = 1.0f * Mathf.Sign(transform.position.x - pos.x);
            var s = body.transform.localScale;
            s.x = bodydir;
            body.transform.localScale = s;
        }


        // 移动动画
        if (Input.GetKey(KeyCode.LeftShift) && speedupRemainTime > 0f)
        {
            player1.SetTrigger("Speedup");
            if (player2)
                player2.SetTrigger("Speedup");
        }
        else if(d > 0.001f)
        {
            player1.SetTrigger("Walk");
            if (player2)
                player2.SetTrigger("Walk");
        }
        else
        {
            player1.SetTrigger("Stop");
            if (player2)
                player2.SetTrigger("Stop");
        }

        transform.position = pos;
    }
}
