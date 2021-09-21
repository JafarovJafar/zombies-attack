using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ZombieController : CharacterController, IControllable, IPoolItem, IDamageable
{
    #region Unity API
    private void Update()
    {
        GetMoveVector();
        Move();
    }

    private void LateUpdate()
    {
        ProcessDamage();
    }
    #endregion

    #region Inputs
    public float HorAxis
    {
        get => _horAxis;
        set => _horAxis = Mathf.Clamp(value, -1f, 1f);
    }

    public float VertAxis
    {
        get => _vertAxis;
        set => _vertAxis = Mathf.Clamp(value, -1f, 1f);
    }

    public Vector2 TouchPosition
    {
        get => _touchPosition;
        set => _touchPosition = value;
    }
    #endregion

    #region Interaction
    private bool _isDamaged;
    private float _lastDamage;

    private RaycastHit2D _raycastHit;
    private IDamageable _tempIDamageable;
    #endregion

    #region Animations
    [SerializeField] private string _walkAnimationName;
    [SerializeField] private string _attackAnimationName;
    #endregion

    #region Other
    protected override CharacterModel BaseModel => _model;
    [SerializeField] private ZombieModel _model;
    public ZombieModel Model => _model;
    #endregion

    #region Methods
    public void Init(ZombieModel model)
    {
        _model = model;

        _spriteRenderer.color = _model.Color;

        _health = _model.DefaultHealth;
    }

    public void Enable()
    {

    }

    public void Attack(UnityAction Finished)
    {
        PlayAnimation(_attackAnimationName);
        StartCoroutine(AttackAsync(Finished));
    }

    private IEnumerator AttackAsync(UnityAction Finished)
    {
        yield return new WaitForSeconds(_model.AttackPrepareDuration);

        _raycastHit = Physics2D.Raycast(transform.position, RootTransform.up, _model.AttackDistance);

        if (_raycastHit.transform != null)
        {
            _tempIDamageable = _raycastHit.transform.GetComponent<IDamageable>();

            if (_tempIDamageable != null)
            {
                _tempIDamageable.TakeDamage(_model.Strength);
            }
        }

        yield return new WaitForSeconds(_model.AttackReleaseDuration);

        Finished?.Invoke();
        yield return null;
    }

    public void TakeDamage(float damage)
    {
        _isDamaged = true;
        _lastDamage = damage;
    }

    private void ProcessDamage()
    {
        if (_isDamaged)
        {
            _health -= _lastDamage;

            if (_health <= _model.MinHealth)
            {
                Destroy();
            }

            _isDamaged = false;
        }
    }

    private void Destroy()
    {
        EventsPool.Instance.ZombieDead?.Invoke(_model);
        gameObject.SetActive(false);
    }

    public void CopyTransform(Transform goalTransform)
    {
        transform.position = goalTransform.position;
        _rootTransform.rotation = goalTransform.rotation;
    }

    private void PlayAnimation(string animationName)
    {
        _animator.Play(animationName);
    }

    public void Follow()
    {
        PlayAnimation(_walkAnimationName);
    }
    #endregion
}