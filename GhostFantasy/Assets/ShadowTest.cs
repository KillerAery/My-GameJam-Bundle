using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTest : MonoBehaviour {
    public bool available = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Barreir")
        {
            available = false;
        }
    }
}
