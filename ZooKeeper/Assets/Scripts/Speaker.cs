using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public GameObject SpeakerPrefab;
    public Transform speaker1;
    public Transform speaker2;
    IEnumerator SpeakWords(string text)
    {
        var go1 = Instantiate(SpeakerPrefab, speaker1);
        go1.GetComponentInChildren<TextMesh>().text = text;
        var go2 = Instantiate(SpeakerPrefab, speaker2);
        go2.GetComponentInChildren<TextMesh>().text = text;
        yield return new WaitForSeconds(3.0f);
        GameObject.Destroy(go1);
        GameObject.Destroy(go2);
    }
    public void Speak(string text)
    {
        StartCoroutine(SpeakWords(text));
    }
}
