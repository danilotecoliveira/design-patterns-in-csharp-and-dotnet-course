using System;

namespace CreationalPatterns.Builder.FacetedBuilder
{
    class Program
    {
        public class Person
        {
            public string StreetAddress, Postcode, City, CompanyName, Position;

            public int AnnualIncome;

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
            }
        }

        public class PersonBuilder
        {
            protected Person person = new();

            public PersonAddressBuilder Lives => new(person);
            public PersonJobBuilder Works => new(person);

            public static implicit operator Person(PersonBuilder pb)
            {
                return pb.person;
            }
        }

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public PersonJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }

            public PersonJobBuilder Earning(int annualIncome)
            {
                person.AnnualIncome = annualIncome;
                return this;
            }
        }

        public class PersonAddressBuilder : PersonBuilder
        {
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostcode(string postcode)
            {
                person.Postcode = postcode;
                return this;
            }

            public PersonAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }
        }

        static void Main(string[] args)
        {
            var personBuilder = new PersonBuilder();
            Person person = personBuilder
              .Lives
                .At("Avenida Alvaro Ramos")
                .In("Sao Paulo")
                .WithPostcode("03058060")
              .Works
                .At("DOTI")
                .AsA("Programmer")
                .Earning(4321);

            Console.WriteLine(person);

            Console.ReadKey();
        }
    }
}
