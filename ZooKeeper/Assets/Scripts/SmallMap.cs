using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallMap : MonoBehaviour
{
    public GameObject player;
    public GameObject map;
    public Slider slider;
    public float minx;
    public float maxx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = player.transform.position.x;
        slider.value = (x-minx)/(maxx-minx);
    }
}
