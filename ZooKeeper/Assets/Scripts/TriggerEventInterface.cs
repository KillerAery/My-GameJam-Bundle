using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerEventInterface : MonoBehaviour
{
    public string hint = "";
    public string hintW = "";
    public string hintS = "";

    public bool hasEventW = true;
    public bool hasEventS = true;
    public bool hasEventFinished = true;

    protected GameplaySystem gameplaySystem = null;
    public void Start()
    {
        var o = GameObject.FindGameObjectWithTag("System");
        if (o != null)
        {
            gameplaySystem = o.GetComponent<GameplaySystem>();
        }
    }
    public abstract void EventStart();
    public abstract void EventW();

    public abstract void EventS();
    public abstract void EventFinished();
}
