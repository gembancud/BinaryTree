namespace BinaryTrees
{
    public class Leaf
    {
        public Leaf Left { get; set; }
        public Leaf Right { get; set; }
        public int Data { get; set; }

        public Leaf(int data)
        {
            Data = data;
        }

        public Leaf()
        {
            
        }

    }
}