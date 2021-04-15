using System;

namespace DesignPatternsCourse.Solid.I
{
    class Program
    {
        public class Document
        {
        }

        public interface IMachine
        {
            void Print(Document d);
            void Fax(Document d);
            void Scan(Document d);
        }

        public class MultiFunctionPrinter : IMachine
        {
            public void Print(Document d)
            {
            }

            public void Fax(Document d)
            {
            }

            public void Scan(Document d)
            {
            }
        }

        public class OldFashionedPrinter : IMachine
        {
            public void Print(Document d)
            {
            }

            public void Fax(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public class Printer : IPrinter
        {
            public void Print(Document d)
            {
            }
        }

        public class Photocopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public interface IMultiFunctionDevice : IPrinter, IScanner
        {
        }

        public struct MultiFunctionMachine : IMultiFunctionDevice
        {
            private IPrinter _printer;
            private IScanner _scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                _printer = printer;
                _scanner = scanner;
            }

            public void Print(Document d)
            {
                _printer.Print(d);
            }

            public void Scan(Document d)
            {
                _scanner.Scan(d);
            }
        }

        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}
