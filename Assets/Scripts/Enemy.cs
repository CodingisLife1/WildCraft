using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations;
using TMPro;
using System;
using MalbersAnimations.Controller;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int hp_multiplier;
    [SerializeField] private int damage_multiplier;
    [SerializeField] private TextMeshProUGUI hp_txt;
    [SerializeField] private TextMeshProUGUI level_txt;
    [SerializeField] private Slider hp_slider;
    [SerializeField] private Stats hp;
    [SerializeField] private MAttackTrigger frontAttack; 
    [SerializeField] private MAttackTrigger mouthAttack; 
    [SerializeField] private MAttackTrigger frontRightAttack;
    [SerializeField] private MAttackTrigger frontLeftAttack;
    [SerializeField] private bool isAgressive;

    private void Start()
    {
        if (isAgressive)
        {
            frontAttack.statModifier.MinValue += level * damage_multiplier;
            frontAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;

            mouthAttack.statModifier.MinValue = frontAttack.statModifier.MinValue;
            mouthAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;

            frontRightAttack.statModifier.MinValue = frontAttack.statModifier.MinValue;
            frontRightAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;

            frontLeftAttack.statModifier.MinValue = frontAttack.statModifier.MinValue;
            frontLeftAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;
        }
        

        hp.Stat_Get(1).maxValue = hp.Stat_Get(1).maxValue + (level * hp_multiplier);
        hp.Stat_Get(1).value = hp.Stat_Get(1).maxValue;
        hp_slider.maxValue = hp.Stat_Get(1).maxValue;
        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        level_txt.text = level.ToString();
    }

    private void Update()
    {
        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        hp_slider.gameObject.transform.LookAt(GameManager.instance.cam_transform.position);
    }

    public IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void StartCor_OnDeath()
    {
        StartCoroutine(OnDeath());
    }
}
