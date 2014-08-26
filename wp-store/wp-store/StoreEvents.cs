/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoomlaWpCore;
using SoomlaWpStore.domain;
using SoomlaWpStore.domain.virtualGoods;
using SoomlaWpStore.domain.virtualCurrencies;
namespace SoomlaWpStore
{
    public delegate void ItemPurchaseStartedEventHandler(PurchasableVirtualItem purchasableVirtualItem);
    public delegate void ItemPurchasedEventHandler(PurchasableVirtualItem purchasableVirtualItem, String payload);
    public delegate void SoomlaStoreInitializedEventHandler();
    public delegate void UnexpectedStoreErrorEventHandler(String message);
    public delegate void GoodUpgradeEventHandler(VirtualGood good, UpgradeVG upgradeVG);
    public delegate void GoodBalanceChangedEventHandler(VirtualGood good, int balance, int amountAdded);
    public delegate void GoodEquippedEventHandler(EquippableVG good);
    public delegate void GoodUnEquippedEventHandler(EquippableVG good);
    public delegate void CurrencyBalanceChangedEventHandler(VirtualCurrency currency, int balance, int amountAdded);
    public delegate void MarketItemsRefreshStartedEventHandler();
    public delegate void MarketItemsRefreshFinishedEventHandler(List<MarketItem> marketItems);
    public delegate void RestoreTransactionsStartedEventHandler();
    public delegate void RestoreTransactionsFinishedEventHandler(bool success);
    public delegate void MarketPurchaseStartedEventHandler(PurchasableVirtualItem purchasableVirtualItem);
    public delegate void MarketPurchaseEventHandler(PurchasableVirtualItem purchasableVirtualItem, String payload,String token);
    public delegate void MarketPurchaseCancelledEventHandler(PurchasableVirtualItem purchasableVirtualItem);

    public class StoreEvents
    {
        public static StoreEvents instance;
        public static StoreEvents GetInstance()
        {
            if(instance==null)
            {
                instance = new StoreEvents();
            }
            return instance;
        }
        
        public event ItemPurchaseStartedEventHandler OnItemPurchaseStartedEvent;
        public void PostItemPurchaseStartedEvent(PurchasableVirtualItem purchasableVirtualItem)
        {
            LogEvent("ItemPurchaseStarted");
            if (OnItemPurchaseStartedEvent != null)
            {
                OnItemPurchaseStartedEvent(purchasableVirtualItem);
            }
        }

        public event ItemPurchasedEventHandler OnItemPurchasedEvent;
        public void PostItemPurchasedEvent(PurchasableVirtualItem purchasableVirtualItem, String payload)
        {
            LogEvent("ItemPurchased");
            if (OnItemPurchasedEvent != null)
            {
                OnItemPurchasedEvent(purchasableVirtualItem,payload);
            }
        }

        public event SoomlaStoreInitializedEventHandler OnSoomlaStoreInitializedEvent;
        public void PostSoomlaStoreInitializedEvent()
        {
            LogEvent("SoomlaStoreInitialized");
            if (OnSoomlaStoreInitializedEvent != null)
            {
                OnSoomlaStoreInitializedEvent();
            }
        }

        public event UnexpectedStoreErrorEventHandler OnUnexpectedStoreErrorEvent;
        public void PostUnexpectedStoreErrorEvent(String message)
        {
            LogEvent("UnexpectedStoreError message:"+message);
            if (OnUnexpectedStoreErrorEvent != null)
            {
                OnUnexpectedStoreErrorEvent(message);
            }
        }

        public event GoodUpgradeEventHandler OnGoodUpgradeEvent;
        public void PostGoodUpgradeEvent(VirtualGood good, UpgradeVG upgradeVG)
        {
            LogEvent("GoodUpgrade");
            if (OnGoodUpgradeEvent != null)
            {
                OnGoodUpgradeEvent(good, upgradeVG);
            }
        }

        public event GoodBalanceChangedEventHandler OnGoodBalanceChangedEvent;
        public void PostGoodBalanceChangedEvent(VirtualGood good, int balance, int amountAdded)
        {
            LogEvent("GoodBalanceChanged");
            if (OnGoodBalanceChangedEvent != null)
            {
                OnGoodBalanceChangedEvent(good, balance, amountAdded);
            }
        }

