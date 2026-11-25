using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ShopItemButton.cs: one item in a shop list. ShopMenu creates it and calls Setup.
public class ShopItemButton : MonoBehaviour
{
    ShopItem item;
    CoinCounter coins;
    Button button;

    string Key => "owned_" + item.name.ToLower();   // owned_apple, owned_cake, ...

    public void Setup(ShopItem shopItem, CoinCounter coinCounter)
    {
        item = shopItem;
        coins = coinCounter;
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
        GetComponentInChildren<TMP_Text>().text = item.comingSoon
            ? $"{item.name}: {item.price} coins (soon)"
            : $"{item.name}: {item.price} coins";
    }

    // Every frame, so the button greys out as soon as coins run low.
    void Update() => button.interactable = !item.comingSoon && Services.Save.GetInt("coins") >= item.price;

    void Buy()
    {
        if (item.comingSoon || Services.Save.GetInt("coins") < item.price) return;
        coins.Add(-item.price);
        Services.Save.SetInt(Key, Services.Save.GetInt(Key) + 1);
    }
}
