using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyEvent : TriggerEventInterface
{
    public Animator jelly1;
    public Animator jelly2;
    public override void EventFinished()
    {
        // ������ˮĸ΢Ц����ͼƬ����Զ���ƺ���С���ӵļ�У���Ч��������ƫ��ֵ+1
        gameplaySystem.emo -= 1;
        jelly1.SetTrigger("Dark");
        jelly2.SetTrigger("Dark");
    }
    public override void EventW()
    {        
        // ������ˮĸ������������ˮĸ�Ʊ����ã���ͼƬ��������ƫ��ֵ+1
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
