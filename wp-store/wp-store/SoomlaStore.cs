using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Collections.Generic;
using SoomlaWpCore;
using SoomlaWpCore.data;
using SoomlaWpStore.events;
using SoomlaWpStore.domain;
using SoomlaWpStore.data;
using SoomlaWpStore.purchasesTypes;
using SoomlaWpStore.billing.wp.store;
using SoomlaWpStore.exceptions;
namespace SoomlaWpStore
{
    public class SoomlaStore
    {
        
        /**
     * Initializes the SOOMLA SDK.
     * This initializer also initializes {@link com.soomla.store.data.StoreInfo}.
     *
     * @param storeAssets the definition of your application specific assets.
     */
    public bool initialize(IStoreAssets storeAssets, bool testMode) {
        if (mInitialized) {
            String err = "SoomlaStore is already initialized. You can't initialize it twice!";
            handleErrorResult(err);
            return false;
        }

        StoreConfig.STORE_TEST_MODE = testMode;

        StoreManager.OnItemPurchasedCB += handleSuccessfulPurchase;
        StoreManager.OnItemPurchaseCancelCB += handleCancelledPurchase;
        StoreManager.OnListingLoadedCB += refreshMarketItemsDetails;
        StoreManager.GetInstance().Initialize();

        SoomlaUtils.LogDebug(TAG, "SoomlaStore Initializing ...");

        StoreInfo.setStoreAssets(storeAssets);

        // Update SOOMLA store from DB
        StoreInfo.initializeFromDB();

        refreshInventory();

        mInitialized = true;
        EventManager.GetInstance().OnSoomlaStoreInitializedEvent(this,null);
        return true;
    }

    /**
     * Restoring old purchases for the current user (device).
     * Here we just call the private function without refreshing market items details.
     */
    public void restoreTransactions() {
        restoreTransactions(false);
    }

    /**
     * Restoring old purchases for the current user (device).
     *
     * @param followedByRefreshItemsDetails determines weather we should perform a refresh market
     *                                      items operation right after a restore purchase success.
     */
    private void restoreTransactions(bool followedByRefreshItemsDetails) {
        SoomlaUtils.LogDebug(TAG, "TODO restore Transaction");

        EventManager.GetInstance().OnRestoreTransactionsStartedEvent(this, new RestoreTransactionsStartedEventArgs());

        if (StoreConfig.STORE_TEST_MODE)
        {
            foreach (var item in StoreManager.licInfosMock.ProductLicenses)
            {
                if (item.Value.IsActive)
                {
                    SoomlaUtils.LogDebug(TAG, "Got owned item: " + item.Value.ProductId);

                    handleSuccessfulPurchase(item.Value.ProductId);
                }
            }
        }
        else
        {
            foreach (var item in StoreManager.licInfos.ProductLicenses)
            {
                if (item.Value.IsActive)
                {
                    SoomlaUtils.LogDebug(TAG, "Got owned item: " + item.Value.ProductId);

                    handleSuccessfulPurchase(item.Value.ProductId);
                }
            }
        }
        


        EventManager.GetInstance().OnRestoreTransactionsFinishedEvent(this, new RestoreTransactionsFinishedEventArgs(true));

        if (followedByRefreshItemsDetails)
        {
            StoreManager.GetInstance().LoadListingInfo();
        }
        /*
        mInAppBillingService.initializeBillingService(
                new IabCallbacks.IabInitListener() {

                    @Override
                    public void success(boolean alreadyInBg) {
                        if (!alreadyInBg) {
                            notifyIabServiceStarted();
                        }

                        SoomlaUtils.LogDebug(TAG,
                                "Setup successful, restoring purchases");

                        IabCallbacks.OnRestorePurchasesListener restorePurchasesListener = new IabCallbacks.OnRestorePurchasesListener() {
                            @Override
                            public void success(List<IabPurchase> purchases) {
                                SoomlaUtils.LogDebug(TAG, "Transactions restored");

                                if (purchases.size() > 0) {
                                    for (IabPurchase iabPurchase : purchases) {
                                        SoomlaUtils.LogDebug(TAG, "Got owned item: " + iabPurchase.getSku());

                                        handleSuccessfulPurchase(iabPurchase);
                                    }
                                }

                                BusProvider.getInstance().post(
                                        new RestoreTransactionsFinishedEvent(true));

                                if (followedByRefreshItemsDetails) {
                                    refreshMarketItemsDetails();
                                }
                            }

                            @Override
                            public void fail(String message) {
                                BusProvider.getInstance().post(new RestoreTransactionsFinishedEvent(false));
                                handleErrorResult(message);
                            }
                        };

                        mInAppBillingService.restorePurchasesAsync(restorePurchasesListener);

                        BusProvider.getInstance().post(new RestoreTransactionsStartedEvent());
                    }

                    @Override
                    public void fail(String message) {
                        reportIabInitFailure(message);
                    }
                }
        );
        */
    }

