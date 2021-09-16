using UnityEngine;

public class BulletModel : ScriptableObject
{
    public float MoveSpeed => _moveSpeed;
    public float Strength => _strength;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _strength;
}