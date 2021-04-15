using System;
using System.Collections.Generic;

namespace DesignPatternsCourse.Solid.O
{
    class Program
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Yuge
        }

        public class Product
        {
            public string Name;
            public Color Color;
            public Size Size;

            public Product(string name, Color color, Size size)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException(paramName: nameof(name));
                }

                Name = name;
                Color = color;
                Size = size;
            }
        }

        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach(var product in products)
                {
                    if (product.Size == size)
                    {
                        yield return product;
                    }
                }
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (var product in products)
                {
                    if (product.Color == color)
                    {
                        yield return product;
                    }
                }
            }

            public IEnumerable<Product> FilterBySizeColor(IEnumerable<Product> products, Size size, Color color)
            {
                foreach (var product in products)
                {
                    if (product.Size == size && product.Color == color)
                    {
                        yield return product;
                    }
                }
            }
        }

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color color;

            public ColorSpecification(Color color)
            {
                this.color = color;
            }

            public bool IsSatisfied(Product p)
            {
                return p.Color == color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }

            public bool IsSatisfied(Product p)
            {
                return p.Size == size;
            }
        }

        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var item in items)
                {
                    if (spec.IsSatisfied(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> first, second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
                this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
            }

            public bool IsSatisfied(T t)
            {
                return first.IsSatisfied(t) && second.IsSatisfied(t);
            }
        }

        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var filter = new ProductFilter();
            
            Console.WriteLine("Green products (old):");
            
            foreach (var p in filter.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            var betterFilter = new BetterFilter();
            Console.WriteLine($"Green products (new):");

            var colorSpec = new ColorSpecification(Color.Green);

            foreach (var item in betterFilter.Filter(products, colorSpec))
            {
                Console.WriteLine($" - {item.Name} is green");
            }

            Console.WriteLine("Large products");
            foreach (var p in betterFilter.Filter(products, new SizeSpecification(Size.Large)))
            {
                Console.WriteLine($" - {p.Name} is large");
            }

            Console.WriteLine("Large blue items");
            foreach (var p in betterFilter.Filter(products,
              new AndSpecification<Product>(
                  new ColorSpecification(Color.Blue), 
                  new SizeSpecification(Size.Large)))
            )
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }

            Console.ReadKey();
        }
    }
}
