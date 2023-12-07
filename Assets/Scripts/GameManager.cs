using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager uIManager;
    public Player player;
    public LootZone lootZone;
    public Transform cam_transform;


    public void Awake()
    {
        instance = this;
    }
}
