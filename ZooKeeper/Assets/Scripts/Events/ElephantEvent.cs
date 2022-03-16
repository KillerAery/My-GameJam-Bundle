using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantEvent : TriggerEventInterface
{
    Animator elephant1;
    Animator elephant2;

    public override void EventFinished()
    {
        // 该园区刷新的游客全部归零（游客渐变消失），地上出现几滩血迹，动物偏向值+5
        gameplaySystem.emo -= 5;
        gameplaySystem.SpawnRandomBlood();
        gameplaySystem.SpawnRandomBlood();
        gameplaySystem.SpawnRandomBlood();
        gameplaySystem.SpawnRandomBlood();
        gameplaySystem.SpawnRandomBlood();
        gameplaySystem.SpawnRandomBlood();
        gameplaySystem.SpawnRandomBlood();
        gameplaySystem.allclear = true;
    }

    public override void EventS()
    {
        EventFinished();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventStart()
    {
        elephant1 = GameObject.Find("Elephant1").GetComponent<Animator>();
        elephant1.SetTrigger("Danger");
    }

    public override void EventW()
    {
        //大象回复原状（图片切换），一切无事发生，人类值+1
        gameplaySystem.emo += 1;
        elephant1.SetTrigger("Normal");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }
}
