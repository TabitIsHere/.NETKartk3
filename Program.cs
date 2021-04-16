using System;
using System.Net;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace zad2
{
        public class StudentDB
    {
        public string Imie {get;set;}
        public string Nazwisko  {get;set;}
        public int ID {get;set;}
        public double Ocena {get;set;}
    
        public int getStudent()
        {
            Console.WriteLine("Podaj imie studenta: ");
            Imie = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko studenta: ");
            Nazwisko = Console.ReadLine();
            Console.WriteLine("Podaj indeks studenta: ");
            ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj ocene studenta: ");
            Ocena = Convert.ToDouble(Console.ReadLine());
            return 0; 
        }
    }

    public class Students: DbContext
    {
        public virtual DbSet<StudentDB> StudentDB {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=.\database\blogging.db");
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new Students();
            StudentDB actualStudentDB = new StudentDB();

            Console.WriteLine("1. Dodaj studenta \n2. Pokaz studentow o danym imieniu\n3. Pokaz wszystkich");
            int caseSwitch = Convert.ToInt32(Console.ReadLine());
            
            switch (caseSwitch)
            {
                case 1:
                    actualStudentDB.getStudent();
                    context.StudentDB.Add(actualStudentDB);
                    context.SaveChanges();
                    break;
                case 2:
                    Console.WriteLine("Podaj imie: ");
                    string chosenStudent = Console.ReadLine();
                    var selectedStudents = context.StudentDB.FromSqlRaw("SELECT * FROM StudentDB WHERE Imie = '"+chosenStudent+"'");
                    foreach(var selectedStudent in selectedStudents)
                    {
                        Console.WriteLine(selectedStudent.Imie+" "+selectedStudent.Nazwisko+" - "+selectedStudent.ID+" : "+selectedStudent.Ocena);
                    }
                    break;
                case 3:
                    var selectedStudentsAll = context.StudentDB.FromSqlRaw("SELECT * FROM StudentDB");
                    foreach(var selectedStudentAll in selectedStudentsAll)
                    {
                        Console.WriteLine(selectedStudentAll.Imie+" "+selectedStudentAll.Nazwisko+" - "+selectedStudentAll.ID+" : "+selectedStudentAll.Ocena);
                    }
                    break;
                default:
                    Console.WriteLine("Nie ma takiej opcji");
                    break;
            }
        }
    }
}
