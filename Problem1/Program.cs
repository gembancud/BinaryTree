using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTrees;


namespace Problem1
{
    class Program
    {
        static void Main(string[] args)
        {
            IntTree newIntTree = new IntTree();
            newIntTree.AddLeaf(6);
            newIntTree.AddLeaf(5);
            newIntTree.AddLeaf(10);
            newIntTree.AddLeaf(3);
            newIntTree.AddLeaf(9);
            newIntTree.AddLeaf(15);
            newIntTree.AddLeaf(2);
            newIntTree.AddLeaf(4);

            newIntTree.DeleteAllLeaves();

            Console.ReadLine();

        }
    }
}
