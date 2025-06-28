using System.Collections;
using UnityEngine;
public class PlayerHealth : MonoBehaviour, IHealth
{
    int maxHealth = 100;
    int currentHealth;
    float unbeatableTime = 1f;
    float knockbackForce = 10f;
    bool unbeatable = false;
    WaitForSeconds GetWaitForSeconds;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    [SerializeField] HealthBar healthBar;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // 체력 조정 함수 => IHealth 인터페이스를 통해 호출
    public void TakeDamage(int damage)
    {
        if (unbeatable)
        {
            
            return;
        }
        

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.SetHealth(currentHealth);

        KnockBack();

        StartCoroutine(SetUnbeatable());
    }

    // 데미지를 입었을 시 밀려나는 함수
    void KnockBack()
    {
        rb.AddForce(Vector2.up * knockbackForce, ForceMode2D.Impulse);
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GetWaitForSeconds = new WaitForSeconds(unbeatableTime);
        rb = GetComponent<Rigidbody2D>();
    }

    // 무적 판정 함수
    IEnumerator SetUnbeatable()
    {
        unbeatable = true;
        yield return GetWaitForSeconds;
        unbeatable = false;
    }
}
