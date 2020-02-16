namespace Siakod3_tree
{
    public class Tree
    {
        public int Data;
        public Tree Left;
        public Tree Right;

        public Tree(int data, Tree left = null, Tree right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public static string Output(Tree tree)
        {
            string s = "";
            if (tree.Left != null)
                s += Output(tree.Left) + ", ";
            if (tree.Right != null)
                s += Output(tree.Right) + ", ";
            s += tree.Data.ToString();
            return s;
        }

        public static Tree MakeTree(int[] array)
        {
            int number = 0;
            return MakeTree(array, array.Length, ref number);
        }

        public static Tree MakeTree(int[] array, int count, ref int number)
        {
            var tree = new Tree(array[number]);
            number++;
            int nl = count / 2;
            if (nl > 0)
                tree.Left = MakeTree(array, nl, ref number);
            int nr = count - nl - 1;
            if (nr > 0)
                tree.Right = MakeTree(array, nr, ref number);
            return tree;
        }
    }
}
