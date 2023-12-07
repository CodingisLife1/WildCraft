using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startGame_button;

    private void Start()
    {
        startGame_button.onClick.AddListener(() => SceneManager.LoadScene(1));
    }
}
