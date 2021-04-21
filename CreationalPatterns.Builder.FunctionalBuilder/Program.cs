using System;
using System.Collections.Generic;

namespace CreationalPatterns.Builder.FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public sealed class PersonBuilder
    {
        public readonly List<Action<Person>> Actions = new();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });

            return this;
        }

        public Person Build()
        {
            var p = new Person();

            Actions.ForEach(item => item(p));

            return p;
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAsA(this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });

            return builder;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var personBuilder = new PersonBuilder();
            
            var person = personBuilder.Called("Danilo").WorksAsA("Programmer").Build();

            Console.WriteLine(person);

            Console.ReadKey();
        }
    }
}
