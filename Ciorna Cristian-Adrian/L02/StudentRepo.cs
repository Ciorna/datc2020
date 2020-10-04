using System.Collections.Generic;

namespace ApiStudent
{
    public static class StudentRepo
    {
        public static List<Student> Student=new List<Student>()
        {
            new Student()
            {
                id=1,
                nume="Ciorna",
                prenume="Cristi",
                facultate="AC",
                an=4
            },
            new Student()
            {
                id=2,
                nume="Popescu",
                prenume="Romeo",
                facultate="AC",
                an=2
            }
        };
    }
}