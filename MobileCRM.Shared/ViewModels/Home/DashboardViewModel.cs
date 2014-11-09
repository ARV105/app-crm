﻿using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels.Home
{
    public class DashboardViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders
        {
            get;
            set;
        }

        IDataManager dataManager;
        BarGraphHelper chartHelper;

        public DashboardViewModel()
        {
            this.Title = "Sales Dashboard";
            this.Icon = "dashboard.png";

            dataManager = DependencyService.Get<IDataManager>();
            Orders = new ObservableCollection<Order>();


            IsInitialized = false;

        }

        //private Command loadOrdersCommand;
        ///// <summary>
        ///// Command to load accounts
        ///// </summary>
        //public Command LoadOrdersCommand
        //{
        //    get
        //    {
        //        return loadOrdersCommand ??
        //               (loadOrdersCommand = new Command(async () =>
        //                await ExecuteLoadOrdersCommand()));
        //    }
        //}

        //public async Task ExecuteLoadOrdersCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    Orders.Clear();

        //    IEnumerable<Order> orders = new List<Order>();

        //    orders = await dataManager.GetAllAccountOrdersAsync();

        //    foreach (var order in orders)
        //        Orders.Add(order);

        //    IsBusy = false;
        //}



        private Command loadSeedDataCommand;

        public Command LoadSeedDataCommand
        {
            get
            {
                return loadSeedDataCommand ??
                    (loadSeedDataCommand = new Command(async () =>
                        await ExecuteLoadSeedDataCommand()));
            }
        }

        public async Task ExecuteLoadSeedDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            //await dataManager.SyncAccounts();
            //await dataManager.SyncContacts();
            //await dataManager.SyncOrders();
            await dataManager.SeedData();

            IEnumerable<Order> orders = new List<Order>();
            orders = await dataManager.GetAllAccountOrdersAsync();

            foreach (var order in orders)
                Orders.Add(order);


            chartHelper = new BarGraphHelper(Orders, false);
            this.GetSalesPcts();

            IsBusy = false;
        }


        private string salesPctCombo;
        private string salesPctInk;
        private string salesPctPaper;
        private string salesPctPrinter;
        private string salesPctScanner;

        private void GetSalesPcts()
        {
            double dblTotalSales = (from o in chartHelper.CategoryData
                                    select o.Amount).Sum();

            double dblSalesPaper = (from o in chartHelper.CategoryData
                                    where o.Category == Order.PAPER
                                    select o).First().Amount / dblTotalSales * 100;

            double dblSalesCombo = (from o in chartHelper.CategoryData
                                    where o.Category == Order.COMBO
                                    select o).First().Amount / dblTotalSales * 100;

            double dblSalesInk = (from o in chartHelper.CategoryData
                                    where o.Category == Order.INK
                                    select o).First().Amount / dblTotalSales * 100;

            double dblSalesPrinter = (from o in chartHelper.CategoryData
                                    where o.Category == Order.PRINTER
                                    select o).First().Amount / dblTotalSales * 100;

            double dblSalesScanner = (from o in chartHelper.CategoryData
                                    where o.Category == Order.SCANNER
                                    select o).First().Amount / dblTotalSales * 100;

            this.ComboSalesPct = String.Format("{0:0}%", dblSalesPaper);
            this.InkSalesPct = String.Format("{0:0}%", dblSalesInk);
            this.PaperSalesPct = String.Format("{0:0}%", dblSalesPaper);
            this.PrinterSalesPct = String.Format("{0:0}%", dblSalesPrinter);
            this.ScannerSalesPct = String.Format("{0:0}%", dblSalesScanner);
                           
        }


        public string ComboSalesPct
        {
            get
            {
                return salesPctCombo;
            }
            set
            {
                salesPctCombo = value;
                OnPropertyChanged("ComboSalesPct");
            }
        }


        public string InkSalesPct
        {
            get
            {
                return salesPctInk;
            }
            set
            {
                salesPctInk = value;
                OnPropertyChanged("InkSalesPct");
            }
        }

        public string PaperSalesPct
        {
            get
            {
                return salesPctPaper;
            }
            set
            {
                salesPctPaper = value;
                OnPropertyChanged("PaperSalesPct");
            }
        }

        public string PrinterSalesPct
        {
            get
            {
                return salesPctPrinter;
            }
            set
            {
                salesPctPrinter = value;
                OnPropertyChanged("PrinterSalesPct");
            }
        }

        public string ScannerSalesPct
        {
            get
            {
                return salesPctScanner;
            }
            set
            {
                salesPctScanner = value;
                OnPropertyChanged("ScannerSalesPct");
            }
        }


    }
}
