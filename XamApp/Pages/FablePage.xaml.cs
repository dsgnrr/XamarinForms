using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FablePage : ContentPage
    {
        private readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private Dictionary<string, string> fables = new Dictionary<string, string>()
        {
            { "when.txt", "Однажды утром\nВ солнечный день\nЛетом\nВ полдень\nВечером\nВесной\nПосле обеда\nВо время засухи\nВ день открытия\nВ обеденное время" },
            { "where.txt","В лесу\nна берегу реки\nв поле\nв джунглях\nУ реки\nВ гнезде\nВ реке\nВ саванне\nВ зоопарке\nНа мосту" },
            { "who.txt" ,"лиса\nчерепаха\nмуравей\nлев\nволк\nкукушка\nкрокодил\nслон\nпавлин\nкозел"},
            { "withWhom.txt", "С вороной\nС зайцем\nС цикадой\nС мышью\nС ягненком\nС воробьем\nС обезьяной\nС муравьем\nС вороной\nС другим козлом" },
            { "whatTheyDid.txt","Лиса уговаривала ворону отпустить сыр\nУстроили соревнование на скорость\nМуравей работал, а цикада пела\nЛев освободил мышь, которую поймал\nВолк искал повод съесть ягненка\nКукушка похитила гнездо воробья\nКрокодил пытался съесть обезьяну\nСлон наступил на муравья\nПавлин хвастался своими перьями\nОба козла пытались пройти по мосту одновременно" },
            { "whatHappened.txt", "Ворона открыла клюв, и сыр упал прямо в лапы лисы\nЧерепаха выиграла, потому что заяц заснул\nЗимой муравей имел еду, а цикада голодала\nПозже мышь освободила льва из сети\nВолк съел ягненка без всякой причины\nВоробей остался без дома\nОбезьяна убежала, используя свою хитрость\nМуравей укусил слона\nВорона почувствовала себя некрасивой\nОба козла упали в реку" },
            { "morality.txt","Не верьте лести\nУпорство и терпение ведут к успеху\nТруд прежде всего\nНикогда не недооценивайте маленькие добрые дела\nТиран найдет любую причину для своих действий\nНе делайте другим то, что не хотите для себя\nИнтеллект часто превосходит силу\nНикто не слишком мал, чтобы отстоять свои права\nКрасота - это не все\nКомпромисс важнее самолюбия" }
        };
        public FablePage()
        {
            InitializeComponent();
            CreateFableFiles();
        }
        private void generateFable()
        {
            fableStack.Children.Clear();
            foreach(var fable in fables)
            {
                var filename = Path.Combine(folderPath, fable.Key);
                using (StreamReader reader=new StreamReader(filename))
                {
                    string line;
                    var randomString = new Random().Next(0, 9);
                    var count = 0;
                    while((line = reader.ReadLine()) != null)
                    {
                        if(count == randomString)
                        {
                            var label = CreateLabel(line.ToLower());
                            fableStack.Children.Add(label);
                            break;
                        }
                        count++;
                    }
                }
            }
        }
        private Label CreateLabel(string text)
        {
            Color[] colors = new Color[] { Color.DarkRed, Color.DarkGreen, Color.DarkBlue, Color.DarkKhaki, Color.DarkOrange, Color.DarkSalmon, Color.DarkSeaGreen };
            var label = new Label();
            var randColor = new Random().Next(0, colors.Length);
            label.TextColor = colors[randColor];
            label.FontSize = 20;
            label.Text = text;
            return label;

        }
        private void CreateFableFiles()
        {
            foreach(var fable in fables)
            {
                var filename = Path.Combine(folderPath, fable.Key);
                if (!File.Exists(filename))
                {
                    File.WriteAllText(filename, fable.Value);
                }
            }
        }
        private void generateFableClick(object sender, EventArgs e)
        {
            generateFable();
        }
    }
}