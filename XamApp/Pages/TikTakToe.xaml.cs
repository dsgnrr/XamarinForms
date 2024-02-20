using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamApp.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TikTakToe : ContentPage
    {
        private string currentSymbol;
        private Button[,] buttons;
        public TikTakToe()
        {
            InitializeComponent();

            currentSymbol = "X";

            buildGameField(gamePage);
          
        }
        public void buildGameField(Grid mainScreen)
        {
            Grid grid = new Grid()
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.FromHex("#806491"),
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition()
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };
            currentSymbol = "X";
            walkId.Text = $"Now: {currentSymbol}";
            buttons = new Button[3, 3];
            int number = 0;
            for (int row = 0; row < (buttons.GetUpperBound(0) + 1); row++)
            {
                for (int col = 0; col < (buttons.GetUpperBound(1) + 1); col++)
                {
                    buttons[row, col] = new Button()
                    {
                        CornerRadius = 8,
                        FontSize = 30,
                        BackgroundColor = Color.FromHex("#B9848C")
                    };
                    number++;
                    buttons[row, col].Clicked += new EventHandler(ClickHandler);
                    grid.Children.Add(buttons[row, col], col, row);
                }
            }
            Grid.SetRow(grid, 2);
            mainScreen.Children.Add(grid);
        }
        private bool checkRows(string sign)
        {
            bool haveWin = true;
            if (buttons != null)
            {
                for (int row = 0; row < (buttons.GetUpperBound(0) + 1); row++)
                {
                    haveWin = true;
                    for (int col = 0; col < (buttons.GetUpperBound(1) + 1); col++)
                    {
                        if (buttons[row, col].Text != sign)
                        {
                            haveWin = false;
                            break;
                        }
                    }
                    if (haveWin)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool checkCols(string sign)
        {
            bool haveWin = true;
            if (buttons != null)
            {
                for (int col = 0; col < (buttons.GetUpperBound(0) + 1); col++)
                {
                    haveWin = true;
                    for (int row = 0; row < (buttons.GetUpperBound(1) + 1); row++)
                    {
                        if (buttons[row, col].Text != sign)
                        {
                            haveWin = false;
                            break;
                        }
                    }
                    if (haveWin)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool checkDiagonales(string sign)
        {
            int col = 0;
            bool haveWin = true;
            for (int row = 0; row < (buttons.GetUpperBound(0) + 1); row++)
            {
                if (buttons[row, col].Text != sign)
                {
                    haveWin = false;
                    break;
                }
                col++;
            }
            if (haveWin)
            {
                return true;
            }
            haveWin = true;
            col = 2;
            for (int row = 0; row < (buttons.GetUpperBound(0) + 1); row++)
            {
                if (buttons[row, col].Text != sign)
                {
                    haveWin = false;
                    break;
                }
                col--;
            }
            if (haveWin)
            {
                return true;
            }
            return false;
        }
        private bool checkFieldEmpty()
        {
            for (int row = 0; row < (buttons.GetUpperBound(0) + 1); row++)
            {
                for (int col = 0; col < (buttons.GetUpperBound(1) + 1); col++)
                {
                    if (string.IsNullOrEmpty(buttons[row,col].Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void checkWin()
        {
            string[] signs = new string[] { "X", "O" };
            for(int i = 0; i < signs.Length; i++)
            {
                if (checkRows(signs[i])|| checkCols(signs[i])|| checkDiagonales(signs[i]))
                {
                    buildGameField(gamePage);
                    DependencyService.Get<IMessage>().LongAlert($"{signs[i]} win");
                    currentSymbol = "X";
                    return;
                }
            }
            if (!checkFieldEmpty())
            {
                buildGameField(gamePage);
                DependencyService.Get<IMessage>().LongAlert("No winners");
                return;
            }
        }
        private void ClickHandler(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (string.IsNullOrEmpty(button.Text))
            {
                button.Text = currentSymbol;
                currentSymbol = currentSymbol == "X" ? "O" : "X";
                walkId.Text = $"Now: {currentSymbol}";
                checkWin();
            }
            //DependencyService.Get<IMessage>().LongAlert("ffffff");
        }

        private void startNewGame(object sender, EventArgs e)
        {
            buildGameField(gamePage);
        }
    }

}