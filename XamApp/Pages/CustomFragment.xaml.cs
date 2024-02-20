using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamApp.Classes;
using Java.Lang;

namespace XamApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Note
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
    public partial class CustomFragment : ContentPage
    {
        private View fragmentList;
        private View currentFragmentContent;
        private List<Note> notes;
        private bool _isAlbum;

        private double width = 0;
        private double height = 0;
        private bool isAlbum
        {
            get
            {
                return _isAlbum;
            }
            set
            {
                mainPage.RowDefinitions[0]
                    .Height = value == true
                    ? new GridLength(0.6, GridUnitType.Star)
                    : new GridLength(0.2, GridUnitType.Star);
                _isAlbum = value;
            }
        }
        public CustomFragment()
        {
            InitializeComponent();
            notes = new List<Note>()
            {
                new Note()
                {
                    title="First",
                    content="Its first Note"
                },
                new Note()
                {
                    title="Second",
                    content="Its second Note"
                },
                new Note()
                {
                    title="Third",
                    content="Its third Note"
                },
            };
        }
        private void resetMainPage()
        {
            fragmentPage.ColumnDefinitions.Clear();
            fragmentPage.RowDefinitions.Clear();
            fragmentPage.Children.Clear();
        }
        private void portraitOrientation(View content)
        {
            resetMainPage();

            if (currentFragmentContent != null)
            {
                fragmentPage.RowDefinitions = new RowDefinitionCollection()
            {
                new RowDefinition(){Height=new GridLength(1,GridUnitType.Star)},
                new RowDefinition(){Height=new GridLength(0.2,GridUnitType.Star)}
            };
                Grid.SetRow(content, 0);
                fragmentPage.Children.Add(content);
                Button but = new Button();
                but.Text = "Back to list";
                but.Clicked += new EventHandler(backToList);
                Grid.SetRow(but, 1);
                fragmentPage.Children.Add(but);
            }
            else
            {
                fragmentPage.Children.Add(content);
            }
        }
        private void albumOrientation(View leftContent, View rightContent = null)
        {
            resetMainPage();
            fragmentPage.ColumnDefinitions = new ColumnDefinitionCollection()
            {
                new ColumnDefinition() {Width=new GridLength(0.5,GridUnitType.Star)},
                new ColumnDefinition() {Width=new GridLength(1,GridUnitType.Star)}
            };
            var frame = new Frame();
            frame.BorderColor = Color.FromHex("#1f1f1f");
            frame.Content = leftContent;
            Grid.SetColumn(leftContent, 0);
            fragmentPage.Children.Add(leftContent);
            if (rightContent != null)
            {
                Grid.SetColumn(rightContent, 1);
                fragmentPage.Children.Add(rightContent);
            }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    isAlbum = true;
                    albumOrientation(buildFragmentList(),
                        currentFragmentContent == null ? null : currentFragmentContent);
                }
                else
                {
                    fragmentList = buildFragmentList();
                    isAlbum = false;
                    portraitOrientation(currentFragmentContent == null ? buildFragmentList() : currentFragmentContent);
                }
            }
        }
        private ScrollView buildFragmentContent(string content)
        {
            ScrollView scroll = new ScrollView();
            StackLayout stackList = new StackLayout();
            Label label = new Label();
            label.Text = content;
            label.FontSize = 18;
            stackList.Children.Add(label);
            scroll.Content = stackList;
            return scroll;
        }
        private ScrollView buildFragmentList()
        {
            ScrollView scroll = new ScrollView();
            StackLayout stackList = new StackLayout();
            foreach (var note in notes)
            {
                var button = new Button();
                note.id = button.Id;
                button.Text = note.title;
                button.Clicked += new EventHandler(openFragment);
                stackList.Children.Add(button);
            }
            scroll.Content = stackList;
            return scroll;
        }
        private void openFragment(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            if (!isAlbum)
            {
                fragmentList = fragmentPage.Children[0];
                resetMainPage();
                var noteContent = notes.Where(n => n.id == but.Id).FirstOrDefault();

                currentFragmentContent = buildFragmentContent(noteContent.content);
                portraitOrientation(currentFragmentContent);
            }
            else
            {
                fragmentList = fragmentPage.Children[0];
                var noteContent = notes.Where(n => n.id == but.Id).FirstOrDefault();
                currentFragmentContent = buildFragmentContent(noteContent.content);
                albumOrientation(fragmentList, currentFragmentContent);
            }

        }
        private void backToList(object sender, EventArgs e)
        {
            resetMainPage();
            currentFragmentContent = null;
            fragmentPage.Children.Add(fragmentList);
        }

    }
}