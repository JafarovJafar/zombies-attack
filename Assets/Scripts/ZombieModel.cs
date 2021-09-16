using UnityEngine;

public class ZombieModel : CharacterModel
{
    public float AttackDistance => _attackDistance;

    [SerializeField] private float _attackDistance;
}