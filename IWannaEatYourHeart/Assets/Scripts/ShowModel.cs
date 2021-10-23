using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowModel : MonoBehaviour
{
    public float rotatespeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private Vector3 axis = new Vector3(0,1,0);
    void Update()
    {
        transform.Rotate(axis, rotatespeed*Time.deltaTime);
    }
}
