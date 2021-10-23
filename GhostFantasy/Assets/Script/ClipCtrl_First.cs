using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipCtrl_First : MonoBehaviour {
    public GameObject clickObject;
    public TextMesh text;
    private GameObject MoveSupport;
    private GameObject wall;
    bool clickingClip = false;
    public float DefaultHight = 1f;
    public float Speed = 1f;
    public float CanvasDeep = -3.8f;
    public float CameraHeight = 8f;
    public float RotateSpeed = 90f;
    private float ClipHeight;
    public GameObject flashLight;
    //幽灵对象
    public GameObject ghost;
    bool ghostAppear = false;
    //影子遮盖物
    public GameObject shadowObject;

    // Use this for initialization
    void Start()
    {
        MoveSupport = GameObject.FindGameObjectWithTag("MoveSupport");
        wall = GameObject.FindGameObjectWithTag("wall");
        flashLight = GameObject.FindGameObjectWithTag("FlashLight");
        ClipHeight = DefaultHight;
        clickingClip = false;
    }

    // Update is called once per frame
    void Update()
    {
        /////////鼠标按下处理
        if (Input.GetMouseButtonDown(0) && !clickingClip)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                clickObject = hit.collider.gameObject;
                //如果点到幽灵，则溶解幽灵，进入控制拼图阶段
                if (clickObject.tag == "Player")
                {
                    //幽灵消失
                    ghost.transform.localEulerAngles = Vector3.zero;//角度复原
                    ghost.SetActive(false);
                    ghost.GetComponent<BoxCollider>().enabled = false;
                    ghostAppear = false;
                }
                //如果点到影子，则生成幽灵，进入幽灵控制阶段
                else if (clickObject.tag == "Shadow")
                {

                    ghost.SetActive(true);
                    ghost.transform.localPosition = Vector3.zero;
                    ghost.transform.localEulerAngles = Vector3.zero;//角度复原
                    ghost.GetComponent<BoxCollider>().enabled = true;
                    ghost.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ghostAppear = true;
                    gameObject.GetComponent<AudioSource>().Play();
                    StartCoroutine("GhostRotate");
                    text.GetComponent<TextShow>().show();
                }
                else if (clickObject.tag == "clip")
                {
                    Vector3 clipPos = clickObject.transform.position;
                    MoveSupport.transform.position = new Vector3(clipPos.x, clipPos.y - 0.5f, clipPos.z);
                    ClipHeight = clickObject.transform.position.y;
                }
            }
        }
        //鼠标按住时操作
        if (Input.GetMouseButton(0) && !ghostAppear)
        {
            if (clickObject != null && clickObject.tag == "clip")
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    if (clickObject.tag == "clip")//拼图
                    {
                        //正在拖拽 拼图
                        clickingClip = true;
                        Vector3 dir = new Vector3(hit.point.x, ClipHeight, hit.point.z);
                        clickObject.transform.position = dir;
                    }
                }
            }
        }
        //鼠标松开时操作
        else if (Input.GetMouseButtonUp(0))
        {
            //没有拖拽拼图
            clickingClip = false;
            MoveSupport.transform.position = wall.transform.position;
        }
        //////////按键处理
        if (Input.anyKey)
        {
            float x = 0, y = 0;
            //前后移动
            if (Input.GetKey(KeyCode.W))
            {
                y += Speed * Time.deltaTime;
                ClipHeight += Speed * Time.deltaTime;
            }

            else
                if (Input.GetKey(KeyCode.S))
            {
                y -= Speed * Time.deltaTime;
                ClipHeight -= Speed * Time.deltaTime;
            }

            //ClipHeight = ClipHeight > CanvasDeep ? ClipHeight : CanvasDeep;
            //ClipHeight = ClipHeight < CameraHeight ? ClipHeight : CameraHeight;
            //幽灵出现时操控模式
            if (ghostAppear)
            {
                return;
            }
            ////拼图时操控模式
            //else if (clickObject != null && clickObject.tag == "clip")
            //{
            //    Vector3 clipPos = clickObject.transform.position;
            //    MoveSupport.transform.position = new Vector3(clipPos.x, clipPos.y - 0.5f, clipPos.z);
            //    clickObject.transform.position = new Vector3(clipPos.x, clipPos.y + y, clipPos.z);
            //}
            ////光源操作模式
            //else if (flashLight != null)
            //{
            //    Vector3 lightPos = flashLight.transform.position;
            //    flashLight.transform.position = new Vector3(lightPos.x, lightPos.y + y, lightPos.z);
            //}
        }
    }

    IEnumerator GhostRotate()
    {
        while (Mathf.Abs(ghost.transform.localEulerAngles.y - 49.8f) > 2)
        {
            ghost.transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ghost.transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
