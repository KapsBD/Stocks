using Microsoft.Identity.Client;
using Prism.Mvvm;
using System;

namespace SignalRStocks.Models
{
    public class StockUpdate : BindableBase
    {
        private DateTime updatedTime;
        public DateTime UpdatedTime
        {
            get => updatedTime;
            set => SetProperty(ref updatedTime, value);
        }
        private string symbol;
        public string Symbol
        {
            get => symbol;
            set => SetProperty(ref symbol, value);
        }
        private double price;
        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        private double change;
        public double Change
        {
            get => change;
            set => SetProperty(ref change, value);
        }
    }
}

