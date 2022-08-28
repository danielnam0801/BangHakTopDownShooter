using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHitAble, IAgent
{
    public bool IsEnemy => false;

    public Vector3 HitPoint
    {
        get; private set;
    }
    #region
    [SerializeField]
    private float _maxHealth;
    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            OnUpdateHealthUI?.Invoke(_health);  
        }
    }
    #endregion
    private bool _isDead = false;
    [field: SerializeField] public UnityEvent OnDie {  get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent<float> OnUpdateHealthUI { get; set; }

    private AgentMovement _agentMovement;

    private void Awake()
    {
        _agentMovement = GetComponent<AgentMovement>();
        _health = _maxHealth;

    }
    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_isDead) return;
        Health -= damage;
        OnUpdateHealthUI?.Invoke(Health);
        OnGetHit?.Invoke();
        if(Health <= 0)
        {
            OnDie?.Invoke();
            _isDead = true;
        }
    }
}
