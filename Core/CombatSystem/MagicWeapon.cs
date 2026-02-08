using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/MagicWeapon")]
public class MagicWeapon : ScriptableObject
{
    public string weaponName;
    public float bonusAccuracy;
    public float bonusDamage;
}
