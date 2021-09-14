using UnityEngine;

/// <summary>
/// ��������� ������������� ������
/// </summary>
public class WeaponCharacteristics : ScriptableObject
{
    #region Properties
    /// <summary>
    /// ��������
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// �������� ����� ���������� � ��������
    /// </summary>
    public float ShootInterval => _shootInterval;
    /// <summary>
    /// ������ ���������������� ����
    /// </summary>
    public GameObject BulletPrefab => _bulletPrefab;
    #endregion

    [SerializeField] private string _name;
    [SerializeField] private float _shootInterval;
    [SerializeField] private GameObject _bulletPrefab;
}