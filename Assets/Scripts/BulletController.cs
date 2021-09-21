using UnityEngine;

public class BulletController : MonoBehaviour, IPoolItem, IDamageable
{
    #region Unity API
    private void Update()
    {
        _rigidbody.velocity = transform.up * _moveSpeed;
    }

    private void LateUpdate()
    {
        ProcessDamage();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_strength);
        }

        TakeDamage(1000f);
    }
    #endregion

    #region Vars
    public float MoveSpeed => _moveSpeed;
    public float Strength => _strength;

    private float _moveSpeed;
    private float _strength;

    private bool _isDamaged;
    private float _lastDamage;
    private float _defaultHealth = 1f;
    private float _health = 1f;
    private float _minHealth = 0;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;
    #endregion

    #region Methods
    public void Init(WeaponModel characteristics)
    {
        _moveSpeed = characteristics.BulletSpeed;
        _strength = characteristics.Strength;
    }

    public void Enable()
    {
        // в дальнейшем можно будет делать всякие штуки при появлении
        // допустим анимация появления, звук

        gameObject.SetActive(true);

        _rigidbody.simulated = true;
        _collider.enabled = true;

        _health = _defaultHealth;
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

            _isDamaged = false;

            if (_health <= _minHealth)
            {
                Destroy();
            }
        }
    }

    private void Destroy()
    {
        _rigidbody.simulated = false;
        _collider.enabled = false;

        gameObject.SetActive(false);
    }
    #endregion
}