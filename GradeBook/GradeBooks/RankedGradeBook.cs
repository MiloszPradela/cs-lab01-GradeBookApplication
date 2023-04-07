using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public  RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5) throw new InvalidOperationException();
            double[] averageGrades = new double[Students.Count];

            for (int i = 0; i < Students.Count; i++)
            {
                for (int j = 0; j < Students[i].Grades.Count; j++)
                {
                    averageGrades[i] += Students[i].Grades[j];
                }
                averageGrades[i] /= Students[i].Grades.Count; // obliczona średnia ucznia i dodana do tablicy
            }

            Array.Sort(averageGrades); // tablica posortowana rosnaco

            double currentPercentage = 0;

            for (int i = 0; i < averageGrades.Length; i++) // sprawdzanie o jaki % innych srednich jest wiekszy
            {
                if (averageGrade > averageGrades[i])
                {
                    currentPercentage = (i + 1) / (double)averageGrades.Length; // aktualny procent na pozycji
                }
            }

            if (currentPercentage >= 0.8) return 'A';
            else if (currentPercentage >= 0.6) return 'B';
            else if (currentPercentage >= 0.4) return 'C';
            else if (currentPercentage >= 0.2) return 'D';
            else return 'F';
        }
        public override void CalculateStatistics()
        {
            if (Students.Count < 5) Console.WriteLine("Ranked grading requires at least 5 students.");
            else base.CalculateStatistics();
        }
        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5) Console.WriteLine("Ranked grading requires at least 5 students.");
            else base.CalculateStudentStatistics(name);
        }
    }
}
