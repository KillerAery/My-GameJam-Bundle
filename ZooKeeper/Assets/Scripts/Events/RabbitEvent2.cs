using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEvent2 : TriggerEventInterface
{
    public Animator rabbit1;
    public Animator rabbit2;
    public Speaker speaker;
    public bool die = false;
    public Trigger trigger1;

    public override void EventFinished()
    {
        // ����˵����лл���ı��򣩣�����ƫ��ֵ+1
        gameplaySystem.emo -= 1;
        speaker.Speak("\"�x�x\"");
    }
    public override void EventW()
    {
        // �������ӣ���ɺ�ɫ����ʧ��ͼƬ�������ֶԻ���"�������һ����
        // ���������ӣ��������ͼƬ�任��������ƫ��ֵ+1
        gameplaySystem.emo += 1;
        speaker.Speak("\"�������һ��\"");
        die = true;

        trigger1.done = true;

        rabbit1.SetTrigger("Black");
        rabbit2.SetTrigger("Big");
        gameplaySystem.rabbitCount--;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventS()
    {
        EventFinished();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventStart()
    {

    }
}
