using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamageDealer : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 1f;

    private float lastAttackTime = -999f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}
