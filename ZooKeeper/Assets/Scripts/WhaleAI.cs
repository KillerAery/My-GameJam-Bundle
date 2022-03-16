using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> waypoints;
    public float speed = 1.0f;
    int currentTarget = 0;

    // Update is called once per frame
    void Update()
    {
        var dis = waypoints[currentTarget].position - transform.position;
        if(dis.magnitude > 0.1f)
        {
           dis = dis.normalized * speed *Time.deltaTime;

            var s = transform.localScale;
            if (dis.x < 0)
            {
                s.x = 1;
            }
            else
            {
                s.x = -1;
            }
            transform.localScale = s;

            transform.position += dis;
        }
        else
        {
            currentTarget = Random.Range(0,waypoints.Count);
        }
    }
}
