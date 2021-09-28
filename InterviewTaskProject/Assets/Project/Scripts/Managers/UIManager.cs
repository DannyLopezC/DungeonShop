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
    public Image lifeBar;

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
    [BoxGroup("Menu")]
    public TMP_Text damageAmount;
    [BoxGroup("Menu")]
    public TMP_Text forceAmount;

    public GameObject shopWeaponPrefab;
    public GameObject shopClothesPrefab;
    //public ShopItem

    [InlineEditor]
    public List<Weapon> availableWeapons;
    [InlineEditor]
    public List<Clothes> availableClothes;

    [InlineEditor]
    public List<ShopItem> sellClothes;
    [InlineEditor]
    public List<ShopItem> sellWeapons;
    public Transform sellContent;

    public ScrollRect scrollRect;

    private Attack playerAttackComponent;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
        playerAttackComponent = gm.player.GetComponentInChildren<Attack>();
        weaponSprite.sprite = playerAttackComponent.weapons[playerAttackComponent.equipped].sprite;
        UpdateUIValues();

        SetSellItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnInventory(!opened);

        UpdateUIValues();
    }

    public void SetSellItems()
    {
        //set skins to sell
        for (int i = 0; i < gm.player.clothes.Count; i++)
        {
            ShopItem sp = Instantiate(shopClothesPrefab, sellContent).GetComponent<ShopItem>();
            sp.price.text = gm.player.clothes[i].price.ToString() + "$";

            sp.weaponImage.sprite = gm.player.clothes[i].sprite;

            sellClothes.Add(sp);
        }

        //set weapons to sell
        for (int i = 0; i < playerAttackComponent.weapons.Count; i++)
        {
            ShopItem sp = Instantiate(shopWeaponPrefab, sellContent).GetComponent<ShopItem>();
            sp.damage.text = playerAttackComponent.weapons[i].damage.ToString();
            sp.force.text = playerAttackComponent.weapons[i].force.ToString();
            sp.price.text = playerAttackComponent.weapons[i].price.ToString() + "$";

            sp.weaponImage.sprite = playerAttackComponent.weapons[i].sprite;

            sellClothes.Add(sp);
        }

        for (int i = 0; i < sellClothes.Count; i++)
        {
            sellClothes[i].id = i;
        }


    }

    public void DeselectAllSellItems(ShopItem origin)
    {
        for (int i = 0; i < sellClothes.Count; i++)
        {
            if (sellClothes[i].id != origin.id)
            {
                sellClothes[i].selected = false;
            }
        }

        for (int i = 0; i < sellWeapons.Count; i++)
        {
            if (sellClothes[i].id != origin.id)
            {
                sellWeapons[i].selected = false;
            }
        }
    }

    public void SetBuyItems()
    {
        List<Clothes> clothesToAdd = availableClothes;

        //add items to the sell list
        for (int i = 0; i < gm.player.clothes.Count; i++)
        {
            for (int j = 0; j < gm.player.clothes.Count; j++)
            {
                if (gm.player.clothes[i] == clothesToAdd[j])
                {
                    clothesToAdd.Remove(clothesToAdd[j]);
                }
            }
        }
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
        damageAmount.text = playerAttackComponent.weapons[playerAttackComponent.equipped].damage.ToString();
        forceAmount.text = playerAttackComponent.weapons[playerAttackComponent.equipped].force.ToString();

        menuLifeBar.fillAmount = gm.player.life / gm.player.maxLife;
        lifeBar.fillAmount = gm.player.life / gm.player.maxLife;
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
