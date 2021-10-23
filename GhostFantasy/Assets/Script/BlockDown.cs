using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDown : MonoBehaviour {
    public GameObject[] blockList;
    public GameObject Ball;
    public float blockHeight = 0.1f;
    public float GroundHeight = -3.27f;
    public float cameraHeight = 9f;
    public float DownRaiseSpeed = 10f;
    public float ScaleSpeed = 2f;
	// Use this for initialization
	void Start () {
        Raise();//初始化时归位

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Down()
    {
        StartCoroutine("DownAction");
    }
    public void Raise()
    {
        StartCoroutine("RaiseAction");
    }

    IEnumerator DownAction()
    {
        StopCoroutine("RaiseAction");
        foreach (GameObject block in blockList)
        {
            Vector3 pos = block.transform.position;
            block.GetComponent<PlayerCheck>().SetIsTopState(false);
            while (block.transform.position.y > GroundHeight - blockHeight / 2)
            {
                block.transform.position -= new Vector3(0, DownRaiseSpeed * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            block.GetComponent<PlayerCheck>().SetIsBottomState(true);
            block.transform.position = new Vector3(pos.x, GroundHeight - blockHeight / 2 - 0.01f, pos.z);
        }
        Ball.SetActive(true);
        while (Ball.transform.localScale.x < 2)
        {
            Ball.transform.localScale += new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed) * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Ball.transform.localScale = new Vector3(2, 2, 2);
    }

    IEnumerator RaiseAction()
    {
        StopCoroutine("DownAction");
        foreach (GameObject block in blockList)
        {
            Vector3 pos = block.transform.position;
            block.GetComponent<PlayerCheck>().SetIsBottomState(false);
            while (block.transform.position.y < cameraHeight)
            {
                block.transform.position += new Vector3(0, DownRaiseSpeed * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            block.GetComponent<PlayerCheck>().SetIsTopState(true);
            block.transform.position = new Vector3(pos.x, cameraHeight, pos.z);
        }
        
    }
}
