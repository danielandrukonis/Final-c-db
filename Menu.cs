using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsSystem;

namespace StudentsSystem;

public class Menu
{
    public void InitiateMenu()
    {
        bool isAlive = true;
        while (isAlive)
        {

            Console.WriteLine("Welcome to Daniels School Management System");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("1. Create department / Add students / Add lectures");
            Console.WriteLine("2. Add student or lecture to deparment");
            Console.WriteLine("3. Add student to department");
            Console.WriteLine("4. Transfer Student to Department");
            Console.WriteLine("5. Print Student in selected department");
            Console.WriteLine("6. Print lectures in selected department");
            Console.WriteLine("7. Print lectures by student");
            Console.WriteLine("8. Exit");
            Console.WriteLine("");

            var input = GetSelection();

            switch (input)
            {
                case 1:
                    printChoiceDeparmentStudentLecture();
                    InitiateMenu();
                    break;
                case 2:
                    AddLectureToDepartment();
                    InitiateMenu();
                    break;
                case 3:
                    AddStudentToDepartment();
                    InitiateMenu();
                    break;
                case 4:
                    TransferStudentToDepartment();
                    InitiateMenu();
                    break;
                case 5:
                    PrintStudentsInSelectedDepartment();
                    InitiateMenu();
                    break;
                case 6:
                    PrintLecturesInSelectedDepartment();
                    InitiateMenu();
                    break;
                case 7:
                    PrintLecturesByStudent();
                    InitiateMenu();
                    break;

                case 8:
                    Console.WriteLine("Thank you for visiting Daniels restaurant");
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("This is not valid choice");
                    break;
            }
        }
    }


    public int GetSelection()
    {
        bool isSuccess = Int32.TryParse(Console.ReadLine(), out int result);
        if (isSuccess)
        {
            return result;
        }
        Console.WriteLine("Wrong format");
        return 0;
    }

    public void printChoiceDeparmentStudentLecture()
    {
        Console.WriteLine("1. Create department, add student and add lecture");
        Console.WriteLine("2. Main menu");
        Console.WriteLine("3. Exit");

        var input = GetSelection();

        switch (input)
        {
            case 1:
                CreateDepartmentStudentsLectures();
                Console.WriteLine("New department, student and lecture added");
                printChoiceDeparmentStudentLecture();
                break;
            case 2:
                InitiateMenu();
                break;
            case 3:
                Console.WriteLine("Thank you for visiting Daniels School System");
                System.Environment.Exit(0);
                break;
              
        }
    }

    public void CreateDepartmentStudentsLectures()
    {
        using SystemContext context = new SystemContext();
        Console.WriteLine("Please enter Department name");
        var department = new Department(Console.ReadLine());
        Console.WriteLine("Enter Student name");
        department.Students.Add(new Student(Console.ReadLine()));
        Console.WriteLine("Enter Lecture name");
        department.Lectures.Add(new Lecture(Console.ReadLine()));
        context.Departments.Add(department);
        context.SaveChanges();

    }

    public static void AddLectureToDepartment()
    {
        using var db = new SystemContext();

        Console.WriteLine("Enter lecture name");
        string newLectureName = Console.ReadLine();
        var lecture = new Lecture(newLectureName);

        Console.Write("Department name to add details.\n Existing departments: ");
        foreach (Department department1 in db.Departments)
        {
            Console.Write($"{department1.Name} ");
        }

        Console.WriteLine("\nwhere to add");
        string departmentName = Console.ReadLine();

        Byte[] b1 = new Byte[16];
        Guid lecId = new Guid(b1);
        Guid depId = new Guid(b1);
        string depName;

        foreach (Department department in db.Departments)
        {
            if (departmentName == department.Name)
            {
                depId = department.Id;
                depName = department.Name;
                Console.WriteLine($"checking {department.Id} {department.Name}");
            }
        }
        if (depId == Guid.Empty)
        {
            Console.WriteLine("department doesn't exist");
            var department = new Department(departmentName);
            department.Lectures.Add(lecture);
            db.Departments.Add(department);
            db.SaveChanges();
        }
        else
        {
            var department = new Department(departmentName);
            lecture.DepartmentId = depId;
            db.Lectures.Add(lecture);
            db.SaveChanges();
        }
    }

