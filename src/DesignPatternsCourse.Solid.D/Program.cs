using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternsCourse.Solid.D
{
    class Program
    {
        public enum Relationship
        {
            Parent,
            Child,
            Sibling
        }

        public class Person
        {
            public string Name;
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        public class Relationships : IRelationshipBrowser
        {
            private List<(Person, Relationship, Person)> relations = new();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            public List<(Person, Relationship, Person)> Relations => relations;

            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations
                  .Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(r => r.Item3);
            }
        }

        public class Research
        {
            public Research(IRelationshipBrowser browser)
            {
                foreach (var p in browser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {p.Name}");
                }
            }

            static void Main(string[] args)
            {
                var parent = new Person { Name = "John" };
                var child1 = new Person { Name = "Chris" };
                var child2 = new Person { Name = "Matt" };

                var relationships = new Relationships();
                relationships.AddParentAndChild(parent, child1);
                relationships.AddParentAndChild(parent, child2);

                _ = new Research(relationships);

                Console.ReadKey();
            }
        }
    }
}
