using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour {
    public Camera camera;
    public Vector3 nextCameraPosition;
    public Vector3 OriginCameraPos;
    public GameObject nextScene;
    public GameObject thisScene;
    public ClipCtrl clipCtrl;
    public float AnimationSpeed = 5f;
    // Use this for initialization
    void Start () {
        
        OriginCameraPos = camera.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            
            EnterNextScene();
            
        }
    }
    void EnterNextScene()
    {
        nextScene.SetActive(true);
        StartCoroutine("ChangeScene");
    }
    IEnumerator ChangeScene()
    {
        GameObject.FindGameObjectWithTag("LevelUp").GetComponent<AudioSource>().Play();
        Vector3 vec = nextCameraPosition - camera.transform.position;
        while (Mathf.Abs(Vector3.Distance(camera.transform.position, nextCameraPosition)) > 1)
        {
            camera.transform.position += Time.deltaTime * vec * AnimationSpeed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        camera.transform.position = nextCameraPosition;
        clipCtrl = GameObject.FindGameObjectWithTag("ClipCtrl").GetComponent<ClipCtrl>();
        clipCtrl.SetGhostAppearState(false);
        thisScene.SetActive(false);
    }
}
