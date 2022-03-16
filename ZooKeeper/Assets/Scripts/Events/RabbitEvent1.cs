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
        // �������������������õ�ͼĳ������Ѫ������ͼƬ��������ƫ��ֵ+1
        gameplaySystem.emo -= 1;
        gameplaySystem.SpawnRandomBlood();
    }
    public override void EventW()
    {
        // ���ӿ���ȥƽ������ֱࣨ�ӳ����İ�������ͼƬ��������ƫ��ֵ + 1
        gameplaySystem.emo += 1;
        speaker.Speak("���ӿ���ȥƽ�o���S��");
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
