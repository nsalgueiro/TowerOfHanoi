using System;
using System.Collections;
using System.Linq;

namespace HanoiTowers.Model
{
    public class Tower
    {
        public string Name { get; internal set; }
        public Stack Elements { get; set; }

        public Tower(string name)
        {
            this.Name = name;
            this.Elements = new Stack();
        }

        public int getTopElementValue()
        {
            return Elements.Count > 0 ? (int)Elements.Peek() : int.MaxValue;
        }

        public override string ToString()
        {
            return this.Name + ": { " + GetElementsAsString(Elements.ToArray()) + " }";
        }

        private String GetElementsAsString(object[] objects)
        {
            int[] objectValues = objects.Cast<int>().ToArray();

            return string.Join(", ", objectValues);
        }
    }
}
