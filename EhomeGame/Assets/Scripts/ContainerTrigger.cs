﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTrigger : MonoBehaviour
{
    //要触发的话
    public string words;
    private TextManager textManager;
    // Start is called before the first frame update
    void Start()
    {
        textManager = GameObject.FindGameObjectWithTag("TextManager").GetComponent<TextManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
                textManager.ShowText(words);
        }
    }

}
