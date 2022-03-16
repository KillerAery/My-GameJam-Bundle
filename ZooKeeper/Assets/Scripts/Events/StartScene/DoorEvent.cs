using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEvent : TriggerEventInterface
{
    public override void EventW()
    {
        Debug.Log("Door");
        SceneManager.LoadScene("GameScene0");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventS()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventFinished()
    {
        throw new System.NotImplementedException();
    }

    public override void EventStart()
    {
    }
}
