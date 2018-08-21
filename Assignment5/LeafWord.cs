using System;
using System.Collections.Generic;

namespace Assignment5
{
    public class LeafWord:IComparable<LeafWord>
    {
        public List<int> Lines { get; set; }

        private string word;

        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        public LeafWord(int line, string word)
        {
            Lines= new List<int>();
            this.Lines.Add(line);
            this.word = word;
        }

        public int CompareTo(LeafWord other)
        {
            return this.word.CompareTo(other.Word);
        }

        public override string ToString()
        {
            string tmp="";
            foreach (var line in Lines)
            {
                tmp += line + ",";
            }
            return word +$" [{tmp}] ";
        }
    }
}