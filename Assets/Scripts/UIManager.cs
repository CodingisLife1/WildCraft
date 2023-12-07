using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button findAnimal_button;
    public Button looting_button;


    
    // Start is called before the first frame update
    void Start()
    {
        findAnimal_button.onClick.AddListener(GameManager.instance.player.StartCorFind);
        //looting_button.onClick.AddListener(StartCorLooting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


}
