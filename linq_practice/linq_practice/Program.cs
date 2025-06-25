using System.Linq;

namespace linq_practice
{
    class Program
    {
        private static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, };

            IEnumerable<int> n = from x in numbers
                                 where x % 2 == 0
                                 select x;

            foreach (var number in n)
            {
                Console.WriteLine(number);

            }

            List<Student> students = new List<Student>()
        {
            new Student(){ID=1,Name="jnjjk"},
            new Student(){ID=2,Name="dhuy"}
        };

            List<Score> scores = new List<Score>()
        {
            new Score(){StudentID=1,Marks=52},
            new Score(){StudentID=2,Marks=74 }
        };

            var student = students.Join(scores,
                s => s.ID,
                sc => sc.StudentID,
                (s, sc) => new { s.Name, sc.Marks });

            Console.WriteLine(student);
            Console.ReadLine();

        }

        class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        class Score
        {
            public int StudentID { get; set; }
            public int Marks { get; set; }
        }
    }
}