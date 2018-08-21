using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using BinaryTrees;

namespace Assignment1
{
    public class PolishArithmeticExpressionEvaluator
    {

        public PolishArithmeticExpressionEvaluator()
        {
            PolishArithmeticTree<char> sampleTree = new PolishArithmeticTree<char>();

            while (true)
            {
                Console.Write("Input arithmetic string:");
                string input = Console.ReadLine();

                foreach (char c in input) sampleTree.AddLeaf(c);

                Console.WriteLine($"Evaluated Result: {sampleTree.Evaluate()}");
            }
        }
    }

    class PolishArithmeticTree<T> : IntTree<T>
    {
        public override void AddLeaf(T data)
        {
            if (RootLeaf == null)
            {
                RootLeaf = new Leaf<T>(data);
                RootData = RootLeaf.Data;
            }
            else
            {
                RecursiveAddLeaf(ref data, RootLeaf);
            }


            Count++;

        }

        private void RecursiveAddLeaf(ref T data, Leaf<T> leaf)
        {
            if (char.IsDigit(leaf.Data.ToString()[0])) return;

            if (leaf.Left != null)
            {
                RecursiveAddLeaf(ref data, leaf.Left);
            }

            else if (leaf.Left == null && !EqualityComparer<T>.Default.Equals(data, default(T)))
            {
                leaf.Left = new Leaf<T>(data);
                data = default(T);
                return;
            }

            if (leaf.Right != null)
            {
                RecursiveAddLeaf(ref data, leaf.Right);
            }

            else if (leaf.Right == null && !EqualityComparer<T>.Default.Equals(data, default(T)))
            {
                leaf.Right = new Leaf<T>(data);
                data = default(T);
                return;
            }
        }

        public float Evaluate()
        {
            return EvaluateRecursion(RootLeaf);
        }

        private float EvaluateRecursion(Leaf<T> leaf)
        {
            if (char.IsDigit(leaf.Data.ToString()[0])) return Convert.ToInt32(leaf.Data.ToString());

            switch (leaf.Data.ToString()[0])
            {
                case '-':
                    return EvaluateRecursion(leaf.Left) - EvaluateRecursion(leaf.Right);
                    break;
                case '+':
                    return EvaluateRecursion(leaf.Left) + EvaluateRecursion(leaf.Right);
                    break;
                case '*':
                    return EvaluateRecursion(leaf.Left) * EvaluateRecursion(leaf.Right);
                    break;
                case '/':
                    return EvaluateRecursion(leaf.Left) / EvaluateRecursion(leaf.Right);
                    break;
            }

            return 0f;
        }

    }
}