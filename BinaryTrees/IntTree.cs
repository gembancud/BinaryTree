using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BinaryTrees
{
    public class IntTree<T>
    {
        public Leaf<T> Left { get; set; }
        public Leaf<T> Right { get; set; }
        protected Leaf<T> RootLeaf { get; set; }
        public T RootData { get; set; }
        public int Level { get; set; }
        public int Count { get; set; }

        public IntTree()
        {
            Count = 0;
        }

        public virtual void AddLeaf(T data)
        {
            if (RootLeaf == null)
            {
                RootLeaf = new Leaf<T>(data);
                RootData = RootLeaf.Data;
            }
            else
                RecursiveAddLeaf(data, RootLeaf);

            Left = RootLeaf.Left;
            Right = RootLeaf.Right;

            Count++;
        }

        private void RecursiveAddLeaf(T data, Leaf<T> leaf)
        {
            if (Comparer<T>.Default.Compare(data, leaf.Data) == -1) 
            {
                if (leaf.Left == null) leaf.Left = new Leaf<T>(data);
                else RecursiveAddLeaf(data, leaf.Left);
            }
            else if (Comparer<T>.Default.Compare(data, leaf.Data) == 1)
            {
                if (leaf.Right == null) leaf.Right = new Leaf<T>(data);
                else RecursiveAddLeaf(data, leaf.Right);
            }
        }

        #region BreadthFirstSearch

        public Queue<Leaf<T>> BreadthFirstSearch()
        {
            Queue<Leaf<T>> ResultQeue = new Queue<Leaf<T>>();
            var BreadQeue = new Queue<Leaf<T>>();
            BreadQeue.Enqueue(RootLeaf);
            while (BreadQeue.Count != 0)
            {
                _maxHeightResult++;
                var Temp = BreadQeue.Peek();

                if (Temp.Left != null) BreadQeue.Enqueue(Temp.Left);
                if (Temp.Right != null) BreadQeue.Enqueue(Temp.Right);

                ResultQeue.Enqueue(BreadQeue.Dequeue());
            }

            return ResultQeue;
        }

        #endregion

        #region InOrderSearch


        Queue<Leaf<T>> _inOrderResultQeue = new Queue<Leaf<T>>();
        public Queue<Leaf<T>> InOrderTraverse()
        {
            _inOrderResultQeue.Clear();
            InOrderTraverseRecursion(RootLeaf);
            return _inOrderResultQeue;
        }

        private void InOrderTraverseRecursion(Leaf<T> leaf)
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

        Queue<Leaf<T>> _preOrderResultQeue = new Queue<Leaf<T>>();
        public Queue<Leaf<T>> PreOrderTraverse()
        {
            _preOrderResultQeue.Clear();
            PreOrderTraverseRecursion(RootLeaf);
            return _preOrderResultQeue;
        }

        private void PreOrderTraverseRecursion(Leaf<T> leaf)
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

        Queue<Leaf<T>> _postOrderResultQeue = new Queue<Leaf<T>>();
        public Queue<Leaf<T>> PostOrderTraverse()
        {
            _postOrderResultQeue.Clear();
            PostOrderTraverseRecursion(RootLeaf);
            return _postOrderResultQeue;
        }

        private void PostOrderTraverseRecursion(Leaf<T> leaf)
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
        private void RightChildrenCountRecursion(Leaf<T> leaf)
        {
            if (leaf == null)
                return;
            RightChildrenCountResult++;
            RightChildrenCountRecursion(leaf.Right);
        }

        #endregion

        #region MaxHeight

        private int _maxHeightResult;
        public int MaxHeight()
        {
            _maxHeightResult = 0;
            BreadthFirstSearch();
            return _maxHeightResult;
        }

        #endregion

        #region DeleteAllLeaves

        public void DeleteAllLeaves()
        {
            DeleteAllLeavesRecursion(RootLeaf);
        }

        private void DeleteAllLeavesRecursion(Leaf<T> leaf)
        {
            if (leaf == null) return;

            if (CheckIfChildIsLeaf(leaf.Left) && CheckIfChildIsLeaf(leaf.Right))
                leaf.Left = leaf.Right = null;

            DeleteAllLeavesRecursion(leaf.Left);
            DeleteAllLeavesRecursion(leaf.Right);

        }

        private bool CheckIfChildIsLeaf(Leaf<T> leaf)
        {
            if (leaf.Left == null && leaf.Right == null)
                return true;

            return false;
        }

        #endregion

        public Leaf<T> Search(T data)
        {
            Queue<Leaf<T>> ResultQeue = new Queue<Leaf<T>>();
            var BreadQeue = new Queue<Leaf<T>>();
            BreadQeue.Enqueue(RootLeaf);
            while (BreadQeue.Count != 0)
            {
                _maxHeightResult++;
                var Temp = BreadQeue.Peek();

                if (Temp.Left != null) BreadQeue.Enqueue(Temp.Left);
                if (Temp.Right != null) BreadQeue.Enqueue(Temp.Right);

                if (Comparer<T>.Default.Compare(Temp.Data, data) == 0) return Temp;
                ResultQeue.Enqueue(BreadQeue.Dequeue());

            }
            return null;
        }

        public Leaf<T> DeleteByMerging(T data)
        {
            //Arrange
            var byeLeaf = Search(data);
            var newLeaf = byeLeaf.Left;
            var rightBranch = byeLeaf.Right;
            var rightMostLeaf = newLeaf;
            while (rightMostLeaf != null)
            {
                rightMostLeaf = rightMostLeaf.Right;
            }
            //Act
            if (byeLeaf.Right == null) byeLeaf = newLeaf;
            else if (byeLeaf.Left == null) byeLeaf = rightBranch;
            else {
                byeLeaf = newLeaf;
                rightMostLeaf.Right = rightBranch;
            }

            //End
            return byeLeaf;
        }


    }
}