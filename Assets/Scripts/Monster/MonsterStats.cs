using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour, IHealth
{
    public int maxHp = 10;
    public int currentHp;
    public int attack = 1;
    public int defense = 1;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    //데미지 받는 함수
    public void TakeDamage(int damage)
    {
        // Calculate effective damage after defense
        int effectiveDamage = Mathf.Max(damage - defense, 1);   // Ensure at least 1 damage is dealt
        currentHp -= effectiveDamage;

        Debug.Log($"{gameObject.name}가 데미지 {effectiveDamage} 입음. 남은 HP: {currentHp}/{maxHp}");
        // Check if the monster is dead
        if (currentHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        // Handle monster death (e.g., play animation, drop loot, etc.)
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject); // Destroy the monster game object
    }
    void Update()
    { 
        // 임시: a 키 누르면 데미지 10 오브젝트 파괴 확인용
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    GetComponent<MonsterStats>().TakeDamage(10);
        //}
    }
}
