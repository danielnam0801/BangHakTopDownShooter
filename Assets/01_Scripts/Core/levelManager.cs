using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class levelManager : AudioPlayer
{
    public float totalExp = 0;  
    public int levelsExpGauge = 0;
    public float sum = 0;

    public List<int> levels;
    public int currentLevel;
    public bool isLevelUp;
    private GameObject clickButton;
    private bool isClickAuto;


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

    private int panelChildCount;
    private int index;
    private int cnt;
    private List<int> _enabledList;

    [SerializeField]
    ExpBar expbar;


    protected override void Awake()
    {
        base.Awake();
        index = 0;

        isLevelUp = false;
        totalExp = 0;
        //levels = new List<int>() {5, 25, 45, 65, 90, 120, 155, 195, 250};
        currentLevel = 0;
        panel = panel.gameObject.GetComponent<RectTransform>();
        panel.gameObject.SetActive(false);
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
    }

    private void Update()
    {  

    }
    private void FixedUpdate()
    {

        if(totalExp >= levels[currentLevel])
        {   
          
            currentLevel++;
            isLevelUp=true;
            
          
            if (isLevelUp == true)
            {
                LevelUpSetting();
                LevelUpUi();
                isLevelUp = false;
            }
            Debug.Log( "currentLevel : "+ currentLevel);       


        }
        
        if(currentLevel == 0) {
            Debug.Log(expbar);
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
        levelUp.Invoke();// levelUPSetting과 LevelUPUI넣어ㅜ저야함
    }

    public void LevelUpSetting()
    { 
        PlayClip(_levelUpSound);
        Time.timeScale = 0.0f;
    }

    public void LevelUpUi()
    {
        StartCoroutine(LevelUPUI());
       
    }

    IEnumerator LevelUPUI()
    {
        yield return null;
        Debug.Log("ALive");
        cnt = 0;
        panel.gameObject.SetActive(true);
        for(int i = 1; i > -2; --i)
        {
            int index;
            if(isClickAuto == true)
            {
                index = Random.Range(0, panelChildCount-1);
            }
            else
            {
                index = Random.Range(0, panelChildCount);
            }
            Debug.Log(index);
            panel.gameObject.transform.GetChild(index).gameObject.SetActive(true);
            panel.gameObject.transform.GetChild(index).gameObject.transform.localPosition = new Vector3(-500 * i, 0, 0);
            _enabledList[cnt] = index;
            cnt++;
            
        }
    }

    public void IfButtonClick()
    {
        
        isLevelUp = false;
        clickButton = EventSystem.current.currentSelectedGameObject;
        GetTypeScript();
        if(clickButton.name == "AttackMode")
        {
            isClickAuto = true;
        }
        /// 들어올때마다 설정해줘야함
        for(int i =0; i< _enabledList.Count; i++)
        {
            panel.transform.GetChild(_enabledList[i]).gameObject.SetActive(false);
            _enabledList[i] = 0;
        }
        panel.gameObject.SetActive(false);
        
        
        Time.timeScale = 1f;
        PlayClip(_cllickSound);
    }

    public void GetTypeScript()
    {
        Debug.Log("GetType");
        int i = 0;
        for(i = 0 ; i< buffcnt; i++ )
        {
            Debug.Log(clickButton);
            if(clickButton.name == BuffObjects[i].buff.name)
            {
                if(BuffObjects[i].dataType == DataType.Weapon)
                {
                    switch (BuffObjects[i].detail)
                    {
                        case DataTypeDetail.capacity:
                            weapon.ammoCapacity += 10;
                            break;
                        case DataTypeDetail.CriticalDamage:
                            weapon.criticalDmg *= 1.2f;
                            break;
                        case DataTypeDetail.AttackMode:
                            weapon.automaticFire = true;
                            break;
                        case DataTypeDetail.AttackSpeed:
                            weapon.weaponDelay -= 0.1f;
                            break;
                        case DataTypeDetail.CriticalRate:
                            weapon.criticalRate *= 1.07f;
                            break;
                        case DataTypeDetail.CriticalRate2:
                            weapon.criticalRate *= 1.15f;
                            break;
                            
                    }
                    Debug.Log("Weaponn");
                    break;
                }
                else if(BuffObjects[i].dataType == DataType.Agent)
                {
                    agent.maxSpeed *= 1.3f;//agent 관련 변경사항이 많아 질시 switch문으로 변경
                    Debug.Log("Agent");
                    break;
                }
                else if(BuffObjects[i].dataType == DataType.Bullet)
                {
                    switch (BuffObjects[i].detail)
                    {
                        case DataTypeDetail.Damage:
                            bullet.damage += 1;
                            break;
                        case DataTypeDetail.BulletSpeed:
                            bullet.bulletSpeed *= 1.2f;
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
