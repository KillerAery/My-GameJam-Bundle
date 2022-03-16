using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyEvent : TriggerEventInterface
{
    public Animator jelly1;
    public Animator jelly2;
    public override void EventFinished()
    {
        // 表世界水母微笑（换图片），远处似乎有小孩子的尖叫（音效），动物偏向值+1
        gameplaySystem.emo -= 1;
        jelly1.SetTrigger("Dark");
        jelly2.SetTrigger("Dark");
    }
    public override void EventW()
    {        
        // 表世界水母灯亮，里世界水母灯变骷髅（换图片），人类偏向值+1
        gameplaySystem.emo += 1;
        jelly1.SetTrigger("Light");
        jelly2.SetTrigger("Skull");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventS()
    {
        EventFinished();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventStart()
    {
        jelly1.SetTrigger("Dark");
        jelly2.SetTrigger("Dark");
    }
}
