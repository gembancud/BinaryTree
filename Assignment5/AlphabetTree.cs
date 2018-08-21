using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using BinaryTrees;

namespace Assignment5
{
    public class AlphabetTree
    {
        public List<LeafWord> _leafWordList { get; set; }
        public CustomAlphabetBinaryTree<LeafWord> _leafWordTree { get; set; }

        public AlphabetTree() //starts the program
        {
            //arrange
            _leafWordList = new List<LeafWord>();
            _leafWordTree = new CustomAlphabetBinaryTree<LeafWord>();

            Console.Write("Filename: ");
            string fileinput = Console.ReadLine();

            //act
            OpenFile(fileinput);

            var result = _leafWordTree.InOrderTraverse();
            foreach (Leaf<LeafWord> leaf in result)
            {
                Console.WriteLine(leaf.Data);
            }

            //_leafWordList.Sort(); //test case fcn this works w/o tree!
            //foreach (LeafWord leafWord in _leafWordList)
            //{
            //    Console.WriteLine(leafWord);
            //}
        }

        private void OpenFile(string fileinput)
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @fileinput);
            //string filePath = @fileinput;
            string[] tempfile = null;
            try
            {
                tempfile = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                throw new Exception("File not Found", e);
            }

            int i = 0;
            foreach (string line in tempfile)
            {
                string[] templine = line.Split(new char[] { ' ', ',' });
                foreach (string s in templine)
                {
                    if (s == "") continue;
                    _leafWordTree.AddLeaf(new LeafWord(i, s));
                    // _leafWordList.Add(new LeafWord(i, s)); //w/o binary search tree
                }
                i++;
            }
        }

    }

    public class CustomAlphabetBinaryTree<T> : IntTree<LeafWord>
    {
        public override void AddLeaf(LeafWord data)
        {
            if (RootLeaf == null)
            {
                RootLeaf = new Leaf<LeafWord>(data);
                RootData = RootLeaf.Data;
            }
            else
            {
                RecursiveAddLeaf(data, RootLeaf);
            }
            Count++;

        }

        private void RecursiveAddLeaf(LeafWord data, Leaf<LeafWord> leaf)
        {
            if (leaf == null) return;
            if (leaf.Data.Word == data.Word) leaf.Data.Lines.Add(data.Lines[0]);

            if (data.Word.CompareTo(leaf.Data.Word) < 0)
            {
                if (leaf.Left == null) leaf.Left = new Leaf<LeafWord>(data);
                else RecursiveAddLeaf(data, leaf.Left);
            }

            else if (data.Word.CompareTo(leaf.Data.Word) > 0)
            {
                if (leaf.Right == null) leaf.Right = new Leaf<LeafWord>(data);
                else RecursiveAddLeaf(data, leaf.Right);
            }

        }

    }
}