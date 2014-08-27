using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoomlaWpStore.domain;
using SoomlaWpStore.domain.virtualCurrencies;
using SoomlaWpStore.domain.virtualGoods;
using SoomlaWpCore;
using Newtonsoft.Json.Linq;
namespace SoomlaWpStore.data
{
    /// <summary>   Generic IStoreAssets to build a StoreAssets from a JSON string. </summary>
    public class GenericStoreAssets : IStoreAssets
    {
        VirtualCurrency[] mVirtualCurrency;
        VirtualGood[] mVirtualGood;
        VirtualCurrencyPack[] mVirtualCurrencyPack;
        VirtualCategory[] mVirtualCategory;
        NonConsumableItem[] mNonConsumableItem;
        int mVersion;

        private static GenericStoreAssets instance;

        public static GenericStoreAssets GetInstance()
        {
            if(instance == null)
            {
                instance = new GenericStoreAssets();
            }
            return instance;
        }
        public void Prepare(int version, String JsonStoreAssets)
        {
            try
            {
                mVersion = version;

                JObject JObject = JObject.Parse(JsonStoreAssets);

                JArray virtualCurrencies = JObject.Value<JArray>(StoreJSONConsts.STORE_CURRENCIES);
                mVirtualCurrency = new VirtualCurrency[virtualCurrencies.Count];
                for (int i = 0; i < virtualCurrencies.Count; i++)
                {
                    JObject o = virtualCurrencies.Value<JObject>(i);
                    VirtualCurrency c = new VirtualCurrency(o);
                    mVirtualCurrency[i] = c;
                }

                JArray currencyPacks = JObject.Value<JArray>(StoreJSONConsts.STORE_CURRENCYPACKS);
                mVirtualCurrencyPack = new VirtualCurrencyPack[currencyPacks.Count];
                for (int i = 0; i < currencyPacks.Count; i++)
                {
                    JObject o = currencyPacks.Value<JObject>(i);
                    VirtualCurrencyPack pack = new VirtualCurrencyPack(o);
                    mVirtualCurrencyPack[i] = pack;
                }

                // The order in which VirtualGoods are created matters!
                // For example: VGU and VGP depend on other VGs
                JObject virtualGoods = JObject.Value<JObject>(StoreJSONConsts.STORE_GOODS);
                JArray suGoods = virtualGoods.Value<JArray>(StoreJSONConsts.STORE_GOODS_SU);
                JArray ltGoods = virtualGoods.Value<JArray>(StoreJSONConsts.STORE_GOODS_LT);
                JArray eqGoods = virtualGoods.Value<JArray>(StoreJSONConsts.STORE_GOODS_EQ);
                JArray upGoods = virtualGoods.Value<JArray>(StoreJSONConsts.STORE_GOODS_UP);
                JArray paGoods = virtualGoods.Value<JArray>(StoreJSONConsts.STORE_GOODS_PA);
                List<VirtualGood> goods = new List<VirtualGood>();
                for (int i = 0; i < suGoods.Count; i++)
                {
                    JObject o = suGoods.Value<JObject>(i);
                    SingleUseVG g = new SingleUseVG(o);
                    goods.Add(g);
                }
                for (int i = 0; i < ltGoods.Count; i++)
                {
                    JObject o = ltGoods.Value<JObject>(i);
                    LifetimeVG g = new LifetimeVG(o);
                    goods.Add(g);
                }
                for (int i = 0; i < eqGoods.Count; i++)
                {
                    JObject o = eqGoods.Value<JObject>(i);
                    EquippableVG g = new EquippableVG(o);
                    goods.Add(g);
                }
                for (int i = 0; i < paGoods.Count; i++)
                {
                    JObject o = paGoods.Value<JObject>(i);
                    SingleUsePackVG g = new SingleUsePackVG(o);
                    goods.Add(g);
                }
                for (int i = 0; i < upGoods.Count; i++)
                {
                    JObject o = upGoods.Value<JObject>(i);
                    UpgradeVG g = new UpgradeVG(o);
                    goods.Add(g);
                }

                mVirtualGood = new VirtualGood[goods.Count];
                for(int i = 0; i < goods.Count; i++)
                {
                    mVirtualGood[i] = goods[i];
                }

                // categories depend on virtual goods. That's why the have to be initialized after!
                JArray virtualCategories = JObject.Value<JArray>(StoreJSONConsts.STORE_CATEGORIES);
                mVirtualCategory = new VirtualCategory[virtualCategories.Count];
                for (int i = 0; i < virtualCategories.Count; i++)
                {
                    JObject o = virtualCategories.Value<JObject>(i);
                    VirtualCategory category = new VirtualCategory(o);
                    mVirtualCategory[i] = category;
                }

                JArray nonConsumables = JObject.Value<JArray>(StoreJSONConsts.STORE_NONCONSUMABLES);
                mNonConsumableItem = new NonConsumableItem[nonConsumables.Count];
                for (int i = 0; i < nonConsumables.Count; i++)
                {
                    JObject o = nonConsumables.Value<JObject>(i);
                    NonConsumableItem non = new NonConsumableItem(o);
                    mNonConsumableItem[i] = non;
                }

            }
            catch (Exception ex)
            {
                SoomlaUtils.LogError(TAG, "An error occurred while trying to prepare storeAssets" + ex.Message);
            }

        }

        public int GetVersion()
        {
            return mVersion;
        }
        public VirtualCurrency[] GetCurrencies()
        {
            return mVirtualCurrency;
        }
        public VirtualGood[] GetGoods()
        {
            return mVirtualGood;
        }
        public VirtualCurrencyPack[] GetCurrencyPacks()
        {
            return mVirtualCurrencyPack;
        }
        public VirtualCategory[] GetCategories()
        {
            return mVirtualCategory;
        }
        public NonConsumableItem[] GetNonConsumableItems()
        {
            return mNonConsumableItem;
        }

        private const String TAG = "SOOMLA GenericStoreAssets"; //used for Log messages

    }
}
