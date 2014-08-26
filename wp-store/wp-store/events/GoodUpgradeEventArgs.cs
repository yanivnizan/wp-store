/*
 * Copyright (C) 2012-2014 Soomla Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using System;
using SoomlaWpStore.domain.virtualGoods;
namespace SoomlaWpStore.events
{
    /**
     * This event is fired when a specific <code>UpgradeVG</code> has been upgraded/downgraded.
     *
     * Real Game Example:
     *  Example Inventory: { currency_coin: 100, Characters: Robot_X_1 }
     *  Suppose your user upgrades "Characters". Robot_X_1 is the first Robot_X in the series.
     *  After the upgrade, his/her new balance of Characters will be { Characters: Robot_X_2 }.
     *  A <code>OnGoodUpgradeEvent</code> is fired.
     */
    public class GoodUpgradeEventArgs : EventArgs
    {

        /**
         * Constructor
         *
         * @param good good that has been upgraded/downgraded
         * @param upgradeVG upgrade details
         */
        public GoodUpgradeEventArgs(VirtualGood good, UpgradeVG upgradeVG)
        {
            mGood = good;
            mCurrentUpgrade = upgradeVG;
        }


        /** Setters and Getters */

        public VirtualGood getGood()
        {
            return mGood;
        }

        public UpgradeVG getCurrentUpgrade()
        {
            return mCurrentUpgrade;
        }


        /** Private Members */

        private VirtualGood mGood; //good that has been upgraded/downgraded

        private UpgradeVG mCurrentUpgrade; //upgrade details

    }
}