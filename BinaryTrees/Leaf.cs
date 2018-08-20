namespace BinaryTrees
{
    public class Leaf<T>
    {
        public Leaf<T> Left { get; set; }
        public Leaf<T> Right { get; set; }
        public T Data { get; set; }

        public Leaf(T data)
        {
            Data = data;
        }

        public Leaf()
        {
            
        }

    }
}