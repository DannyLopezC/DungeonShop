using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Button inventoryButton;

    public GameObject menuGo;
    public Animator animator;
    public bool opened = false;
    public bool changing = false;

    [BoxGroup("Menu")]
    public Image weaponSprite;
    [BoxGroup("Menu")]
    public Image skinSprite;
    [BoxGroup("Menu")]
    public TMP_Text moneyAmount;
    [BoxGroup("Menu")]
    public TMP_Text skinsAmount;
    [BoxGroup("Menu")]
    public TMP_Text weaponsAmount;
    [BoxGroup("Menu")]
    public Image menuLifeBar;

    private Attack playerAttackComponent;
    private GameManager gm;

    private const float fullLifeBar = 24f;

    private void Start()
    {
        gm = GameManager.instance;
        playerAttackComponent = gm.player.GetComponentInChildren<Attack>();
        weaponSprite.sprite = playerAttackComponent.weapons[playerAttackComponent.equipped].sprite;
        UpdateUIValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnInventory(!opened);

        menuLifeBar.fillAmount = gm.player.life / gm.player.maxLife;
    }

    #region buttons
    public void OnNextWeapon()
    {
        Weapon nextWeapon = playerAttackComponent.weapons[playerAttackComponent.equipped];

        //going back to first if is the last
        if (playerAttackComponent.equipped + 1 >= playerAttackComponent.weapons.Count)
            nextWeapon = playerAttackComponent.weapons[0];

        //looking for next in list
        for (int i = 0; i < playerAttackComponent.weapons.Count; i++)
        {
            if (playerAttackComponent.equipped + 1 == playerAttackComponent.weapons[i].id)
                nextWeapon = playerAttackComponent.weapons[i];
        }

        //setting sprites
        weaponSprite.sprite = nextWeapon.sprite;
        playerAttackComponent.equipped = nextWeapon.id;
    }

    public void OnBackWeapon()
    {
        Weapon backWeapon = playerAttackComponent.weapons[playerAttackComponent.equipped];

        //going to last if is the first
        if (playerAttackComponent.equipped - 1 < 0)
            backWeapon = playerAttackComponent.weapons[playerAttackComponent.weapons.Count - 1];

        //looking for prev in list
        for (int i = 0; i < playerAttackComponent.weapons.Count; i++)
        {
            if (playerAttackComponent.equipped - 1 == playerAttackComponent.weapons[i].id)
                backWeapon = playerAttackComponent.weapons[i];
        }

        //setting sprites
        weaponSprite.sprite = backWeapon.sprite;
        playerAttackComponent.equipped = backWeapon.id;
    }

    public void OnNextSkin()
    {
        Clothes nextClothes = gm.player.clothes[gm.player.clothesId];

        //going back to first if is the last
        if (gm.player.clothesId + 1 >= gm.player.clothes.Count - 1)
            nextClothes = gm.player.clothes[0];

        //looking for next in list
        for (int i = 0; i < gm.player.clothes.Count; i++)
        {
            if (gm.player.clothesId + 1 == gm.player.clothes[i].id)
                nextClothes = gm.player.clothes[i];
        }

        //setting sprites
        skinSprite.sprite = nextClothes.sprite;
        gm.player.clothesId = nextClothes.id;
    }

    public void OnBackSkin()
    {
        Clothes nextClothes = gm.player.clothes[gm.player.clothesId];

        //going to last if is the first
        if (gm.player.clothesId - 1 < 0)
            nextClothes = gm.player.clothes[gm.player.clothes.Count - 1];

        //looking for prev in list
        for (int i = 0; i < gm.player.clothes.Count; i++)
        {
            if (gm.player.clothesId - 1 == gm.player.clothes[i].id)
                nextClothes = gm.player.clothes[i];
        }

        //setting sprites
        skinSprite.sprite = nextClothes.sprite;
        gm.player.clothesId = nextClothes.id;
    }
    #endregion

    public void UpdateUIValues()
    {
        moneyAmount.text = gm.player.money.ToString();
        skinsAmount.text = gm.player.clothes.Count.ToString();
        weaponsAmount.text = playerAttackComponent.weapons.Count.ToString();
    }

    public void OnInventory(bool open)
    {
        if (changing) return;

        Image buttonImage = inventoryButton.gameObject.GetComponent<Image>();

        if (open)
        {
            changing = true;
            UpdateUIValues();
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
