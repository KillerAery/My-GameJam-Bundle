using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Debug.Log(hit.point);
                obj.transform.position = hit.point;
            }
        }
	}
}
