using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookEvent : TriggerEventInterface
{
    public GameObject bookPage;
    public override void EventW()
    {
        bookPage.SetActive(true);
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventS()
    {
        bookPage.SetActive(false);
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
