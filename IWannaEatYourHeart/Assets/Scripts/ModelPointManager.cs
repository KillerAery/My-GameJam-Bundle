using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPointManager : MonoBehaviour
{
    public GameObject model; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeModel(GameObject prefab)
    {
        GameObject.Destroy(model);
        model = Instantiate(prefab, transform);

    }
}
