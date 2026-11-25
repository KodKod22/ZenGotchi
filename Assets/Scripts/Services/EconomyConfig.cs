using UnityEngine;

// EconomyConfig.cs: every economy number of ZenGotchi in one asset.
// Code only reads this asset. Values that change in play (coins, levels) go through Services.Save.
[CreateAssetMenu(menuName = "Game/Economy Config")]
public class EconomyConfig : ScriptableObject
{
    [Header("Sources")]
    public int coinsPerStar = 5;                // Hoops: each star pays this
    public int coinsPerStudyHour = 5;           // app closed during a study session
    public int dailyGift = 5;
    public int adReward = 10;                   // rewarded ad, later in the course

    [Header("Shop: every item, by category")]
    public ShopItem[] shopItems =
    {
        new ShopItem { name = "Apple",    category = ShopCategory.Food,    price = 5 },
        new ShopItem { name = "Sandwich", category = ShopCategory.Food,    price = 8 },
        new ShopItem { name = "Cake",     category = ShopCategory.Food,    price = 12 },
        new ShopItem { name = "Hat",      category = ShopCategory.Clothes, price = 10, comingSoon = true },
        new ShopItem { name = "Scarf",    category = ShopCategory.Clothes, price = 15, comingSoon = true },
        new ShopItem { name = "Ball",     category = ShopCategory.Tools,   price = 15, comingSoon = true },
        new ShopItem { name = "Toy",      category = ShopCategory.Tools,   price = 20, comingSoon = true },
    };

    [Header("Food upgrade, the big sink")]
    public int upgradeBaseCost = 10;
    public float upgradeGrowth = 1.25f;
    public int upgradeMaxLevel = 8;

    [Header("Hunger")]
    public int fullnessPerMeal = 25;            // percent of the bar
    public float hoursToEmpty = 12f;            // upgrade level 0
    public float hoursToEmptyPerUpgrade = 1.2f; // each food upgrade adds this

    [Header("Sleep, no hunger while asleep")]
    public int sleepStartHour = 22;
    public int sleepEndHour = 6;

    // Price of the next upgrade: base x growth, once per level bought. Same as the sheet.
    public int UpgradeCost(int level) =>
        Mathf.RoundToInt(upgradeBaseCost * Mathf.Pow(upgradeGrowth, level));

    // Hours for a full bar to empty at this upgrade level.
    public float HoursToEmpty(int upgradeLevel) =>
        hoursToEmpty + hoursToEmptyPerUpgrade * upgradeLevel;
}

public enum ShopCategory { Food, Clothes, Tools }

[System.Serializable]
public class ShopItem
{
    public string name;
    public ShopCategory category;
    public int price;
    public bool comingSoon;     // shown with its price, but not for sale yet
}
