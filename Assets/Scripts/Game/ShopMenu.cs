using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// ShopMenu.cs: on ShopPanel. Shows the categories; a category shows its items from EconomyConfig.
// The buttons are copies of one template, so the scene holds a single button.
public class ShopMenu : MonoBehaviour
{
    [SerializeField] TMP_Text title;            // Card/Title
    [SerializeField] Transform list;            // the object with the Vertical Layout Group
    [SerializeField] Button template;           // inactive button inside the list
    [SerializeField] GameObject upgradeButton;  // the food upgrade, inside the list
    [SerializeField] GameObject backButton;     // inside the list, On Click → ShopMenu.ShowCategories
    [SerializeField] CoinCounter coins;

    readonly List<GameObject> spawned = new List<GameObject>();

    void OnEnable() => ShowCategories();        // the shop always opens on the categories

    public void ShowCategories()
    {
        Clear("Shop", false);
        foreach (ShopCategory category in Enum.GetValues(typeof(ShopCategory)))
        {
            ShopCategory c = category;
            Add(c.ToString(), () => ShowItems(c));
        }
        Add("Upgrades", ShowUpgrades);
    }

    void ShowItems(ShopCategory category)
    {
        Clear(category.ToString(), true);
        foreach (ShopItem item in Services.Config.Economy.shopItems)
            if (item.category == category)
                Add("", null).gameObject.AddComponent<ShopItemButton>().Setup(item, coins);
        backButton.transform.SetAsLastSibling();
    }

    void ShowUpgrades()
    {
        Clear("Upgrades", true);
        upgradeButton.SetActive(true);
        backButton.transform.SetAsLastSibling();
    }

    Button Add(string text, UnityAction onClick)
    {
        Button b = Instantiate(template, list);
        b.gameObject.SetActive(true);
        b.GetComponentInChildren<TMP_Text>().text = text;
        if (onClick != null) b.onClick.AddListener(onClick);
        spawned.Add(b.gameObject);
        return b;
    }

    void Clear(string heading, bool showBack)
    {
        foreach (GameObject g in spawned) Destroy(g);
        spawned.Clear();
        title.text = heading;
        upgradeButton.SetActive(false);
        backButton.SetActive(showBack);
    }
}
