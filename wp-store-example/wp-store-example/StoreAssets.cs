using System;
using System.Collections;
using System.Collections.Generic;
using SoomlaWpStore;
using SoomlaWpStore.domain;
using SoomlaWpStore.domain.virtualGoods;
using SoomlaWpStore.domain.virtualCurrencies;
using SoomlaWpStore.purchasesTypes;

public class StoreAssets : IStoreAssets {

	public int GetVersion() {
		return 1;
	}
	
	public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{WEAK_CURRENCY,STRONG_CURRENCY};
	}
	
	public VirtualGood[] GetGoods() {
        return new VirtualGood[] { GLOVE1_GOOD, GLOVE2_GOOD, GLOVE3_GOOD, BOOTS1_GOOD, BOOTS2_GOOD, BOOTS3_GOOD };
	}
	
	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] {WEAK_SMALL_CURRENCY_PACK,WEAK_MEDIUM_CURRENCY_PACK,WEAK_BIG_CURRENCY_PACK,STRONG_SMALL_CURRENCY_PACK,STRONG_MEDIUM_CURRENCY_PACK,STRONG_BIG_CURRENCY_PACK};
	}
	
	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{GLOVE_CATEGORY,BOOTS_CATEGORY,OTHER_ITEMS_CATEGORY};
	}
	
	public NonConsumableItem[] GetNonConsumableItems() {
		return new NonConsumableItem[]{NO_ADS};
	}
	
	/** Static Final members **/
	public const string WEAK_CURRENCY_ITEM_ID      = "currency_weak";
	public const string STRONG_CURRENCY_ITEM_ID      = "currency_strong";

    public const string GLOVE1_GOOD_ITEM_ID = "glove1";
    public const string GLOVE2_GOOD_ITEM_ID = "glove2";
    public const string GLOVE3_GOOD_ITEM_ID = "glove3";

    public const string BOOTS1_GOOD_ITEM_ID = "boots1";
    public const string BOOTS2_GOOD_ITEM_ID = "boots2";
    public const string BOOTS3_GOOD_ITEM_ID = "boots3";


	public const string WEAK_SMALL_CURRENCY_PACK_ID = "weakpack1";
	public const string WEAK_MEDIUM_CURRENCY_PACK_ID = "weakpack2";
	public const string WEAK_BIG_CURRENCY_PACK_ID = "weakpack3";

	public const string STRONG_SMALL_CURRENCY_PACK_ID = "strongpack1";
	public const string STRONG_MEDIUM_CURRENCY_PACK_ID = "strongpack2";
	public const string STRONG_BIG_CURRENCY_PACK_ID = "strongpack3";
	

	public static string NO_ADS_ID = "noads";


	/** Virtual Currencies **/

		public static VirtualCurrency WEAK_CURRENCY = new VirtualCurrency(
		"Coin",
		"",
		WEAK_CURRENCY_ITEM_ID
		);

		public static VirtualCurrency STRONG_CURRENCY = new VirtualCurrency(
		"Gem",
		"",
		STRONG_CURRENCY_ITEM_ID
		);

	
	/** Virtual Currency Packs **/

	public static VirtualCurrencyPack STRONG_SMALL_CURRENCY_PACK = new VirtualCurrencyPack(
		"",                                   // name
		"",                       // description
		STRONG_SMALL_CURRENCY_PACK_ID,                                   // item id
		4,												// number of currencies in the pack
		STRONG_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(STRONG_SMALL_CURRENCY_PACK_ID, 0.99)
		);

	public static VirtualCurrencyPack STRONG_MEDIUM_CURRENCY_PACK = new VirtualCurrencyPack(
		"",                                   // name
		"",                       // description
		STRONG_MEDIUM_CURRENCY_PACK_ID,                                   // item id
		10,												// number of currencies in the pack
		STRONG_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(STRONG_MEDIUM_CURRENCY_PACK_ID, 0.99)
		);

	public static VirtualCurrencyPack STRONG_BIG_CURRENCY_PACK = new VirtualCurrencyPack(
		"",                                   // name
		"",                       // description
		STRONG_BIG_CURRENCY_PACK_ID,                                   // item id
		45,												// number of currencies in the pack
		STRONG_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(STRONG_BIG_CURRENCY_PACK_ID, 0.99)
		);

	public static VirtualCurrencyPack WEAK_SMALL_CURRENCY_PACK = new VirtualCurrencyPack(
		"",                                   // name
		"",                       // description
		WEAK_SMALL_CURRENCY_PACK_ID,                                   // item id
		500,												// number of currencies in the pack
		WEAK_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithVirtualItem(STRONG_CURRENCY_ITEM_ID, 1)
		);

	public static VirtualCurrencyPack WEAK_MEDIUM_CURRENCY_PACK = new VirtualCurrencyPack(
		"",                                   // name
		"",                       // description
		WEAK_MEDIUM_CURRENCY_PACK_ID,                                   // item id
		3000,												// number of currencies in the pack
		WEAK_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithVirtualItem(STRONG_CURRENCY_ITEM_ID, 5)
		);

	public static VirtualCurrencyPack WEAK_BIG_CURRENCY_PACK = new VirtualCurrencyPack(
		"",                                   // name
		"",                       // description
		WEAK_BIG_CURRENCY_PACK_ID,                                   // item id
		8000,												// number of currencies in the pack
		WEAK_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithVirtualItem(STRONG_CURRENCY_ITEM_ID, 12)
		);


	
	/** Virtual Goods **/

	public static VirtualGood GLOVE1_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"Glove1",                                       // name
		"", // description
		GLOVE1_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500)); // the way this virtual good is purchased

    public static VirtualGood GLOVE2_GOOD = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Glove2",                                       // name
        "", // description
        GLOVE2_GOOD_ITEM_ID,                                       // item id
        new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500)); // the way this virtual good is purchased

    public static VirtualGood GLOVE3_GOOD = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Glove3",                                       // name
        "", // description
        GLOVE3_GOOD_ITEM_ID,                                       // item id
        new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500)); // the way this virtual good is purchased

    public static VirtualGood BOOTS1_GOOD = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Boots1",                                       // name
        "", // description
        BOOTS1_GOOD_ITEM_ID,                                       // item id
        new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500)); // the way this virtual good is purchased

    public static VirtualGood BOOTS2_GOOD = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Boots2",                                       // name
        "", // description
        BOOTS2_GOOD_ITEM_ID,                                       // item id
        new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500)); // the way this virtual good is purchased
    
    public static VirtualGood BOOTS3_GOOD = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Boots3",                                       // name
        "", // description
        BOOTS3_GOOD_ITEM_ID,                                       // item id
        new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500)); // the way this virtual good is purchased
	
    

	/** Virtual Categories **/

	public static VirtualCategory GLOVE_CATEGORY = new VirtualCategory(
		"Glove", new List<string>(new string[] { GLOVE1_GOOD_ITEM_ID,GLOVE2_GOOD_ITEM_ID,GLOVE3_GOOD_ITEM_ID })
	);

    public static VirtualCategory BOOTS_CATEGORY = new VirtualCategory(
        "Boots", new List<string>(new string[] { BOOTS1_GOOD_ITEM_ID, BOOTS2_GOOD_ITEM_ID, BOOTS3_GOOD_ITEM_ID })
    );

	public static VirtualCategory OTHER_ITEMS_CATEGORY = new VirtualCategory(
		"OtherItems", new List<string>(new string[] {WEAK_SMALL_CURRENCY_PACK_ID,WEAK_MEDIUM_CURRENCY_PACK_ID,WEAK_BIG_CURRENCY_PACK_ID,STRONG_SMALL_CURRENCY_PACK_ID,STRONG_MEDIUM_CURRENCY_PACK_ID,STRONG_BIG_CURRENCY_PACK_ID,NO_ADS_ID})
		);

	
	/** Market MANAGED Items **/

	public static NonConsumableItem NO_ADS  = new NonConsumableItem(
		"No Ads",
		"Remove all ads from the game.",
		NO_ADS_ID,
		new PurchaseWithMarket(new MarketItem(NO_ADS_ID, MarketItem.Managed.MANAGED , 0.99))
		);
	
}

