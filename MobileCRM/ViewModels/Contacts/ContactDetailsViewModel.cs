﻿using System.Threading.Tasks;
using MobileCRM.Helpers;
using MobileCRM.Interfaces;
using MobileCRM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MobileCRM.ViewModels.Contacts
{
    public class ContactDetailsViewModel : BaseViewModel
    {
        IDataManager dataManager;

        Geocoder coder;

        public Contact Contact { get; set; }

        public ContactDetailsViewModel(Contact contact)
        {
            if (contact == null)
            {
                Contact = new Contact();
                this.Title = "New Contact";
            }
            else
            {
                Contact = contact;
                this.Title = contact.FirstName;
            }

            this.Icon = "contact.png";

            dataManager = DependencyService.Get<IDataManager>();
            //this.navigation = navigation;
            coder = new Geocoder();
        }
        //end ctor

        Command saveContactCommand;

        /// <summary>
        /// Command to load contacts
        /// </summary>
        public Command SaveContactCommand
        {
            get
            {
                return saveContactCommand ??
                (saveContactCommand = new Command(async () =>
                  await ExecuteSaveContactCommand()));
            }
        }

        public async Task<Pin> LoadPin()
        {
            Position p = Utils.NullPosition;
            var address = Contact.AddressString;

            //Lookup Lat/Long all the time.
            //TODO: Only look up if no value, or if address properties have changed.
            //if (Contact.Latitude == 0)
            if (true)
            {
                p = await Utils.GeoCodeAddress(address);
                //p = p == null ? Utils.NullPosition : p;

                Contact.Latitude = p.Latitude;
                Contact.Longitude = p.Longitude;
            }

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = p,
                Label = Contact.DisplayName,
                Address = address.ToString()
            };

            return pin;
        }

        async Task ExecuteSaveContactCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await dataManager.SaveContactAsync(Contact);

            MessagingCenter.Send(Contact, "SaveContact");

            IsBusy = false;

            Navigation.PopAsync();
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
    }
}