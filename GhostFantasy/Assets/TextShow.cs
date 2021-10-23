using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour {
    public TextMesh text;
    public string[] ComStringList;
    public string comString;
	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void show()
    {
        StartCoroutine("ShowText");
    }
    IEnumerator ShowText()
    {
        foreach (string str in ComStringList)
        {
            comString += str;
            text.text = comString;
            yield return new WaitForSeconds(0.2f);
        }
        comString = null;
    }
}
