using System;
using System.Collections.Generic;
using System.Text;

namespace CreationalPatterns.Builder.Exercise
{
    public class ClassElement
    {
        public string Class, Key, Value;
        public List<ClassElement> Elements = new List<ClassElement>();
        private const int indentSize = 2;

        public ClassElement()
        {
        }

        public ClassElement(string key, string value)
        {
            Key = key;
            Value = value;
        }

        private string ToStringImplement(int indent)
        {
            var sb = new StringBuilder();
            
            var i = new string(' ', indentSize * indent);

            sb.Append($"public class {Class}\n");

            sb.Append("{\n");

            foreach (var e in Elements)
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append($"public {e.Value} {e.Key};");
                sb.Append("\n");
            }

            sb.Append(i + "}\n");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImplement(0);
        }
    }

    public class CodeBuilder
    {
        private readonly string rootClass;
        ClassElement root = new ClassElement();

        public CodeBuilder(string rootClass)
        {
            this.rootClass = rootClass;
            root.Class = rootClass;
        }

        public CodeBuilder AddField(string childName, string childText)
        {
            var e = new ClassElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");

            Console.WriteLine(cb);
        }
    }
}
