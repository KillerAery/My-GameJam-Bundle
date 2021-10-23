using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonSkyCon : MonoBehaviour
{
	//记录---------------------------------------------------------------
	Vector3 movetoPos = Vector3.zero;//当前移动方向
	public float Speed = 1.0f;//当前速度
	CharacterController controller;

	public Transform cmabaseTrans;

	public GameObject mycamera;

	public Transform body;
	//内部调用-----------------------------------------------------------
	Vector3 moveVector;
	Vector3 baseDir;
	Vector3 lookDir;
	Quaternion now;
	Quaternion lookto;
	void speedUpdateSet()//在Update中设置速度
	{
		movetoPos.x = Input.GetAxisRaw("Horizontal");
		movetoPos.y = 0.0f;
		movetoPos.z = Input.GetAxisRaw("Vertical");

		moveVector= movetoPos * Speed * Time.deltaTime * 100.0f;
		controller.SimpleMove(moveVector);

		if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
			baseDir = movetoPos;
			cmabaseTrans.position = new Vector3(mycamera.transform.position.x, gameObject.transform.position.y, mycamera.transform.position.z);
			cmabaseTrans.LookAt(gameObject.transform);

			lookDir = cmabaseTrans.TransformVector(baseDir);
			now = body.rotation;
			lookto = Quaternion.LookRotation(lookDir, Vector3.up);

			body.rotation = Quaternion.RotateTowards(now, lookto, 270 * Time.deltaTime);
		}


	}
	//Behaviour----------------------------------------------------------
	// Start is called before the first frame update
	void Start()
    {
		controller = gameObject.GetComponent<CharacterController>();
		if (!mycamera)
		{
			mycamera = Camera.main.gameObject;
		}
		if (!cmabaseTrans)
		{
			cmabaseTrans = gameObject.transform.Find("cmabaseTrans");
		}
		if (!body)
		{
			body = gameObject.transform.Find("Body");
		}
	}

    // Update is called once per frame
    void Update()
    {
		speedUpdateSet();//速度设置
	}
}
