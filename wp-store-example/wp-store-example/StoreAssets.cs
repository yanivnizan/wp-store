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
		return new VirtualGood[] {ALGERIE_GOOD,ALLEMAGNE_GOOD,ANGLETERRE_GOOD,ARGENTINE_GOOD,AUSTRALIE_GOOD,BELGIQUE_GOOD,BOSNIE_GOOD,BRESIL_GOOD,CAMEROUN_GOOD,CHILI_GOOD,COLOMBIE_GOOD,COSTA_RICA_GOOD,COTE_D_IVOIRE_GOOD,CROATIE_GOOD,EQUATEUR_GOOD,ESPAGNE_GOOD,FRANCE_GOOD,GHANA_GOOD,GRECE_GOOD,HONDURAS_GOOD,IRAN_GOOD,ITALIE_GOOD,JAPON_GOOD,MEXIQUE_GOOD,NIGERIA_GOOD,PAYS_BAS_GOOD,PORTUGAL_GOOD,REPUBLIQUE_DE_COREE_GOOD,RUSSIE_GOOD,SUISSE_GOOD,URUGUAY_GOOD,USA_GOOD};
	}
	
	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] {WEAK_SMALL_CURRENCY_PACK,WEAK_MEDIUM_CURRENCY_PACK,WEAK_BIG_CURRENCY_PACK,STRONG_SMALL_CURRENCY_PACK,STRONG_MEDIUM_CURRENCY_PACK,STRONG_BIG_CURRENCY_PACK};
	}
	
	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{COUNTRY_CATEGORY,OTHER_ITEMS_CATEGORY};
	}
	
	public NonConsumableItem[] GetNonConsumableItems() {
		return new NonConsumableItem[]{NO_ADS};
	}
	
	/** Static Final members **/
	public const string WEAK_CURRENCY_ITEM_ID      = "currency_weak";
	public const string STRONG_CURRENCY_ITEM_ID      = "currency_strong";

	public const string ALGERIE_GOOD_ITEM_ID      = "algerie";
	public const string ALLEMAGNE_GOOD_ITEM_ID = "allemagne";
	public const string ANGLETERRE_GOOD_ITEM_ID = "angleterre";
	public const string ARGENTINE_GOOD_ITEM_ID = "argentine";
	public const string AUSTRALIE_GOOD_ITEM_ID = "australie";
	public const string BELGIQUE_GOOD_ITEM_ID = "belgique";
	public const string BOSNIE_GOOD_ITEM_ID = "bosnie";
	public const string BRESIL_GOOD_ITEM_ID = "bresil";
	public const string CAMEROUN_GOOD_ITEM_ID = "cameroun";
	public const string CHILI_GOOD_ITEM_ID = "chili";
	public const string COLOMBIE_GOOD_ITEM_ID = "colombie";
	public const string COSTA_RICA_GOOD_ITEM_ID = "costa_rica";
	public const string COTE_D_IVOIRE_GOOD_ITEM_ID = "cote_d_ivoire";
	public const string CROATIE_GOOD_ITEM_ID = "croatie";
	public const string EQUATEUR_GOOD_ITEM_ID = "equateur";
	public const string ESPAGNE_GOOD_ITEM_ID = "espagne";
	public const string FRANCE_GOOD_ITEM_ID = "france";
	public const string GHANA_GOOD_ITEM_ID = "ghana";
	public const string GRECE_GOOD_ITEM_ID = "grece";
	public const string HONDURAS_GOOD_ITEM_ID = "honduras";
	public const string IRAN_GOOD_ITEM_ID = "iran";
	public const string ITALIE_GOOD_ITEM_ID = "italie";
	public const string JAPON_GOOD_ITEM_ID = "japon";
	public const string MEXIQUE_GOOD_ITEM_ID = "mexique";
	public const string NIGERIA_GOOD_ITEM_ID = "nigeria";
	public const string PAYS_BAS_GOOD_ITEM_ID = "pays_bas";
	public const string PORTUGAL_GOOD_ITEM_ID = "portugal";
	public const string REPUBLIQUE_DE_COREE_GOOD_ITEM_ID = "republique_de_coree";
	public const string RUSSIE_GOOD_ITEM_ID = "russie";
	public const string SUISSE_GOOD_ITEM_ID = "suisse";
	public const string URUGUAY_GOOD_ITEM_ID = "uruguay";
	public const string USA_GOOD_ITEM_ID = "usa";

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

	public static VirtualGood ALGERIE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"Algeria Team",                                       // name
		"", // description
		ALGERIE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500)); // the way this virtual good is purchased

	public static VirtualGood ALLEMAGNE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		ALLEMAGNE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 2000));

	public static VirtualGood ANGLETERRE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		ANGLETERRE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 1000));

	public static VirtualGood ARGENTINE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		ARGENTINE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 1500));

	public static VirtualGood AUSTRALIE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		AUSTRALIE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood BELGIQUE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		BELGIQUE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood BOSNIE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		BOSNIE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood BRESIL_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		BRESIL_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 3000));

	public static VirtualGood CAMEROUN_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		CAMEROUN_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood CHILI_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		CHILI_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood COLOMBIE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		COLOMBIE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood COSTA_RICA_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		COSTA_RICA_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood COTE_D_IVOIRE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		COTE_D_IVOIRE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood CROATIE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		CROATIE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood EQUATEUR_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		EQUATEUR_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood ESPAGNE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		ESPAGNE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 1000));

	public static VirtualGood FRANCE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		FRANCE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 1000));

	public static VirtualGood GHANA_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		GHANA_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood GRECE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		GRECE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood HONDURAS_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		HONDURAS_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood IRAN_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		IRAN_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood ITALIE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		ITALIE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 2500));

	public static VirtualGood JAPON_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		JAPON_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood MEXIQUE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		MEXIQUE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood NIGERIA_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		NIGERIA_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood PAYS_BAS_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		PAYS_BAS_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood PORTUGAL_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		PORTUGAL_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood REPUBLIQUE_DE_COREE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		REPUBLIQUE_DE_COREE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood RUSSIE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		RUSSIE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood SUISSE_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		SUISSE_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	public static VirtualGood URUGUAY_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		URUGUAY_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 1500));

	public static VirtualGood USA_GOOD = new EquippableVG(
		EquippableVG.EquippingModel.CATEGORY,
		"",                                       // name
		"", // description
		USA_GOOD_ITEM_ID,                                       // item id
		new PurchaseWithVirtualItem(WEAK_CURRENCY_ITEM_ID, 500));

	/** Virtual Categories **/

	public static VirtualCategory COUNTRY_CATEGORY = new VirtualCategory(
		"Country", new List<string>(new string[] { ALGERIE_GOOD_ITEM_ID,ALLEMAGNE_GOOD_ITEM_ID,ANGLETERRE_GOOD_ITEM_ID,ARGENTINE_GOOD_ITEM_ID,AUSTRALIE_GOOD_ITEM_ID,BELGIQUE_GOOD_ITEM_ID,BOSNIE_GOOD_ITEM_ID,BRESIL_GOOD_ITEM_ID,CAMEROUN_GOOD_ITEM_ID,CHILI_GOOD_ITEM_ID,COLOMBIE_GOOD_ITEM_ID,COSTA_RICA_GOOD_ITEM_ID,COTE_D_IVOIRE_GOOD_ITEM_ID,CROATIE_GOOD_ITEM_ID,EQUATEUR_GOOD_ITEM_ID,ESPAGNE_GOOD_ITEM_ID,FRANCE_GOOD_ITEM_ID,GHANA_GOOD_ITEM_ID,GRECE_GOOD_ITEM_ID,HONDURAS_GOOD_ITEM_ID,IRAN_GOOD_ITEM_ID,ITALIE_GOOD_ITEM_ID,JAPON_GOOD_ITEM_ID,MEXIQUE_GOOD_ITEM_ID,NIGERIA_GOOD_ITEM_ID,PAYS_BAS_GOOD_ITEM_ID,PORTUGAL_GOOD_ITEM_ID,REPUBLIQUE_DE_COREE_GOOD_ITEM_ID,RUSSIE_GOOD_ITEM_ID,SUISSE_GOOD_ITEM_ID,URUGUAY_GOOD_ITEM_ID,USA_GOOD_ITEM_ID })
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

