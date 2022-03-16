using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEvent : TriggerEventInterface
{
    public Animator child1;
    public Animator child2;
    public override void EventFinished()
    {
        // 出现贱笑的音效（如果玩家不选择，限定时间一到自动播放音效） （动物偏向值+1）
        gameplaySystem.emo -= 1;

    }

    public override void EventW()
    {
        //小孩恢复了正常（人类偏向值 + 1）
        gameplaySystem.emo += 1;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
        child1.SetTrigger("Happy");
        child2.SetTrigger("Happy");
    }

    public override void EventS()
    {
        EventFinished();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventStart()
    {
        child1.SetTrigger("Normal");
        child2.SetTrigger("Normal");
    }
}