        public event GoodEquippedEventHandler OnGoodEquippedEvent;
        public void PostGoodEquippedEvent(EquippableVG good)
        {
            LogEvent("GoodEquipped");
            if (OnGoodEquippedEvent != null)
            {
                OnGoodEquippedEvent(good);
            }
        }

        public event GoodUnEquippedEventHandler OnGoodUnEquippedEvent;
        public void PostGoodUnEquippedEvent(EquippableVG good)
        {
            LogEvent("GoodUnEquipped");
            if (OnGoodUnEquippedEvent != null)
            {
                OnGoodUnEquippedEvent(good);
            }
        }

        public event CurrencyBalanceChangedEventHandler OnCurrencyBalanceChangedEvent;
        public void PostCurrencyBalanceChangedEvent(VirtualCurrency currency, int balance, int amountAdded)
        {
            LogEvent("CurrencyBalanceChanged");
            if (OnCurrencyBalanceChangedEvent != null)
            {
                OnCurrencyBalanceChangedEvent(currency, balance, amountAdded);
            }
        }

        public event MarketItemsRefreshStartedEventHandler OnMarketItemsRefreshStartedEvent;
        public void PostMarketItemsRefreshStartedEvent()
        {
            LogEvent("MarketItemsRefreshStarted");
            if (OnMarketItemsRefreshStartedEvent != null)
            {
                OnMarketItemsRefreshStartedEvent();
            }
        }

        public event MarketItemsRefreshFinishedEventHandler OnMarketItemsRefreshFinishedEvent;
        public void PostMarketItemsRefreshFinishedEvent(List<MarketItem> marketItems)
        {
            LogEvent("MarketItemsRefreshFinished");
            if (OnMarketItemsRefreshFinishedEvent != null)
            {
                OnMarketItemsRefreshFinishedEvent(marketItems);
            }
        }

        public event RestoreTransactionsStartedEventHandler OnRestoreTransactionsStartedEvent;
        public void PostRestoreTransactionsStartedEvent()
        {
            LogEvent("RestoreTransactionsStarted");
            if (OnRestoreTransactionsStartedEvent != null)
            {
                OnRestoreTransactionsStartedEvent();
            }
        }

        public event RestoreTransactionsFinishedEventHandler OnRestoreTransactionsFinishedEvent;
        public void PostRestoreTransactionsFinishedEvent(bool success)
        {
            LogEvent("RestoreTransactionsFinished");
            if (OnRestoreTransactionsFinishedEvent != null)
            {
                OnRestoreTransactionsFinishedEvent(success);
            }
        }

        public event MarketPurchaseStartedEventHandler OnMarketPurchaseStartedEvent;
        public void PostMarketPurchaseStartedEvent(PurchasableVirtualItem purchasableVirtualItem)
        {
            LogEvent("MarketPurchaseStarted");
            if (OnMarketPurchaseStartedEvent != null)
            {
                OnMarketPurchaseStartedEvent(purchasableVirtualItem);
            }
        }

        public event MarketPurchaseEventHandler OnMarketPurchaseEvent;
        public void PostMarketPurchaseEvent(PurchasableVirtualItem purchasableVirtualItem, String payload, String token)
        {
            LogEvent("MarketPurchase");
            if (OnMarketPurchaseEvent != null)
            {
                OnMarketPurchaseEvent(purchasableVirtualItem, payload, token);
            }
        }

        public event MarketPurchaseCancelledEventHandler OnMarketPurchaseCancelledEvent;
        public void PostMarketPurchaseCancelledEvent(PurchasableVirtualItem purchasableVirtualItem)
        {
            LogEvent("MarketPurchaseCancelled");
            if (OnMarketPurchaseCancelledEvent != null)
            {
                OnMarketPurchaseCancelledEvent(purchasableVirtualItem);
            }
        }

        private void LogEvent(String eventName)
        {
            SoomlaUtils.LogDebug(TAG, "Event " + eventName + " raise");
        }

        private const String TAG = "SOOMLA EventManager"; //used for Log messages
    }
}
