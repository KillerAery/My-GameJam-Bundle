using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 10;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
            GameObject.FindGameObjectWithTag("System").GetComponent<GameplaySystem>().FinishGame();
        }

        timerText.text = "æ‡¿Îœ¬∞‡ £”‡" + ((int)timer).ToString();
    }

}
