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
	public partial class AccountOrdersView
	{
    OrdersViewModel viewModel;
    string accountId;
		public AccountOrdersView (string accountId)
		{
			InitializeComponent ();
      this.accountId = accountId; ;
      this.BindingContext = this.viewModel = new OrdersViewModel(true, accountId);
		}

    public void NewOrderClicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new NewOrderView(accountId));
    }


    public void OnItemSelected(object sender, ItemTappedEventArgs e)
    {
      if (e.Item == null)
        return;

      //we push modal for signature pad
      Navigation.PushModalAsync(new AccountOrderDetailsView(e.Item as Order));

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
