using System;
using System.Net;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Linq;
using SoomlaWpCore;
using SoomlaWpStore.events;


using MockIAPLib;
using MockStore = MockIAPLib;
using MockCurApp = MockIAPLib.CurrentApp;

using Store = Windows.ApplicationModel.Store;
using CurApp = Windows.ApplicationModel.Store.CurrentApp;



namespace SoomlaWpStore.billing.wp.store
{
    public class StoreManager
    {
        static private StoreManager instance;
        public static Store.LicenseInformation licInfos;
        public static Store.ListingInformation listingInfos;
        public static MockStore.LicenseInformation licInfosMock;
        public static MockStore.ListingInformation listingInfosMock;

        public static Dictionary<string,MarketProductInfos> marketProductInfos;
        private bool Initialized = false;

        public void Initialize()
        {
            if(Initialized==false)
            {
                if (StoreConfig.STORE_TEST_MODE)
                {
                    SoomlaUtils.LogDebug(TAG, "WARNING You are running in Store Test Mode! Don't forget to disable the test mode before you publish the app.");
                    SetupMockIAP();
                    licInfosMock = MockCurApp.LicenseInformation;

                }
                else
                {
                    licInfos = CurApp.LicenseInformation;
                }
                marketProductInfos = new Dictionary<string, MarketProductInfos>();
                //LoadListingInfo();
                Initialized = true;
            }
        }

