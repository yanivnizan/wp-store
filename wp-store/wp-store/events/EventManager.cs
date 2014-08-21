using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoomlaWpStore.events;
using SoomlaWpCore;
namespace SoomlaWpStore.events
{
    public delegate void ItemPurchaseStartedEventHandler(Object o, ItemPurchaseStartedEventArgs e);
    public delegate void ItemPurchasedEventHandler(Object o, ItemPurchasedEventArgs e);
    public delegate void SoomlaStoreInitializedEventHandler(Object o, EventArgs e);
    public delegate void UnexpectedStoreErrorEventHandler(Object o, UnexpectedStoreErrorEventArgs e);
    public delegate void GoodUpgradeEventHandler(Object o, GoodUpgradeEventArgs e);
    public delegate void GoodBalanceChangedEventHandler(Object o, GoodBalanceChangedEventArgs e);
    public delegate void GoodEquippedEventHandler(Object o, GoodEquippedEventArgs e);
    public delegate void GoodUnEquippedEventHandler(Object o, GoodUnEquippedEventArgs e);
    public delegate void CurrencyBalanceChangedEventHandler(Object o, CurrencyBalanceChangedEventArgs e);
    public delegate void MarketItemsRefreshStartedEventHandler(Object o, MarketItemsRefreshStartedEventArgs e);
    public delegate void MarketItemsRefreshFinishedEventHandler(Object o, MarketItemsRefreshFinishedEventArgs e);
    public delegate void RestoreTransactionsStartedEventHandler(Object o, RestoreTransactionsStartedEventArgs e);
    public delegate void RestoreTransactionsFinishedEventHandler(Object o, RestoreTransactionsFinishedEventArgs e);
    public delegate void MarketPurchaseStartedEventHandler(Object o, MarketPurchaseStartedEventArgs e);
    public delegate void MarketPurchaseEventHandler(Object o, MarketPurchaseEventArgs e);
    public delegate void MarketPurchaseCancelledEventHandler(Object o, MarketPurchaseCancelledEventArgs e);
    
    public class EventManager
    {
        public static EventManager instance;
        public static EventManager GetInstance()
        {
            if(instance==null)
            {
                instance = new EventManager();
            }
            return instance;
        }
        
        public event ItemPurchaseStartedEventHandler ItemPurchaseStartedEvent;
        public void OnItemPurchaseStartedEvent(Object o, ItemPurchaseStartedEventArgs e)
        {
            LogEvent(o, "ItemPurchaseStarted");
            if (ItemPurchaseStartedEvent != null)
            {
                ItemPurchaseStartedEvent(o, e);
            }
        }

        public event ItemPurchasedEventHandler ItemPurchasedEvent;
        public void OnItemPurchasedEvent(Object o, ItemPurchasedEventArgs e)
        {
            LogEvent(o, "ItemPurchased");
            if (ItemPurchasedEvent != null)
            {
                ItemPurchasedEvent(o, e);
            }
        }

        public event SoomlaStoreInitializedEventHandler SoomlaStoreInitializedEvent;
        public void OnSoomlaStoreInitializedEvent(Object o, EventArgs e)
        {
            LogEvent(o, "SoomlaStoreInitialized");
            if (SoomlaStoreInitializedEvent != null)
            {
                SoomlaStoreInitializedEvent(o, null);
            }
        }

        public event UnexpectedStoreErrorEventHandler UnexpectedStoreErrorEvent;
        public void OnUnexpectedStoreErrorEvent(Object o, UnexpectedStoreErrorEventArgs e)
        {
            LogEvent(o, "UnexpectedStoreError");
            if (UnexpectedStoreErrorEvent != null)
            {
                UnexpectedStoreErrorEvent(o, e);
            }
        }

        public event GoodUpgradeEventHandler GoodUpgradeEvent;
        public void OnGoodUpgradeEvent(Object o, GoodUpgradeEventArgs e)
        {
            LogEvent(o, "GoodUpgrade");
            if (GoodUpgradeEvent != null)
            {
                GoodUpgradeEvent(o, e);
            }
        }

        public event GoodBalanceChangedEventHandler GoodBalanceChangedEvent;
        public void OnGoodBalanceChangedEvent(Object o, GoodBalanceChangedEventArgs e)
        {
            LogEvent(o, "GoodBalanceChanged");
            if (GoodBalanceChangedEvent != null)
            {
                GoodBalanceChangedEvent(o, e);
            }
        }

