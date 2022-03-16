using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeEvent : TriggerEventInterface
{
    public override void EventFinished()
    {
        // ��Ч�����Ͱ���
        GetComponent<Speaker>().Speak("\"���Ͱ���\"");
    }

    public override void EventS()
    {
        // BGMͻȻ���ټӿ죬���Ǹ��������ֶ�ħ�����ӣ�����ͼƬ��
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventStart()
    {
    }

    public override void EventW()
    {
        EventFinished();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }
}
