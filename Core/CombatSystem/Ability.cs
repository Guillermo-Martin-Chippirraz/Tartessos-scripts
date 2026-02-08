using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float accuracy = 0.8f;
    public float damage = 20f;

    public virtual void Use(GameObject caster, GameObject target)
    {
        bool hit = Random.value <= accuracy;

        if (hit)
        {
            target.GetComponent<HealthSystem>().TakeDamage(damage);
        }
        else
        {
            target.GetComponent<AgroSystem>().TriggerAgro();
        }
    }
}
