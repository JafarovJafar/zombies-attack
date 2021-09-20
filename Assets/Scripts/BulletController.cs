using UnityEngine;

public class BulletController : MonoBehaviour, IPoolItem, IDamageable
{
    #region Base
    private void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
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

        Destroy();
    }
    #endregion

    #region Vars
    public float MoveSpeed => _moveSpeed;
    public float Strength => _strength;

    private float _moveSpeed;
    [SerializeField] private float _strength;

    private bool _isDamaged;
    private float _lastDamage;
    private float _health = 1f;
    private float _minHealth = 0;

    [SerializeField] private Rigidbody2D _rigidbody;
    #endregion

    #region Methods
    public void Init(WeaponModel characteristics)
    {
        _moveSpeed = characteristics.BulletSpeed;
        _strength = characteristics.Strength;
    }

    public void Enable()
    {
        Debug.Log(1);

        // в дальнейшем можно будет делать всякие штуки при появлении
        // допустим анимация появления, звук

        _rigidbody.simulated = true;
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

            if (_health <= _minHealth)
            {
                Destroy();
            }

            _isDamaged = false;
        }
    }

    private void Destroy()
    {
        _rigidbody.simulated = false;

        gameObject.SetActive(false);
    }
    #endregion
}