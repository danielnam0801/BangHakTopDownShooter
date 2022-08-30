using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class levelManager : AudioPlayer
{
    public float totalExp = 0;  
    public int levelsExpGauge = 0;
    public float sum = 0;

    public List<int> levels;
    public int currentLevel;
    public bool isLevelUp;
    private string clickButton;
    private bool isClickAuto;

    public Action levelUP;
    public UnityEvent levelUp;
    public List<GetDataSo> BuffObjects;
    public int buffcnt;
    WeaponDataSO weapon;
    BulletdataSO bullet;
    MovementDataSO agent;


    [SerializeField]
    private AudioClip _cllickSound = null, _levelUpSound = null;
    [SerializeField]
    RectTransform panel;
    [SerializeField]
    RectTransform coverPanel;
    private int panelChildCount;
    private int index;
    private int cnt;
    private List<int> _enabledList;

    [SerializeField]
    ExpBar expbar;
    [SerializeField]
    TextMeshProUGUI leveltext;
    bool endChoice = false;
    [SerializeField]
    TextMeshProUGUI movespeed, criticalRate, criticalDmg, Dmg, bulletSpeed, AttackDelay, ReloadTime, AutomaicShot,AmmoCapacity;
    [SerializeField]
    Ease ease;

    protected override void Awake()
    {
        base.Awake();
        index = 0;

        isLevelUp = false;
        totalExp = 0;
        //levels = new List<int>() {5, 25, 45, 65, 90, 120, 155, 195, 250};
        currentLevel = 0;
        panel = panel.gameObject.GetComponent<RectTransform>();
        coverPanel = coverPanel.gameObject.GetComponent<RectTransform>();
        panel.gameObject.SetActive(false);
        coverPanel.gameObject.SetActive(false);
        panelChildCount = panel.transform.childCount;
        _enabledList = new List<int>() { 0, 0, 0 };
        buffcnt = BuffObjects.Count;
        weapon = GameObject.Find("G.U.N").GetComponent<Weapon>().WeaponData;
        agent = GameObject.Find("Player").GetComponent<AgentMovement>()._movementSO;
        bullet = GameObject.Find("G.U.N").GetComponent<Weapon>().WeaponData.bulletdata;
        weapon.ammoCapacity = 6;
        weapon.automaticFire = false;
        weapon.weaponDelay = 0.7f;
        weapon.reloadTime = 2f;
        weapon.criticalDmg = 1.5f;
        weapon.criticalRate = 0.3f;
        bullet.damage = 1;
        bullet.bulletSpeed = 7;
        agent.maxSpeed = 3.3f;
        isClickAuto = false;
        expbar = GameObject.Find("ExpBar").GetComponent<ExpBar>();
        levelsExpGauge = levels[0];
        leveltext = GameObject.Find("levelCntTxt").GetComponent<TextMeshProUGUI>();
        //AmmoCapacity = GetComponent<TextMeshProUGUI>();
        //movespeed = GetComponent<TextMeshProUGUI>();
        //criticalDmg = GetComponent<TextMeshProUGUI>();
        //criticalRate = GetComponent<TextMeshProUGUI>();
        //Dmg = GetComponent<TextMeshProUGUI>();
        //bulletSpeed = GetComponent<TextMeshProUGUI>();  
        //AttackDelay = GetComponent<TextMeshProUGUI>();  
        //ReloadTime = GetComponent<TextMeshProUGUI>();   
        //AutomaicShot = GetComponent<TextMeshProUGUI>(); 
        
        
    }

    private void FixedUpdate()
    {

        if(totalExp >= levels[currentLevel])
        {   
            
            currentLevel++;
            isLevelUp=true;
            leveltext.text = currentLevel.ToString();
          
            if (isLevelUp == true)
            {
                LevelUp();
                //LevelUpSetting();
                //LevelUpUi();
                isLevelUp = false;
            }
            Debug.Log( "currentLevel : "+ currentLevel);       


        }
        
        if(currentLevel == 0) {
            //Debug.Log(expbar);
            expbar.SetExpBar(totalExp / levelsExpGauge);
        }

        if(currentLevel >= 1)
        {
            levelsExpGauge = levels[currentLevel] - levels[currentLevel-1];      
            sum = totalExp - levels[currentLevel-1];
            expbar.SetExpBar(sum / levelsExpGauge);
        }




        
    }

    private void LevelUp()
    {
        //levelUP += LevelUpUi;
        //levelUP += LevelUpSetting;
        levelUp.Invoke();// levelUPSetting과 LevelUPUI넣어ㅜ저야함
        
    }

    public void LevelUpSetting()
    { 

        PlayClip(_levelUpSound);
        //StartCoroutine(FreeTime());
        //if(endChoice == true)
        //{
        //    if (TimeController.instance.isActiveTime == false)
        //    {
        //        Debug.Log("LEVELUP TIME");
        //        TimeController.instance.ModifyTimeScale(0f, 0.02f);
        //        endChoice = false;
        //    }
        //    Debug.Log(".");
            
        //}
        
    }
    //IEnumerator FreeTime()
    //{
    //    yield return new WaitForSeconds(0.02f);
    //}

    public void LevelUpUi()
    {
        //Debug.Log(1);
        StopCoroutine(LevelUPUI());
        StartCoroutine(LevelUPUI());
       
    }

    IEnumerator LevelUPUI()
    {
        coverPanel.gameObject.SetActive(true);
        yield return null;
        Debug.Log("ALive");
        //Debug.Log(isClickAuto + "!!LEVELUP!!");
        cnt = 0;
        panel.gameObject.SetActive(true);

        for(int i = 0; i < 3; i++)
        {
            int indexy;
            if(isClickAuto == true)
            {
                indexy = UnityEngine.Random.Range(0, panelChildCount-1);
            }
            else
            {
                indexy = UnityEngine.Random.Range(0, panelChildCount);
                //Debug.Log(indexy);
            }
               
            _enabledList[cnt] = indexy;

            cnt++;
            if (i >= 1 && i < 3)
            {
                if (_enabledList[i] == _enabledList[i - 1])
                {
                    while (_enabledList[i] == _enabledList[i - 1])
                    {
                        indexy = UnityEngine.Random.Range(0, panelChildCount - 1);
                        if (indexy != _enabledList[i - 1])
                        {
                            _enabledList[i] = indexy;
                            break;
                        }
                    }

                }
            }
            //Debug.Log(_enabledList[i]);

            
        }            
        
            Debug.Log(_enabledList[0]);
            Debug.Log(_enabledList[1]);
            Debug.Log(_enabledList[2]);

        for(int i = 0; i< 3; i++)
        {
            panel.gameObject.transform.GetChild(_enabledList[i]).gameObject.SetActive(true);
            panel.gameObject.transform.GetChild(_enabledList[i]).gameObject.transform.DOMove(new Vector3(400 + 580*i, 500, 0), 1f, true).SetEase(ease);//.localPosition = new Vector3(-500 * i, 0, 0);
            yield return new WaitForSeconds(0.8f);

        }
        endChoice = true;
        coverPanel.gameObject.SetActive(false);
    }

    public void IfButtonClick()
    {
        Debug.Log("!");
        
        isLevelUp = false;
        clickButton = EventSystem.current.currentSelectedGameObject.name;
        GetTypeScript();
        if(clickButton == "AttackMode")
        {
            isClickAuto = true;
        }
        /// 들어올때마다 설정해줘야함
        for(int i =0; i< _enabledList.Count; i++)
        {
            panel.gameObject.transform.GetChild(_enabledList[i]).gameObject.transform.localPosition = new Vector3(0, 1500, 0);
            panel.transform.GetChild(_enabledList[i]).gameObject.SetActive(false);
            _enabledList[i] = 0;
        }
        panel.gameObject.SetActive(false);
        
        
        //TimeController.instance.ResetTimeScale();
        PlayClip(_cllickSound);
    }

    public void GetTypeScript()
    {
        Debug.Log("GetType");
        int i = 0;
        for(i = 0 ; i< buffcnt; i++ )
        {
            Debug.Log(clickButton);
            if(clickButton == BuffObjects[i].buff.name)
            {
                Debug.Log(BuffObjects[i].buff.name);
                if(BuffObjects[i].dataType == DataType.Weapon)
                {
                    switch (BuffObjects[i].detail)
                    {
                        case DataTypeDetail.capacity:
                            weapon.ammoCapacity += 10;
                            AmmoCapacity.text = "AmmoCapacity : " + weapon.ammoCapacity.ToString();
                            break;
                        case DataTypeDetail.CriticalDamage:
                            weapon.criticalDmg *= 1.2f;
                            criticalDmg.text = "Critical Dmg : " + weapon.criticalDmg.ToString();
                            break;
                        case DataTypeDetail.AttackMode:
                            weapon.automaticFire = true;
                            AutomaicShot.text = "AutomaticShot : O";
                            break;
                        case DataTypeDetail.AttackSpeed:
                            weapon.weaponDelay -= 0.1f;
                            AttackDelay.text = "Attack Delay : " + weapon.weaponDelay.ToString();
                            break;
                        case DataTypeDetail.CriticalRate:
                            weapon.criticalRate *= 1.07f;
                            criticalRate.text = "Critical Rate: "+ weapon.criticalRate.ToString();  
                            break;
                        case DataTypeDetail.CriticalRate2:
                            weapon.criticalRate *= 1.15f;
                            criticalRate.text = "Critical Rate: " + weapon.criticalRate.ToString();
                            break;
                            
                    }
                    Debug.Log("Weaponn");
                    break;
                }
                else if(BuffObjects[i].dataType == DataType.Agent)
                {
                    agent.maxSpeed *= 1.3f;//agent 관련 변경사항이 많아 질시 switch문으로 변경
                    movespeed.text = "MoveSpeed : " + agent.maxSpeed.ToString();
                    Debug.Log("Agent");
                    break;
                }
                else if(BuffObjects[i].dataType == DataType.Bullet)
                {
                    switch (BuffObjects[i].detail)
                    {
                        case DataTypeDetail.Damage:
                            bullet.damage += 1;
                            Dmg.text = "Damage : " + bullet.damage.ToString();  
                            break;
                        case DataTypeDetail.BulletSpeed:
                            bullet.bulletSpeed *= 1.2f;
                            bulletSpeed.text =  "BulletSpeed : " + bullet.bulletSpeed.ToString();
                            break;
                    }
                    Debug.Log("Bullet");
                    break;
                }
                Debug.Log("Comein");
            }
            
        }
    }
}
