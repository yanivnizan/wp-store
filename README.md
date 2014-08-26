wp-store
========

Windows Phone 8 F2P game economy library.

Part of The SOOMLA Project - framework for virtual economies in mobile games.  http://project.soom.la

*This project is a part of The [SOOMLA](http://www.soom.la) Framework which is a series of open source initiatives with a joint goal to help mobile game developers do more together. SOOMLA encourages better game designing, economy modeling and faster development.*

Haven't you ever wanted an in-app purchase one liner that looks like this ?!

```cs
StoreInventory.BuyItem("[itemId]");
```

wp-store
---

*SOOMLA's Store Module for Windows Phone 8*

**August 25th, 2014**: v0.0.1 

* More documentation and information in SOOMLA's [Knowledge Base](http://know.soom.la/docs/platforms/unity)  
* For issues you can use the [issues](https://github.com/soomla/wp-store/issues) section or SOOMLA's [Answers Website](http://answers.soom.la)

wp-store is the Windows Phone 8 flavor of SOOMLA's Store Module.

## Economy Model
![SOOMLA's Economy Model](http://know.soom.la/img/tutorial_img/soomla_diagrams/EconomyModel.png)

## Download

The core module [soomla-wp-core master](https://github.com/soomla/soomla-wp-core/archive/master.zip)  
The store module [wp-store master](https://github.com/soomla/wp-store/archive/master.zip)

>When downloading Zip instead of cloning recursive, don't forget to add the soomla-wp-core module on the "submodules\soomla-wp-core" folder or you will have to referenced soomla-wp-core manually in the example and wp-store projects.

## Debugging

If you want to see full debug messages from wp-store you just need to add before initialization:

```cs
SoomlaConfig.logDebug = true;
```	

## Cloning

There are some necessary files in submodules. If you're cloning the project make sure you clone it with the `--recursive` flag.

```
$ git clone --recursive git@github.com:soomla/wp-store.git
```

## Getting Started

1. Download the [soomla-wp-core](https://github.com/soomla/soomla-wp-core/archive/master.zip) and [wp-store](https://github.com/soomla/wp-store/archive/master.zip)
2. Add wp-store.csproj and soomla-wp-core.csproj to your solution and reference them in your project.
3. Initialize the soomla-wp-core module with the encryption key and the testMode switch.
	
	```cs
	bool testMode = true;
    Soomla.Initialize("YOU_ENCRYPTION_KEY",testMode);
    ```

4. Create your own implementation of _IStoreAssets_ in order to describe your specific game's assets ([example](https://github.com/soomla/wp-store/blob/master//wp-store-example/wp-store-example/StoreAssets.cs)). Initialize _SoomlaStore_ with the class you just created:

    ```cs
    SoomlaStore.Initialize(new YourStoreAssetsImplementation());
    ```

    > Initialize _SoomlaStore_ ONLY ONCE when your application loads.

    > You can initialize _SoomlaStore_ in the "MainPage()" function of a 'PhoneApplicationPage'.

5. Write IAPMock.xml ([example](https://github.com/soomla/wp-store-example/blob/master//wp-store-example/wp-store-example/IAPMock.xml)) and save it in the root folder of your project.

This file is used in TestMode to simulate the Windows Store informations. YOU DON'T NEED TO PUBLISH YOUR APP IN THE BETA PROGRAM TO TEST IAP!

6. You'll need an event handler in order to be notified about in-app purchasing related events. refer to the [Event Handling](https://github.com/soomla/wp-store#event-handling) section for more information.

And that's it ! You have storage and in-app purchasing capabilities... ALL-IN-ONE.

## What's next? In App Purchasing.

When we implemented modelV3, we were thinking about ways that people buy things inside apps. We figured out many ways you can let your users purchase stuff in your game and we designed the new modelV3 to support 2 of them: PurchaseWithMarket and PurchaseWithVirtualItem.

**PurchaseWithMarket** is a PurchaseType that allows users to purchase a VirtualItem with the Windows Store.  
**PurchaseWithVirtualItem** is a PurchaseType that lets your users purchase a VirtualItem with a different VirtualItem. For Example: Buying 1 Sword with 100 Gems.

In order to define the way your various virtual items (Goods, Coins ...) are purchased, you'll need to create your implementation of IStoreAsset (the same one from step 4 in the "Getting Started" above).

Here is an example:

Lets say you have a _VirtualCurrencyPack_ you call `TEN_COINS_PACK` and a _VirtualCurrency_ you call `COIN_CURRENCY`:

```cs
VirtualCurrencyPack TEN_COINS_PACK = new VirtualCurrencyPack(
	            "10 Coins",                    // name
	            "A pack of 10 coins",      // description
	            "10_coins",                    // item id
				10,								// number of currencies in the pack
	            COIN_CURRENCY_ITEM_ID,         // the currency associated with this pack
	            new PurchaseWithMarket("com.soomla.ten_coin_pack", 1.99)
		);
```

Now you can use _StoreInventory_ to buy your new VirtualCurrencyPack:

```cs
StoreInventory.buyItem(TEN_COINS_PACK.ItemId);
```

And that's it! wp-store knows how to contact Windows Store for you and will redirect your users to their purchasing system to complete the transaction. Don't forget to subscribe to store events in order to get the notified of successful or failed purchases (see [Event Handling](https://github.com/soomla/wp-store#event-handling)).


Storage & Meta-Data
---

When you initialize _SoomlaStore_, it automatically initializes two other classes: _StoreInventory_ and _StoreInfo_:  
* _StoreInventory_ is a convenience class to let you perform operations on VirtualCurrencies and VirtualGoods. Use it to fetch/change the balances of VirtualItems in your game (using their ItemIds!)  
* _StoreInfo_ is where all meta data information about your specific game can be retrieved. It is initialized with your implementation of `IStoreAssets` and you can use it to retrieve information about your specific game.

The on-device storage is encrypted and kept in a SQLite database. SOOMLA is preparing a cloud-based storage service that will allow this SQLite to be synced to a cloud-based repository that you'll define.

**Example Usages**

* Get VirtualCurrency with itemId "currency_coin":

    ```cs
    VirtualCurrency coin = StoreInfo.GetVirtualCurrencyByItemId("currency_coin");
    ```

* Give the user 10 pieces of a virtual currency with itemId "currency_coin":

    ```cs
    StoreInventory.GiveItem("currency_coin", 10);
    ```

* Take 10 virtual goods with itemId "green_hat":

    ```cs
    StoreInventory.TakeItem("green_hat", 10);
    ```

* Get the current balance of green hats (virtual goods with itemId "green_hat"):

    ```cs
    int greenHatsBalance = StoreInventory.GetItemBalance("green_hat");
    ```

Event Handling
---

SOOMLA lets you subscribe to store events, get notified and implement your own application specific behavior to those events.

> Your behavior is an addition to the default behavior implemented by SOOMLA. You don't replace SOOMLA's behavior.

The 'StoreEvents' class is where all event go through. To handle various events, just add your specific behavior to the delegates in the Events class.

For example, if you want to 'listen' to a MarketPurchase event:

```cs
StoreEvents.GetInstance().OnMarketPurchase += new MarketPurchaseEventHandler(onMarketPurchase);

public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, string purchaseToken) {
    Debug.Log("Just purchased an item with itemId: " + pvi.ItemId);
}
```

One thing you need to make sure is that you add your specific behavior before initializing SoomlaStore.  

you'll need to do:
````
StoreEvents.GetInstance().OnCurrencyBalanceChangedEvent += new CurrencyBalanceChangedEventHandler(UpdateCurrencyBalance);
````
before
````
SoomlaStore.Initialize(new Soomla.Example.MuffinRushAssets());
````

Contribution
---

We want you!

Fork -> Clone -> Implement -> Insert Comments -> Test -> Pull-Request.

We have great RESPECT for contributors.

Code Documentation
---

wp-store follows strict code documentation conventions. If you would like to contribute please read our [Documentation Guidelines](https://github.com/soomla/wp-store/tree/master/documentation.md) and follow them. Clear, consistent  comments will make our code easy to understand.

SOOMLA, Elsewhere ...
---

+ [Framework Website](http://www.soom.la/)
+ [On Facebook](https://www.facebook.com/pages/The-SOOMLA-Project/389643294427376).
+ [On AngelList](https://angel.co/the-soomla-project)

License
---
Apache License. Copyright (c) 2012-2014 SOOMLA. http://www.soom.la
+ http://opensource.org/licenses/Apache-2.0