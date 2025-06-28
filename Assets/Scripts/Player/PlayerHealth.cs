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
    Rigidbody2D rb;

    [SerializeField] HealthBar healthBar;

    public void TakeDamage(int damage)
    {
        if (unbeatable)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.SetHealth(currentHealth);

        KnockBack();

        StartCoroutine(SetUnbeatable());
    }

    void KnockBack()
    {
        rb.AddForce(Vector2.up * knockbackForce);
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GetWaitForSeconds = new WaitForSeconds(unbeatableTime);
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator SetUnbeatable()
    {
        unbeatable = true;
        yield return GetWaitForSeconds;
        unbeatable = false;
    }
}
