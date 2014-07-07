﻿using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using MobileCRM.Shared.Helpers;

namespace MobileCRM.Shared.ViewModels.Contacts
{
    public class ContactsViewModel : BaseViewModel
    {
      public ObservableCollection<Contact> Contacts
      {
        get;
        set;
      }

      IDataManager dataManager;
      public ContactsViewModel()
      {
        this.Title = "Contacts";
        this.Icon = "list.png";

        dataManager = DependencyService.Get<IDataManager>();
        Contacts = new ObservableCollection<Contact>();

        MessagingCenter.Subscribe<Contact>(this, "NewContact", (contact) =>
          {
            Contacts.Add(contact);
          });
      }

      private Command loadContactsCommand;
      /// <summary>
      /// Command to load contacts
      /// </summary>
      public Command LoadContactsCommand
      {
        get
        {
          return loadContactsCommand ??
                 (loadContactsCommand = new Command(async () =>
                  await ExecuteLoadContactsCommand()));
        }
      }

      private async Task ExecuteLoadContactsCommand()
      {
        if (IsBusy)
          return;

        IsBusy = true;

        Contacts.Clear();
        var contacts = await dataManager.GetContactsAsync();
        foreach (var contact in contacts)
          Contacts.Add(contact);

        IsBusy = false;

      }
       
      public List<Pin> LoadPins()
      {

        var pins = Contacts.Select(model =>
        {
          var item = model;
          var address = item.AddressString;

          var position = address != null ? new Position(item.Latitude, item.Longitude) : Utils.NullPosition;
          var pin = new Pin
          {
            Type = PinType.Place,
            Position = position,
            Label = item.ToString(),
            Address = address.ToString()
          };
          return pin;
        }).ToList();

        return pins; 
      }

    }
}
