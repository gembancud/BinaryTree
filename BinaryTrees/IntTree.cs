using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BinaryTrees
{
    public class IntTree<T>
    {
        public Leaf<T> Left
        {
            get { return RootLeaf.Left; }
        }
        public Leaf<T> Right
        {
            get { return RootLeaf.Right; }
        }

        protected Leaf<T> RootLeaf { get; set; }

        public T RootData
        {
            get { return RootLeaf.Data; }
            set { RootLeaf.Data = value; }

        }
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
            }
            else
                RecursiveAddLeaf(data, RootLeaf);

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

        #region DeleteByMerging

        public Leaf<T> DeleteByMerging(T data)
        {
            //Arrange
            var byeLeaf = Search(data);
            if (byeLeaf == null) throw new Exception("Node not found in Tree");
            var parentLeaf = FindParentRecursion(byeLeaf, RootLeaf);
            var newLeaf = byeLeaf.Left;

            if (parentLeaf == byeLeaf) //Special Case: Delete root
            {
                parentLeaf = parentLeaf.Left;
                RootLeaf = parentLeaf;
            }

            var rightBranch = byeLeaf.Right;

            if (newLeaf != null)
            {
                //Find Rightmost node of left subtree
                var attachpoint = newLeaf;
                while (attachpoint.Right != null)
                {
                    attachpoint = attachpoint.Right;
                }

                //Attach right subtree to rightmost node
                attachpoint.Right = rightBranch;
            }
            else if (newLeaf == null) newLeaf = rightBranch;

            if (parentLeaf.Left == byeLeaf) parentLeaf.Left = newLeaf;
            else if (parentLeaf.Right == byeLeaf) parentLeaf.Right = newLeaf;
            Count--;
            return byeLeaf;
        }

        //Finds ParentLeaf
        private Leaf<T> FindParentRecursion(Leaf<T> byeleaf, Leaf<T> leaf)
        {
            var result = byeleaf;
            if (leaf.Left != null)
            {
                if (byeleaf == leaf.Left)
                    result = leaf;
                else result = FindParentRecursion(result, leaf.Left);
            }

            if (leaf.Right != null)
            {
                if (byeleaf == leaf.Right)
                    result = leaf;
                else result = FindParentRecursion(result, leaf.Right);
            }

            return result;
        }

        #endregion

        public Leaf<T> DeleteByCopying(T data)
        {
            var byeLeaf = Search(data);
            if (byeLeaf == null) throw new Exception("Node not found in Tree");
            var parentLeaf = FindParentRecursion(byeLeaf, RootLeaf);

            var newLeaf = byeLeaf.Left; // left of byeleaf
            var replacingLeaf = newLeaf;
            var rightBranch = byeLeaf.Right;

            //case: is leaf, left then right
            if (newLeaf == null)
            {
                if (rightBranch == null)
                {
                    if (parentLeaf.Left == byeLeaf) parentLeaf.Left = null;
                    else if (parentLeaf.Right == byeLeaf) parentLeaf.Right = null;
                    return byeLeaf;
                }
                else
                {
                    parentLeaf.Right = rightBranch;
                    if (parentLeaf == byeLeaf) RootLeaf = RootLeaf.Right;
                    return byeLeaf;
                }
            }

            //Find the replacing Leaf
            while (replacingLeaf.Right != null)
            {
                replacingLeaf = replacingLeaf.Right;
            }

            //case: delete root
            if (parentLeaf == byeLeaf)
            {
                RootLeaf.Data = replacingLeaf.Data;
                if (replacingLeaf == newLeaf) RootLeaf.Left = newLeaf.Left;
            }

            //Specialcase: Replacingleaf is the left 
            if (replacingLeaf == newLeaf)
            {
                if (parentLeaf.Left == byeLeaf) parentLeaf.Left = replacingLeaf;
                else if (parentLeaf.Right == byeLeaf) parentLeaf.Right = replacingLeaf;

                replacingLeaf.Right = rightBranch;
                return byeLeaf;
            }

            //Connects Children of replacing leaf to replacing leafs parent
            if (replacingLeaf.Left != null) FindParentRecursion(replacingLeaf, RootLeaf).Right = replacingLeaf.Left;
            else if (replacingLeaf.Left == null) FindParentRecursion(replacingLeaf, RootLeaf).Right = null;

            if (parentLeaf.Left == byeLeaf) parentLeaf.Left.Data = replacingLeaf.Data;
            else if (parentLeaf.Right == byeLeaf) parentLeaf.Right.Data = replacingLeaf.Data;
            Count--;
            return byeLeaf;
        }
    }
}