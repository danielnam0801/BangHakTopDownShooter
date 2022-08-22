using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIBrain : MonoBehaviour
{

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged;
    public UnityEvent OnFireButtonPress;
    public UnityEvent OnFireButtonRealease;

    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private Transform _target;// 게임매니저를 통해 가져올것
    public Transform Target
    {
        get => _target;
    }
    
    private AIActionData _aiActionData;
    public AIActionData AIActionData { get => _aiActionData; }
    private AIMovementData _aiMovementData;
    public AIMovementData AIMovementData { get => _aiMovementData; }

    private Transform _basePosition;
    public Transform BasePosition { get => _basePosition; }

    public Enemy _enemy;
    public Enemy Enemy => _enemy;

    protected virtual void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
        _aiMovementData = transform.Find("AI").GetComponent<AIMovementData>();
        _basePosition = transform.Find("BasePosition");
        _enemy = GetComponent<Enemy>();
    }

    protected virtual void Start()
    {
        _target = GameManager.instance.Player;
    }
    protected void Update()
    {
        if(_target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
            return;
        }
        else
        {
            _currentState.UpdateState(); ;
        }
    }

    public void ChangeSstate(AIState state)
    {
        _currentState = state;
    }

    public virtual void Attack()
    {
        OnFireButtonPress?.Invoke();
    }

    public void Move(Vector2 direction, Vector2 targetPos)
    {
        OnMovementKeyPress?.Invoke(direction);
        OnPointerPositionChanged?.Invoke(targetPos);
    }
}
