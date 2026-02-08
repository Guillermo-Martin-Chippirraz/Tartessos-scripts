using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 1000f;
    public float currentHealth;

    public System.Action OnDeath;
    public System.Action<float> OnDamage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        OnDamage?.Invoke(amount);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
    }
}
