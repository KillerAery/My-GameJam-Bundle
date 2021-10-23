using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour {
    bool isBottom = false;
    bool isTop = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetIsBottomState(bool state)
    {
        isBottom = state;
    }
    public void SetIsTopState(bool state)
    {
        isTop = state;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !isBottom)
        {
            GameObject.FindGameObjectWithTag("ClipCtrl").GetComponent<ClipCtrl>().HideGhost();
            GetComponent<AudioSource>().Play();
        }
    }
    private void OnCollisionStay(Collision other)
    {
        Debug.Log("SSSSSSSSSShit");
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("ClipCtrl").GetComponent<ClipCtrl>().HideGhost();
            GetComponent<AudioSource>().Play();
        }
            
    }
}
