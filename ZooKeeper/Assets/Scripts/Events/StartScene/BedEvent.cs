using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedEvent : TriggerEventInterface
{
    public override void EventW()
    {
        Debug.Log("Bed");
        Application.Quit();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventS()
    {
        throw new System.NotImplementedException();
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
