using JudoDotNetXamarin;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JudoPayApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCheckOut_Clicked(object sender, EventArgs e)
        {
            var judo = new Judo()
            {
                JudoId = "100699-396",
                Token = "vNJCgGYhlY1CSPCh",
                Secret = "9b9311eadf62a180d8e7f7c9224f831f6fae0dbdb15e61c29f52b85b33deb8a3",
                Environment = JudoEnvironment.Sandbox,
                Amount = 1.50m,
                Currency = "USD",
                ConsumerReference = "YourUniqueReference"
            };

            var paymentPage = new PaymentPage(judo);
            Navigation.PushAsync(paymentPage);
            paymentPage.resultHandler += Handler;

        }

        private async void Handler(object sender, IResult<ITransactionResult> e)
        {
            await Navigation.PopAsync();

            if (e.HasError)
            {
                await DisplayAlert("Payment error", "Code: " + e.Error.Code, "OK");
            }
            else if ("Success".Equals(e.Response.Result))
            {
                await DisplayAlert("Payment succeful", "Receipt ID: " + e.Response.ReceiptId, "OK");
            }
        }
    }
}
