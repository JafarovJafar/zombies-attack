using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region Properties
    public bool CanShoot => Time.time > _lastShootTime + _characteristics.ShootInterval; // в будущем можно модифицировать и добавить проверку на то, что оружие не перезаряжается
    #endregion

    [SerializeField] private ObjectPool _bulletsPool;

    private float _lastShootTime;

    private WeaponCharacteristics _characteristics;

    [SerializeField] private Transform _muzzleTransform;

    public void Init(WeaponCharacteristics characteristics)
    {
        _characteristics = characteristics;

        _bulletsPool.Init(characteristics.BulletPrefab);
    }

    public void Shoot()
    {
        _lastShootTime = Time.time;

        GameObject tempGO = _bulletsPool.GetItem();

        tempGO.transform.position = _muzzleTransform.position;
        tempGO.transform.rotation = _muzzleTransform.rotation;
    }
}