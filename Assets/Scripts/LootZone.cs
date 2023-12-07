using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootZone : MonoBehaviour
{
    public bool canLoot = false;
    [SerializeField] private Loot loot;
    [SerializeField] private Slider looting_slider;
    [SerializeField] private TextMeshProUGUI lootAmount_txt;
    [SerializeField] private Transform lookAtObject;

    private void Update()
    {
        lookAtObject.LookAt(GameManager.instance.cam_transform.position);
        if (canLoot == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCorLooting();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.player.input.EnableInput("Dig", true);
            canLoot = true;
            GameManager.instance.uIManager.looting_button.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.player.input.EnableInput("Dig", false);
            canLoot = false;
            GameManager.instance.uIManager.looting_button.gameObject.SetActive(false);
        }
    }

    IEnumerator Looting()
    {
        looting_slider.gameObject.SetActive(true);
        lootAmount_txt.text = "";
        int count = 0;

        for (int i = 0; i < 100; i++)
        {
            looting_slider.value += 1;
            if (i % 20 == 0)
            {
                count += 1;
                lootAmount_txt.text = "+ " + count.ToString();
            }
            yield return new WaitForSeconds(0.01f);
        }

        if (loot.lootID == 0)
        {
            Init.Instance.playerData.woodAmount += count;
        }
        else if (loot.lootID == 1)
        {
            Init.Instance.playerData.bonesAmount += count;
        }
        else if (loot.lootID == 2)
        {
            Init.Instance.playerData.meatAmount += count;
        }
        else if (loot.lootID == 3)
        {
            Init.Instance.playerData.leavesAmount += count;
        }

        looting_slider.gameObject.SetActive(false);
        looting_slider.value = 0;
        lootAmount_txt.text = "";

    }

    void StartCorLooting()
    {
        StartCoroutine(Looting());
    }
}
