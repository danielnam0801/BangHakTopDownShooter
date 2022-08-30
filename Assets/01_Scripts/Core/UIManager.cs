using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform StatusPanel;
    [SerializeField] SpriteRenderer Panel;

    [SerializeField] float ftime = 1f;
    float time = 0f;
    bool isDead = false;


    private void Awake()
    {
        StatusPanel.gameObject.SetActive(false);
        Panel.gameObject.SetActive(false);
        Panel = Panel.gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        //string message = mousePos.ToString();
        //Debug.Log(message);
        if (mousePos.x <= 209 && mousePos.x >= 50 && mousePos.y >= 19 && mousePos.y <= 104)
        {
            StatusPanel.gameObject.SetActive(true);
            //ATK.text = "ATK : " + attackRange.damage;
            //ASPD.text = "ASPD : " + attackSpeed.cooltime;
        }
        else
        {
            StatusPanel.gameObject.SetActive(false);
        }

        if(isDead == true)
        {
            SceneManager.LoadScene("Die");
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOUT());
    }
    public void FadeIn()
    {
        StartCoroutine(FadeIN());
    }

    IEnumerator FadeIN()
    {
        yield return new WaitForSeconds(0.7f);
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / ftime;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            //Debug.Log("aaaa");
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Die");
        yield return null;
        
    }
    IEnumerator FadeOUT()
    {
        yield return new WaitForSeconds(0.5f);
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a >= 0f)
        {
            time += Time.deltaTime / ftime;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }
}


