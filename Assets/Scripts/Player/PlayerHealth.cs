using System.Collections;
using UnityEngine;
public class PlayerHealth : MonoBehaviour, IHealth
{
    int maxHealth = 100;
    int currentHealth;
    float unbeatableTime = 1f;
    float knockbackForce = 10f;
    float blinkRate = 0.1f;
    bool unbeatable = false;
    WaitForSeconds GetWaitForSeconds;
    WaitForSeconds GetBlinkSeconds;
    
    Coroutine blinkCoroutine;
    Rigidbody2D rb;

    MeshRenderer[] meshRenderers;
    [SerializeField] HealthBar healthBar;
    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GetWaitForSeconds = new WaitForSeconds(unbeatableTime);
        GetBlinkSeconds = new WaitForSeconds(blinkRate);
        rb = GetComponent<Rigidbody2D>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    // 체력 조정 함수 => IHealth 인터페이스를 통해 호출
    public void TakeDamage(int damage)
    {
        // 무적이라면 넘어가기
        if (unbeatable)
        {
            return;
        }
        

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.SetHealth(currentHealth);

        KnockBack();
        SoundManager.Instance.PlaySound(SoundType.GetHit);


        StartCoroutine(SetUnbeatable());
    }

    // 데미지를 입었을 시 밀려나는 함수
    void KnockBack()
    {
        rb.AddForce(Vector2.up * knockbackForce, ForceMode2D.Impulse);
    }

    // 무적 판정 함수
    IEnumerator SetUnbeatable()
    {
        unbeatable = true;
        blinkCoroutine = StartCoroutine(Blink());
        Physics2D.IgnoreLayerCollision(9, 13, true);
        yield return GetWaitForSeconds;
        StopCoroutine(blinkCoroutine);
        Physics2D.IgnoreLayerCollision(9, 13, false);
        foreach(var mesh in meshRenderers)        
            mesh.material.color = Color.white;
        unbeatable = false;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            foreach (var mesh in meshRenderers)
                mesh.material.color = Color.gray;
            yield return GetBlinkSeconds;
            foreach (var mesh in meshRenderers)
                mesh.material.color = Color.white;
            yield return GetBlinkSeconds;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Item item))
        {
            item.Use(this.gameObject);
        }
    }
}