    /**
     * Queries the store for the details for all of the game's market items by product ids.
     * This operation will "fill" up the MarketItem objects with the information you provided in
     * the developer console including: localized price (as string), title and description.
     */
    public void refreshMarketItemsDetails(Dictionary<string, MarketProductInfos> marketInfos)
    {
        SoomlaUtils.LogDebug(TAG, "TODO refreshMarketItemsDetails");
        

        List<MarketItem> marketItems = new List<MarketItem>();
        foreach (var mpi in marketInfos)
        {

            String productId = mpi.Value.ProductId;
            String title = mpi.Value.Name;
            String price = mpi.Value.FormattedPrice;
            String desc = mpi.Value.Description;

            try
            {
                PurchasableVirtualItem pvi = StoreInfo.
                        getPurchasableItem(productId);
                MarketItem mi = ((PurchaseWithMarket)
                        pvi.getPurchaseType()).getMarketItem();
                mi.setMarketTitle(title);
                mi.setMarketPrice(price);
                mi.setMarketDescription(desc);

                marketItems.Add(mi);
            }
            catch (VirtualItemNotFoundException e)
            {
                String msg = "(refreshInventory) Couldn't find a "
                        + "purchasable item associated with: " + productId;
                SoomlaUtils.LogError(TAG, msg);
            }
        }
        EventManager.GetInstance().OnMarketItemsRefreshFinishedEvent(this,new MarketItemsRefreshFinishedEventArgs(marketItems));
        
        /*
        mInAppBillingService.initializeBillingService(
                new IabCallbacks.IabInitListener() {

                    @Override
                    public void success(boolean alreadyInBg) {
                        if (!alreadyInBg) {
                            notifyIabServiceStarted();
                        }
                        SoomlaUtils.LogDebug(TAG,
                                "Setup successful, refreshing market items details");

                        IabCallbacks.OnFetchSkusDetailsListener fetchSkusDetailsListener =
                                new IabCallbacks.OnFetchSkusDetailsListener() {

                                    @Override
                                    public void success(List<IabSkuDetails> skuDetails) {
                                        SoomlaUtils.LogDebug(TAG, "Market items details refreshed");

                                        List<MarketItem> marketItems = new ArrayList<MarketItem>();
                                        if (skuDetails.size() > 0) {
                                            for (IabSkuDetails iabSkuDetails : skuDetails) {
                                                String productId = iabSkuDetails.getSku();
                                                String price = iabSkuDetails.getPrice();
                                                String title = iabSkuDetails.getTitle();
                                                String desc = iabSkuDetails.getDescription();

                                                SoomlaUtils.LogDebug(TAG, "Got item details: " +
                                                        "\ntitle:\t" + iabSkuDetails.getTitle() +
                                                        "\nprice:\t" + iabSkuDetails.getPrice() +
                                                        "\nproductId:\t" + iabSkuDetails.getSku() +
                                                        "\ndesc:\t" + iabSkuDetails.getDescription());

                                                try {
                                                    PurchasableVirtualItem pvi = StoreInfo.
                                                            getPurchasableItem(productId);
                                                    MarketItem mi = ((PurchaseWithMarket)
                                                            pvi.getPurchaseType()).getMarketItem();
                                                    mi.setMarketTitle(title);
                                                    mi.setMarketPrice(price);
                                                    mi.setMarketDescription(desc);

                                                    marketItems.add(mi);
                                                } catch (VirtualItemNotFoundException e) {
                                                    String msg = "(refreshInventory) Couldn't find a "
                                                            + "purchasable item associated with: " + productId;
                                                    SoomlaUtils.LogError(TAG, msg);
                                                }
                                            }
                                        }
                                        BusProvider.getInstance().post(new MarketItemsRefreshFinishedEvent(marketItems));
                                    }

                                    @Override
                                    public void fail(String message) {

                                    }
                                };

                        final List<String> purchasableProductIds = StoreInfo.getAllProductIds();
                        mInAppBillingService.fetchSkusDetailsAsync(purchasableProductIds, fetchSkusDetailsListener);

                        BusProvider.getInstance().post(new MarketItemsRefreshStartedEvent());
                    }

                    @Override
                    public void fail(String message) {
                        reportIabInitFailure(message);
                    }
                }
        );
        */
    }

