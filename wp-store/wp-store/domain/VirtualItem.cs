using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoomlaWpCore.data;
using SoomlaWpCore;
using Newtonsoft.Json.Linq;
namespace SoomlaWpStore.domain
{
    public abstract class VirtualItem : SoomlaEntity
    {
        public VirtualItem(String Name, String Description, String ItemId) : base(Name, Description, ItemId)
        {

        }

        public VirtualItem(JObject jsonObject) : base(jsonObject)
        {
        }

        public virtual new JObject toJSONObject()
        {
            return base.toJSONObject();
        }

        public int give(int amount)
        {
            return give(amount, true);
        }

        public abstract int give(int amount, bool notify);

        public int take(int amount)
        {
            return take(amount, true);
        }

        public abstract int take(int amount, bool notify);

        public int resetBalance(int balance)
        {
            return resetBalance(balance, true);
        }

        public abstract int resetBalance(int balance, bool notify);

        public String getItemId()
        {
            return mID;
        }

        public String getName()
        {
            return mName;
        }

        private const String TAG = "SOOMLA VirtualItem"; //used for Log messages
    }
}
