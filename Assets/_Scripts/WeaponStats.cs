using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float attackSpeed = 1f;
    public float weight = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || !other.CompareTag("Enemy"))
            return;

        IDamage damageComponent = other.GetComponent<IDamage>();
        if (damageComponent != null)
            damageComponent.TakeDamage(damage);
    }
}
