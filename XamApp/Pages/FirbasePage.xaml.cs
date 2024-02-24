using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamApp.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirbasePage : ContentPage
    {
        private DataContext _dataContext;
        public FirbasePage()
        {
            InitializeComponent();
            _dataContext = new DataContext("YOUR_DB_URL");
        }
        private Label CreateLabel(string text)
        {
            var label = new Label();
            label.FontSize = 20;
            label.Text = text;
            return label;

        }
        private async void addStudent_Click(object sender, EventArgs e)
        {
            studentsStack.Children.Clear();
            Student stud = new Student()
            {
                FirstName=fNameEntry.Text,
                LastName=lNameEntry.Text,
                GroupName=gNameEntry.Text,
                Speciality=specEntry.Text
            };
            await _dataContext.AddPerson(stud);
            var students = await _dataContext.GetAllStudents();
            foreach(var student in students)
            {
                var label = CreateLabel($"Id: {student.Id},First name: {student.FirstName}, Last name: {student.LastName}, Group name: {student.GroupName}, Speciality: {student.Speciality}");
                studentsStack.Children.Add(label);
            }
        }
    }
}