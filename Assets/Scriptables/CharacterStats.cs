using UnityEngine;
[CreateAssetMenu (fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats", order = 1)]
public class CharacterStats : ScriptableObject
{
    public string Name;
    public int Health;
    public int MaxHealth;
    public int DamageAmount;
    public int skill1;
    public int skill2;
    public Sprite icon;
    public GameObject Prefab;

}
