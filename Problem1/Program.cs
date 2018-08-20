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
            IntTree<int> newIntTree = new IntTree<int>();
            newIntTree.AddLeaf(50);
            newIntTree.AddLeaf(30);
            newIntTree.AddLeaf(15);
            newIntTree.AddLeaf(40);
            newIntTree.AddLeaf(35);
            newIntTree.AddLeaf(31);
            newIntTree.AddLeaf(37);
            newIntTree.AddLeaf(47);
            newIntTree.AddLeaf(49);
            newIntTree.AddLeaf(46);
            newIntTree.AddLeaf(48);
            newIntTree.AddLeaf(80);
            newIntTree.AddLeaf(70);
            newIntTree.AddLeaf(95);
            newIntTree.AddLeaf(85);
            newIntTree.AddLeaf(100);

            Console.ReadLine();

        }
    }
}
