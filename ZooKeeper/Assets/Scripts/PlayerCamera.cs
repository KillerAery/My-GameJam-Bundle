using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float minx = -61;
    public float maxx  = 43;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        pos.x = Mathf.Clamp(pos.x, minx, maxx);
        transform.position = pos;
    }
}
