using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] StaminaBar staminaBar;

    float maxStamina = 10f;
    float currentStamina;
    float recovery = 0.01f;
    
    void Awake()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (maxStamina > currentStamina)
        {
            currentStamina += recovery;
            staminaBar.SetStamina(currentStamina);
        }
    }

    public bool CanDash(float cost) => currentStamina > cost;
    public float CurrentStamina => currentStamina;
    public void UseStamina(float value)
    {
        currentStamina = Mathf.Max(currentStamina - value, 0f);
        staminaBar.SetStamina(currentStamina);
    }
}
