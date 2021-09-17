using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    #region Base
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

                    ChangeState(States.Follow);
                }

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
    private IControllable _zombie;
    [SerializeField]private ZombieController _zombieController;

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
    #endregion
}