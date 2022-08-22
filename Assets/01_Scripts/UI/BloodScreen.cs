using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScreen : MonoBehaviour
{
    private Image _image;
    private float _screenTime = 0.3f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ShowBloodScreen()
    {
        StopAllCoroutines();
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine()
    {
        _image.enabled = true;
        yield return new WaitForSeconds(_screenTime);
        _image.enabled = false;
    }
    
}
