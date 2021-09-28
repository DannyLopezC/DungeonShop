using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem : MonoBehaviour
{
    public TMP_Text damage;
    public TMP_Text force;
    public TMP_Text price;

    public Image selectedImage;
    public Image weaponImage;

    public bool isWeapon;

    public int id;
    public int itemId;

    private bool _selected;

    public void Start()
    {

    }

    public bool selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            Select();
        }
    }

    public void Select(bool origin = false)
    {
        if (origin)
        {
            GameManager.instance.DeselectAllSellItems(this);
            selectedImage.gameObject.SetActive(true);
        }
        else
        {
            selectedImage.gameObject.SetActive(false);
        }
    }

}