    /**
     * This runs restoreTransactions followed by market items refresh.
     * There are docs that explains restoreTransactions and refreshMarketItemsDetails on the actual
     * functions in this file.
     */
    public void refreshInventory() {
        restoreTransactions(true);
    }

    /**
     * Starts a purchase process in the market.
     *
     * @param marketItem The item to purchase - this item has to be defined EXACTLY the same in
     *                   the market
     * @param payload A payload to get back when this purchase is finished.
     * @throws IllegalStateException
     */
    public void buyWithMarket(MarketItem marketItem, String payload) {
        SoomlaUtils.LogDebug(TAG, "TODO buyWithMarket");
        
        PurchasableVirtualItem pvi;
        try {
            pvi = StoreInfo.getPurchasableItem(marketItem.getProductId());
        } catch (VirtualItemNotFoundException e) {
            String msg = "Couldn't find a purchasable item associated with: " + marketItem.getProductId();
            SoomlaUtils.LogError(TAG, msg);
            EventManager.GetInstance().OnUnexpectedStoreErrorEvent(this, new UnexpectedStoreErrorEventArgs(msg));
            return;
        }

        EventManager.GetInstance().OnMarketPurchaseStartedEvent(this, new MarketPurchaseStartedEventArgs(pvi));
        StoreManager.GetInstance().PurchaseProduct(marketItem.getProductId());
        /*
        mInAppBillingService.initializeBillingService
                (new IabCallbacks.IabInitListener() {

                    @Override
                    public void success(boolean alreadyInBg) {
                        if (!alreadyInBg) {
                            notifyIabServiceStarted();
                        }

                        IabCallbacks.OnPurchaseListener purchaseListener =
                                new IabCallbacks.OnPurchaseListener() {

                                    @Override
                                    public void success(IabPurchase purchase) {
                                        handleSuccessfulPurchase(purchase);
                                    }

                                    @Override
                                    public void cancelled(IabPurchase purchase) {
                                        handleCancelledPurchase(purchase);
                                    }

                                    @Override
                                    public void alreadyOwned(IabPurchase purchase) {
                                        String sku = purchase.getSku();
                                        SoomlaUtils.LogDebug(TAG, "Tried to buy an item that was not" +
                                                " consumed (maybe it's an already owned " +
                                                "NonConsumable). productId: " + sku);

                                        try {
                                            PurchasableVirtualItem pvi = StoreInfo.getPurchasableItem(sku);
                                            consumeIfConsumable(purchase, pvi);

                                            if (pvi instanceof NonConsumableItem) {
                                                String message = "(alreadyOwned) the user tried to " +
                                                        "buy a NonConsumableItem that was already " +
                                                        "owned. itemId: " + pvi.getItemId() +
                                                        "    productId: " + sku;
                                                SoomlaUtils.LogDebug(TAG, message);
                                                BusProvider.getInstance().post(new UnexpectedStoreErrorEvent(message));
                                            }
                                        } catch (VirtualItemNotFoundException e) {
                                            String message = "(alreadyOwned) ERROR : Couldn't find the "
                                                    + "VirtualCurrencyPack with productId: " + sku
                                                    + ". It's unexpected so an unexpected error is being emitted.";
                                            SoomlaUtils.LogError(TAG, message);
                                            BusProvider.getInstance().post(new UnexpectedStoreErrorEvent(message));
                                        }
                                    }

                                    @Override
                                    public void fail(String message) {
                                        handleErrorResult(message);
                                    }
                                };
                        mInAppBillingService.launchPurchaseFlow(marketItem.getProductId(),
                                purchaseListener, payload);
                        BusProvider.getInstance().post(new MarketPurchaseStartedEvent(pvi));
                    }

                    @Override
                    public void fail(String message) {
                        reportIabInitFailure(message);
                    }

                });
         */

    }

