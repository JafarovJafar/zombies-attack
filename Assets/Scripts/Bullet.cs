using UnityEngine;

public class Bullet : MonoBehaviour, IPoolItem
{
    #region Base
    private void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
    }
    #endregion

    #region Vars
    public float MoveSpeed => _moveSpeed;
    public float Strength => _strength;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _strength;


    #endregion

    #region Methods
    public void Init(WeaponCharacteristics characteristics)
    {
        _moveSpeed = characteristics.BulletSpeed;
        _strength = characteristics.Strength;
    }

    public void Enable()
    {
        // в дальнейшем можно будет делать вс€кие штуки при по€влении
        // допустим анимаци€ по€влени€, звук
    }
    #endregion
}