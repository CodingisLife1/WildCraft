using UnityEngine;

[CreateAssetMenu(fileName = "LootParams", menuName = "ScriptableObjects/Loot")]
public class Loot : ScriptableObject
{
    public int lootID; /* 0 - wood | 1 - bones | 2 - meat | 3 - leaves */
    public int lootAmount;
    public Sprite icon;
}
