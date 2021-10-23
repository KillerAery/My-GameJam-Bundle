using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBAIDBASI : MonoBehaviour {
    public TextShow ts;
    public AudioSource ass;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            EndScene();
        }
    }

    void EndScene()
    {
        ts.show();
        ass.Play();
    }
}
