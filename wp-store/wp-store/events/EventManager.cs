using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoomlaWpStore.events;
namespace SoomlaWpStore.events
{
    

    class EventManager
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
        public delegate void ItemPurchaseStartedEventHandler(Object o, ItemPurchaseStartedEventArgs e);
        public void OnItemPurchaseStartedEvent(Object o, ItemPurchaseStartedEventArgs e)
        {
            ItemPurchaseStartedEvent(o, e);
        }

        public event ItemPurchasedEventHandler ItemPurchasedEvent;
        public delegate void ItemPurchasedEventHandler(Object o, ItemPurchasedEventArgs e);
        public void OnItemPurchasedEvent(Object o, ItemPurchasedEventArgs e)
        {
            ItemPurchasedEvent(o, e);
        }

        public event SoomlaStoreInitializedEventHandler SoomlaStoreInitializedEvent;
        public delegate void SoomlaStoreInitializedEventHandler(Object o, EventArgs e);
        public void OnSoomlaStoreInitializedEvent(Object o, EventArgs e)
        {
            SoomlaStoreInitializedEvent(o, null);
        }

        public event UnexpectedStoreErrorEventHandler UnexpectedStoreErrorEvent;
        public delegate void UnexpectedStoreErrorEventHandler(Object o, UnexpectedStoreErrorEventArgs e);
        public void OnUnexpectedStoreErrorEvent(Object o, UnexpectedStoreErrorEventArgs e)
        {
            UnexpectedStoreErrorEvent(o, e);
        }

    }
}
