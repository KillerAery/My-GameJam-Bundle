using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeEvent : TriggerEventInterface
{
    public override void EventFinished()
    {
        // 音效：阿巴阿巴
        GetComponent<Speaker>().Speak("\"阿巴阿巴\"");
    }

    public override void EventS()
    {
        // BGM突然变速加快，主角副本身后出现恶魔大兔子（闪现图片）
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
