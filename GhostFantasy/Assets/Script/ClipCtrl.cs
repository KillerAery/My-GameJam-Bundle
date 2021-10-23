using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipCtrl : MonoBehaviour {
    public GameObject clickObject;
    public TextShow ts;
    private GameObject MoveSupport;
    private GameObject wall;
    private GameObject[] clipList;//拼图碎片列表
    bool clickingClip = false;
    public float DefaultHight = 1f;
    public float Speed = 1f;
    public float CanvasDeep = -3.27f;
    public float CameraHeight = 8f;
    private float ClipHeight;
    public float LightHeight = 15.87f;
    //地板Y值
    public float groundY = 0.0f;
    public GameObject flashLight;
    //幽灵对象
    public GameObject ghost;
    bool ghostAppear = false;
    //影子遮盖物
    public GameObject shadowObject;
    //真正正确的拼图
    public GameObject[] trueClip;
    //3个检测拼图相对位置向量(第1和2，2和3个拼图)
    public Vector3[] relativePosCheck = new Vector3[3];

    // Use this for initialization
    void Start() {
        MoveSupport = GameObject.FindGameObjectWithTag("MoveSupport");
        wall = GameObject.FindGameObjectWithTag("wall");
        flashLight = GameObject.FindGameObjectWithTag("FlashLight");
        clipList = GameObject.FindGameObjectsWithTag("clip");
        ClipHeight = DefaultHight;
        LightHeight = flashLight.transform.position.y;//把灯光的高度初始值设置为点光源高度
        clickingClip = false;
        ghostAppear = false;
    }

    // Update is called once per frame
    void Update()
    {
        //记录是否拼正确图块
        bool correctlyCombineClip = CorrectlyCombineClip();
        //如果不正确，则隐藏图块
        if (!correctlyCombineClip)
        {
            shadowObject.SetActive(false);
        }
        else
        {
            CaculateShadowTransform();
            
        }
        /////////鼠标按下处理
        if (Input.GetMouseButtonDown(0) && !clickingClip)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.tag);
                clickObject = hit.collider.gameObject;
                //如果点到幽灵，则溶解幽灵，进入控制拼图阶段
                if (clickObject.tag == "Player")
                {
                    //幽灵消失
                    HideGhost();
                }
                //如果点到影子，则生成幽灵，进入幽灵控制阶段
                else if (clickObject.tag == "Shadow")
                {
                    
                    ghost.SetActive(true);
                    ghost.transform.localPosition = Vector3.zero;
                    ghost.GetComponent<BoxCollider>().enabled = true;
                    ghost.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ghostAppear = true;
                }
                else if (clickObject.tag == "clip")//防止不同拼图间冲突
                {
                    //如果碰到了碎片，那就把这个碎片的影子显示出来
                    clickObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    //拖动时使其他碎片的colider无效化
                    foreach (GameObject clip in clipList)
                    {
                        if (clip != clickObject)
                        {
                            clip.GetComponent<Collider>().enabled = false;
                        }
                    }
                    Vector3 clipPos = clickObject.transform.position;
                    MoveSupport.transform.position = new Vector3(clipPos.x, clipPos.y - 0.5f, clipPos.z);
                    ClipHeight = clickObject.transform.position.y;
                }
            }
        }
        //鼠标按住时操作
        //Debug.Log(ghostAppear);
        if (Input.GetMouseButton(0) && !ghostAppear)
        {
            if (clickObject != null && clickObject.tag == "clip")
            {
                Debug.Log("clip is selected");
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
            clickObject = null;
            //将Colider还原
            foreach (GameObject clip in clipList)
            {
                clip.GetComponent<Collider>().enabled = true;
            }
            //如果正确拼图且松开了鼠标，则出现影子
            if (correctlyCombineClip)
            {
                

                for (int i = 0; i < 3; ++i)
                    trueClip[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                shadowObject.SetActive(true);
                CaculateShadowTransform();
                //顺便设置三个拼图最合适的位置（吸引）
                for (int i =0; i < trueClip.Length - 1; ++i)
                {
                    trueClip[i + 1].transform.position = trueClip[i].transform.position - relativePosCheck[i];
                }
                //哲学
                if (!ghostAppear)
                    gameObject.GetComponent<AudioSource>().Play();
                /*进入新关卡*/
                GameObject.FindGameObjectWithTag("BlockDown").GetComponent<BlockDown>().Down();//掉落新关卡方块
            }
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

            ClipHeight = ClipHeight > CanvasDeep ? ClipHeight : CanvasDeep;
            ClipHeight = ClipHeight < CameraHeight ? ClipHeight : CameraHeight;
            //幽灵出现时操控模式
            if (ghostAppear)
            {
                return;
            }
            //拼图时操控模式
            else if (clickObject != null && clickObject.tag == "clip")
            {
                Vector3 clipPos = clickObject.transform.position;
                MoveSupport.transform.position = new Vector3(clipPos.x, clipPos.y - 0.5f, clipPos.z);
                clickObject.transform.position = new Vector3(clipPos.x, clipPos.y + y, clipPos.z);
            }
            //光源操作模式
            else if (flashLight != null)
            {
                Vector3 lightPos = flashLight.transform.position;
                flashLight.transform.position = new Vector3(lightPos.x, lightPos.y + y, lightPos.z);
            }
        }
    }
    public void SetGhostAppearState(bool a)
    {
        ghostAppear = a;
        ts.show();
    }
    //判断拼图匹配
    bool CorrectlyCombineClip()
    {
        float yF = flashLight.transform.position.y;
        float y0 = yF - 4.0f;
        float value = 0.0f;
        for (int i = 0; i < trueClip.Length; ++i)
        {
            Vector3 relativePos;
            if (i < 2)
            {
                float scalei = y0 / (yF - trueClip[i].transform.position.y);
                float scalei_1 = y0 / (yF - trueClip[i + 1].transform.position.y);
                relativePos = trueClip[i].transform.position * scalei - trueClip[i + 1].transform.position * scalei_1;
            }
            else
            {
                float scalei = y0 / (yF - trueClip[i].transform.position.y);
                float scalei_1 = y0 / (yF - trueClip[0].transform.position.y);
                relativePos = trueClip[i].transform.position * scalei - trueClip[0].transform.position * scalei_1;
            }
            Vector3 delta = relativePos - relativePosCheck[i];
            delta.y = 0.0f;
            value += (delta).sqrMagnitude;
            if (value > 2.0f)
                return false;
        }
        return true;
    }

    //根据光源位置和拼图位置，计算出影子大小+位置
    void CaculateShadowTransform()
    {
        Vector3 lightPos = flashLight.transform.position;
        Vector3 clipPos = trueClip[1].transform.position;
        float y = groundY;
        float scale = (y - lightPos.y) / (clipPos.y - lightPos.y);

        float x = (clipPos.x - lightPos.x) * scale + lightPos.x;
        float z = (clipPos.z - lightPos.z) * scale + lightPos.z;

        shadowObject.transform.position = new Vector3(x, y, z * 1.015f);
        shadowObject.transform.localScale = new Vector3(scale * 1.6f, shadowObject.transform.localScale.y, scale * 1.6f);

        float value = scale * 0.75f - 0.5f;
        if (value < 0.8f) value = 0.8f;
        ghost.GetComponent<Rigidbody>().mass = value;
    }

    public void HideGhost()
    {
        GameObject.FindGameObjectWithTag("BlockDown").GetComponent<BlockDown>().Raise();
        ghost.SetActive(false);
        ghost.GetComponent<BoxCollider>().enabled = false;
        ghostAppear = false;
    }
}
