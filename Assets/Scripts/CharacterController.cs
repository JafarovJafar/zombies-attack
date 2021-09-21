using UnityEngine;

/// <summary>
/// Базовый класс для всех юнитов (солдаты и зомби)
/// </summary>
public abstract class CharacterController : MonoBehaviour
{
    #region Public Properties
    public Transform RootTransform => _rootTransform;
    public Vector3 Up => _rootTransform.up;
    #endregion

    #region Inputs
    protected float _horAxis;
    protected float _vertAxis;
    protected Vector2 _touchPosition;
    #endregion

    #region Movement
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected Transform _rootTransform;
    
    protected Vector2 _moveVector = new Vector2();
    private Vector3 _goalEulerAngles = new Vector3();
    #endregion

    #region Other
    protected abstract CharacterModel BaseModel { get; }
    [SerializeField] protected float _health;

    [SerializeField] protected Animator _animator;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    #endregion

    #region Methods
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
        _goalEulerAngles.z = Vector2.SignedAngle(transform.up, _touchPosition - (Vector2)transform.position);

        _rootTransform.eulerAngles = _goalEulerAngles;
    }
    #endregion
}