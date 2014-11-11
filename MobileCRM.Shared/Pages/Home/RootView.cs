﻿using MobileCRM.Shared.Models;
using MobileCRM.Shared.Services;
using MobileCRM.Shared.Pages.Accounts;
using MobileCRM.Shared.Pages.Catalog;
using MobileCRM.Shared.Pages.Settings;
using MobileCRM.Shared.Pages.Base;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.ViewModels.Home;
using MobileCRM.Shared.ViewModels.Catalog;

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Home
{
    public class RootView : MasterDetailPage
    {
        MenuType previousItem;
        public RootView ()
        {
          this.Title = "Mobile CRM";

          this.BackgroundImage = "menubk.png";

            var optionsPage = new MenuView ();

            optionsPage.Menu.ItemSelected += (sender, e) =>
            {

              var item = e.SelectedItem as MenuItem;
              if (item == null)
                return;

              NavigateTo(item.MenuType);
              optionsPage.Menu.SelectedItem = null;
            };

            Master = optionsPage;

            previousItem = MenuType.Leads;//set first time to force
            NavigateTo(MenuType.Dashboard);

            //Authentication notifications
            MessagingCenter.Subscribe<ILogin>(this, "Authenticated", (sender) =>
            {
                this.CloseAuth();
            });    
        }

        void NavigateTo(MenuType option)
        {
            if (previousItem == option)
              return;
           
            previousItem = option;

            var displayPage = PageForOption(option);


            displayPage.BarBackgroundColor = Helpers.AppColors.CONTENTBKGCOLOR;
            displayPage.BarTextColor = Color.White;
            
             
#if WINDOWS_PHONE
            Detail = new ContentPage();
#endif

            Detail = displayPage;

            IsPresented = false;
        }

        NavigationPage dashboard, accounts, leads, contacts, catalog, settings;

        NavigationPage PageForOption (MenuType option)
        {

          switch (option)
          {
            case MenuType.Dashboard:
              {
                if (dashboard != null)
                  return dashboard;
                  
                var vm = new DashboardViewModel() { Navigation = Navigation };

                dashboard = new NavigationPage(new DashboardView(vm));
                return dashboard;
              }
            case MenuType.Accounts:
              {
                if (accounts != null)
                  return accounts;

                var vm = new AccountsViewModel() { Navigation = Navigation };
                accounts = new NavigationPage(new AccountsView(vm));

                return accounts; 
              }
            case MenuType.Leads:
              {
                if (leads != null)
                  return leads;
                
                leads =  new NavigationPage(new Leads.LeadsView(new ViewModels.Leads.LeadsViewModel() { Navigation = Navigation }));
                return leads;
              }
            case MenuType.Contacts:
              {
                if (contacts != null)
                  return contacts;
                var vm = new ContactsViewModel();
                contacts = new NavigationPage(new Contacts.ContactsView(vm));
                return contacts;
              } 
            case MenuType.Catalog:
              {
                  if (catalog != null)
                      return catalog;

                  //var vm = new CatalogViewModel() { Navigation = Navigation };
                  catalog = new NavigationPage(new Catalog.CatalogCarouselView());
                  return catalog;
              }

              case MenuType.Settings:
              {
                  if (settings != null)
                      return settings;

                  settings = new NavigationPage(new Settings.UserSettingsView());
                  return settings;
              }

          }
            
          throw new NotImplementedException("Unknown menu option: " + option.ToString());
        }




        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.CheckUserAuthenticated();
        }

        private void CheckUserAuthenticated()
        {
            if (AuthInfo.Instance.User == null)
            {
                Navigation.PushModalAsync(new LoginPage());
            }
        //    else
        //    {
        //        this.SetBinding();
        //    }
        }


        private void CloseAuth()
        {
            Navigation.PopModalAsync();
        }

    }
}
