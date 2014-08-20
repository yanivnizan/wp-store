using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoomlaWpStore.domain;
namespace SoomlaWpStore.purchasesTypes
{
    public abstract class PurchaseType
    {
        /// <exception cref="InsufficientFundsException"></exception>
        public abstract void buy(String payload);


        public void setAssociatedItem(PurchasableVirtualItem associatedItem)
        {
            mAssociatedItem = associatedItem;
        }

        public PurchasableVirtualItem getAssociatedItem()
        {
            return mAssociatedItem;
        }
        //the PurchasableVirtualItem associated with this PurchaseType
        private PurchasableVirtualItem mAssociatedItem;

        
    }
}
