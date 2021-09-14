using UnityEngine;

/// <summary>
/// Контейнер характеристик оружия
/// </summary>
public class WeaponCharacteristics : ScriptableObject
{
    #region Properties
    /// <summary>
    /// Название
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Интервал между выстрелами в секундах
    /// </summary>
    public float ShootInterval => _shootInterval;
    /// <summary>
    /// Наносимый урон
    /// </summary>
    public float Strength => _strength;
    /// <summary>
    /// Скорость полета пули
    /// </summary>
    public float BulletSpeed => _bulletSpeed;
    /// <summary>
    /// Префаб инициализируемой пули
    /// </summary>
    public GameObject BulletPrefab => _bulletPrefab;
    #endregion

    [SerializeField] private string _name;
    [SerializeField] private float _shootInterval;
    [SerializeField] private float _strength;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
}