using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Idamageable damageable))
        {
            damageable.GetDamage();
        }
    }
}
