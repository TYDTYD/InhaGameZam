using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] StaminaBar[] staminaBar;
    [SerializeField] CanvasGroup staminaGroup;
    float maxStamina = 10f;
    float currentStamina;
    float recovery = 0.01f;
    float transparentRate = 0.02f;
    
    void Awake()
    {
        currentStamina = maxStamina;
        foreach(var stamina in staminaBar)
        {
            stamina.SetMaxStamina(maxStamina);
        }        
    }

    void FixedUpdate()
    {
        if (maxStamina > currentStamina)
        {
            currentStamina += recovery;
            foreach (var stamina in staminaBar)
            {
                stamina.SetStamina(currentStamina);
            }            
        }
        else
        {
            if (staminaGroup.alpha != 0f)
            {
                staminaGroup.alpha -= transparentRate;
            }
        }
    }

    public bool CanDash(float cost) => currentStamina > cost;
    public float CurrentStamina => currentStamina;
    public void UseStamina(float value)
    {
        staminaGroup.alpha = 1f;
        currentStamina = Mathf.Max(currentStamina - value, 0f);
        foreach (var stamina in staminaBar)
        {
            stamina.SetStamina(currentStamina);
        }
    }
}
