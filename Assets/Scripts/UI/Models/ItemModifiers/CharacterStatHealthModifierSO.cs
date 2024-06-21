using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{

    public override void AffectCharacter(GameObject character, int val)
    {
        Damageable damageable = character.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.HealthRestore(val);
        }
    }
}