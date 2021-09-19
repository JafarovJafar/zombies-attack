using UnityEngine;

/// <summary>
/// Базовый класс для всех юнитов (солдаты и зомби)
/// </summary>
public abstract class CharacterController : MonoBehaviour
{
    #region Inputs
    protected float _horAxis;
    protected float _vertAxis;
    protected Vector2 _touchPosition;
    #endregion

    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected Animator _animator;

    #region Movement
    protected Vector2 _moveVector = new Vector2();

    [SerializeField] protected Transform _rootTransform;

    public Transform RootTransform => _rootTransform;

    public Vector3 Up => _rootTransform.up;

    private Vector3 _goalEulerAngles = new Vector3();
    #endregion

    [SerializeField] protected float _health;

    protected abstract CharacterModel BaseModel { get; }

    protected virtual void GetMoveVector()
    {
        _moveVector.x = _horAxis;
        _moveVector.y = _vertAxis;

        _moveVector = Vector2.ClampMagnitude(_moveVector, 1f);
    }

    protected virtual void Move()
    {
        _rigidbody.velocity = BaseModel.MoveSpeed * _moveVector;
    }

    protected virtual void Rotate()
    {
        _goalEulerAngles.z = Vector2.SignedAngle(transform.up, _touchPosition);

        _rootTransform.eulerAngles = _goalEulerAngles;
    }
}