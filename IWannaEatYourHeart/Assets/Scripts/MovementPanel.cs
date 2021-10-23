using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementPanel : MonoBehaviour
{
    public Text text;

    public Toggle toggle;


    NightLogic nightLogic;
    new Animation animation;
    int targetIndex = 6;


    public ThirdPersonULCon thirdPersonULCon;



    private void Awake()
    {
        nightLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<NightLogic>();
        animation = GetComponent<Animation>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowProbePanel(HouseKillTrigger houseKillTrigger, int index)
    {
        toggle.isOn = houseKillTrigger.marker;
        thirdPersonULCon.enabled = false;
        Cursor.visible = true;
        text.text = (1+index).ToString();
        targetIndex = index;
        animation.Play("ShowMovement");
    }

    public void HideProbePanel()
    {
        thirdPersonULCon.enabled = true;
        Cursor.visible = false;

        animation.Play("EndMovement");
    }

    public void Judge(){
        nightLogic.Judgement(targetIndex);
    }
}
