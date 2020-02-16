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

    public partial class TreeForm : Form
    {
        Tree tree;
        public TreeForm()
        {
            InitializeComponent();


            //создаем массив чисел
            var numbers = new int[26];
            var random = new Random();

            //задаем случайным образом двузначными числами
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = random.Next(10, 100);

            //создаем дерево
            tree = Tree.MakeTree(numbers);

            //выводим вспомогательную информацию
            inputLabel.Text = "(NLR)Ввод: " + string.Join(", ", numbers);
            outLabel.Text = "(LRN)Вывод: " + Tree.Output(tree);
        }

        
        const int HEIGHT = 60;
        const int RADIUS = 14;
        Color COLOR_TEXT = Color.Black;
        Color COLOR_NODE = Color.Red;


        public void Draw(Graphics graphics, int width, int startX, Tree tree, int level = 0, bool isLeft = true)
        {
            //LRN
            if (tree.Left != null)
                Draw(graphics, width / 2, startX, tree.Left, level + 1, isLeft: true);
            if (tree.Right != null)
                Draw(graphics, width / 2, startX + width / 2, tree.Right, level + 1, isLeft: false);

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
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, this.Width - 2 * RADIUS, RADIUS, tree);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
