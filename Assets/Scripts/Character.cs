using UnityEngine;

/// <summary>
/// ������� ����� ��� ���� ������ (������� � �����)
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _minHealth;
    [SerializeField] protected float _maxHealth;

    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Animator _animator;
}