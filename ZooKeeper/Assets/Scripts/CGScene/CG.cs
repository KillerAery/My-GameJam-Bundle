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
        go1.GetComponentInChildren<TextMesh>().text = "���ǰؔ�����@���T���}\n������@ò���Ѓ|�c�c���Ŷ";

        yield return new WaitForSeconds(6.0f);
        go1.SetActive(false);
        var go2 = Instantiate(textPrefab, transform);
        go2.GetComponentInChildren<TextMesh>().text = "ǰ݅���V��\nƽ�r��혿ɐ۵Ą����п��ܕ�����\n����Ҫ�����փ��ϵ�Ҏ�t֔������";

        yield return new WaitForSeconds(6.0f);
        go2.SetActive(false);
        var go3 = Instantiate(textPrefab, transform);
        go3.GetComponentInChildren<TextMesh>().text = "�M����������\n��������@�ӵĆ᣿�������Ҳ�ܿ�(you)�z(qu)�ţ�";

        yield return new WaitForSeconds(6.0f);
        go3.SetActive(false);
        var go4 = Instantiate(textPrefab, transform);
        go4.GetComponentInChildren<TextMesh>().text = "Ҫ�Ǆ��������M��\n��Ҳ�ܰ���~";

        yield return new WaitForSeconds(6.0f);
        go4.SetActive(false);
        var go5 = Instantiate(textPrefab, transform);
        go5.GetComponentInChildren<TextMesh>().text = "A��D����������\nW��S������x�����X�����_������\n�����ٶ�����Ԓ���԰�shift�M���j܇";

        yield return new WaitForSeconds(10.0f);
        StartScene();
    }



}
