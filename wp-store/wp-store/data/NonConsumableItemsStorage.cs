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
using SoomlaWpCore.data;
using SoomlaWpStore.domain;


namespace SoomlaWpStore.data 
{

/// <summary>   This class provides basic storage operations on the Market's MANAGED items. </summary>
public class NonConsumableItemsStorage {

    /**
     * Constructor
     */
    public NonConsumableItemsStorage() {
    }

    /// <summary>   Checks if the given <c>NonConsumableItem</c> exists. </summary>
    ///
    /// <param name="nonConsumableItem">    The non consumable item. </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    public bool nonConsumableItemExists(NonConsumableItem nonConsumableItem){

        SoomlaUtils.LogDebug(TAG, "Checking if the given MANAGED item exists.");

        String itemId = nonConsumableItem.getItemId();
        String key = keyNonConsExists(itemId);

        String val = KeyValueStorage.GetValue(key);
        
        return val != null;
    }

    /// <summary>   Adds the given non-consumable item to the storage. </summary>
    ///
    /// <param name="nonConsumableItem">    The non consumable item. </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    public bool add(NonConsumableItem nonConsumableItem){
        SoomlaUtils.LogDebug(TAG, "Adding " + nonConsumableItem.getItemId());

        String itemId = nonConsumableItem.getItemId();
        String key = keyNonConsExists(itemId);

        KeyValueStorage.SetValue(key, "");

        return true;
    }

    /// <summary>   Removes the given nonConsumableItem. </summary>
    ///
    /// <param name="nonConsumableItem">    The non consumable item. </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    public bool remove(NonConsumableItem nonConsumableItem){
        SoomlaUtils.LogDebug(TAG, "Removing " + nonConsumableItem.getName());

        String itemId = nonConsumableItem.getItemId();
        String key = keyNonConsExists(itemId);

        KeyValueStorage.DeleteKeyValue(key);

        return false;
    }


    /** Private Members **/
    private const String TAG = "SOOMLA NonConsumableItemsStorage"; //used for Log messages

    private static String keyNonConsExists(String productId) {
        return "nonconsumable." + productId + ".exists";
    }
}
}