using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsCourse.CreationalPatterns.Builder
{
    class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new();
        private const int indentSize = 2;

        public HtmlElement()
        {
        }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImplement(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.Append($"{i}<{Name}>\n");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append("\n");
            }

            foreach (var e in Elements)
                sb.Append(e.ToStringImplement(indent + 1));

            sb.Append($"{i}</{Name}>\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImplement(0);
        }
    }

    class HtmlBuilder
    {
        private readonly string rootName;

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
        }

        public HtmlBuilder AddChildFluent(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }

        HtmlElement root = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hello = "hello";
            
            var sb = new StringBuilder();
            
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            
            Console.WriteLine(sb);

            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            
            sb.Append("</ul>");
            
            Console.WriteLine(sb);

            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");

            Console.WriteLine(builder.ToString());

            sb.Clear();
            builder.Clear();
            builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
            
            Console.WriteLine(builder);

            Console.ReadKey();
        }
    }
}
