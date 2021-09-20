using UnityEngine;

public class ZombieModel : CharacterModel
{
    public float AttackDistance => _attackDistance;
    public float AttackPrepareDuration => _attackPrepareDuration;
    public float AttackReleaseDuration => _attackReleaseDuration;
    public float Strength => _strength;
    public float Cost => _cost;
    public Color Color => _color;

    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackPrepareDuration;
    [SerializeField] private float _attackReleaseDuration;
    [SerializeField] private float _strength;
    [SerializeField] private float _cost;
    [SerializeField] private Color _color;
}