using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartSceneScript : MonoBehaviour
{
    public Ease ease;
    [SerializeField] RectTransform gameTitle;
    [SerializeField] Image Panel;
    [SerializeField] float time = 0;
    [SerializeField] float ftime = 1;

    private void Awake()
    {
        gameTitle.transform.DOMove(new Vector3(-30, 200, 0), 2.5f, false).SetEase(ease);
        gameTitle.transform.DOScale(1.4f, 2.5f).SetEase(ease);
    }

    public void SceneLoader(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void FadeIn()
    {
        StartCoroutine(FadeIN());
        SceneLoader("SampleScene");
    }

    IEnumerator FadeIN()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a <= 1f)
        {
            time += Time.deltaTime / ftime;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
