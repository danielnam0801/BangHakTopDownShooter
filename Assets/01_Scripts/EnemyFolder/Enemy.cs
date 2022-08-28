using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyType
{
    small,
    medium,
    big
}
public class Enemy : PoolAbleMono, IHitAble, IAgent
{
    [SerializeField]
    protected EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData { get => _enemyData; }

    public bool IsEnemy => true;

    public Vector3 HitPoint { get; private set; }
    [field: SerializeField]
    public float Health { get; private set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    protected bool _isDead = false;

    [SerializeField]
    protected bool _isActive = false;
    protected EnemyAIBrain _brain;
    protected EnemyAttack _attack;

    protected CapsuleCollider2D _bodyCollider;
    protected SpriteRenderer _spriteRenderer = null;
    protected AgentMovement _agentMovement = null;
    EnemyManager _EnemyManager;
    levelManager _level;

    public EnemyType enemyType;

    protected virtual void Awake()
    {
        _brain = GetComponent<EnemyAIBrain>();
        _attack = GetComponent<EnemyAttack>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _agentMovement = GetComponent<AgentMovement>();
        _EnemyManager = GameObject.Find("Manager").GetComponent<EnemyManager>();
        _spriteRenderer = transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
        _level = GameObject.Find("Manager").GetComponent<levelManager>();
        SetEnemyData();
        Init();
    }

    public override void Init()
    {
        _brain.enabled = false;
        _isActive = false;
        _bodyCollider.enabled = false;
        _agentMovement.enabled = false;
        _isDead = false;

        if (_spriteRenderer.material.HasProperty("_Dissolve"))
        {
            _spriteRenderer.material.SetFloat("_Dissolve", 0);
        }
    }


    public void Spawn()
    {
        Sequence seq = DOTween.Sequence();
        Tween dissolve = DOTween.To(
            () => _spriteRenderer.material.GetFloat("_Dissolve"),
            x => _spriteRenderer.material.SetFloat("_Dissolve", x),
            1f,
            1f);

        seq.Append(dissolve);
        seq.AppendCallback(() => ActiveObject());
    }

    public void ActiveObject()
    {
        _brain.enabled = true;
        _isActive = true;
        _bodyCollider.enabled = true;
        _agentMovement.enabled = true;
        Health = _enemyData.maxHealth;
    }

    private void SetEnemyData()
    {
        _attack.AttackDelay = _enemyData.attackDelay;

        transform.Find("AI/IdleState/TranChase")
            .GetComponent<DecisionInner>().Distance = _enemyData.chaseRange;
        transform.Find("AI/ChaseState/TranIdle")
            .GetComponent<DecisionInner>().Distance = _enemyData.chaseRange;

        transform.Find("AI/ChaseState/TranAttack")
            .GetComponent<DecisionInner>().Distance = _enemyData.attackRange;
        transform.Find("AI/AttackState/TranChase")
            .GetComponent<DecisionOuter>().Distance = _enemyData.attackRange;

        Health = _enemyData.maxHealth;
    }

    public virtual void PerformAttack()
    {
        if (_isDead == false && _isActive == true)
        {
            _attack.Attack(_enemyData.damage);
        }
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_isDead == true) return;
        
        bool isCritical = GameManager.instance.IsCritical;
        if (isCritical)
        {
            damage = GameManager.instance.GetCriticalDamage(damage);
        }
        
        Health -= damage;
        HitPoint = damageDealer.transform.position;

        OnGetHit?.Invoke();

       
        PopUpText popUpText = PoolManager.Instance.Pop("PopupText") as PopUpText;
        popUpText?.SetUp(damage, transform.position + new Vector3(0, 0.3f), isCritical, Color.white);
        if (Health <= 0)
            DeadProcess();
    }

    private void DeadProcess()
    {
        switch (this.enemyType)
        {
            case EnemyType.small:
                _level.totalExp += 1;
                break;
            case EnemyType.medium:
                _level.totalExp += 2;
                break;
            case EnemyType.big:
                _level.totalExp += 4;
                break;
        }
        Health = 0;
        _isDead = true;
        OnDie?.Invoke();
    }

    public void Die()
    {
        PoolManager.Instance.Push(this);
        _EnemyManager.enemyAliveCnt--;

    }
}