        public event GoodEquippedEventHandler GoodEquippedEvent;
        public void OnGoodEquippedEvent(Object o, GoodEquippedEventArgs e)
        {
            LogEvent(o, "GoodEquipped");
            if (GoodEquippedEvent != null)
            {
                GoodEquippedEvent(o, e);
            }
        }

        public event GoodUnEquippedEventHandler GoodUnEquippedEvent;
        public void OnGoodUnEquippedEvent(Object o, GoodUnEquippedEventArgs e)
        {
            LogEvent(o, "GoodUnEquipped");
            if (GoodUnEquippedEvent != null)
            {
                GoodUnEquippedEvent(o, e);
            }
        }

        public event CurrencyBalanceChangedEventHandler CurrencyBalanceChangedEvent;
        public void OnCurrencyBalanceChangedEvent(Object o, CurrencyBalanceChangedEventArgs e)
        {
            LogEvent(o, "CurrencyBalanceChanged");
            if (CurrencyBalanceChangedEvent != null)
            {
                CurrencyBalanceChangedEvent(o, e);
            }
        }

        public event MarketItemsRefreshStartedEventHandler MarketItemsRefreshStartedEvent;
        public void OnMarketItemsRefreshStartedEvent(Object o, MarketItemsRefreshStartedEventArgs e)
        {
            LogEvent(o, "MarketItemsRefreshStarted");
            if (MarketItemsRefreshStartedEvent != null)
            {
                MarketItemsRefreshStartedEvent(o, e);
            }
        }

        public event MarketItemsRefreshFinishedEventHandler MarketItemsRefreshFinishedEvent;
        public void OnMarketItemsRefreshFinishedEvent(Object o, MarketItemsRefreshFinishedEventArgs e)
        {
            LogEvent(o, "MarketItemsRefreshFinished");
            if (MarketItemsRefreshFinishedEvent != null)
            {
                MarketItemsRefreshFinishedEvent(o, e);
            }
        }

        public event RestoreTransactionsStartedEventHandler RestoreTransactionsStartedEvent;
        public void OnRestoreTransactionsStartedEvent(Object o, RestoreTransactionsStartedEventArgs e)
        {
            LogEvent(o, "RestoreTransactionsStarted");
            if (RestoreTransactionsStartedEvent != null)
            {
                RestoreTransactionsStartedEvent(o, e);
            }
        }

        public event RestoreTransactionsFinishedEventHandler RestoreTransactionsFinishedEvent;
        public void OnRestoreTransactionsFinishedEvent(Object o, RestoreTransactionsFinishedEventArgs e)
        {
            LogEvent(o, "RestoreTransactionsFinished");
            if (RestoreTransactionsFinishedEvent != null)
            {
                RestoreTransactionsFinishedEvent(o, e);
            }
        }

        public event MarketPurchaseStartedEventHandler MarketPurchaseStartedEvent;
        public void OnMarketPurchaseStartedEvent(Object o, MarketPurchaseStartedEventArgs e)
        {
            LogEvent(o, "MarketPurchaseStarted");
            if (MarketPurchaseStartedEvent != null)
            {
                MarketPurchaseStartedEvent(o, e);
            }
        }

        public event MarketPurchaseEventHandler MarketPurchaseEvent;
        public void OnMarketPurchaseEvent(Object o, MarketPurchaseEventArgs e)
        {
            LogEvent(o, "MarketPurchase");
            if (MarketPurchaseEvent != null)
            {
                MarketPurchaseEvent(o, e);
            }
        }

        public event MarketPurchaseCancelledEventHandler MarketPurchaseCancelledEvent;
        public void OnMarketPurchaseCancelledEvent(Object o, MarketPurchaseCancelledEventArgs e)
        {
            LogEvent(o, "MarketPurchaseCancelled");
            if (MarketPurchaseCancelledEvent != null)
            {
                MarketPurchaseCancelledEvent(o, e);
            }
        }

        private void LogEvent(Object o, String eventName)
        {
            SoomlaUtils.LogDebug(TAG, "Event " + eventName + " raise from " + o.GetType().ToString());
        }

        private const String TAG = "SOOMLA EventManager"; //used for Log messages
    }
}
