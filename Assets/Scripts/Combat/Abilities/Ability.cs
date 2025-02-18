using UnityEngine;

public abstract class Ability : ScriptableObject
{

    public abstract bool ShouldUse();

    public abstract void Use(GameObject caster);

}