using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    //-----跨场景数据
    static public int round = 1;

    static public int targetIndex;

    static public int[] houseIndexOfModel = new int[9];

    static public float totalTime = 0.0f;

    static public bool[] markers = new bool[9];

    static bool inited = false;

    static public void Init()
    {
        if (inited)
        {
            return;
        }
        inited = true;
        targetIndex = 0;
        //先列好序列
        for(int i = 0;i<houseIndexOfModel.Length ;++i)
        {
            houseIndexOfModel[i] = i;
        }
        //随机交换
        for(int i = 0; i < houseIndexOfModel.Length * 5; ++i)
        {
            int a = Random.Range(0, 9);
            int b = Random.Range(0, 9);
            int t = houseIndexOfModel[a];
            houseIndexOfModel[a] = houseIndexOfModel[b];
            houseIndexOfModel[b] = t;
        }
        //初始化标记
        for(int i = 0; i < markers.Length; ++i)
        {
            markers[i] = false;
        }


    }


    //------
    static public int maxRound = 999;
    [HideInInspector]
    public int probeCount = 0;
    public int maxProbeCount = 3;

    public float timelimit = 10.0f;
    [HideInInspector]
    public float timer = 0.0f;

    public CharacterController player;
    public Transform spawnPoint;
    
    public Animation newRoundUI;
    public Animation endRoundUI;
    //public Animation movementPanel;
    public Slider timeLimitSlider;

    public HouseTrigger[] houseTriggers;

    private void Awake()
    {
        Logic.Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetRound();
    }


    private bool alreadyEnd = false;
    // Update is called once per frame
    void Update()
    {
        if (alreadyEnd) { return; }
        totalTime += Time.deltaTime;
        timer += Time.deltaTime;
        if(timer >= timelimit && !pauseTimer)
        {
            EndRound();
        }
        timeLimitSlider.value =  (timelimit-timer) / timelimit;

        //跳过白天
        if(probeCount <= 0 && Input.GetKey(KeyCode.Escape))
        {
            EndRound();
        }
    }


    public void ResetRound()
    {
        //TODO
        player.transform.position = spawnPoint.position;

        timer = 0;
        probeCount = maxProbeCount;

        for(int i = 0; i< houseTriggers.Length; ++i)
        {
            houseTriggers[i].marker = Logic.markers[i];
            houseTriggers[i].index = i;
            houseTriggers[i].TrueIndex = Logic.houseIndexOfModel[i];
            for (int j =0;j<6; ++j)
            {
                houseTriggers[i].probed[j] = false;

            }
        }

        newRoundUI.Play();
        newRoundUI.GetComponentInChildren<Text>().text = "第"+round+"天白天";
        
    }

    private bool pauseTimer = false;
    public void EndRound()
    {
        alreadyEnd = true;
        timer = timelimit;
        pauseTimer = true;
        endRoundUI.Play();
        endRoundUI.GetComponentInChildren<Text>().text = "第" + round + "天白天已结束";
        StartCoroutine(ShowMovementPanel());
    }

    IEnumerator ShowMovementPanel(){
        yield return new WaitForSeconds(5.0f);
        ChangeNightScene();
        //movementPanel.Play("ShowMovement");
    }

    public void ChangeNightScene()
    {
        SceneManager.LoadScene("NightScene");
    }

    public void UpdateMaker(HouseTrigger trigger,bool isMarker)
    {
        trigger.marker = isMarker;
        for(int i = 0;i < houseTriggers.Length; ++i)
        {
            if(houseTriggers[i] == trigger)
            {
                Logic.markers[i] = isMarker;
            }
        }

    }

}
