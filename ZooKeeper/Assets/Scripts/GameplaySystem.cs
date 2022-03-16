using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplaySystem : MonoBehaviour
{
    public int currentLevel = 0;
    public float emo = 10f;
    public Slider slider;
    public float inter = 5.0f;
    public float offset = 2.0f;

    public GameObject bloodPrefab;
    public GameObject rabbitPrefab;
    public float minx = -69;
    public float maxx = 47;
    public int eventCount = 0;

    GameObject player;

    // passanger
    public List<GameObject> passangers;
    public bool allclear = false;

    // rabbit
    float rabbitSpawntime = 10.0f;
    public int rabbitCount = 3;

    // Event
    public List<Trigger> triggers;

    // UI
    public GameObject goodEndPanel;
    public GameObject normalEndPanel;
    public GameObject badEndPanel1;
    public GameObject badEndPanel2;
    public GameObject leftWarning;
    public GameObject rightWarning;
    public Text Record;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SelectOneEvent());
        leftWarning.SetActive(false);
        rightWarning.SetActive(false);

        StartCoroutine(RefreshRabbit());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = emo/20.0f;

        // 检查事件警告
        rightWarning.SetActive(false);
        leftWarning.SetActive(false);
        foreach (var trigger in triggers)
        {
            if (!trigger.running || trigger.done) continue;

            float distance = trigger.transform.position.x - player.transform.position.x;
            if (distance > Camera.main.orthographicSize / 2)
            {
                rightWarning.SetActive(true);
            }
            if (distance < -Camera.main.orthographicSize / 2)
            {
                leftWarning.SetActive(true);
            }
        }

        // 兔子过多，会触发全体黑化
        if(rabbitCount >= 20)
        {
            rabbitCount = -1000;
            emo -= 3.0f;

            foreach (var trigger in triggers)
            {
                if (trigger.tag == "Rabbit") {
                    var re1 = trigger.GetComponent<RabbitEvent1>();
                    var re2 = re1.trigger2.GetComponent<RabbitEvent2>();
                    if(!re2.die)
                    {
                        re1.rabbit1.SetTrigger("Revolution");
                        re1.rabbit2.SetTrigger("Revolution");
                    }
                    trigger.done = true;
                }
            }

            player.GetComponent<Speaker>().Speak("兔子全都怎么？！");
        }

        // 游客清理
        if (allclear)
        {
            foreach(var passanger in passangers)
            {
                if (!passanger.activeSelf) continue;

                if (Mathf.Abs(passanger.transform.position.x - player.transform.position.x) >= 12.0f)
                {
                    passanger.SetActive(false);
                }
            }
        }
    }

    public void FinishGame()
    {
        PauseGame();
        if(emo <= 0.0f)
        {
            badEndPanel1.SetActive(true);
        }
        else if (emo >= 20.0f)
        {
            badEndPanel2.SetActive(true);
        }
        else{
            if(emo <= 11.0f && emo >= 9.0f)
            {
                // 平衡大师结局
                goodEndPanel.SetActive(true);
            }
            else
            {
                normalEndPanel.SetActive(true);
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    IEnumerator RefreshRabbit()
    {
        while (true)
        {
            yield return new WaitForSeconds(rabbitSpawntime);
            SpawnRandomRabbit();
            SpawnRandomRabbit();
        }
    }

    IEnumerator SelectOneEvent()
    {
        float randomtime = Random.value * 2 * offset - offset + inter;
        Debug.Log(randomtime.ToString() + "秒后触发事件");
        yield return new WaitForSeconds(randomtime);

        //...
        List<Trigger> runableTriggers = new List<Trigger>();
        foreach (var trigger in triggers)
        {
            if (!trigger.running && !trigger.done)
                runableTriggers.Add(trigger);
        }
        if (runableTriggers.Count>0)
        {
            int index = Random.Range(0, runableTriggers.Count);
            Trigger t = runableTriggers[index];
            //tricky
            if (t.tag == "Rabbit")
            {
                index = Random.Range(0, runableTriggers.Count);
                t = runableTriggers[index];
            }
            
            // 触发事件的提示
            string hint = t.RunOneEvent();
            player.GetComponent<Speaker>().Speak(hint);

            eventCount++;
            Record.text += ("记录"+eventCount.ToString()+"："+hint+"\n");


            Debug.Log("触发事件");
        }

        StartCoroutine(SelectOneEvent());
    }

    public void SpawnRandomBlood()
    {
        float randomx = 0;
        do
        {
            randomx = Random.Range(minx, maxx);
        }
        while (Mathf.Abs(randomx - player.transform.position.x) < 12.0f);

        Vector3 pos1 = new Vector3(randomx,1+Random.Range(-0.1f,0.1f),0);
        Vector3 pos2 = new Vector3(randomx, -5 + Random.Range(-0.1f, 0.1f), 0);
        GameObject.Instantiate(bloodPrefab,pos1,Quaternion.identity);
        GameObject.Instantiate(bloodPrefab,pos2,Quaternion.identity);
    }
    

     public void SpawnRandomRabbit()
    {
        float randomx = 0;
        do
        {
            randomx = Random.Range(minx, maxx);
         }
        while(Mathf.Abs(randomx-player.transform.position.x)< 12.0f);

        Vector3 pos1 = new Vector3(randomx,-0.4f, 0);
        var go1 = GameObject.Instantiate(rabbitPrefab, pos1, Quaternion.identity);
        var t = go1.transform.Find("Trigger").GetComponent<Trigger>();
        triggers.Add(t);
        var ts = go1.transform.Find("IMG").transform;
        int dir = Random.Range(0, 2) * 2 - 1;
        var scale = ts.localScale;
        scale.x *= dir;
        ts.localScale = scale;
        rabbitCount++;
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene("StartScene");
    }
}
