using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            IntTree newIntTree = new IntTree();
            newIntTree.AddLeaf(15);
            newIntTree.AddLeaf(4);
            newIntTree.AddLeaf(20);
            newIntTree.AddLeaf(17);
            newIntTree.AddLeaf(21);
            newIntTree.AddLeaf(1);
            newIntTree.AddLeaf(9);

            newIntTree.IsBalanced();

            newIntTree.MaxHeight();

            Console.ReadLine();
        }
    }
}
