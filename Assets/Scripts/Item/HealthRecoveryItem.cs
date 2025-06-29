
using UnityEngine;

public class HealthRecoveryItem : Item
{
    int amount = 10;
    protected override void Use(GameObject obj)
    {
        if(obj.TryGetComponent(out IHealth health))
        {
            health.Heal(amount);
        }
    }
}
