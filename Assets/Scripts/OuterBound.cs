using UnityEngine;

public class OuterBound : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_strength);
        }
    }

    private float _strength = 1000f;
}
