using Microsoft.AspNetCore.SignalR.Client;
using Prism.Mvvm;
using SignalRStocks.Models;
using Stocks;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SignalRStocks.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Properties
        private static string _baseUrl = "https://js.devexpress.com/Demos/NetCore/liveUpdateSignalRHub";

        private ObservableCollection<StockUpdate> _stocks;
        public ObservableCollection<StockUpdate> Stocks
        {
            get => _stocks;
            set => SetProperty(ref _stocks, value);
        }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            Stocks = new ObservableCollection<StockUpdate>();

            // Initialize stocks from database service
            StartHub();
        }

        #endregion

        #region Methods
        public void StartHub()
        {
            StockUpdate stockUpdate = new StockUpdate();
            var _hubConnection = new HubConnectionBuilder()
                .WithUrl(_baseUrl).Build();
            _hubConnection.On<StockUpdate>("updateStockPrice", data =>
            {
                App.Current?.Dispatcher.Invoke(() =>
                {
                    StockUpdate stockUpdate = new StockUpdate();
                    var item = Stocks?.FirstOrDefault(x => x.Symbol == data.Symbol);
                    if (item != null)
                    {
                        int i = Convert.ToInt32(Stocks?.IndexOf(item));
                        if (i > 0 && Stocks != null)
                        {
                            Stocks[i].Change = data.Change;
                            Stocks[i].Price = data.Price;
                            Stocks[i].UpdatedTime = DateTime.Now;
                        }
                    }
                    else
                    {

                        Stocks?.Add(new StockUpdate()
                        {
                            Change = data.Change,
                            Price = data.Price,
                            Symbol = data.Symbol,
                            UpdatedTime = DateTime.Now
                });
                    }
                });
            });
            try
            {
                _hubConnection.StartAsync().Wait();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        #endregion
    }
}
