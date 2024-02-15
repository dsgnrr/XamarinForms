using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Grid grid = new Grid()
            { 
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition()
                }
            };
            
            Content = grid;
        }
        public void buildGameField() 
        {
            
        }
        private void ClickHandler(object sender, EventArgs e)
        {
        }
    }
}
