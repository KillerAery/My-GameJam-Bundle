using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEvent1 : TriggerEventInterface
{
    public Trigger trigger2;
    public Speaker speaker;
    public Animator rabbit1;
    public Animator rabbit2;
    public override void EventFinished()
    {
        // 背景响起诡异的嘻嘻，该地图某处出现血迹（换图片），动物偏向值+1
        gameplaySystem.emo -= 1;
        gameplaySystem.SpawnRandomBlood();
    }
    public override void EventW()
    {
        // 兔子看上去平静了许多（直接出现文案，不用图片），人类偏向值 + 1
        gameplaySystem.emo += 1;
        speaker.Speak("兔子看上去平靜了許多");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }
    public override void EventS()
    {
        EventFinished();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventStart()
    {
        trigger2.done = true;
    }
}
