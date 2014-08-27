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

using System;
using SoomlaWpCore;
using SoomlaWpStore.data;
using SoomlaWpStore.exceptions;
using SoomlaWpStore.purchasesTypes;
using Newtonsoft.Json.Linq;

namespace SoomlaWpStore.domain
{
    /// <summary>
    /// A representation of a <code>VirtualItem</code> that you can actually purchase.
    /// </summary>
public abstract class PurchasableVirtualItem : VirtualItem {

    /// <summary>
    /// Initializes a new instance of the <see cref="PurchasableVirtualItem"/> class.
    /// </summary>
    /// <param name="mName">Name</param>
    /// <param name="mDescription">Description.</param>
    /// <param name="mItemId">item identifier.</param>
    /// <param name="purchaseType">Type of the purchase.</param>
    public PurchasableVirtualItem(String mName, String mDescription, String mItemId,
                                  PurchaseType purchaseType) : base(mName, mDescription, mItemId) {
        
        mPurchaseType = purchaseType;
        mPurchaseType.setAssociatedItem(this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PurchasableVirtualItem"/> class.
    /// </summary>
    /// <param name="jsonObject">The json object.</param>
    public PurchasableVirtualItem(JObject jsonObject) : base(jsonObject){
        
        JObject purchasableObj = jsonObject.Value<JObject>(StoreJSONConsts.PURCHASABLE_ITEM);
        String purchaseType = purchasableObj.Value<String>(StoreJSONConsts.PURCHASE_TYPE);

        if (purchaseType == StoreJSONConsts.PURCHASE_TYPE_MARKET) {
            JObject marketItemObj =
                    purchasableObj.Value<JObject>(StoreJSONConsts.PURCHASE_MARKET_ITEM);

            mPurchaseType = new PurchaseWithMarket(new MarketItem(marketItemObj));
        } else if (purchaseType == StoreJSONConsts.PURCHASE_TYPE_VI) {
            String itemId = purchasableObj.Value<String>(StoreJSONConsts.PURCHASE_VI_ITEMID);
            int amount = purchasableObj.Value<int>(StoreJSONConsts.PURCHASE_VI_AMOUNT);

            mPurchaseType = new PurchaseWithVirtualItem(itemId, amount);
        } else {
            SoomlaUtils.LogError(TAG, "IabPurchase type not recognized !");
        }

        if (mPurchaseType != null) {
            mPurchaseType.setAssociatedItem(this);
        }
    }

    /**
     * @{inheritDoc}
     */
    public override JObject toJSONObject(){
        JObject parentJsonObject = base.toJSONObject();
        JObject jsonObject = new JObject();
        try {
            foreach(var entry in parentJsonObject)
            {
				jsonObject.Add(entry.Key,entry.Value);
            }

            JObject purchasableObj = new JObject();

            if(mPurchaseType is PurchaseWithMarket) {
                purchasableObj.Add(StoreJSONConsts.PURCHASE_TYPE, StoreJSONConsts.PURCHASE_TYPE_MARKET);

                MarketItem mi = ((PurchaseWithMarket) mPurchaseType).getMarketItem();
                purchasableObj.Add(StoreJSONConsts.PURCHASE_MARKET_ITEM, mi.toJSONObject());
            } else if(mPurchaseType is PurchaseWithVirtualItem) {
                purchasableObj.Add(StoreJSONConsts.PURCHASE_TYPE, StoreJSONConsts.PURCHASE_TYPE_VI);

                purchasableObj.Add(StoreJSONConsts.PURCHASE_VI_ITEMID,((PurchaseWithVirtualItem)mPurchaseType).getTargetItemId());
                purchasableObj.Add(StoreJSONConsts.PURCHASE_VI_AMOUNT,((PurchaseWithVirtualItem) mPurchaseType).getAmount());
            }

            jsonObject.Add(StoreJSONConsts.PURCHASABLE_ITEM, purchasableObj);
        } catch (Exception e) {
            SoomlaUtils.LogError(TAG, "An error occurred while generating JSON object." + " " + e.Message);
        }

        return jsonObject;
    }

    /**
     * Buys the <code>PurchasableVirtualItem</code>, after checking if the user is in a state that allows him/her to buy. This action uses the associated <code>PurchaseType</code> to perform the purchase.
     *
     * @param payload a string you want to be assigned to the purchase. This string
     *   is saved in a static variable and will be given bacl to you when the
     *   purchase is completed.
     * @throws InsufficientFundsException if the user does not have enough funds for buying.
     */
    public void buy(String payload) {
        if (!CanBuy()) return;

        mPurchaseType.buy(payload);
    }

    /**
     * Determines if user is in a state that allows him/her to buy a specific
     * <code>VirtualItem</code>.
     *
     * @return true if can buy, false otherwise
     */
    protected abstract bool CanBuy();


    /** Setters and Getters */

    public PurchaseType GetPurchaseType() {
        return mPurchaseType;
    }


    /** Private Members */

    private const String TAG = "SOOMLA PurchasableVirtualItem"; //used for Log messages

    private PurchaseType mPurchaseType; //the way this PurchasableVirtualItem is purchased.
}
}