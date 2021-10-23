using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public ProbePanelUI probePanel;
    public bool marker = false;
    public bool[] probeables;
    public bool[] probed;
    public int index;
    public int TrueIndex;

    MarkerTextTrick markerText;

    private void Awake()
    {
        probed = new bool[6];
        for(int i = 0; i < 6; ++i)
        {
            probed[i] = false;
        }
        markerText = GetComponentInChildren<MarkerTextTrick>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        markerText.gameObject.SetActive(marker);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        probePanel.ShowProbePanel(this);
    }


}
