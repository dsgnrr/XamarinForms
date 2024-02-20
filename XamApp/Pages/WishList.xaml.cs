using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp
{
	public class Gift
	{
		public string imageUrl { get; set; }
		public string name { get; set; }
		public double price { get; set; }
	}
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WishList : ContentPage
	{
		private List<Gift> gifts;
		public WishList ()
		{
			InitializeComponent ();

			gifts = new List<Gift>()
			{
				new Gift()
				{
					imageUrl="ps5.jpg",
					name="Play Station 5",
					price=96.75
				},
                new Gift()
                {
                    imageUrl="xbox.jpg",
                    name="Xbox",
                    price=150.99
                },
                new Gift()
                {
                    imageUrl="lviv.jpg",
                    name="Львівське Різдвяне 20х0.5л",
                    price=999.99
                },
                new Gift()
                {
                    imageUrl="f16.jpg",
                    name="F-16",
                    price=13000000
                }
            };
			totalPrice.Text = $"Total: ${gifts.Sum(t => t.price)}";
			foreach(var gift in gifts)
			{
				var item = buildListItem(gift);
				giftListView.Children.Add(item);
			}
        }
		private Grid buildListItem(Gift gift)
		{
			Grid listItem = new Grid()
			{
				ColumnDefinitions =
				{
					new ColumnDefinition(),
					new ColumnDefinition(),
					new ColumnDefinition(),
					new ColumnDefinition()
				}
			};
			Image img = new Image();
			img.Source = gift.imageUrl;
			Grid.SetColumn(img, 0);
			listItem.Children.Add(img);

			Label nameView = new Label();
			nameView.VerticalTextAlignment = TextAlignment.Center;
			nameView.FontSize = 16;
			nameView.TextColor = Color.Black;
			nameView.Text = gift.name;
            Grid.SetColumn(nameView, 1);
            listItem.Children.Add(nameView);

            Label priceView = new Label();
			priceView.VerticalTextAlignment = TextAlignment.Center;
            priceView.FontSize = 16;
			priceView.TextColor = Color.Black;
            priceView.Text = $"${gift.price}";
            Grid.SetColumn(priceView, 2);
            listItem.Children.Add(priceView);

			CheckBox check = new CheckBox();
            Grid.SetColumn(check, 3);
            listItem.Children.Add(check);

			return listItem;
        }
		
	}
}