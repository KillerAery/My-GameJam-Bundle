using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HowDefendCoverLook
{
	skyLook,//unity官方5点SkyLook处理
	Transparency//透明化处理
}
public class CameraMovement : MonoBehaviour
{
	public float smooth = 1.5f;
	public Transform m_player;
	private Transform m_transform;
	private Vector3 relCameraPos;//player到摄像机的向量
	private float relCameraPosMag;//向量的长度
	private Vector3 newPos;//循环变量
	public HowDefendCoverLook howDefend=HowDefendCoverLook.skyLook;

	void Awake()
	{
		//m_player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>();
		m_transform = GetComponent<Transform>();
		relCameraPos = m_transform.position - m_player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;

		switch (howDefend)
		{
			case HowDefendCoverLook.skyLook:
				checkPoints = new Vector3[5];//检查点
				break;
			case HowDefendCoverLook.Transparency:
				onCmaCollision = new List<GameObject>();
				onRemoveCollision = new List<GameObject>();
				mask = 1 << (LayerMask.NameToLayer("Collision"));
				mask2 = 1 << (LayerMask.NameToLayer("out"));
				mask3 = 1 << (LayerMask.NameToLayer("check"));
				break;
		}
		

	}

	private void LateUpdate()
	{
		standardPos = m_player.position + relCameraPos;//锁死情况下Cma的当前(Player移动后)的位置
		switch (howDefend)
		{
			case HowDefendCoverLook.skyLook:
				skyLookUpdate();
				break;
			case HowDefendCoverLook.Transparency:
				m_transform.position = Vector3.Lerp(m_transform.position, standardPos, smooth * Time.deltaTime);
				SmoothLookAt();
				shelterCheckUpdate();
				break;
		}
		
	}
	Vector3 standardPos;
	Vector3 abovePos;
	Vector3[] checkPoints;
//SkyLook----------------------------------------------------------
	void skyLookUpdate()
	{
		
		abovePos = m_player.position + Vector3.up * relCameraPosMag;//Player正上方同初始Cma到Player距离的位置
		
											   //根据百分比分割并计算了abovePos 到 standardPos 间包括这两个点在内的5个点(5点共弧) 
		checkPoints[0] = standardPos;
		checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
		checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
		checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
		checkPoints[4] = abovePos;
		for (int i = 0; i < checkPoints.Length; i++)
		{
			if (ViewingPositionCheck(checkPoints[i]))//这里改变了检测值newPos
			{
				break;//检测到了就跳出
			}
		}
		m_transform.position = Vector3.Lerp(m_transform.position, newPos, smooth * Time.deltaTime);//平滑的将摄像机位置过度到需要的位置
																								   //先改变位置在改变朝向
		SmoothLookAt();
	}
	bool ViewingPositionCheck(Vector3 checkPos)
	{
		RaycastHit hit;
		//从检查点向Player所在方向做射线检测 距离是初始Cma到Player距离减去0.5f
		if (Physics.Raycast(checkPos, m_player.position - checkPos, out hit, relCameraPosMag))
		{
			if (hit.transform != m_player)//发现中间有障碍
			{
				return false;
			}
		}
		//中间没有障碍
		newPos = checkPos;//记录newPos
		return true;
	}
	Vector3 relPlayerPosition;
	Quaternion lookAtRotation;
	void SmoothLookAt()
	{
		relPlayerPosition = m_player.position - m_transform.position;
		lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
		//计算从自身z轴对齐relPlayerPosition，y轴对齐Vector3.up的四元数
		m_transform.rotation = Quaternion.Lerp(m_transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}
//Transparency-----------------------------------------------------
	Vector3 basePos0;
	Vector3 basePos1;
	Vector3 basePos2;
	Vector3 basePos3;
	Vector3 basePos4;
	Vector3 basePos5;
	Vector3 basePos6;
	Vector3 cmaPos;
	Ray baser0;
	Ray baser1;
	Ray baser2;
	Ray baser3;
	Ray baser4;
	Ray baser5;
	Ray baser6;
	GameObject onCmaObj;
	List<GameObject> onCmaCollision;
	List<GameObject> onRemoveCollision;
	LayerMask mask;
	LayerMask mask2;
	LayerMask mask3;
	RaycastHit onrcHit;
	Color lastColor;
	void shelterCheckUpdate()//遮挡透明化处理
	{
		basePos0 = m_player.position;
		basePos1 = new Vector3(m_player.position.x, m_player.position.y + 1f, m_player.position.z);
		basePos2 = new Vector3(m_player.position.x, m_player.position.y - 0.8f, m_player.position.z);
		basePos3 = new Vector3(m_player.position.x, m_player.position.y + 2f, m_player.position.z);
		basePos4 = new Vector3(m_player.position.x, m_player.position.y + 1.5f, m_player.position.z);
		basePos5 = new Vector3(m_player.position.x + 0.5f, m_player.position.y + 1.5f, m_player.position.z);
		basePos6 = new Vector3(m_player.position.x - 0.5f, m_player.position.y + 1.5f, m_player.position.z);

		cmaPos = gameObject.transform.position;

		baser0 = new Ray(cmaPos, basePos0 - cmaPos);
		baser1 = new Ray(cmaPos, basePos1 - cmaPos);
		baser2 = new Ray(cmaPos, basePos2 - cmaPos);
		baser3 = new Ray(cmaPos, basePos3 - cmaPos);
		baser4 = new Ray(cmaPos, basePos4 - cmaPos);
		baser5 = new Ray(cmaPos, basePos5 - cmaPos);
		baser6 = new Ray(cmaPos, basePos6 - cmaPos);
		//checkOnCmaCollision
		for (int i = 0; i < onCmaCollision.Count; i++)
		{
			onCmaObj = onCmaCollision[i];
			if (null == onCmaObj)
			{
				onRemoveCollision.Add(onCmaObj);
				continue;
			}
			onCmaObj.layer = 11;
			if (Physics.Raycast(baser0, out onrcHit, Vector3.Distance(cmaPos, basePos0), mask3))
			{
				onCmaObj.layer = 10;
				continue;
			}
			if (Physics.Raycast(baser1, out onrcHit, Vector3.Distance(cmaPos, basePos1), mask3))
			{
				onCmaObj.layer = 10;
				continue;
			}
			if (Physics.Raycast(baser2, out onrcHit, Vector3.Distance(cmaPos, basePos2), mask3))
			{
				onCmaObj.layer = 10;
				continue;
			}
			if (Physics.Raycast(baser3, out onrcHit, Vector3.Distance(cmaPos, basePos3), mask3))
			{
				onCmaObj.layer = 10;
				continue;
			}
			if (Physics.Raycast(baser4, out onrcHit, Vector3.Distance(cmaPos, basePos4), mask3))
			{
				onCmaObj.layer = 10;
				continue;
			}
			if (Physics.Raycast(baser5, out onrcHit, Vector3.Distance(cmaPos, basePos5), mask3))
			{
				onCmaObj.layer = 10;
				continue;
			}
			if (Physics.Raycast(baser6, out onrcHit, Vector3.Distance(cmaPos, basePos6), mask3))
			{
				onCmaObj.layer = 10;
				continue;
			}

			onCmaObj.layer = 9;
			onRemoveCollision.Add(onCmaObj);
		}
		for (int i = 0; i < onRemoveCollision.Count; i++)
		{
			onCmaObj = onRemoveCollision[i];
			if (null == onCmaObj)
			{
				onCmaCollision.Remove(onCmaObj);
				continue;
			}
			lastColor = onCmaObj.GetComponent<MeshRenderer>().material.color;
			onCmaObj.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Diffuse");
			onCmaObj.GetComponent<MeshRenderer>().material.color = new Color(lastColor.r, lastColor.g, lastColor.b, 1f);
			onCmaCollision.Remove(onCmaObj);
		}
		onRemoveCollision.Clear();

		//addin
		checkCmaCollision(baser0, cmaPos, basePos0);
		checkCmaCollision(baser1, cmaPos, basePos1);
		checkCmaCollision(baser2, cmaPos, basePos2);
		checkCmaCollision(baser3, cmaPos, basePos3);
		checkCmaCollision(baser4, cmaPos, basePos4);
		checkCmaCollision(baser5, cmaPos, basePos5);
		checkCmaCollision(baser6, cmaPos, basePos6);
	}
	void checkCmaCollision(Ray baser0, Vector3 cmaPos, Vector3 basePos0)//检测是否产生遮挡
	{
		for (; ; )
		{
			if (Physics.Raycast(baser0, out onrcHit, Vector3.Distance(cmaPos, basePos0), mask))
			{
				if (onrcHit.collider.gameObject.name == "Plane")
				{
					break;
				}
				objAddToOnCmaCollision(onrcHit.collider.gameObject);
			}
			else
			{
				break;
			}
		}

	}
	void objAddToOnCmaCollision(GameObject obj)//添加遮挡物体
	{
		obj.layer = 10;
		Material objm = obj.GetComponent<MeshRenderer>().material;
		lastColor = objm.color;
		objm.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
		objm.color = new Color(lastColor.r, lastColor.g, lastColor.b, 0.3f);
		onCmaCollision.Add(obj);
	}

}
