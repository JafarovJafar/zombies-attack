using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    #region Unity API
    private void Start()
    {
        EventsPool.Instance.PlayerDead += ProcessPlayerDeath;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case States.Idle:

                // тут в будущем можно делать проверку на поле зрения
                // допустим сделать, чтобы зомби начинал преследовать того, кто попал в поле зрения

                _raycastHit = Physics2D.Raycast(transform.position, _zombieController.Up);

                if (_raycastHit.transform.CompareTag(CONSTANTS.PLAYER_TAG))
                {
                    _goalTransform = _raycastHit.transform;

                    _zombieController.Follow();

                    ChangeState(States.Follow);
                }

                break;

            case States.Follow:
                if (_goalTransform == null)
                {
                    _zombieController.HorAxis = 0;
                    _zombieController.VertAxis = 0;

                    ChangeState(States.Idle);
                    return;
                }

                _goalVector = _goalTransform.position - transform.position;
                _zombieController.HorAxis = _goalVector.x;
                _zombieController.VertAxis = _goalVector.y;
                _zombieController.TouchPosition = _goalTransform.position;

                if (_goalVector.magnitude <= _zombieController.Model.AttackDistance)
                {
                    _zombieController.HorAxis = 0;
                    _zombieController.VertAxis = 0;
                    ChangeState(States.Attack);

                    _zombieController.Attack(() =>
                    {
                        ChangeState(States.Idle);
                    });
                }
                break;
        }
    }
    #endregion

    #region Vars
    #region State Machine
    private enum States
    {
        Idle,
        Follow,
        Attack,
    }
    private States _currentState;
    #endregion

    #region Other
    [SerializeField] private ZombieController _zombieController; // вообще тут в идеале указать интерфейс IControllable, но интерфейсы нельзя прокидывать через инспектор

    public Transform _goalTransform;
    private Vector3 _goalVector;

    private RaycastHit2D _raycastHit;
    #endregion
    #endregion

    #region Methods
    private void ChangeState(States state)
    {
        _currentState = state;
    }

    private void ProcessPlayerDeath()
    {
        _goalTransform = null;
    }
    #endregion
}