using UnityEngine;

public class Player : Character, IControllable
{
    #region Base
    private void Start()
    {
        _weaponController.Init(_weaponCharacteristics);
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
    #endregion

    #region Vars
    #region Inputs
    public float HorAxis
    {
        get => _horAxis;
        set => _horAxis = Mathf.Clamp(_horAxis,-1f, 1f);
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

    private float _horAxis;
    private float _vertAxis;
    private Vector2 _touchPosition;
    #endregion

    #region State Machine
    private enum States
    {
        Default,
    }

    private States _currentState;
    #endregion

    #region Movement
    [SerializeField] private Transform _rootTransform;

    //private Vector3 _goalPosition = new Vector3();
    private Vector3 _goalEulerAngles = new Vector3();
    #endregion

    #region Shooting
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private WeaponCharacteristics _weaponCharacteristics;
    #endregion

    #region Animations
    private readonly string SHOOT_ANIMATION_NAME = "Shoot";
    #endregion

    #endregion

    #region Methods
    private void Rotate()
    {
        _goalEulerAngles.z = Vector2.SignedAngle(transform.up, _touchPosition);

        _rootTransform.eulerAngles = _goalEulerAngles;
    }

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
    #endregion
}