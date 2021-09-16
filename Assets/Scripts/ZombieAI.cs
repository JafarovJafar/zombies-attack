using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    #region Base
    private void Update()
    {
        switch (_currentState)
        {
            case States.Idle:

                // ��� � ������� ����� ������ �������� �� ���� ������
                // �������� �������, ����� ����� ������� ������������ ����, ��� ����� � ���� ������

                //ChangeState(States.Follow);

                break;

            case States.Follow:
                _goalVector = _goalTransform.position - transform.position;
                _zombieController.HorAxis = _goalVector.x;
                _zombieController.VertAxis = _goalVector.y;

                if (_goalVector.magnitude <= _zombieController.Model.AttackDistance)
                {
                    _zombieController.HorAxis = 0;
                    _zombieController.VertAxis = 0;
                    ChangeState(States.Attack);

                    StartCoroutine(_zombieController.AttackAsync(() =>
                    {
                        Debug.Log("finished");
                        ChangeState(States.Follow);
                    }));
                }
                break;

            case States.Attack:

                break;
        }
    }
    #endregion

    #region Vars
    public IControllable _zombie;
    public ZombieController _zombieController;

    public Transform _goalTransform;

    private Vector3 _goalVector;

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
    #endregion
}