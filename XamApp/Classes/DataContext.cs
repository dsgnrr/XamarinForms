using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamApp.Classes
{
    public class DataContext
    {
        private FirebaseClient firebase;
        public DataContext(string databaseUrl)
        {
            firebase = new FirebaseClient(databaseUrl);
        }
        public async Task<List<Student>> GetAllStudents()
        {
            var students = await firebase
            .Child("Students") // Это будет вашей "таблицей"
            .OnceAsync<Student>();

            return students.Select(item => new Student
            {
                Id = item.Object.Id,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                GroupName = item.Object.GroupName,
                Speciality=item.Object.Speciality
            }).ToList();

        }

        public async Task AddPerson(Student stud)
        {
            await firebase
                .Child("Students")
                .PostAsync(new Student() 
                { 
                    FirstName=stud.FirstName,
                    LastName=stud.LastName,
                    GroupName=stud.GroupName,
                    Id=Guid.NewGuid(),
                    Speciality=stud.Speciality
                });
        }
    }
}
