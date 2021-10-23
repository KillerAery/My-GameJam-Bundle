using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGhostCtrl : MonoBehaviour {
    public Transform[] groundChecks = new Transform[3];
    private Rigidbody body;
    float jumpTimer = 0.0f;
    bool grounded = false;
    bool jump = false;
    bool facingRight = true;
    public float maxSpeed = 1.0f;
    public float jumpForce = 100.0f;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        //累计时间
        jumpTimer += dt;

        //TODO 待优化代码
        //是否到地面 用三个groundCheck来检测
        for (int i = 0; i < 3; ++i)
        {
            grounded = Physics.Linecast(transform.position, groundChecks[i].position, 1 << LayerMask.NameToLayer("Barrier"));
            if (grounded) break;
        }

        //跳跃
        //按下K时
        if (Input.GetKeyDown(KeyCode.K) && jumpTimer >= 0.08f)
        {
            //正常跳跃
            if (grounded)//跳跃
            {
                jump = true;
                jumpTimer = 0.0f;
            }
        }

    }

    private void FixedUpdate()
    {
        //处理水平移动（根据水平输入）
        HorizontalMoveControll();
        //处理跳跃
        JumpControll();
    }

    //翻转
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //水平移动（根据水平输入值）
    void HorizontalMoveControll()
    {
        float h = Input.GetAxis("Horizontal");
        //设置水平移动速度
        if (Mathf.Abs(body.velocity.x) <= maxSpeed + 10.0f)
        {
            float vx = 0;
            //float vy = 0;
            //if (checkResult.rigidbody)
            //{
            //    vx = checkResult.rigidbody.velocity.x;
            //    //vy = CheckResult.rigidbody.velocity.y;
            //}


            if (Mathf.Abs(h) > 0.15f)
            {
                body.velocity = new Vector3(Mathf.Sign(h) * maxSpeed + vx, body.velocity.y, body.velocity.z);
                //    GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(h));
                //     GetComponent<Animator>().speed = Mathf.Abs(h) * 3;
            }
            else
            {
                body.velocity = new Vector3(vx, body.velocity.y, body.velocity.z);
                //   GetComponent<Animator>().SetFloat("Speed", 0);
                //    GetComponent<Animator>().speed = 1;
            }
        }

        //翻转
        if ((h > 0 && !facingRight) || (h < 0 && facingRight))
            Flip();
    }

    //跳跃控制
    void JumpControll()
    {
        //跳跃
        if (jump)
        {
            ////跳跃音效
            //   SoundManager.GetInstance().PlaySoundEffect(SoundManager.Vida_Jump, transform.position);
            //    GetComponent<Animator>().SetTrigger("Jump");
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, body.velocity.z);
            body.AddForce(new Vector3(0, jumpForce, 0));
            jump = false;
        }

    }
}

