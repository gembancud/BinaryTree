using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;

namespace BinaryTrees
{
    public class IntTree
    {
        public Leaf Left { get; set; }
        public Leaf Right { get; set; }
        private Leaf RootLeaf { get; set; }
        public int RootData { get; set; }
        public int Level { get; set; }
        public int Count { get; set; }

        public IntTree()
        {
            Count = 0;
        }

        public void AddLeaf(int data)
        {
            if (RootLeaf == null)
            {
                RootLeaf = new Leaf(data);
                RootData = RootLeaf.Data;
            }
            else
                RecursiveAddLeaf(data, RootLeaf);

            Left = RootLeaf.Left;
            Right = RootLeaf.Right;

            Count++;
        }

        private void RecursiveAddLeaf(int data, Leaf leaf)
        {
            if (data <= leaf.Data)
            {
                if (leaf.Left == null) leaf.Left = new Leaf(data);
                else RecursiveAddLeaf(data, leaf.Left);
            }
            else if (data > leaf.Data)
            {
                if (leaf.Right == null) leaf.Right = new Leaf(data);
                else RecursiveAddLeaf(data, leaf.Right);
            }
        }

        #region BreadthFirstSearch

        public Queue<Leaf> BreadthFirstSearch()
        {
            Queue<Leaf> ResultQeue = new Queue<Leaf>();
            var BreadQeue = new Queue<Leaf>();
            BreadQeue.Enqueue(RootLeaf);
            while (BreadQeue.Count != 0)
            {
                _nodeCount++;
                var Temp = BreadQeue.Peek();

                if (Temp.Left != null) BreadQeue.Enqueue(Temp.Left);
                if (Temp.Right != null) BreadQeue.Enqueue(Temp.Right);

                ResultQeue.Enqueue(BreadQeue.Dequeue());
            }

            return ResultQeue;
        }

        #endregion

        #region InOrderSearch


        Queue<Leaf> _inOrderResultQeue = new Queue<Leaf>();
        public Queue<Leaf> InOrderTraverse()
        {
            _inOrderResultQeue.Clear();
            InOrderTraverseRecursion(RootLeaf);
            return _inOrderResultQeue;
        }

        private void InOrderTraverseRecursion(Leaf leaf)
        {
            if (leaf == null)
            {
                return;
            }

            InOrderTraverseRecursion(leaf.Left);
            _inOrderResultQeue.Enqueue(leaf);
            InOrderTraverseRecursion(leaf.Right);

        }

        #endregion

        #region PreOrderSearch

        Queue<Leaf> _preOrderResultQeue = new Queue<Leaf>();
        public Queue<Leaf> PreOrderTraverse()
        {
            _preOrderResultQeue.Clear();
            PreOrderTraverseRecursion(RootLeaf);
            return _preOrderResultQeue;
        }

        private void PreOrderTraverseRecursion(Leaf leaf)
        {
            if (leaf == null)
            {
                return;
            }

            _preOrderResultQeue.Enqueue(leaf);
            PreOrderTraverseRecursion(leaf.Left);
            PreOrderTraverseRecursion(leaf.Right);

        }


        #endregion

        #region PostOrderSearch

        Queue<Leaf> _postOrderResultQeue = new Queue<Leaf>();
        public Queue<Leaf> PostOrderTraverse()
        {
            _postOrderResultQeue.Clear();
            PostOrderTraverseRecursion(RootLeaf);
            return _postOrderResultQeue;
        }

        private void PostOrderTraverseRecursion(Leaf leaf)
        {
            if (leaf == null)
            {
                return;
            }

            PostOrderTraverseRecursion(leaf.Left);
            PostOrderTraverseRecursion(leaf.Right);
            _postOrderResultQeue.Enqueue(leaf);

        }

        #endregion

        #region NodeCount

        private int _nodeCount;
        public int NodeCount()
        {
            _nodeCount = 0;
            BreadthFirstSearch();
            return _nodeCount;
        }

        #endregion

        #region RightChildren

        public int RightChildrenCount
        {
            get
            {
                RightChildrenCountResult = 0;
                RightChildrenCountRecursion(RootLeaf);
                return RightChildrenCountResult;
            }
        }

        private int RightChildrenCountResult;
        private void RightChildrenCountRecursion(Leaf leaf)
        {
            if (leaf == null)
                return;
            RightChildrenCountResult++;
            RightChildrenCountRecursion(leaf.Right);
        }

        #endregion

        #region MaxHeight

        public int MaxHeight()
        {
           return MaxHeightRecursion(RootLeaf);
        }

        private int MaxHeightRecursion(Leaf leaf)
        {
            if (leaf == null) return 0;
            return Math.Max(MaxHeightRecursion(leaf.Left), MaxHeightRecursion(leaf.Right)) + 1;
        }
        #endregion

        #region DeleteAllLeaves

        public void DeleteAllLeaves()
        {
            DeleteAllLeavesRecursion(RootLeaf);
        }

        private void DeleteAllLeavesRecursion(Leaf leaf)
        {
            if (leaf == null) return;

            if (CheckIfChildIsLeaf(leaf.Left) && CheckIfChildIsLeaf(leaf.Right))
                leaf.Left = leaf.Right = null;

            DeleteAllLeavesRecursion(leaf.Left);
            DeleteAllLeavesRecursion(leaf.Right);

        }

        #endregion

        private bool CheckIfChildIsLeaf(Leaf leaf)
        {
            if (leaf.Left == null && leaf.Right == null)
                return true;

            return false;
        }

        #region LeafCount

        private int _leafCount;
        public int LeafCount()
        {
            _leafCount = 0;
            LeafCountRecursion(RootLeaf);
            return _leafCount;
        }

        public void LeafCountRecursion(Leaf leaf)
        {
            if (leaf == null) return;

            if (leaf.Left == null && leaf.Right == null) _leafCount++;

            LeafCountRecursion(leaf.Left);
            LeafCountRecursion(leaf.Right);
        }

        #endregion

        public bool IsBalanced()
        {
            return Math.Pow(2,MaxHeight()-1) == LeafCount() ? true :false; 
        }
    }
}