using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;
using SoomlaWpCore;
using SoomlaWpCore.data;
namespace SoomlaWpStore
{
    public class WPStore
    {
        public WPStore()
        {
            Soomla.initialize("THE SECRET");

            KeyValueStorage.SetValue("testKey", "testData");

            Debug.WriteLine("Retrieved Value : " + KeyValueStorage.GetValue("testKey")); ;
        }
    }
}
