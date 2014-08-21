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
using SoomlaWpStore.data;
using SoomlaWpStore.domain;
using SoomlaWpStore.domain.virtualGoods;
using SoomlaWpStore.domain.virtualCurrencies;
using SoomlaWpStore.purchasesTypes;
using SoomlaWpStore.exceptions;

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
            SoomlaStore.GetInstance().initialize(new StoreAssets());

            EventManager.GetInstance().CurrencyBalanceChangedEvent += new CurrencyBalanceChangedEventHandler(UpdateCurrencyBalance);
            EventManager.GetInstance().GoodBalanceChangedEvent += new GoodBalanceChangedEventHandler(UpdateGoodBalance);
            EventManager.GetInstance().GoodEquippedEvent += new GoodEquippedEventHandler(UpdateGoodEquip);
            EventManager.GetInstance().GoodUnEquippedEvent += new GoodUnEquippedEventHandler(UpdateGoodUnequip);

            UpdateCurrencyBalance(null, null);
            buildShop();
            
        }

        private void buildShop()
        {
            foreach (VirtualCurrencyPack vc in StoreInfo.getCurrencyPacks())
            {
                buildShopLine(vc);
            }

            foreach (NonConsumableItem nci in StoreInfo.getNonConsumableItems())
            {
                buildShopLine(nci);
            }
            
            foreach (string id in StoreAssets.BOOTS_CATEGORY.getGoodsItemIds())
            {
                buildShopLine(StoreInfo.getVirtualItem(id));
            }

            foreach (string id in StoreAssets.GLOVE_CATEGORY.getGoodsItemIds())
            {
                buildShopLine(StoreInfo.getVirtualItem(id));
            }
        }

        private void buildShopLine(VirtualItem item)
        {
            StackPanel stackP = new StackPanel();
            stackP.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stackP.Margin = new Thickness(0, 15, 0, 0);

            StackPanel buttonStack = new StackPanel();
            buttonStack.Orientation = System.Windows.Controls.Orientation.Vertical;

            StackPanel textStack = new StackPanel();
            textStack.Orientation = System.Windows.Controls.Orientation.Vertical;

            stackP.Children.Add(buttonStack);
            stackP.Children.Add(textStack);

            Button buy = new Button();
            buy.Margin = new Thickness(0, 0, 10, 0);
            buy.Click += buyItem;
            buy.Content = "buy";
            buy.CommandParameter = item.getItemId();
            buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            buy.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            buttonStack.Children.Add(buy);

            if(item is EquippableVG)
            {
                Button equip = new Button();
                equip.Name = item.getItemId()+"equip";
                equip.Margin = new Thickness(0, 0, 10, 0);
                equip.CommandParameter = item.getItemId();
                equip.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                equip.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                EquippableVG evg = (EquippableVG)item;
                if (StoreInventory.isVirtualGoodEquipped(item.getItemId()))
                {
                    equip.Content = "unequip";
                    equip.Click += unequipItem;
                }
                else
                {
                    equip.Content = "equip";
                    equip.Click += equipItem;
                }
                buttonStack.Children.Add(equip);
            }

            TextBlock balance = new TextBlock();
            balance.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            balance.Margin = new Thickness(0,0,10,0);
            if (!(item is VirtualCurrencyPack) && !(item is NonConsumableItem))
            {
                balance.Text = "balance: "+StoreInventory.getVirtualItemBalance(item.getItemId()).ToString();
            }
            balance.Name = item.getItemId() + "balance";
            textStack.Children.Add(balance);

            TextBlock name = new TextBlock();
            name.Margin = new Thickness(0, 0, 10, 0);
            name.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            name.Text = "id: "+item.getItemId();
            textStack.Children.Add(name);

            TextBlock price = new TextBlock();
            price.Margin = new Thickness(0, 0, 10, 0);
            price.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            TextBlock currency = new TextBlock();
            currency.Margin = new Thickness(0, 0, 10, 0);
            currency.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            if(item is PurchasableVirtualItem)
            {
                PurchasableVirtualItem pvi = (PurchasableVirtualItem)item;
                if(pvi.getPurchaseType() is PurchaseWithVirtualItem)
                {
                    PurchaseWithVirtualItem purchaseType = (PurchaseWithVirtualItem)pvi.getPurchaseType();
                    price.Text = "price: "+purchaseType.getAmount().ToString();
                    currency.Text = "currency: "+purchaseType.getTargetItemId();
                }
                if (pvi.getPurchaseType() is PurchaseWithMarket)
                {
                    PurchaseWithMarket purchaseType = (PurchaseWithMarket)pvi.getPurchaseType();
                    price.Text = "price: "+purchaseType.getMarketItem().getMarketPrice();
                    
                }

                textStack.Children.Add(price);
                textStack.Children.Add(currency);
            }

            if (item is VirtualCurrencyPack)
            {
                VirtualCurrencyPack vcp = (VirtualCurrencyPack)item;

                TextBlock currencyId = new TextBlock();
                currencyId.Text = "give currency: "+vcp.getCurrencyItemId().ToString();

                TextBlock currencyAmount = new TextBlock();
                currencyAmount.Text = "give amount: "+vcp.getCurrencyAmount().ToString();
                textStack.Children.Add(currencyId);
                textStack.Children.Add(currencyAmount);
            }

            MainStackPanel.Children.Add(stackP);
        }

        private void buyItem(object sender, RoutedEventArgs e)
        {
            Button buyButton = (Button)sender;
            try
            {
                StoreInventory.buy(buyButton.CommandParameter.ToString(), null);
            }
            catch (InsufficientFundsException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void equipItem(object sender, RoutedEventArgs e)
        {
            Button equipButton = (Button)sender;
            try
            {
                StoreInventory.equipVirtualGood(equipButton.CommandParameter.ToString());
            }
            catch (NotEnoughGoodsException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void unequipItem(object sender, RoutedEventArgs e)
        {
            Button equipButton = (Button)sender;
            StoreInventory.unEquipVirtualGood(equipButton.CommandParameter.ToString());
        }

        private void UpdateGoodEquip(Object o, GoodEquippedEventArgs e)
        {
            Button equipB = (Button)MainStackPanel.FindName(e.getGood().getItemId() + "equip");
            equipB.Content = "unequip";
            equipB.Click -= equipItem;
            equipB.Click += unequipItem;
        }

        private void UpdateGoodUnequip(Object o, GoodUnEquippedEventArgs e)
        {
            Button equipB = (Button)MainStackPanel.FindName(e.getGood().getItemId() + "equip");
            equipB.Content = "equip";
            equipB.Click += equipItem;
            equipB.Click -= unequipItem;
        }

        private void UpdateGoodBalance(Object o, GoodBalanceChangedEventArgs e)
        {
            TextBlock balance = (TextBlock)MainStackPanel.FindName(e.getGood().getItemId() + "balance");
            balance.Text = "balance: "+e.getBalance().ToString();
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