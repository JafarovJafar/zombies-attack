using UnityEngine;

public class Player : MonoBehaviour, IControllable
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
    #region State Machine
    private enum States
    {
        Default,
    }

    private States _currentState;
    #endregion

    #region Movement
    [SerializeField] private Transform _rootTransform;

    private Vector3 _goalPosition = new Vector3();
    private Vector3 _goalEulerAngles = new Vector3();
    #endregion

    #region Shooting
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private WeaponCharacteristics _weaponCharacteristics;
    #endregion

    #endregion

    #region Methods
    private void Rotate()
    {
        _goalEulerAngles.z = Vector2.SignedAngle(transform.up, _goalPosition);

        _rootTransform.eulerAngles = _goalEulerAngles;
    }

    public void SetGoalPosition(Vector3 goalPosition)
    {
        _goalPosition = goalPosition;
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
        // anim.play(shoot)

        _weaponController.Shoot();
    }
    #endregion
}