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
                
                break;

            case States.Follow:

                break;

            case States.Attack:

                break;
        }
    }
    #endregion

    #region Vars
    public IControllable _zombie;

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

    #endregion
}