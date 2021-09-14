using UnityEngine;

public class PlayerTouchInput : MonoBehaviour
{
    #region Base
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _goalPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _goalPosition.z = 0;

            _controllable.SetGoalPosition(_goalPosition);
        }
    }
    #endregion

    #region Vars
    [SerializeField] private Camera _camera;
    [SerializeField] private IControllable _controllable;
    private Vector3 _goalPosition = new Vector3();
    #endregion

    #region Methods
    public void Init(IControllable controllable)
    {
        _controllable = controllable;
    }
    #endregion
}