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
        // 兔子说话：谢谢（文本框），动物偏向值+1
        gameplaySystem.emo -= 1;
        speaker.Speak("\"謝謝\"");
    }
    public override void EventW()
    {
        // 上面兔子：变成黑色后消失（图片），出现对话框"你会是下一个”
        // 里世界兔子：身躯变大（图片变换），人类偏向值+1
        gameplaySystem.emo += 1;
        speaker.Speak("\"你会是下一个\"");
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
