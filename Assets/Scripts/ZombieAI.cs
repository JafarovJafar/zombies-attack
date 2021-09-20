using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    #region Base
    private void Start()
    {
        EventsPool.Instance.PlayerDead += ProcessPlayerDeath;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case States.Idle:

                // ��� � ������� ����� ������ �������� �� ���� ������
                // �������� �������, ����� ����� ������� ������������ ����, ��� ����� � ���� ������

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
    [SerializeField] private ZombieController _zombieController; // ������ ��� � ������ ������� ��������� IControllable, �� ���������� ������ ����������� ����� ���������

    public Transform _goalTransform;

    private Vector3 _goalVector;

    private RaycastHit2D _raycastHit;

    #region State Machine
    private enum States
    {
        Idle,
        Follow,
        Attack,
    }
    private States _currentState;
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