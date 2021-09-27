using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public Button inventoryButton;

    public GameObject menuGo;
    public Animator animator;
    public bool opened = false;
    public bool changing = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnInventory(!opened);
    }

    public void OnInventory(bool open)
    {
        if (changing) return;

        Image buttonImage = inventoryButton.gameObject.GetComponent<Image>();

        if (open)
        {
            changing = true;
            buttonImage.DOFade(0, 0.2f)
                .OnComplete(() =>
                {
                    inventoryButton.gameObject.SetActive(!open);
                    changing = false;
                });

            animator.SetTrigger("show");
            opened = true;
        }
        else
        {
            changing = true;
            buttonImage.DOFade(100, 0.2f)
                .OnComplete(() =>
                {
                    inventoryButton.gameObject.SetActive(!open);
                    changing = false;
                });

            animator.SetTrigger("hide");
            opened = false;
        }

    }
}