    /**
     * Determines if Store Controller is initialized
     *
     * @return true if initialized, false otherwise
     */
    public bool isInitialized() {
        return mInitialized;
    }


    /*==================== Common callbacks for success \ failure \ finish ====================*/

    /**
     * Checks the state of the purchase and responds accordingly, giving the user an item,
     * throwing an error, or taking the item away and paying the user back.
     *
     * @param purchase purchase whose state is to be checked.
     */
    private void handleSuccessfulPurchase(/*IabPurchase*/ string productId) {
        SoomlaUtils.LogDebug(TAG, "TODO handleSuccessfulPurchase");

        PurchasableVirtualItem pvi;
        try
        {
            pvi = StoreInfo.getPurchasableItem(productId);
        }
        catch (VirtualItemNotFoundException e)
        {
            SoomlaUtils.LogError(TAG, "(handleSuccessfulPurchase - purchase or query-inventory) "
                    + "ERROR : Couldn't find the " +
                    " VirtualCurrencyPack OR MarketItem  with productId: " + productId +
                    ". It's unexpected so an unexpected error is being emitted.");
            EventManager.GetInstance().OnUnexpectedStoreErrorEvent(this, new UnexpectedStoreErrorEventArgs("Couldn't find the productId "
                    + "of a product after purchase or query-inventory."));
            return;
        }
        
        SoomlaUtils.LogDebug(TAG, "IabPurchase successful.");

        // if the purchasable item is NonConsumableItem and it already exists then we
        // don't fire any events.
        // fixes: https://github.com/soomla/unity3d-store/issues/192
        if (pvi is NonConsumableItem) {
            bool exists = StorageManager.getNonConsumableItemsStorage().
                    nonConsumableItemExists((NonConsumableItem) pvi);
            if (exists) {
                return;
            }
        }

        EventManager.GetInstance().OnMarketPurchaseEvent(this,new MarketPurchaseEventArgs(pvi,null,null));
        pvi.give(1);
        
        EventManager.GetInstance().OnItemPurchasedEvent(this,new ItemPurchasedEventArgs(pvi, null));
        consumeIfConsumable(pvi);

        /*
        String sku = purchase.getSku();
        String developerPayload = purchase.getDeveloperPayload();
        String token = purchase.getToken();

        PurchasableVirtualItem pvi;
        try {
            pvi = StoreInfo.getPurchasableItem(sku);
        } catch (VirtualItemNotFoundException e) {
            SoomlaUtils.LogError(TAG, "(handleSuccessfulPurchase - purchase or query-inventory) "
                    + "ERROR : Couldn't find the " +
                    " VirtualCurrencyPack OR MarketItem  with productId: " + sku +
                    ". It's unexpected so an unexpected error is being emitted.");
            BusProvider.getInstance().post(new UnexpectedStoreErrorEvent("Couldn't find the sku "
                    + "of a product after purchase or query-inventory."));
            return;
        }

        switch (purchase.getPurchaseState()) {
            case 0:
                SoomlaUtils.LogDebug(TAG, "IabPurchase successful.");

                // if the purchasable item is NonConsumableItem and it already exists then we
                // don't fire any events.
                // fixes: https://github.com/soomla/unity3d-store/issues/192
                if (pvi instanceof NonConsumableItem) {
                    boolean exists = StorageManager.getNonConsumableItemsStorage().
                            nonConsumableItemExists((NonConsumableItem) pvi);
                    if (exists) {
                        return;
                    }
                }

                BusProvider.getInstance().post(new MarketPurchaseEvent
                        (pvi, developerPayload, token));
                pvi.give(1);
                BusProvider.getInstance().post(new ItemPurchasedEvent(pvi, developerPayload));

                consumeIfConsumable(purchase, pvi);

                break;

            case 1:

            case 2:
                SoomlaUtils.LogDebug(TAG, "IabPurchase refunded.");
                if (!StoreConfig.friendlyRefunds) {
                    pvi.take(1);
                }
                BusProvider.getInstance().post(new MarketRefundEvent(pvi, developerPayload));
                break;
        }
        */
    }

