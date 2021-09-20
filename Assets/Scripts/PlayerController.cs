using UnityEngine;

public class PlayerController : CharacterController, IControllable, IDamageable
{
    #region Unity API
    private void Start()
    {
        _weaponController.Init(_weaponModel);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case States.Default:
                Rotate();
                TryShoot();
                break;
        }
    }

    private void LateUpdate()
    {
        ProcessDamage();
    }
    #endregion

    #region Vars
    #region Inputs
    public float HorAxis
    {
        get => _horAxis;
        set => _horAxis = Mathf.Clamp(_horAxis, -1f, 1f);
    }

    public float VertAxis
    {
        get => _vertAxis;
        set => _vertAxis = Mathf.Clamp(_vertAxis, -1f, 1f);
    }

    public Vector2 TouchPosition // Вдруг потом придется как-то обрабатывать тач. Поэтому пусть будет свойство
    {
        get => _touchPosition;
        set => _touchPosition = value;
    }
    #endregion

    #region State Machine
    private enum States
    {
        Default,
    }

    private States _currentState;
    #endregion

    #region Shooting
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private WeaponModel _weaponModel;
    #endregion

    #region Animations
    private readonly string SHOOT_ANIMATION_NAME = "Shoot";
    #endregion

    #region Other
    protected override CharacterModel BaseModel => _model;

    [SerializeField] private PlayerModel _model;
    public PlayerModel Model => _model;
    #endregion

    #region Interaction
    private bool _isDamaged;
    private float _lastDamage;
    #endregion
    #endregion

    #region Methods
    private void TryShoot()
    {
        // тут можно делать всякие модификации (например если зомби близко, то бьет прикладом вместо стрельбы)

        if (_weaponController.CanShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _animator.Play(SHOOT_ANIMATION_NAME, 0, 0);

        _weaponController.Shoot();
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

            if (_health < _model.MinHealth)
            {
                Destroy();

                EventsPool.Instance.PlayerDead?.Invoke();
            }

            _isDamaged = false;
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    #endregion
}