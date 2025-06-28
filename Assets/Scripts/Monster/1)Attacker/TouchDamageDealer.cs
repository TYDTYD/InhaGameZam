using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TouchDamageDealer : MonoBehaviour
{
    public int baseDamage = 10;
    public float attackCooldown = 1f;
    private float lastAttackTime = -999f;
    private MonsterStats monsterStats;

    void Start()
    {
        monsterStats = GetComponent<MonsterStats>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                int damage = (monsterStats != null) ? monsterStats.attack : baseDamage;
                playerHealth.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}
