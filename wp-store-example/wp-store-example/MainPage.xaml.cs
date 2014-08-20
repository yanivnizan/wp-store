using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using wp_store_example.Resources;
using SoomlaWpStore;
using SoomlaWpCore;
using SoomlaWpStore.events;

namespace wp_store_example
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();
            Soomla.initialize("this_is_my_secret");
            // Exemple de code pour la localisation d'ApplicationBar
            //BuildLocalizedApplicationBar();
            WPStore.GetInstance().initialize(new StoreAssets());

            EventManager.GetInstance().CurrencyBalanceChangedEvent += new CurrencyBalanceChangedEventHandler(UpdateCurrencyBalance);
            UpdateCurrencyBalance(null, null);

            StackPanel stackP = new StackPanel();
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "testTextBlock";
            stackP.Children.Add(textBlock);
            MainStackPanel.Children.Add(stackP);
        }

        private void UpdateCurrencyBalance(Object o, CurrencyBalanceChangedEventArgs e)
        {
            WeakCurrency.Text = StoreInventory.getVirtualItemBalance(StoreAssets.WEAK_CURRENCY_ITEM_ID).ToString();
            StrongCurrency.Text = StoreInventory.getVirtualItemBalance(StoreAssets.STRONG_CURRENCY_ITEM_ID).ToString();
        }

        private void Give1000Weak(object sender, RoutedEventArgs e)
        {
            StoreInventory.giveVirtualItem(StoreAssets.WEAK_CURRENCY_ITEM_ID,1000);

        }
        private void Give100Strong(object sender, RoutedEventArgs e)
        {
            StoreInventory.giveVirtualItem(StoreAssets.STRONG_CURRENCY_ITEM_ID, 100);
        }

        // Exemple de code pour la conception d'une ApplicationBar localisée
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Définit l'ApplicationBar de la page sur une nouvelle instance d'ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}