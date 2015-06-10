﻿using MobileCRM.Models;
using MobileCRM.ViewModels.Contacts;
using Xamarin.Forms;
using Xamarin;

namespace MobileCRM.Pages.Contacts
{
    public partial class ContactsView
    {
        private ContactsViewModel ViewModel
        {
            get { return BindingContext as ContactsViewModel; }
        }

        public ContactsView(ContactsViewModel vm)
        {
            InitializeComponent();

            this.BindingContext = vm;

            ToolbarItems.Add(new ToolbarItem
                {
                    Icon = "add.png",
                    Name = "add",
                    Command = new Command(() =>
                        {
                            //navigate to new page
                            Navigation.PushAsync(new ContactDetailsTabView(null));

                        })
                });

            //SYI: Removed - Pull contacts only on initial load.  
            //ToolbarItems.Add(new ToolbarItem
            //{
            //Icon = "refresh.png",
            //Name = "refresh",
            //Command = ViewModel.LoadContactsCommand
            //});

        }

        public void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Navigation.PushAsync(new ContactDetailsTabView(e.Item as Contact));

            ContactList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track("Contact List Page");

            if (ViewModel.IsInitialized)
            {
                return;
            }
            ViewModel.LoadContactsCommand.Execute(null);
            ViewModel.IsInitialized = true;
        }
    }
}
