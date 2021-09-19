using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ZombieController : CharacterController, IControllable, IPoolItem, IDamageable
{
    #region Base
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

    protected override CharacterModel BaseModel => _model;

    [SerializeField] private ZombieModel _model;
    public ZombieModel Model => _model;

    private bool _isDamaged;
    private float _lastDamage;

    private RaycastHit2D _raycastHit;
    private IDamageable _tempIDamageable;

    public void Init(ZombieModel model)
    {

        _model = model;
    }

    public void Enable()
    {

    }

    public void Attack(UnityAction Finished)
    {
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
        gameObject.SetActive(false);
    }

    public void CopyTransform(Transform goalTransform)
    {
        transform.position = goalTransform.position;
        _rootTransform.rotation = goalTransform.rotation;
    }
}