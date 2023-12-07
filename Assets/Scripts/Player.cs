using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MalbersAnimations;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    private float minCorners;
    private Transform closestAnimal;
    public List<GameObject> animals;
    [SerializeField] private GameObject fieldView;
    [SerializeField] private CinemachineFreeLook cinemachine;
    public MalbersInput input;
    [SerializeField] private Stats hp;

    [SerializeField] private SkinnedMeshRenderer wolf;
    [SerializeField] private SkinnedMeshRenderer magic;
    [SerializeField] private Transform head;
    [SerializeField] private Transform forwardLegR;
    [SerializeField] private Transform forwardLegL;
    [SerializeField] private Transform rearLegR;
    [SerializeField] private Transform rearLegL;
    [SerializeField] private GameObject[] hats;

    [SerializeField] private Material[] wolfMaterials;
    [SerializeField] private Material[] magicMaterials;

    [SerializeField] private Slider hp_slider;
    [SerializeField] private TextMeshProUGUI hp_txt;
    [SerializeField] private GameObject reactionsPanel;

    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController mainAnimController;
    [SerializeField] private RuntimeAnimatorController reactionsAnimController;
    [SerializeField] private Avatar mainAvatar;
    [SerializeField] private Avatar reactionAvatar;
    [SerializeField] private Animation[] animations;
    [SerializeField] private Button reaction1_button;


    // Start is called before the first frame update
    void Start()
    {
        if (Init.Instance.magicColorIndex == 0)
        {
            magic.gameObject.SetActive(false);
        }
        else
        {
            magic.gameObject.SetActive(true);
        }

        magic.material = magicMaterials[Init.Instance.magicColorIndex];

        wolf.material = wolfMaterials[Init.Instance.wolfColorIndex];

        head.localScale = new Vector3(Init.Instance.headSize, Init.Instance.headSize, Init.Instance.headSize);

        forwardLegL.localScale = new Vector3(Init.Instance.forwardLegsSize, 1, 1);
        forwardLegR.localScale = new Vector3(Init.Instance.forwardLegsSize, 1, 1);

        rearLegL.localScale = new Vector3(1, 1, Init.Instance.rearLegsSize);
        rearLegR.localScale = new Vector3(1, 1, Init.Instance.rearLegsSize);

        foreach (var item in hats)
        {
            item.SetActive(false);
        }

        hats[Init.Instance.hatVariantIndex].SetActive(true);

        input.EnableInput("Dig", false);

        hp_slider.maxValue = hp.Stat_Get(1).maxValue;
        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        reaction1_button.onClick.AddListener(() => PlayAnim(0));
    }

    // Update is called once per frame
    void Update()
    {
        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        if (Input.GetKeyDown(KeyCode.U))
        {
            OpenPanel(true);
            ChangeAvatar(false);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenPanel(false);
            ChangeAvatar(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayAnim(0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            animals.Add(other.gameObject);
        }
        
    }

    private IEnumerator Find()
    {
        animals.Clear();
        fieldView.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        fieldView.SetActive(false);
        FindClosestObject();
        
    }

    public void StartCorFind()
    {
        StartCoroutine(Find());
    }

    public void FindClosestObject()
    {
        minCorners = 9999999999;
        foreach (var item in animals)
        {
            if (Vector3.Distance(transform.position, item.transform.position) < minCorners)
            {
                minCorners = Vector3.Distance(gameObject.transform.position, item.transform.position);
                closestAnimal = item.transform;
            }

        }
    }

    public void ChangeAvatar(bool main)
    {
        if (main)
        {
            animator.avatar = mainAvatar;
            animator.runtimeAnimatorController = mainAnimController;
            
        }
        else
        {
            animator.avatar = reactionAvatar;
            animator.runtimeAnimatorController = reactionsAnimController;
        }
    }

    public void OpenPanel(bool open)
    {
        if (open)
        {
            reactionsPanel.SetActive(true);
        }
        else
        {
            reactionsPanel.SetActive(false);
        }    
    }

    public void PlayAnim(int index)
    {
        
        animator.SetTrigger("Anim");
        //animations[index].Play();
    }
}
