﻿using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountHistoryView
	{
    OrdersViewModel viewModel;
    string accountId;
		public AccountHistoryView (string accountId)
		{
			InitializeComponent ();
      this.accountId = accountId;
      this.BindingContext = this.viewModel = new OrdersViewModel(false, accountId);
		}

    public void OnItemSelected(object sender, ItemTappedEventArgs e)
    {
      if (e.Item == null)
        return;

      //really we should pop up an invoice here :)
      Navigation.PushAsync(new AccountOrderDetailsView(e.Item as Order) { IsEnabled = false });

      OrdersList.SelectedItem = null;
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      if (viewModel.IsInitialized)
      {
        return;
      }
      viewModel.LoadOrdersCommand.Execute(null);
      viewModel.IsInitialized = true;

    }
	}
}
