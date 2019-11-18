using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Siakod3_tree
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var numbers = new int[26];
            var random = new Random();
            string inputString = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(10, 100);
                inputString += numbers[i].ToString() + ", ";
            }
            

            tree = Tree.MakeTree(numbers);

            label1.Text = "Ввод: " + inputString;
            label2.Text = "Вывод: " + Tree.Output(tree);
        }

        int[] test = { 55, 58, 59, 11, 18, 15, 33, 46, 98, 73, 21 };
        Tree tree;
        const int HEIGHT = 60;
        const int RADIUS = 14;
        const int TEXT_SIZE = 10;
        Color COLOR_TEXT = Color.Black;
        Color COLOR_NODE = Color.Red;


        public void Draw(Graphics graphics, int width, int startX, Tree tree, int level = 0, bool isLeft = true)
        {

            if (tree.Left != null)
            {
                level++;
                Draw(graphics, width / 2, startX, tree.Left, level, true);
                level--;
            }
            if (tree.Right != null)
            {
                level++;
                Draw(graphics, width / 2, startX + width / 2, tree.Right, level, false);
                level--;
            }

            DrawNode(graphics, width, startX, tree.Data, level, isLeft);
        }

        public void DrawNode(Graphics graphics, int width, int startX, int data, int level, bool isLeft)
        {
            //центр верхнего узла
            Point oldCenter = Point.Empty;
            if (isLeft)
            {
                if (level == 0)
                    oldCenter.Offset(startX + width / 2, (HEIGHT / 2));
                else 
                    oldCenter.Offset(startX + width, (HEIGHT / 2) + (HEIGHT * (level - 1)));
            }
            else
            {
                if (level == 0)
                    oldCenter.Offset(startX, (HEIGHT / 2));
                else 
                    oldCenter.Offset(startX, (HEIGHT / 2) + (HEIGHT * (level - 1)));
            }

            //центр текущего узла
            Point center = new Point(startX + width / 2, (HEIGHT / 2) + (HEIGHT * level));

            graphics.DrawLine(Pens.Black, oldCenter, center);

            //сдвиг в центр круга
            graphics.FillEllipse(new SolidBrush(COLOR_NODE), center.X - RADIUS, center.Y - RADIUS, RADIUS * 2, RADIUS * 2);
            graphics.DrawString(data.ToString(), this.Font, new SolidBrush(COLOR_TEXT), new Point(center.X - 10, center.Y - 8));
            //

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //DrawNode(e.Graphics, this.Width, 0, 15, 0);
            //DrawNode(e.Graphics, this.Width, 0, 100, 1);
            Draw(e.Graphics, this.Width - 2 * RADIUS, RADIUS, tree);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //перерисовать принудительно
            Invalidate();
        }
    }
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
