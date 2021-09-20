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

            _goalPosition -= _player.transform.position;

            _player.TouchPosition = _goalPosition;
        }
    }
    #endregion

    #region Vars
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerController _player;
    private Vector3 _goalPosition = new Vector3();
    #endregion
}