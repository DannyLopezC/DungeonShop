using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button inventoryButton;

    public GameObject menuGo;
    private Canvas _menuCanvas;
    public float menuMotion;

    private void Start()
    {
        _menuCanvas = menuGo.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnInventory(!menuGo.activeInHierarchy);

    }

    public void OnInventory(bool open) => menuGo.SetActive(open);
}
