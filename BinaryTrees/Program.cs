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
            IntTree<int> newIntTree = new IntTree<int>();
            newIntTree.AddLeaf(10);
            //newIntTree.AddLeaf(5);
            newIntTree.AddLeaf(15);
            newIntTree.AddLeaf(12);
            newIntTree.AddLeaf(13);
            newIntTree.AddLeaf(18);
            newIntTree.AddLeaf(16);
            newIntTree.AddLeaf(11);
            //newIntTree.AddLeaf(3);
            //newIntTree.AddLeaf(1);
            newIntTree.AddLeaf(20);
            newIntTree.AddLeaf(21);
            //newIntTree.AddLeaf(7);
            //newIntTree.AddLeaf(9);
            //newIntTree.AddLeaf(8);



            //            newIntTree.AddLeaf(50);
            //            newIntTree.AddLeaf(30);
            //            newIntTree.AddLeaf(15);
            //            newIntTree.AddLeaf(40);
            //            newIntTree.AddLeaf(35);
            //            newIntTree.AddLeaf(31);
            //            newIntTree.AddLeaf(37);
            //            newIntTree.AddLeaf(47);
            //            newIntTree.AddLeaf(49);
            //            newIntTree.AddLeaf(46);
            //            newIntTree.AddLeaf(48);
            //            newIntTree.AddLeaf(80);
            //            newIntTree.AddLeaf(70);
            //            newIntTree.AddLeaf(95);
            //            newIntTree.AddLeaf(85);
            //            newIntTree.AddLeaf(100);

            newIntTree.DeleteByCopying(10);

            Console.ReadLine();
        }
    }
}
