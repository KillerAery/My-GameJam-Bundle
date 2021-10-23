using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProbeCountUI : MonoBehaviour
{
    public Logic logic;
    public Button button;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = logic.probeCount.ToString();

        button.gameObject.SetActive(logic.probeCount <= 0);
    }
}
