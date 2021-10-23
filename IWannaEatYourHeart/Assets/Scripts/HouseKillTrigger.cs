using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseKillTrigger : MonoBehaviour
{
    public int index;
    private NightLogic nightLogic;
    public MovementPanel movementPanel;
    public bool marker = false;
    public int TrueIndex;



    MarkerTextTrick markerText;

    private void Awake()
    {
        nightLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<NightLogic>();

        marker = Logic.markers[index];

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
        movementPanel.ShowProbePanel(this,index);
    }


}
