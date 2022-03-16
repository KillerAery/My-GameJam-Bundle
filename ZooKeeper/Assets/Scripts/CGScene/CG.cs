using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CG : MonoBehaviour
{
    public GameObject textPrefab;
    public Animator cg;

    // Start is called before the first frame update
    void Start()
    {


    }

    public void PlayCG()
    {
        StartCoroutine(RunStart());
        cg.SetTrigger("Start");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }


    IEnumerator RunStart()
    {
        yield return new WaitForSeconds(0.5f);
        var go1 = Instantiate(textPrefab, transform);
        go1.GetComponentInChildren<TextMesh>().text = "我是柏攬動物園的新員工噠\n就職動物園貌似有億點點奇怪哦";

        yield return new WaitForSeconds(6.0f);
        go1.SetActive(false);
        var go2 = Instantiate(textPrefab, transform);
        go2.GetComponentInChildren<TextMesh>().text = "前輩告訴我\n平時溫順可愛的動物有可能會暴走\n我需要按照手冊上的規則謹慎行事";

        yield return new WaitForSeconds(6.0f);
        go2.SetActive(false);
        var go3 = Instantiate(textPrefab, transform);
        go3.GetComponentInChildren<TextMesh>().text = "盡可能照顧人類\n（真的是這樣的嗎？動物們好像也很可(you)憐(qu)呐）";

        yield return new WaitForSeconds(6.0f);
        go3.SetActive(false);
        var go4 = Instantiate(textPrefab, transform);
        go4.GetComponentInChildren<TextMesh>().text = "要是動物和人類都滿意\n那也很棒棒~";

        yield return new WaitForSeconds(6.0f);
        go4.SetActive(false);
        var go5 = Instantiate(textPrefab, transform);
        go5.GetComponentInChildren<TextMesh>().text = "A和D為左右行走\nW和S來控制選擇你覺得正確的事情\n嫌我速度慢的話可以按shift進行飆車";

        yield return new WaitForSeconds(10.0f);
        StartScene();
    }



}