        public async void LoadListingInfo()
        {
            EventManager.GetInstance().OnMarketItemsRefreshStartedEvent(this, new MarketItemsRefreshStartedEventArgs());
            try
            {

                
                if (StoreConfig.STORE_TEST_MODE)
                {
                    listingInfosMock = await MockStore.CurrentApp.LoadListingInformationAsync();

                    marketProductInfos.Clear();
                    if (listingInfosMock.ProductListings.Count > 0)
                    {
                        foreach (KeyValuePair<string, MockStore.ProductListing> pair in listingInfosMock.ProductListings)
                        {
                            MarketProductInfos marketProduct = new MarketProductInfos();
                            marketProduct.Name = pair.Value.Name;
                            marketProduct.Description = pair.Value.Description;
                            marketProduct.FormattedPrice = pair.Value.FormattedPrice;
                            marketProduct.ImageUri = pair.Value.ImageUri;
                            marketProduct.Keywords = pair.Value.Keywords;
                            marketProduct.ProductId = pair.Value.ProductId;

                            switch (pair.Value.ProductType)
                            {
                                case Windows.ApplicationModel.Store.ProductType.Consumable:
                                    marketProduct.ProductType = MarketProductInfos.MarketProductType.CONSUMABLE;
                                    break;
                                case Windows.ApplicationModel.Store.ProductType.Durable:
                                    marketProduct.ProductType = MarketProductInfos.MarketProductType.DURABLE;
                                    break;
                                case Windows.ApplicationModel.Store.ProductType.Unknown:
                                    marketProduct.ProductType = MarketProductInfos.MarketProductType.UNKNOWN;
                                    break;
                            }
                            marketProduct.Tag = pair.Value.Tag;
                            marketProductInfos.Add(pair.Key, marketProduct);
                        }
                    }
                }
                else
                {
                    listingInfos = await Store.CurrentApp.LoadListingInformationAsync();
                    IReadOnlyDictionary<string, Store.ProductListing> productListing;
                    productListing = listingInfos.ProductListings;

                    marketProductInfos.Clear();
                    if (productListing.Count > 0)
                    {
                        foreach (KeyValuePair<string, Store.ProductListing> pair in listingInfos.ProductListings)
                        {
                            MarketProductInfos marketProduct = new MarketProductInfos();
                            marketProduct.Name = pair.Value.Name;
                            marketProduct.Description = pair.Value.Description;
                            marketProduct.FormattedPrice = pair.Value.FormattedPrice;
                            marketProduct.ImageUri = pair.Value.ImageUri;
                            marketProduct.Keywords = pair.Value.Keywords;
                            marketProduct.ProductId = pair.Value.ProductId;

                            switch (pair.Value.ProductType)
                            {
                                case Windows.ApplicationModel.Store.ProductType.Consumable:
                                    marketProduct.ProductType = MarketProductInfos.MarketProductType.CONSUMABLE;
                                    break;
                                case Windows.ApplicationModel.Store.ProductType.Durable:
                                    marketProduct.ProductType = MarketProductInfos.MarketProductType.DURABLE;
                                    break;
                                case Windows.ApplicationModel.Store.ProductType.Unknown:
                                    marketProduct.ProductType = MarketProductInfos.MarketProductType.UNKNOWN;
                                    break;
                            }
                            marketProduct.Tag = pair.Value.Tag;
                            marketProductInfos.Add(pair.Key, marketProduct);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                
            }

            OnListingLoadedCB(marketProductInfos);
        }

        private void SetupMockIAP()
        {
            MockIAP.Init();

            MockIAP.RunInMockMode(true);
            MockIAP.SetListingInformation(1, "en-us", "A description", "0", "TestApp");
            var xDocument = XDocument.Load("IAPMock.xml");
            SoomlaUtils.LogDebug(TAG,"WStorePlugin Mock XML "+xDocument.ToString());
            
            MockIAP.PopulateIAPItemsFromXml(xDocument.ToString());
        }

        static public StoreManager GetInstance()
        {
            if (instance == null)
            {
                instance = new StoreManager();
            }
            return instance;
        }

        public void PurchaseProduct(string productId)
        {
            if (Dispatcher.InvokeOnUIThread == null)
            {
                try
                {
                    DoPurchase(productId);
                }
                catch (Exception e)
                {
                    SoomlaUtils.LogError(TAG, e.Message+"\nDid you setup the Dispatcher UIThread ?");
                }
            }
            else
            {
                Dispatcher.InvokeOnUIThread(() =>
                {
                    DoPurchase(productId);
                });
            }
        }

        public async void DoPurchase(string productId)
        {
            try
            {
                bool licenceActiv = false;
                if (StoreConfig.STORE_TEST_MODE)
                {
                    // Kick off purchase; don't ask for a receipt when it returns
                    await MockCurApp.RequestProductPurchaseAsync(productId, false);
                    licInfosMock = MockCurApp.LicenseInformation;
                    licenceActiv = licInfosMock.ProductLicenses[productId].IsActive;
                }
                else
                {
                    // Kick off purchase; don't ask for a receipt when it returns
                    await CurApp.RequestProductPurchaseAsync(productId, false);
                    licInfos = CurApp.LicenseInformation;
                    licenceActiv = licInfos.ProductLicenses[productId].IsActive;
                }

                if (licenceActiv)
                {
                    
                    OnItemPurchasedCB(productId);
                }
                else
                {
                    SoomlaUtils.LogDebug(TAG,"Purchase cancelled " + productId);
                    OnItemPurchaseCancelCB(productId, false);
                }


            }
            catch (Exception ex)
            {
                // When the user does not complete the purchase (e.g. cancels or navigates back from the Purchase Page), an exception with an HRESULT of E_FAIL is expected.
                SoomlaUtils.LogDebug(TAG,ex.Message);
                OnItemPurchaseCancelCB(productId, true);
            }
        }

        public void Consume(string productId)
        {
            SoomlaUtils.LogDebug(TAG, "WStorePlugin consume " + productId);
            try
            {
                if (StoreConfig.STORE_TEST_MODE)
                {
                    MockCurApp.ReportProductFulfillment(productId);
                }
                else
                {
                    CurApp.ReportProductFulfillment(productId);
                }
            }
            catch (InvalidOperationException e)
            {
                SoomlaUtils.LogDebug(TAG, e.Message);
            }
        }
        

        public bool IsPurchased(string productId)
        {
            bool isPurchased = false;
            SoomlaUtils.LogDebug(TAG,"Licence " + productId + " " + licInfos.ProductLicenses[productId].IsActive.ToString());

            bool containKey = false;
            if (StoreConfig.STORE_TEST_MODE)
            {
                containKey = licInfosMock.ProductLicenses.ContainsKey(productId);
            }
            else
            {
                containKey = licInfos.ProductLicenses.ContainsKey(productId);
            }

            if (containKey)
            {
                if (StoreConfig.STORE_TEST_MODE)
                {
                    isPurchased = licInfosMock.ProductLicenses[productId].IsActive;
                }
                else
                {
                    isPurchased = licInfos.ProductLicenses[productId].IsActive;
                }
                
                SoomlaUtils.LogDebug(TAG,productId + " has licence");
                if (isPurchased)
                {
                    SoomlaUtils.LogDebug(TAG,productId + " has active licence");
                }
            }
            else
            {
                SoomlaUtils.LogDebug(TAG,productId + " no license");
            }

            return isPurchased;
        }

        public delegate void OnItemPurchasedEventHandler(string _item);
        public static event OnItemPurchasedEventHandler OnItemPurchasedCB;

        public delegate void OnItemPurchaseCancelEventHandler(string _item, bool _error);
        public static event OnItemPurchaseCancelEventHandler OnItemPurchaseCancelCB;

        public delegate void OnListingLoadedEventHandler(Dictionary<string,MarketProductInfos> marketInfos);
        public static event OnListingLoadedEventHandler OnListingLoadedCB;

        private const String TAG = "SOOMLA StoreManager"; //used for Log messages
    }

}
