using UnityEngine;

public class CharacterModel : ScriptableObject
{
    public float MoveSpeed => _moveSpeed;

    [SerializeField] protected float _moveSpeed;

}