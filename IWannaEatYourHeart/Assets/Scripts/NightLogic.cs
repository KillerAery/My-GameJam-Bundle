using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NightLogic : MonoBehaviour
{

    public Animation successPanel;
    public Animation failPanel;

    ThirdPersonULCon con;

    public Animation newRoundUI;
    public Animation endRoundUI;

    public HouseKillTrigger[] houseTriggers;

    public Button nextRoundButton;

    private void Awake()
    {
        Logic.Init();

        con = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonULCon>();


        for (int i = 0; i < houseTriggers.Length; ++i)
        {
            houseTriggers[i].marker = Logic.markers[i];
            houseTriggers[i].index = i;
            houseTriggers[i].TrueIndex = Logic.houseIndexOfModel[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        newRoundUI.Play();
        newRoundUI.GetComponentInChildren<Text>().text = "第" + Logic.round + "天黑夜";
    }

    bool ended = false;
    // Update is called once per frame
    void Update()
    {
        if (!ended && Input.GetKey(KeyCode.Escape))
        {
            ended = true;
            EndRound();
        }
    }

    private bool judged = false;
    public void Judgement(int index)
    {
        judged = true;
        nextRoundButton.gameObject.SetActive(false);
        if (Logic.targetIndex == Logic.houseIndexOfModel[index]) {
            GameSuccess();
        }
        else{
            GameFail();
        }

    }

    public Text timeText;
    public Text roundText;


    public void GameSuccess()
    {
        timeText.text = Logic.totalTime.ToString();
        roundText.text = Logic.round.ToString();

        con.enabled = false;
        Cursor.visible = true;
        successPanel.Play();
    }

    public void GameFail()
    {
        con.enabled = false;
        Cursor.visible = true;
        failPanel.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }

    //----回合提示UI
    public void EndRound()
    {
        //已经判断了，不可结束
        if (judged)
        {
            return;
        }
        //当回合数是最后一回合，不允许退出
        if(Logic.round >= Logic.maxRound)
        {
            return;
        }
        endRoundUI.Play();
        endRoundUI.GetComponentInChildren<Text>().text = "第" + Logic.round + "天黑夜已结束";
        StartCoroutine(ShowMovementPanel());

        Logic.round += 1;
    }

    IEnumerator ShowMovementPanel()
    {
        yield return new WaitForSeconds(5.0f);
        ChangeDayScene();
    }

    public void ChangeDayScene()
    {
        SceneManager.LoadScene("DayScene");
    }
}
