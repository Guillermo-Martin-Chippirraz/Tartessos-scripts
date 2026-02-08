using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Ability ability1;
    public Ability ability2;

    public void UseAbility1(GameObject target)
    {
        ability1.Use(gameObject, target);
    }

    public void UseAbility2(GameObject target)
    {
        ability2.Use(gameObject, target);
    }
}
