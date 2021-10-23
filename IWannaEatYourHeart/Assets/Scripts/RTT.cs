using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTT : MonoBehaviour
{
    public RenderTexture texture;

    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();


    }

    // Start is called before the first frame update
    void Start()
    {
        camera.targetTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
