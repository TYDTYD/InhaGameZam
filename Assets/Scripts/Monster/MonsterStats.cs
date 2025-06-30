using UnityEngine;

public class MonsterStats : MonoBehaviour, IHealth
{
    public int maxHp = 10;
    public int currentHp;
    public int attack = 1;
    public int defense = 1;
    [SerializeField] HealthBar healthBar;
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
        currentHp = Mathf.Max(currentHp, 0);

        if (healthBar != null)
            healthBar.SetHealth(currentHp);

        // Check if the monster is dead
        if (currentHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        // Handle monster death (e.g., play animation, drop loot, etc.)
        gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        
    }
}
