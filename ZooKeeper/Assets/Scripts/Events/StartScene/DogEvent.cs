using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEvent : TriggerEventInterface
{
    public GameObject dogPage;
    public override void EventW()
    {
        dogPage.SetActive(true);
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic(0);
    }

    public override void EventS()
    {
        dogPage.SetActive(false);
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
