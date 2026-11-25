using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ShopItemButton.cs: a fixed-price item in the shop. Price from EconomyConfig.
// Food is bought and counted. Clothes and Tools show their price but wait for art (comingSoon).
public class ShopItemButton : MonoBehaviour
{
    public enum Item { Food, Clothes, Tools }

    [SerializeField] Item item;
    [SerializeField] bool comingSoon;           // on for Clothes and Tools for now
    [SerializeField] CoinCounter coins;         // the coin counter on Home
    [SerializeField] TMP_Text label;            // this button's text
    [SerializeField] Button button;             // this button

    EconomyConfig Economy => Services.Config.Economy;
    string Key => "owned_" + item.ToString().ToLower();    // owned_food, owned_clothes, owned_tools

    int Price => item switch
    {
        Item.Food => Economy.foodPrice,
        Item.Clothes => Economy.lookPrice,
        _ => Economy.toolPrice,
    };

    void OnEnable() =>
        label.text = comingSoon ? $"{item}: {Price} coins (soon)" : $"{item}: {Price} coins";

    // Every frame, so every button greys out as soon as coins run low.
    void Update() => button.interactable = !comingSoon && Services.Save.GetInt("coins") >= Price;

    public void Buy()                           // On Click () → ShopItemButton.Buy
    {
        if (comingSoon || Services.Save.GetInt("coins") < Price) return;
        coins.Add(-Price);
        Services.Save.SetInt(Key, Services.Save.GetInt(Key) + 1);
    }
}
