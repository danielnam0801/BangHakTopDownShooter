using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartSceneScript : MonoBehaviour
{
    public Ease ease;
    [SerializeField] GameObject gameTitle;
    [SerializeField] Image Panel;
    [SerializeField] float time = 0;
    [SerializeField] float ftime = 1;

    private void Awake()
    {
        gameTitle.transform.DOMove(new Vector3(-5.22f, 1.45f, 0), 4f, false).SetEase(ease);
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
