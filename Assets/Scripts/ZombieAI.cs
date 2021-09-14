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