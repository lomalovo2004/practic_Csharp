using System;

namespace StudentClass
{
    class Program
    {
        static void Main(string[] args)
        {

            Student student1 = new Student("Иванов", "Иван", "Иванович", "М8О", 113, 2023, "C#");


            Console.WriteLine($"Фамилия: {student1.Surname}");
            Console.WriteLine($"Имя : {student1.Name}");
            Console.WriteLine($"Отчество : {student1.Secondname}");
            Console.WriteLine($"Группа: {student1.Group}");
            Console.WriteLine($"Институт: {student1.Univer}");
            Console.WriteLine($"Год поступления : {student1.YearOfAdmission}");
            Console.WriteLine($"Курс практики : {student1.Practic}");
            Console.WriteLine($"Курс: {student1.Kurs(student1)}");
            


            Student student2 = new Student();
            student2.Surname = "Петров";
            student2.Name = "Петр";
            student2.Secondname = "Петрович";
            student2.Univer = "М8О";
            student2.Group = 213;
            student2.YearOfAdmission = 2022;
            student2.Practic = "C#";


            Console.WriteLine($"Фамилия: {student2.Surname}");
            Console.WriteLine($"Имя : {student2.Name}");
            Console.WriteLine($"Отчество : {student2.Secondname}");
            Console.WriteLine($"Группа: {student2.Group}");
            Console.WriteLine($"Институт: {student2.Univer}");
            Console.WriteLine($"Год поступления: {student2.YearOfAdmission}");
            Console.WriteLine($"Курс практики : {student2.Practic}");
            Console.WriteLine($"Курс практики : {student2.Practic}");
            Console.WriteLine($"Курс: {student2.Kurs(student2)}");


            Console.WriteLine($"Равны ли студенты? {student1.Equals(student2)}");
        }
    }

    class Student : IEquatable<Student>
    {
        // Поля класса
        private string surname;
        private string name;
        private string secondname;
        private string univer;
        private int group;
        private int yearOfAdmission;
        private string practic;



        public Student(string Surname, string Name, string Secondname, string univer, int group, int yearOfAdmission, string Practic)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Secondname = Secondname;
            this.univer = univer;
            this.group = group;
            this.yearOfAdmission = yearOfAdmission;
            this.Practic = Practic;
            this.Surname = Surname ?? throw new ArgumentNullException(nameof(Surname));
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Secondname = Secondname ?? throw new ArgumentNullException(nameof(Secondname));
            this.univer = univer ?? throw new ArgumentNullException(nameof(univer));
            this.Practic = Practic ?? throw new ArgumentNullException(nameof(Practic));
        }


        public Student()
        {
        }


        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Secondname
        {
            get { return secondname; }
            set { secondname = value; }
        }

        public string Univer
        {
            get { return univer; }
            set{ univer = value; }
        }
        public int Group
        {
            get { return group; }
            set { group = value; }
        }

        public int YearOfAdmission
        {
            get { return yearOfAdmission; }
            set { yearOfAdmission = value; }
        }

        public string Practic
        {
            get { return practic; }
            set { practic = value; }
        }





        public bool Equals(Student other)
        {
            if (other == null)
                return false;

            return this.group == other.group;
        }


        public int Kurs(Student x)
        {
            return x.group / 100;
        }
        

    }
}