    /**
     * Handles a cancelled purchase by either posting an event containing a
     * <code>PurchasableVirtualItem</code> corresponding to the given purchase, or an unexpected
     * error event if the item was not found.
     *
     * @param purchase cancelled purchase to handle.
     */
    private void handleCancelledPurchase(String productId, bool error) {
        SoomlaUtils.LogDebug(TAG, "TODO handleCancelledPurchase");
        
        try {
            PurchasableVirtualItem v = StoreInfo.getPurchasableItem(productId);
            EventManager.GetInstance().OnMarketPurchaseCancelledEvent(this, new MarketPurchaseCancelledEventArgs(v));
        } catch (VirtualItemNotFoundException e) {
            SoomlaUtils.LogError(TAG, "(purchaseActionResultCancelled) ERROR : Couldn't find the "
                    + "VirtualCurrencyPack OR MarketItem  with productId: " + productId
                    + ". It's unexpected so an unexpected error is being emitted.");
            EventManager.GetInstance().OnUnexpectedStoreErrorEvent(this, new UnexpectedStoreErrorEventArgs(e.Message));
        }
        
    }

    /**
     * Consumes the given purchase, or writes error message to log if unable to consume
     *
     * @param purchase purchase to be consumed
     */
    private void consumeIfConsumable(PurchasableVirtualItem pvi) {
        SoomlaUtils.LogDebug(TAG, "TODO consumeIfConsumable");
        
        try {
            if (!(pvi is NonConsumableItem)) {
                StoreManager.GetInstance().Consume(pvi.getItemId());
            }
        } catch (Exception e) {
            SoomlaUtils.LogDebug(TAG, "Error while consuming: itemId: " + pvi.getItemId());
            EventManager.GetInstance().OnUnexpectedStoreErrorEvent(this, new UnexpectedStoreErrorEventArgs(e.Message));
        }
        
    }

    /**
     * Posts an unexpected error event saying the purchase failed.
     *
     * @param message error message.
     */
    private void handleErrorResult(String message) {
        //BusProvider.getInstance().post(new UnexpectedStoreErrorEvent(message));
        EventManager.GetInstance().OnUnexpectedStoreErrorEvent(this,new UnexpectedStoreErrorEventArgs(message));
        SoomlaUtils.LogError(TAG, "ERROR: IabPurchase failed: " + message);
    }

    /* Singleton */
    private static SoomlaStore sInstance = null;

    /**
     * Retrieves the singleton instance of <code>SoomlaStore</code>
     *
     * @return singleton instance of <code>SoomlaStore</code>
     */
    public static SoomlaStore GetInstance() {
        if (sInstance == null) {
            sInstance = new SoomlaStore();
        }
        return sInstance;
    }

    /**
     * Constructor
     */
    private SoomlaStore()
    {
    }


    /* Private Members */

    private const String TAG = "SOOMLA SoomlaStore"; //used for Log messages
    private bool mInitialized = false;
    }
}
