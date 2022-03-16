using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantEvent : TriggerEventInterface
{
    Animator elephant1;
    Animator elephant2;

    public override void EventFinished()
    {
        // ��԰��ˢ�µ��ο�ȫ�����㣨�οͽ�����ʧ�������ϳ��ּ�̲Ѫ��������ƫ��ֵ+5
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
        //����ظ�ԭ״��ͼƬ�л�����һ�����·���������ֵ+1
        gameplaySystem.emo += 1;
        elephant1.SetTrigger("Normal");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }
}
