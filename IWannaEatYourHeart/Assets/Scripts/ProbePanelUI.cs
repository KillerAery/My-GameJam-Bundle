using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProbePanelUI : MonoBehaviour
{
    public Toggle toggle;
    public Button[] buttons;

    public Logic logic;

    private HouseTrigger houseTrigger = null;

    public ThirdPersonULCon thirdPersonULCon;

    public ModelPointManager modelManager;

    public GameObject[] modelPrefabs;

    public Text text;

    new Animation animation;
    private void Awake()
    {
        animation = GetComponent<Animation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TRICK NEVER MIND
        if (houseTrigger)
        {
            Cursor.visible = true;
        }

        if(showing && Input.GetKey(KeyCode.Escape))
        {
            HideProbePanel();
        }
    }


    private bool showing = false;
    public void ShowProbePanel(HouseTrigger house)
    {
        showing = true;
        thirdPersonULCon.enabled = false;
        text.text = (house.index+1).ToString();

        modelManager.ChangeModel(modelPrefabs[house.TrueIndex]);
        houseTrigger = house;
        for(int i = 0; i < houseTrigger.probeables.Length; ++i)
        {
            //提供选项
            if(houseTrigger.probeables[i] == false)
            {
                buttons[i].gameObject.SetActive(false);
            }
            else
            {
                buttons[i].gameObject.SetActive(true);
            }
            //显示已探测过
            if (houseTrigger.probed[i])
            {
                buttons[i].GetComponentInChildren<RawImage>(true).gameObject.SetActive(true);
            }
            else
            {
                buttons[i].GetComponentInChildren<RawImage>(true).gameObject.SetActive(false);
            }
        }

        animation.Play("ShowUI");
        Cursor.visible = true;
        toggle.isOn = houseTrigger.marker;
    }

    public void HideProbePanel()
    {
        showing = false;
        thirdPersonULCon.enabled = true;

        houseTrigger = null;
        animation.Play("HideUI");
        Cursor.visible = false;
    }

    public void ChangeMarker(bool ifMarker)
    {
        if (houseTrigger)
        {
            logic.UpdateMaker(houseTrigger, ifMarker);
        }
    }


    public GameObject jumpText;
    public void Probe(int index)
    {
        //已经探测过
        if (houseTrigger.probed[index])
        {
            return;
        }

        //探测次数不够
        if(logic.probeCount <= 0)
        {
            return;
        }

        logic.probeCount--;
        houseTrigger.probed[index] = true;

        Instantiate(jumpText,buttons[index].transform);
        buttons[index].GetComponentInChildren<RawImage>(true).gameObject.SetActive(true);
    }
}