    public static void AddStudentToDepartment()
    {
        using var db = new SystemContext();
        Console.WriteLine("Enter student name");

        string newStudentName = Console.ReadLine();
        var student = new Student(newStudentName);

        Console.Write("Which Department for a new student addition");
        foreach (Department department1 in db.Departments)
        {
            Console.Write($"{department1.Name} ");
        }
        Console.WriteLine("");
        string departmentName = Console.ReadLine();

        Byte[] b1 = new Byte[16];
        Guid depId = new Guid(b1);
        string depName;

        foreach (Department department in db.Departments)
        {
            if (departmentName == department.Name)
            {
                depId = department.Id;
                depName = department.Name;
                Console.WriteLine($"Checking {department.Id} {department.Name}");
            }
        }
        if (depId == Guid.Empty)
        {
            Console.WriteLine("deparments doesn't exist");
        }
        else
        {
            student.DepartmentId = depId;
            db.Students.Add(student);
            db.SaveChanges();
        }
    }

    public static void TransferStudentToDepartment()
    {
        using var db = new SystemContext();
        Console.WriteLine("Select student");


        Byte[] b1 = new Byte[16];
        Guid depId = new Guid(b1);

        foreach (Student student2 in db.Students)
        {
            Console.Write($"{student2.Name} ");
        }
        Console.WriteLine("");
        string studentName = Console.ReadLine();

        Console.WriteLine("Select where to transfer");
        foreach (Department department in db.Departments)
        {
            Console.Write($"{department.Name} ");
        }
        Console.WriteLine("");
        var departmentName = Console.ReadLine();

        foreach (Department department in db.Departments)
        {
            if (departmentName == department.Name)
            {
                depId = department.Id;
            }
        }
        var student = new Student(studentName);

        if (depId == Guid.Empty)
        {
            Console.WriteLine("department doesn't exist");
        }
        else
        {
            student.DepartmentId = depId;
            db.Students.Add(student);
            db.SaveChanges();
        }
    }

    public static void PrintStudentsInSelectedDepartment()
    {
        Console.Write("pasirinkite norima department: ");
        using var db = new SystemContext();

        foreach (Department department in db.Departments)
        {
            Console.Write($"{department.Name} ");
        }
        Console.WriteLine($"");
        string departmentName = Console.ReadLine();

        Console.WriteLine($"Students existing {departmentName} in department:");
        foreach (Student student in db.Students)
        {
            if (departmentName == student.Department.Name)
            {
                Console.WriteLine(student.Name);
            }
        }
    }
    public static void PrintLecturesInSelectedDepartment()
    {
        Console.Write("Select favourite department ");
        using var db = new SystemContext();

        foreach (Department department in db.Departments)
        {
            Console.Write($"{department.Name} ");
        }
        Console.WriteLine($"");
        string departmentName = Console.ReadLine();
        Console.WriteLine($"{departmentName} Department lectures:");
        foreach (Lecture lecture in db.Lectures)
        {
            if (departmentName == lecture.Department.Name)
            {
                Console.WriteLine(lecture.Name);
            }
        }
    }

    public static void PrintLecturesByStudent()
    {
        Console.Write("Enter students name ");
        using var db = new SystemContext();

        Byte[] b1 = new Byte[16];
        Guid studDepId = new Guid(b1);

        foreach (Student student in db.Students)
        {
            Console.Write($"{student.Name} ");
        }
        Console.WriteLine($"");
        string studentName = Console.ReadLine();

        foreach (Student student in db.Students)
        {
            if (studentName == student.Name)
            {
                studDepId = student.DepartmentId;
            }
        }
        Console.WriteLine($"{studentName} lectures:");
        foreach (Lecture lecture in db.Lectures)
        {
            if (lecture.DepartmentId == studDepId)
            {
                Console.WriteLine($"{lecture.Name}");
            }
        }
    }
}

