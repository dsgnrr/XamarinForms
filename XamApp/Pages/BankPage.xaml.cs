using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XamApp.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BankPage : ContentPage
    {
        ObservableCollection<Account> accounts = new ObservableCollection<Account>();
        Account selectedSender;
        Account selectedReceiver;
        CancellationTokenSource cancellationTokenSource;



        public BankPage()
        {
            InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            AccountHolder accountHolder1 = new AccountHolder { LastName = "Smith", FirstName = "John", CreditRating = 700, RegistrationDate = DateTime.Now };
            AccountHolder accountHolder2 = new AccountHolder { LastName = "Doe", FirstName = "Jane", CreditRating = 750, RegistrationDate = DateTime.Now };
            Account account1 = new Account(accountHolder1) { Balance = 200 };
            Account account2 = new Account(accountHolder2) { Balance = 1567 };
            accounts.Add(account1);
            accounts.Add(account2);
            senderPicker.ItemsSource = accounts;
            receiverPicker.ItemsSource = accounts;

            RefreshAccounts();
        }

        private async void sendButton_Clicked(object sender, EventArgs e)
        {
            if (selectedSender == null || selectedReceiver == null)
            {
                await DisplayAlert("Error", "Please select sender and receiver accounts.", "OK");
                return;
            }

            double amount;
            if (!double.TryParse(amountEntry.Text, out amount))
            {
                await DisplayAlert("Error", "Please enter a valid amount.", "OK");
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Run(async () =>
                {
                    MoneyTransfer.Transfer(selectedSender, selectedReceiver, amount, cancellationTokenSource.Token);
                    await Task.Delay(10000); 
                });
                RefreshAccounts();
                cancellationTokenSource.Cancel();
                await DisplayAlert("Success", "Money transfer completed.", "OK");
            }
            catch (OperationCanceledException)
            {
                await DisplayAlert("Cancelled", "Money transfer cancelled.", "OK");
                cancellationTokenSource.Cancel();
            }

        }

        private void cancelButton_Clicked(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
            }
            else
            {
                DisplayAlert("Error", "There is no transfer to cancel.", "OK");
            }
        }

        void SenderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedSender = senderPicker.SelectedItem as Account;
        }

        void ReceiverPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedReceiver = receiverPicker.SelectedItem as Account;
        }

        void RefreshAccounts()
        {
            accountsTableView.Root = new TableRoot();
            foreach (var account in accounts)
            {
                var section = new TableSection($"Accounts of {account.Owner.FirstName} {account.Owner.LastName}");
                section.Add(new TextCell { Text = $"Balance: {account.Balance}" });
                accountsTableView.Root.Add(section);
            }
        }
    }
}