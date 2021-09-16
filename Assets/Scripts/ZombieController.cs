using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ZombieController : Character, IControllable, IPoolItem, IDamageable
{
    #region Base
    private void Update()
    {
        GetMoveVector();
        Move();
    }
    #endregion

    public float HorAxis
    {
        get => _horAxis;
        set => _horAxis = Mathf.Clamp(value, -1f, 1f);
    }

    public float VertAxis
    {
        get => _vertAxis;
        set => _vertAxis = Mathf.Clamp(value, -1f, 1f);
    }

    public Vector2 TouchPosition
    {
        get => _touchPosition;
        set => _touchPosition = value;
    }

    protected override CharacterModel BaseModel => _model;

    [SerializeField] private ZombieModel _model;
    public ZombieModel Model => _model;

    public void Init(ZombieModel model)
    {

        _model = model;
    }

    public void Enable()
    {

    }

    public IEnumerator AttackAsync(UnityAction Finished)
    {
        yield return new WaitForSeconds(5);

        yield return null;
    }

    public void TakeDamage(float damage)
    {

    }
}