using System;
using System.Collections.Generic;
using System.IO;

namespace DesignPatternsInCSharpAndDotnetCourse.Solid
{
    public class Journal
    {
        private readonly List<string> entries = new();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");

            return count; // memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
        {
            if (overwrite || !File.Exists(fileName))
            {
                File.WriteAllText(fileName, journal.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("I cried today");
            journal.AddEntry("I ate bug");

            var persistence = new Persistence();

            var fileName = @"c:\temp\journal.txt";

            persistence.SaveToFile(journal, fileName, true);

            // Process.Start(fileName);

            Console.WriteLine(journal.ToString());
        }
    }
}
