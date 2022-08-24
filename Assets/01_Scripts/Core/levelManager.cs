using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class levelManager : AudioPlayer
{
    public float totalExp = 0;
    public List<int> levels;
    public int currentLevel;
    public bool isLevelUp;

    public UnityEvent levelUp;

    [SerializeField]
    private AudioClip _cllickSound = null, _levelUpSound = null;
    [SerializeField]
    RectTransform panel;
    
    private int panelChildCount;
    private int index;
    private int cnt;
    private List<int> _enabledList = null;

    protected override void Awake()
    {
        base.Awake();
        index = 0;

        isLevelUp = false;
        totalExp = 0;
        //levels = new List<int>() {5, 25, 45, 65, 90, 120, 155, 195, 250};
        currentLevel = 0;
        panel = panel.GetComponent<RectTransform>();
        panelChildCount = panel.transform.childCount;
    }
    

    private void Update()
    {
        if(totalExp >= levels[currentLevel])
        {
            currentLevel++;
            isLevelUp=true;
            
            if(isLevelUp == true)
            {
                LevelUp();
                isLevelUp = false;
            }
            Debug.Log( "currentLevel : "+ currentLevel);
        }

    }

    private void LevelUp()
    {
        levelUp.Invoke();// levelUPSetting과 LevelUPUI넣어ㅜ저야함
    }


    public void LevelUpSetting()
    { 
        PlayClip(_levelUpSound);
        Time.timeScale = 0.05f;
    }

    public void LevelUpUi()
    {
        Debug.Log("ALive");
        cnt = 0;
        panel.gameObject.SetActive(true);
        for(int i = 1; i > -2; i--)
        {
            int index = Random.Range(0, panelChildCount);
            panel.transform.GetChild(index).gameObject.SetActive(true);
            panel.transform.GetChild(index).gameObject.transform.position = new Vector3(-500 * i, 0, 0);
            _enabledList[cnt] = index;
            
            cnt++;
            
        }
    }

    public void IfButtonClick()
    {
        isLevelUp = false;
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;
        Debug.Log(clickButton.name);
        /// 들어올때마다 설정해줘야함
        for(int i =0; i< _enabledList.Count; i++)
        {
            panel.transform.GetChild(_enabledList[i]).gameObject.SetActive(false);
        }
        panel.gameObject.SetActive(false);
        
        Time.timeScale = 1f;
        PlayClip(_cllickSound);
    }
}
