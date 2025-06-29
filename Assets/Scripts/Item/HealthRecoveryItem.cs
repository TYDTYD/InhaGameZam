
using UnityEngine;

public class HealthRecoveryItem : Item
{
    int amount = 10;
    public override void Use(GameObject obj)
    {
        if(obj.TryGetComponent(out IHealth health))
        {
            health.Heal(amount);
            this.gameObject.SetActive(false);
        }
    }
}
