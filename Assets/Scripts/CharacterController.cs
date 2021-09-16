using UnityEngine;

/// <summary>
/// Базовый класс для всех юнитов (солдаты и зомби)
/// </summary>
public abstract class Character : MonoBehaviour
{
    #region Inputs
    protected float _horAxis;
    protected float _vertAxis;
    protected Vector2 _touchPosition;
    #endregion

    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Animator _animator;

    #region Movement
    protected Vector2 _moveVector = new Vector2();

    [SerializeField] private Transform _rootTransform;

    private Vector3 _goalEulerAngles = new Vector3();
    #endregion

    protected abstract CharacterModel BaseModel { get; }

    protected virtual void GetMoveVector()
    {
        _moveVector.x = _horAxis;
        _moveVector.y = _vertAxis;

        _moveVector = Vector2.ClampMagnitude(_moveVector, 1f);
    }

    protected virtual void Move()
    {
        transform.Translate(BaseModel.MoveSpeed * _moveVector * Time.deltaTime);
    }

    protected virtual void Rotate()
    {
        _goalEulerAngles.z = Vector2.SignedAngle(transform.up, _touchPosition);

        _rootTransform.eulerAngles = _goalEulerAngles;
    }
}