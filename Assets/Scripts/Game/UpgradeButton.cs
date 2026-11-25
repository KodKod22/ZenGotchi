using TMPro;
using UnityEngine;
using UnityEngine.UI;

// UpgradeButton.cs: the food upgrade in the shop.
// Price from the curve in EconomyConfig, coins out, level saved.
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] CoinCounter coins;     // the coin counter on Home
    [SerializeField] TMP_Text label;        // this button's text
    [SerializeField] Button button;         // this button

    public const string Key = "food_upgrade_level";

    EconomyConfig Economy => Services.Config.Economy;
    int Level => Services.Save.GetInt(Key);
    int Cost => Economy.UpgradeCost(Level);
    bool IsMax => Level >= Economy.upgradeMaxLevel;

    void OnEnable() => Show();              // every time the shop opens

    void Update() => button.interactable = !IsMax && Services.Save.GetInt("coins") >= Cost;

    public void Buy()                       // On Click () → UpgradeButton.Buy
    {
        if (IsMax || Services.Save.GetInt("coins") < Cost) return;
        coins.Add(-Cost);                   // the sink: coins out
        Services.Save.SetInt(Key, Level + 1);
        Show();
    }

    void Show() =>
        label.text = IsMax ? "Food upgrade: max" : $"Food upgrade: {Cost} coins";
}