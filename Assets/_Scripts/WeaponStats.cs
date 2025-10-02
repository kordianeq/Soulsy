using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float ataceSpeed = 1f;
    public float weight = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Do damage to the enemy
            other.GetComponent<IDamage>()?.TakeDamage(damage);
        }
    }
}
