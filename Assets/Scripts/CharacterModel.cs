using UnityEngine;

public class CharacterModel : ScriptableObject
{
    public float MoveSpeed => _moveSpeed;
    public float DefaultHealth => _defaultHealth;
    public float MinHealth => _minHealth;

    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _defaultHealth;
    [SerializeField] protected float _minHealth;
}