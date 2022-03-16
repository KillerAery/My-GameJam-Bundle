using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    GameObject player;
    public bool running = false;    // 是否触发
    public bool done = false;       // 是否完成
    public GameObject operatorHint; // 操作提示
    public GameObject warningHint;  // 警告提示
    public float eventTime = 1000000000f;   //事件持续事件
    public float triggerWidth = 1.6f;
    float remainTime;

    public bool runInstantly = true;

    TriggerEventInterface[] triggerEventList;
    TriggerEventInterface triggerEvent;

    public TextMesh HintW;
    public TextMesh HintS;

    public bool IsStartScene = false;

    // Start is called before the first frame update
    void Start()
    {
        remainTime = eventTime;
        triggerEventList = GetComponents<TriggerEventInterface>();
        operatorHint.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        if (runInstantly)
        {
            RunOneEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!running || done) return;

        remainTime -= Time.deltaTime;
        if(remainTime <= 0 && triggerEvent.hasEventFinished)
        {
            triggerEvent.EventFinished();
            operatorHint.SetActive(false);
            remainTime = eventTime;
            done = true;
            return;
        }

        float minx = transform.position.x- triggerWidth / 2;
        float maxx = transform.position.x+ triggerWidth / 2;

        if(player.transform.position.x >=minx && player.transform.position.x <= maxx)
        {
            if(operatorHint != null)
                operatorHint.SetActive(true);

            if (Input.GetKeyDown(KeyCode.W) && triggerEvent.hasEventW)
            {
                triggerEvent.EventW();
                if (!IsStartScene)
                {
                    done = true;
                    operatorHint.SetActive(false);
                    remainTime = eventTime;
                }
            }
            else if(Input.GetKeyDown(KeyCode.S) && triggerEvent.hasEventS)
            {
                triggerEvent.EventS(); 
                if (!IsStartScene)
                {
                    done = true;
                    operatorHint.SetActive(false);
                    remainTime = eventTime;
                }
            }
        }
        else
        {
            if (operatorHint != null)
                operatorHint.SetActive(false);
        }
    }

    // 返还提示字符
    public string RunOneEvent()
    {
        running = true;
        int index = Random.Range(0,triggerEventList.Length);
        triggerEvent = triggerEventList[index];
        triggerEvent.EventStart();
        HintW.text = triggerEvent.hintW;
        HintS.text = triggerEvent.hintS;

        return triggerEvent.hint;
    }
}
