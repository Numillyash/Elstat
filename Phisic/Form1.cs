using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phisic
{
    public partial class Form1 : Form
    {
        int selected = -1;
        int dalk = 0;
        int fillk = 750;
        bool dow = false;
        int type = 1;
        int cha = 1;


        bool drawer = true;
        bool coor = false;
        List<El_ch> points = new List<El_ch> { };
        List<Atom> atoms = new List<Atom> { };
        List<Prob> probs = new List<Prob> { };
        private readonly Graphics _graphics;
        private Bitmap _bitmap;
        public Form1()
        {
            InitializeComponent();
            _bitmap = new Bitmap(1920, 1080);
            _graphics = Graphics.FromImage(_bitmap);
            pictureBox1.Image = _bitmap;
            //InitializeComponent();
            _graphics.Clear(Color.White);
            paintka();
        }
        private void phisica2()
        {
            //int k = 10;
            int k2 = 2;
            int dk = 300;
            Brush brush = new SolidBrush(Color.Red);

            Brush brush_bl = new SolidBrush(Color.Black);
            Brush brush_b = new SolidBrush(Color.Blue);
            Brush brush_y = new SolidBrush(Color.Yellow);
            Brush brush_g = new SolidBrush(Color.Green);

            foreach (Prob el in probs)
            {


                int x_now = (int)el.x;
                int y_now = (int)el.y;
                int x_old = x_now;
                int y_old = y_now;
                bool kekl = false;
                if (!kekl)
                {
                    Vector new_pos = new Vector();
                    foreach (Atom at in atoms)
                    {
                        new_pos = vsum(new_pos, at.forcer(new El_ch(x_now, y_now, false)));
                        double rad = Math.Sqrt((at.x - x_now) * (at.x - x_now) + (at.y - y_now) * (at.y - y_now));
                        if (rad < 10) kekl = true;
                    }
                    if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) continue;
                    Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) continue;


                    //_graphics.DrawLine(new Pen(brush_g), new Point(x_now, y_now), new Point(x_now + (int)(rv.x * k), y_now + (int)(rv.y * k)));
                    x_now += (int)(rv.x * k2 * k2);
                    y_now += (int)(rv.y * k2 * k2);
                    _graphics.FillEllipse(brush_bl, new Rectangle(x_now - 2, y_now - 2, 4, 4));
                    _graphics.DrawLine(new Pen(brush_y), new Point(x_old, y_old), new Point(x_now, y_now));
                    //mas1.Add(new Point(x_now, y_now));
                    //Refresh();
                }
                for (int i = 1; i < dk; i++)
                {
                    if (!kekl)
                    {
                        Vector new_pos = new Vector();
                        foreach (Atom at in atoms)
                        {
                            new_pos = vsum(new_pos, at.forcer(new El_ch(x_now, y_now, false)));
                            double rad = Math.Sqrt((at.x - x_now) * (at.x - x_now) + (at.y - y_now) * (at.y - y_now));
                            if (rad < 10) kekl = true;
                        }
                        if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) continue;
                        Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                        if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) continue;

                        int a = x_now, b = y_now;
                        //_graphics.DrawLine(new Pen(brush_g), new Point(x_now, y_now), new Point(x_now + (int)(rv.x * k), y_now + (int)(rv.y * k)));
                        x_now = (int)(2 * x_now - x_old + rv.x * k2 * k2);
                        y_now = (int)(2 * y_now - y_old + rv.y * k2 * k2);
                        x_old = a;
                        y_old = b;

                        _graphics.FillEllipse(brush_bl, new Rectangle(x_now - 2, y_now - 2, 4, 4));
                        _graphics.DrawLine(new Pen(brush_y), new Point(x_old, y_old), new Point(x_now, y_now));

                        //mas1.Add(new Point(x_now, y_now));
                        //Refresh();
                    }
                }
            }
            Refresh();
        }
        private void phisica()
        {
            int k = 10;
            //int k2 = 2;
            int dk = 500;
            Brush brush = new SolidBrush(Color.Red);

            Brush brush_bl = new SolidBrush(Color.Black);
            Brush brush_b = new SolidBrush(Color.Blue);
            Brush brush_y = new SolidBrush(Color.Yellow);
            Brush brush_g = new SolidBrush(Color.Green);
            
            foreach (El_ch el in points)
            {
                //List<Point> mas1 = new List<Point> { };
                //List<Point> mas2 = new List<Point> { };
                int x_now = (int) el.x;
                int y_now = (int) el.y;
                bool kekl = false;
                //mas1.Add(new Point(x_now,y_now));
                for (int i = 0; i < dk; i++)
                {
                    if (!kekl)
                    {
                        Vector new_pos = new Vector();
                        foreach (Atom at in atoms)
                        {
                            new_pos = vsum(new_pos, at.forcer(new El_ch(x_now, y_now, false)));
                            double rad = Math.Sqrt((at.x - x_now) * (at.x - x_now) + (at.y - y_now) * (at.y - y_now));
                            if (rad < 10 && at.charge != 0) kekl = true;
                        }
                        if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) continue;
                        Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                        if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) continue;
                        

                        _graphics.DrawLine(new Pen(brush_g), new Point(x_now, y_now), new Point(x_now + (int)(rv.x * k), y_now + (int)(rv.y * k)));
                        x_now += (int)(rv.x * k);
                        y_now += (int)(rv.y * k);
                        //mas1.Add(new Point(x_now, y_now));
                        //Refresh();
                    }
                }
                x_now = (int)el.x;
                y_now = (int)el.y;
                kekl = false;
                //mas2.Add(new Point(x_now, y_now));
                for (int i = 0; i < dk; i++)
                {
                    if (!kekl)
                    {
                        Vector new_pos = new Vector();
                        foreach (Atom at in atoms)
                        {
                            new_pos = vsum(new_pos, at.forcer(new El_ch(x_now, y_now, false)));
                            double rad = Math.Sqrt((at.x - x_now) * (at.x - x_now) + (at.y - y_now) * (at.y - y_now));
                            if (rad < 10 && at.charge != 0) kekl = true;
                        }
                        if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) continue;
                        Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                        if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) break;

                        _graphics.DrawLine(new Pen(brush_g), new Point(x_now, y_now), new Point(x_now - (int)(rv.x * k), y_now - (int)(rv.y * k)));
                        x_now -= (int)(rv.x * k);
                        y_now -= (int)(rv.y * k);
                        //mas1.Add(new Point(x_now, y_now));
                        //Refresh();
                    }
                }/*
                for (int i = 0; i < mas1.Count/2*2; i += 2)
                {
                    _graphics.DrawLine(new Pen(brush_g), mas1[i], mas1[i + 1]);
                }
                for (int i = 0; i < mas2.Count/2*2; i += 2)
                {
                    _graphics.DrawLine(new Pen(brush_g), mas2[i], mas2[i + 1]);
                }*/
                foreach (Atom at in atoms)
                {
                    if (at.charge > 0)
                        _graphics.FillEllipse(brush, new Rectangle(at.x - 10, at.y - 10, 20, 20));
                    if (at.charge == 0)
                        _graphics.FillEllipse(brush_y, new Rectangle(at.x - 10, at.y - 10, 20, 20));
                    if (at.charge < 0)
                        _graphics.FillEllipse(brush_b, new Rectangle(at.x - 10, at.y - 10, 20, 20));
                }
                
                //Refresh();

                
            }
            //Refresh();
        }
        private Vector vsum(Vector v1, Vector v2)
        {
            Vector ans = new Vector(v1.x+v2.x, v1.y+v2.y);
            //.WriteLine(ans);
            return ans;
        }
        private void paintka()
        {
            _graphics.Clear(Color.White);
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            Brush brush = new SolidBrush(Color.Red);
            Brush brush_b = new SolidBrush(Color.Blue);
            Brush brush_bl = new SolidBrush(Color.Black);
            Brush brush_y = new SolidBrush(Color.Yellow);
            foreach (Atom at in atoms)
            {
                if (at.charge > 0)
                    _graphics.FillEllipse(brush, new Rectangle(at.x-10, at.y-10,20,20));
                if (at.charge == 0)
                    _graphics.FillEllipse(brush_bl, new Rectangle(at.x - 10, at.y - 10, 20, 20));
                if (at.charge < 0)
                    _graphics.FillEllipse(brush_b, new Rectangle(at.x - 10, at.y - 10, 20, 20));
            }
            foreach (El_ch el in points)
            {
                //Console.WriteLine("painted");
                //Console.WriteLine("kekk");
                //Console.WriteLine("{0:0.000000000000}, {1:0.000000000000}", el.x, el.y);
                if (drawer) _graphics.FillEllipse(brush_b, new Rectangle((int)(el.x - 2), (int)(el.y - 2), 4, 4));
                //Console.WriteLine("{0:0.000000000000}, {1:0.000000000000}", el.x, el.y);
            }
            if (selected >= 0)
            {
                _graphics.DrawEllipse(new Pen(Color.Purple), new Rectangle(atoms[selected].x - 11, atoms[selected].y - 11, 22, 22));
            }
            //Refresh();
            if (coor)
            {
                _graphics.FillRectangle(brush_bl, new Rectangle(0, 748, 750, 2));
                _graphics.FillRectangle(brush_bl, new Rectangle(0, 0, 2, 750));
                for (int i = 0; i < 750; i += 50)
                {
                    for (int j = 0; j < 750; j += 50)
                    {
                        _graphics.FillRectangle(brush_bl, new Rectangle(i - 1, j - 1, 2, 2));
                        _graphics.FillRectangle(brush_bl, new Rectangle(i - 1, j - 1, 2, 2));
                    }
                    _graphics.FillRectangle(brush_bl, new Rectangle(i - 1, 745, 2, 5));
                    if (i == 50)
                    {
                        _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brush_bl, new Point(i - 9, 732));
                    }
                    else _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brush_bl, new Point(i - 13, 732));
                }
                for (int i = 700; i > 0; i -= 50)
                {
                    _graphics.FillRectangle(brush_bl, new Rectangle(0, i - 1, 5, 2));
                    if (i == 700)
                    {
                        _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brush_bl, new Point(4, (750 - i) - 6));
                    }
                    else _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brush_bl, new Point(4, (750 - i) - 6));

                }
                _graphics.DrawString(string.Format("x"), new Font(this.Font, FontStyle.Bold), brush_bl, new Point(737, 732));
                _graphics.DrawString(string.Format("y"), new Font(this.Font, FontStyle.Bold), brush_bl, new Point(6, 4));
                _graphics.DrawLine(new Pen(brush_bl), new Point(0, 750), new Point(5, 745));
                _graphics.DrawLine(new Pen(brush_bl), new Point(1, 750), new Point(6, 745));
                _graphics.DrawLine(new Pen(brush_bl), new Point(2, 750), new Point(7, 745));
                _graphics.DrawLine(new Pen(brush_bl), new Point(0, 749), new Point(5, 744));
                _graphics.DrawLine(new Pen(brush_bl), new Point(0, 748), new Point(5, 743));
                _graphics.DrawString(string.Format("0"), new Font(this.Font, FontStyle.Bold), brush_bl, new Point(4, 732));
            }
            phisica();
            phisica2();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            /*switch (type)
            {
                case 1:
                    Atom at = new Atom(e.X, e.Y, cha);
                    atoms.Add(at);
                    El_ch ela = new El_ch(e.X - 30, e.Y - 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X - 30, e.Y, false);
                    points.Add(ela);
                    ela = new El_ch(e.X - 30, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y - 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X, e.Y - 30, false);
                    points.Add(ela);
                    /*ela = new El_ch(e.X - 30, e.Y - 15, false);
                    points.Add(ela);
                    ela = new El_ch(e.X - 30, e.Y, false);
                    points.Add(ela);
                    ela = new El_ch(e.X - 30, e.Y + 15, false);
                    points.Add(ela);
                    ela = new El_ch(e.X - 30, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X - 15, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 15, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y + 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y + 15, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y - 15, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 30, e.Y - 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X + 15, e.Y - 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X, e.Y - 30, false);
                    points.Add(ela);
                    ela = new El_ch(e.X - 15, e.Y - 30, false);
                    points.Add(ela);*/
                    /*break;
                case 2:
                    Atom at2 = new Atom(e.X, e.Y, -cha);
                    atoms.Add(at2);
                    break;
                case 3:
                    El_ch el = new El_ch(e.X, e.Y, false);
                    points.Add(el);
                    break;
                default: break;
            }
            //El_ch el = new El_ch(e.X, e.Y, false);
            //points.Add(el);
            //Console.WriteLine("New ell");
            paintka();*/
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dalk = 8;
        }
        private void лууToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void minusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            paintka();
            type = 2;
        }
        private void plusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            type = 1;
            paintka();
        }
        private void elcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            type = 3;
            paintka();
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 1;
            points = new List<El_ch> { };
            atoms = new List<Atom> { };
            probs = new List<Prob> { };
            selected = -1;
            paintka();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //cha = int.Parse(textBox1.Text);
            }
            catch { }
            Console.WriteLine("IChangeTracking");
        }
        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            probs = new List<Prob> { };
            dow = true;
            selected = -1;
            switch (type)
            {
                case 1:
                    dow = true;
                    Atom at = new Atom(e.X, e.Y, Math.Abs(cha));
                    atoms.Add(at);
                    if (dalk == 0)
                    {
                        El_ch ela = new El_ch(e.X - 30, e.Y, false);
                        points.Add(ela);
                        at.x8[0] = points.Count - 1;
                        ela = new El_ch(e.X, e.Y - 30, false);
                        points.Add(ela);
                        at.x8[1] = points.Count - 1;
                        ela = new El_ch(e.X, e.Y + 30, false);
                        points.Add(ela);
                        at.x8[2] = points.Count - 1;
                        ela = new El_ch(e.X + 30, e.Y, false);
                        points.Add(ela);
                        at.x8[3] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * 0.707, e.Y + 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[4] = points.Count - 1;
                        ela = new El_ch(e.X + 30 * 0.707, e.Y - 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[5] = points.Count - 1;
                        ela = new El_ch(e.X - 30 * 0.707, e.Y + 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[6] = points.Count - 1;
                        ela = new El_ch(e.X - 30 * 0.707, e.Y - 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[7] = points.Count - 1;

                        at.sta = 8;
                    }
                    else
                    {
                        El_ch ela = new El_ch(e.X - 30 * Math.Cos(Math.PI * 0), e.Y - 30 * Math.Sin(Math.PI * 0), false);
                        points.Add(ela);
                        at.x16[0] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8), e.Y - 30 * Math.Sin(Math.PI / 8), false);
                        points.Add(ela);
                        at.x16[1] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 4), e.Y - 30 * Math.Sin(Math.PI / 4), false);
                        points.Add(ela);
                        at.x16[2] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 3), e.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);
                        points.Add(ela);
                        at.x16[3] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 2), e.Y - 30 * Math.Sin(Math.PI / 2), false);
                        points.Add(ela);
                        at.x16[4] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 5), e.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);
                        points.Add(ela);
                        at.x16[5] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 6), e.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);
                        points.Add(ela);
                        at.x16[6] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 7), e.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);
                        points.Add(ela);
                        at.x16[7] = points.Count - 1;


                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI * 0), e.Y + 30 * Math.Sin(Math.PI * 0), false);
                        points.Add(ela);
                        at.x16[8] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8), e.Y + 30 * Math.Sin(Math.PI / 8), false);
                        points.Add(ela);
                        at.x16[9] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 4), e.Y + 30 * Math.Sin(Math.PI / 4), false);
                        points.Add(ela);
                        at.x16[10] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 3), e.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);
                        points.Add(ela);
                        at.x16[11] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 2), e.Y + 30 * Math.Sin(Math.PI / 2), false);
                        points.Add(ela);
                        at.x16[12] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 5), e.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);
                        points.Add(ela);
                        at.x16[13] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 6), e.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);
                        points.Add(ela);
                        at.x16[14] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 7), e.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                        points.Add(ela);
                        at.x16[15] = points.Count - 1;
                        at.sta = 16;
                    }
                    break;
                case 2:
                    dow = true;
                    at = new Atom(e.X, e.Y, -Math.Abs(cha));
                    atoms.Add(at);
                    if (dalk == 0)
                    {
                        El_ch ela = new El_ch(e.X - 30, e.Y, false);
                        points.Add(ela);
                        at.x8[0] = points.Count - 1;
                        ela = new El_ch(e.X, e.Y - 30, false);
                        points.Add(ela);
                        at.x8[1] = points.Count - 1;
                        ela = new El_ch(e.X, e.Y + 30, false);
                        points.Add(ela);
                        at.x8[2] = points.Count - 1;
                        ela = new El_ch(e.X + 30, e.Y, false);
                        points.Add(ela);
                        at.x8[3] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * 0.707, e.Y + 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[4] = points.Count - 1;
                        ela = new El_ch(e.X + 30 * 0.707, e.Y - 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[5] = points.Count - 1;
                        ela = new El_ch(e.X - 30 * 0.707, e.Y + 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[6] = points.Count - 1;
                        ela = new El_ch(e.X - 30 * 0.707, e.Y - 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[7] = points.Count - 1;

                        at.sta = 8;
                    }
                    else
                    {
                        El_ch ela = new El_ch(e.X - 30 * Math.Cos(Math.PI * 0), e.Y - 30 * Math.Sin(Math.PI * 0), false);
                        points.Add(ela);
                        at.x16[0] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8), e.Y - 30 * Math.Sin(Math.PI / 8), false);
                        points.Add(ela);
                        at.x16[1] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 4), e.Y - 30 * Math.Sin(Math.PI / 4), false);
                        points.Add(ela);
                        at.x16[2] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 3), e.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);
                        points.Add(ela);
                        at.x16[3] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 2), e.Y - 30 * Math.Sin(Math.PI / 2), false);
                        points.Add(ela);
                        at.x16[4] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 5), e.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);
                        points.Add(ela);
                        at.x16[5] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 6), e.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);
                        points.Add(ela);
                        at.x16[6] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 7), e.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);
                        points.Add(ela);
                        at.x16[7] = points.Count - 1;


                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI * 0), e.Y + 30 * Math.Sin(Math.PI * 0), false);
                        points.Add(ela);
                        at.x16[8] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8), e.Y + 30 * Math.Sin(Math.PI / 8), false);
                        points.Add(ela);
                        at.x16[9] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 4), e.Y + 30 * Math.Sin(Math.PI / 4), false);
                        points.Add(ela);
                        at.x16[10] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 3), e.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);
                        points.Add(ela);
                        at.x16[11] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 2), e.Y + 30 * Math.Sin(Math.PI / 2), false);
                        points.Add(ela);
                        at.x16[12] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 5), e.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);
                        points.Add(ela);
                        at.x16[13] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 6), e.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);
                        points.Add(ela);
                        at.x16[14] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 7), e.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                        points.Add(ela);
                        at.x16[15] = points.Count - 1;
                        at.sta = 16;
                    }
                    break;
                case 3:
                    Prob el = new Prob(e.X, e.Y, false);
                    probs.Add(el);
                    break;
                case 7:
                    foreach (Atom atom in atoms)
                    {
                        double rad = Math.Sqrt((atom.x - e.X)* (atom.x - e.X) + (atom.y - e.Y) * (atom.y - e.Y));
                        if (rad < 10)
                        {
                            selected = atoms.IndexOf(atom);
                            label7.Text = string.Format("{0}", atom.x);
                            label8.Text = string.Format("{0}", 750 - atom.y);
                        }
                    }
                    break;
                case 10:
                    dow = true;
                    at = new Atom(e.X, e.Y, 0);
                    atoms.Add(at);
                    if (dalk == 0)
                    {
                        El_ch ela = new El_ch(e.X - 30, e.Y, false);
                        points.Add(ela);
                        at.x8[0] = points.Count - 1;
                        ela = new El_ch(e.X, e.Y - 30, false);
                        points.Add(ela);
                        at.x8[1] = points.Count - 1;
                        ela = new El_ch(e.X, e.Y + 30, false);
                        points.Add(ela);
                        at.x8[2] = points.Count - 1;
                        ela = new El_ch(e.X + 30, e.Y, false);
                        points.Add(ela);
                        at.x8[3] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * 0.707, e.Y + 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[4] = points.Count - 1;
                        ela = new El_ch(e.X + 30 * 0.707, e.Y - 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[5] = points.Count - 1;
                        ela = new El_ch(e.X - 30 * 0.707, e.Y + 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[6] = points.Count - 1;
                        ela = new El_ch(e.X - 30 * 0.707, e.Y - 30 * 0.707, false);
                        points.Add(ela);
                        at.x8[7] = points.Count - 1;

                        at.sta = 8;
                    }
                    else
                    {
                        El_ch ela = new El_ch(e.X - 30 * Math.Cos(Math.PI * 0), e.Y - 30 * Math.Sin(Math.PI * 0), false);
                        points.Add(ela);
                        at.x16[0] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8), e.Y - 30 * Math.Sin(Math.PI / 8), false);
                        points.Add(ela);
                        at.x16[1] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 4), e.Y - 30 * Math.Sin(Math.PI / 4), false);
                        points.Add(ela);
                        at.x16[2] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 3), e.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);
                        points.Add(ela);
                        at.x16[3] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 2), e.Y - 30 * Math.Sin(Math.PI / 2), false);
                        points.Add(ela);
                        at.x16[4] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 5), e.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);
                        points.Add(ela);
                        at.x16[5] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 6), e.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);
                        points.Add(ela);
                        at.x16[6] = points.Count - 1;

                        ela = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 7), e.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);
                        points.Add(ela);
                        at.x16[7] = points.Count - 1;


                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI * 0), e.Y + 30 * Math.Sin(Math.PI * 0), false);
                        points.Add(ela);
                        at.x16[8] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8), e.Y + 30 * Math.Sin(Math.PI / 8), false);
                        points.Add(ela);
                        at.x16[9] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 4), e.Y + 30 * Math.Sin(Math.PI / 4), false);
                        points.Add(ela);
                        at.x16[10] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 3), e.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);
                        points.Add(ela);
                        at.x16[11] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 2), e.Y + 30 * Math.Sin(Math.PI / 2), false);
                        points.Add(ela);
                        at.x16[12] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 5), e.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);
                        points.Add(ela);
                        at.x16[13] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 6), e.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);
                        points.Add(ela);
                        at.x16[14] = points.Count - 1;

                        ela = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 7), e.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                        points.Add(ela);
                        at.x16[15] = points.Count - 1;
                        at.sta = 16;
                    }
                    break;
                default: break;
            }
            paintka();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dow)
            { 
                switch (type)
                {
                    case 1:
                        atoms[atoms.Count - 1].x = e.X;
                        atoms[atoms.Count - 1].y = e.Y;
                        if (dalk == 0)
                        {
                            points[points.Count - 8] = new El_ch(e.X - 30, e.Y, false);
                            points[points.Count - 7] = new El_ch(e.X, e.Y - 30, false);
                            points[points.Count - 6] = new El_ch(e.X, e.Y + 30, false);
                            points[points.Count - 5] = new El_ch(e.X + 30, e.Y, false);

                            points[points.Count - 4] = new El_ch(e.X + 30 * 0.707, e.Y + 30 * 0.707, false);
                            points[points.Count - 3] = new El_ch(e.X + 30 * 0.707, e.Y - 30 * 0.707, false);
                            points[points.Count - 2] = new El_ch(e.X - 30 * 0.707, e.Y + 30 * 0.707, false);
                            points[points.Count - 1] = new El_ch(e.X - 30 * 0.707, e.Y - 30 * 0.707, false);
                        }
                        else
                        {
                            points[points.Count - 16] = new El_ch(e.X - 30 * Math.Cos(Math.PI * 0), e.Y - 30 * Math.Sin(Math.PI * 0), false);

                            points[points.Count - 15] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8), e.Y - 30 * Math.Sin(Math.PI / 8), false);

                            points[points.Count - 14] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 4), e.Y - 30 * Math.Sin(Math.PI / 4), false);

                            points[points.Count - 13] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 3), e.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);

                            points[points.Count - 12] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 2), e.Y - 30 * Math.Sin(Math.PI / 2), false);

                            points[points.Count - 11] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 5), e.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);

                            points[points.Count - 10] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 6), e.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);

                            points[points.Count - 9] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 7), e.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);


                            points[points.Count - 8] = new El_ch(e.X + 30 * Math.Cos(Math.PI * 0), e.Y + 30 * Math.Sin(Math.PI * 0), false);

                            points[points.Count - 7] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8), e.Y + 30 * Math.Sin(Math.PI / 8), false);

                            points[points.Count - 6] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 4), e.Y + 30 * Math.Sin(Math.PI / 4), false);

                            points[points.Count - 5] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 3), e.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);

                            points[points.Count - 4] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 2), e.Y + 30 * Math.Sin(Math.PI / 2), false);

                            points[points.Count - 3] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 5), e.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);

                            points[points.Count - 2] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 6), e.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);

                            points[points.Count - 1] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 7), e.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                        }
                        break;
                    case 2:
                        atoms[atoms.Count - 1].x = e.X;
                        atoms[atoms.Count - 1].y = e.Y;
                        if (dalk == 0)
                        {
                            points[points.Count - 8] = new El_ch(e.X - 30, e.Y, false);
                            points[points.Count - 7] = new El_ch(e.X, e.Y - 30, false);
                            points[points.Count - 6] = new El_ch(e.X, e.Y + 30, false);
                            points[points.Count - 5] = new El_ch(e.X + 30, e.Y, false);

                            points[points.Count - 4] = new El_ch(e.X + 30 * 0.707, e.Y + 30 * 0.707, false);
                            points[points.Count - 3] = new El_ch(e.X + 30 * 0.707, e.Y - 30 * 0.707, false);
                            points[points.Count - 2] = new El_ch(e.X - 30 * 0.707, e.Y + 30 * 0.707, false);
                            points[points.Count - 1] = new El_ch(e.X - 30 * 0.707, e.Y - 30 * 0.707, false);
                        }
                        else
                        {
                            points[points.Count - 16] = new El_ch(e.X - 30 * Math.Cos(Math.PI * 0), e.Y - 30 * Math.Sin(Math.PI * 0), false);

                            points[points.Count - 15] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8), e.Y - 30 * Math.Sin(Math.PI / 8), false);

                            points[points.Count - 14] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 4), e.Y - 30 * Math.Sin(Math.PI / 4), false);

                            points[points.Count - 13] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 3), e.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);

                            points[points.Count - 12] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 2), e.Y - 30 * Math.Sin(Math.PI / 2), false);

                            points[points.Count - 11] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 5), e.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);

                            points[points.Count - 10] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 6), e.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);

                            points[points.Count - 9] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 7), e.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);


                            points[points.Count - 8] = new El_ch(e.X + 30 * Math.Cos(Math.PI * 0), e.Y + 30 * Math.Sin(Math.PI * 0), false);

                            points[points.Count - 7] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8), e.Y + 30 * Math.Sin(Math.PI / 8), false);

                            points[points.Count - 6] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 4), e.Y + 30 * Math.Sin(Math.PI / 4), false);

                            points[points.Count - 5] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 3), e.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);

                            points[points.Count - 4] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 2), e.Y + 30 * Math.Sin(Math.PI / 2), false);

                            points[points.Count - 3] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 5), e.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);

                            points[points.Count - 2] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 6), e.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);

                            points[points.Count - 1] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 7), e.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                        }
                        break;
                    case 3:
                        probs[probs.Count - 1] = new Prob(e.X, e.Y, false);
                        break;
                    case 7:
                        selected = -1;
                        foreach (Atom atom in atoms)
                        {
                            double rad = Math.Sqrt((atom.x - e.X) * (atom.x - e.X) + (atom.y - e.Y) * (atom.y - e.Y));
                            if (rad < 10)
                                selected = atoms.IndexOf(atom);
                        }
                        if (selected >= 0)
                        {
                            label7.Text = string.Format("{0}", atoms[selected].x);
                            label8.Text = string.Format("{0}", 750 - atoms[selected].y);
                            if (atoms[selected].sta == 8)
                            {
                                /*foreach (int i in atoms[selected].x8)
                                {
                                    Console.WriteLine(i);
                                }*/
                                atoms[selected].x = e.X;
                                atoms[selected].y = e.Y;

                                points[atoms[selected].x8[0]] = new El_ch(e.X - 30, e.Y, false);
                                points[atoms[selected].x8[1]] = new El_ch(e.X, e.Y - 30, false);
                                points[atoms[selected].x8[2]] = new El_ch(e.X, e.Y + 30, false);
                                points[atoms[selected].x8[3]] = new El_ch(e.X + 30, e.Y, false);

                                points[atoms[selected].x8[4]] = new El_ch(e.X + 30 * 0.707, e.Y + 30 * 0.707, false);
                                points[atoms[selected].x8[5]] = new El_ch(e.X + 30 * 0.707, e.Y - 30 * 0.707, false);
                                points[atoms[selected].x8[6]] = new El_ch(e.X - 30 * 0.707, e.Y + 30 * 0.707, false);
                                points[atoms[selected].x8[7]] = new El_ch(e.X - 30 * 0.707, e.Y - 30 * 0.707, false);
                            }
                            else
                            {
                                atoms[selected].x = e.X;
                                atoms[selected].y = e.Y;

                                points[atoms[selected].x16[0]] = new El_ch(e.X - 30 * Math.Cos(Math.PI * 0), e.Y - 30 * Math.Sin(Math.PI * 0), false);

                                points[atoms[selected].x16[1]] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8), e.Y - 30 * Math.Sin(Math.PI / 8), false);

                                points[atoms[selected].x16[2]] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 4), e.Y - 30 * Math.Sin(Math.PI / 4), false);

                                points[atoms[selected].x16[3]] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 3), e.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);

                                points[atoms[selected].x16[4]] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 2), e.Y - 30 * Math.Sin(Math.PI / 2), false);

                                points[atoms[selected].x16[5]] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 5), e.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);

                                points[atoms[selected].x16[6]] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 6), e.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);

                                points[atoms[selected].x16[7]] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 7), e.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);


                                points[atoms[selected].x16[8]] = new El_ch(e.X + 30 * Math.Cos(Math.PI * 0), e.Y + 30 * Math.Sin(Math.PI * 0), false);

                                points[atoms[selected].x16[9]] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8), e.Y + 30 * Math.Sin(Math.PI / 8), false);

                                points[atoms[selected].x16[10]] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 4), e.Y + 30 * Math.Sin(Math.PI / 4), false);

                                points[atoms[selected].x16[11]] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 3), e.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);

                                points[atoms[selected].x16[12]] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 2), e.Y + 30 * Math.Sin(Math.PI / 2), false);

                                points[atoms[selected].x16[13]] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 5), e.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);

                                points[atoms[selected].x16[14]] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 6), e.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);

                                points[atoms[selected].x16[15]] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 7), e.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                            }
                        }
                        break;
                    case 10:
                        atoms[atoms.Count - 1].x = e.X;
                        atoms[atoms.Count - 1].y = e.Y;
                        if (dalk == 0)
                        {
                            points[points.Count - 8] = new El_ch(e.X - 30, e.Y, false);
                            points[points.Count - 7] = new El_ch(e.X, e.Y - 30, false);
                            points[points.Count - 6] = new El_ch(e.X, e.Y + 30, false);
                            points[points.Count - 5] = new El_ch(e.X + 30, e.Y, false);

                            points[points.Count - 4] = new El_ch(e.X + 30 * 0.707, e.Y + 30 * 0.707, false);
                            points[points.Count - 3] = new El_ch(e.X + 30 * 0.707, e.Y - 30 * 0.707, false);
                            points[points.Count - 2] = new El_ch(e.X - 30 * 0.707, e.Y + 30 * 0.707, false);
                            points[points.Count - 1] = new El_ch(e.X - 30 * 0.707, e.Y - 30 * 0.707, false);
                        }
                        else
                        {
                            points[points.Count - 16] = new El_ch(e.X - 30 * Math.Cos(Math.PI * 0), e.Y - 30 * Math.Sin(Math.PI * 0), false);

                            points[points.Count - 15] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8), e.Y - 30 * Math.Sin(Math.PI / 8), false);

                            points[points.Count - 14] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 4), e.Y - 30 * Math.Sin(Math.PI / 4), false);

                            points[points.Count - 13] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 3), e.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);

                            points[points.Count - 12] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 2), e.Y - 30 * Math.Sin(Math.PI / 2), false);

                            points[points.Count - 11] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 5), e.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);

                            points[points.Count - 10] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 6), e.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);

                            points[points.Count - 9] = new El_ch(e.X - 30 * Math.Cos(Math.PI / 8 * 7), e.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);


                            points[points.Count - 8] = new El_ch(e.X + 30 * Math.Cos(Math.PI * 0), e.Y + 30 * Math.Sin(Math.PI * 0), false);

                            points[points.Count - 7] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8), e.Y + 30 * Math.Sin(Math.PI / 8), false);

                            points[points.Count - 6] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 4), e.Y + 30 * Math.Sin(Math.PI / 4), false);

                            points[points.Count - 5] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 3), e.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);

                            points[points.Count - 4] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 2), e.Y + 30 * Math.Sin(Math.PI / 2), false);

                            points[points.Count - 3] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 5), e.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);

                            points[points.Count - 2] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 6), e.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);

                            points[points.Count - 1] = new El_ch(e.X + 30 * Math.Cos(Math.PI / 8 * 7), e.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                        }
                        break;
                    default: break;

                }
            paintka();
        }
            //El_ch el = new El_ch(e.X, e.Y, false);
            //points.Add(el);
            //Console.WriteLine("New ell");
            
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dow = false;
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= fillk; i += 20)
            {
                for (int j = 0; j <= fillk; j += 20)
                {
                    El_ch el = new El_ch(i, j, false);
                    points.Add(el);
                }
            }
            paintka();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= fillk; i += 10)
            {
                for (int j = 0; j <= fillk; j += 10)
                {
                    El_ch el = new El_ch(i, j, false);
                    points.Add(el);
                }
            }
            paintka();
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= fillk; i += 50)
            {
                for (int j = 0; j <= fillk; j += 50)
                {
                    El_ch el = new El_ch(i, j, false);
                    points.Add(el);
                }
            }
            paintka();
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= fillk; i += 100)
            {
                for (int j = 0; j <= fillk; j += 100)
                {
                    El_ch el = new El_ch(i, j, false);
                    points.Add(el);
                }
            }
            paintka();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dalk = 16;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //cha = int.Parse(textBox1.Text);
            Console.WriteLine(cha);
            //Console.WriteLine(textBox1.Text);
            //textBox1.Text = "xer";
            //textBox1.Clear();
            Refresh();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            dalk = 0;
        }
        private void plussqaureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 4;
        }
        private void minussquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 5;
        }
        private void pMsquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 6;
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            probs = new List<Prob> { };
            paintka();
        }
        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawer = true;
            paintka();
        }
        private void dontDrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawer = false;
            paintka();
        }
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 7;
        }
        /*private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
            Console.WriteLine(cha + " " + numericUpDown1.Value);
            cha = (int)numericUpDown1.Value;
            Console.WriteLine(cha + " " + numericUpDown1.Value);
        }*/
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                cha = (int.Parse(textBox1.Text));
                if (selected >= 0)
                {
                    atoms[selected].charge = cha;
                    paintka();
                }
            }
            catch
            { }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            (new Form2()).Show();
        }
        private void nullChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            type = 10;
            paintka();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (selected >= 0)
            {
                if (atoms[selected].sta == 8)
                {
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);

                    atoms.RemoveAt(selected);
                }
                else 
                {
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);

                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);
                    points.RemoveAt(atoms[selected].x8[0]);

                    atoms.RemoveAt(selected);
                }
            }
            selected = -1;
            paintka();            
        }
        private void button6_Click(object sender, EventArgs e)
        {
            probs = new List<Prob> { };
            selected = -1;
            try
            {
                int x = int.Parse(textBox2.Text);
                int y = 750 - int.Parse(textBox3.Text);
                int ch = int.Parse(textBox1.Text);

                Atom at = new Atom(x, y, ch);
                atoms.Add(at);
                if (dalk == 0)
                {
                    El_ch ela = new El_ch(at.x - 30, at.y, false);
                    points.Add(ela);
                    at.x8[0] = points.Count - 1;
                    ela = new El_ch(at.x, at.y - 30, false);
                    points.Add(ela);
                    at.x8[1] = points.Count - 1;
                    ela = new El_ch(at.x, at.y + 30, false);
                    points.Add(ela);
                    at.x8[2] = points.Count - 1;
                    ela = new El_ch(at.x + 30, at.y, false);
                    points.Add(ela);
                    at.x8[3] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * 0.707, at.y + 30 * 0.707, false);
                    points.Add(ela);
                    at.x8[4] = points.Count - 1;
                    ela = new El_ch(at.x + 30 * 0.707, at.y - 30 * 0.707, false);
                    points.Add(ela);
                    at.x8[5] = points.Count - 1;
                    ela = new El_ch(at.x - 30 * 0.707, at.y + 30 * 0.707, false);
                    points.Add(ela);
                    at.x8[6] = points.Count - 1;
                    ela = new El_ch(at.x - 30 * 0.707, at.y - 30 * 0.707, false);
                    points.Add(ela);
                    at.x8[7] = points.Count - 1;

                    at.sta = 8;
                }
                else
                {
                    El_ch ela = new El_ch(at.x - 30 * Math.Cos(Math.PI * 0), at.y - 30 * Math.Sin(Math.PI * 0), false);
                    points.Add(ela);
                    at.x16[0] = points.Count - 1;

                    ela = new El_ch(at.x - 30 * Math.Cos(Math.PI / 8), at.y - 30 * Math.Sin(Math.PI / 8), false);
                    points.Add(ela);
                    at.x16[1] = points.Count - 1;

                    ela = new El_ch(at.x - 30 * Math.Cos(Math.PI / 4), at.y - 30 * Math.Sin(Math.PI / 4), false);
                    points.Add(ela);
                    at.x16[2] = points.Count - 1;

                    ela = new El_ch(at.x - 30 * Math.Cos(Math.PI / 8 * 3), at.y - 30 * Math.Sin(Math.PI / 8 * 3), false);
                    points.Add(ela);
                    at.x16[3] = points.Count - 1;

                    ela = new El_ch(at.x - 30 * Math.Cos(Math.PI / 2), at.y - 30 * Math.Sin(Math.PI / 2), false);
                    points.Add(ela);
                    at.x16[4] = points.Count - 1;

                    ela = new El_ch(at.x - 30 * Math.Cos(Math.PI / 8 * 5), at.y - 30 * Math.Sin(Math.PI / 8 * 5), false);
                    points.Add(ela);
                    at.x16[5] = points.Count - 1;

                    ela = new El_ch(at.x - 30 * Math.Cos(Math.PI / 8 * 6), at.y - 30 * Math.Sin(Math.PI / 8 * 6), false);
                    points.Add(ela);
                    at.x16[6] = points.Count - 1;

                    ela = new El_ch(at.x - 30 * Math.Cos(Math.PI / 8 * 7), at.y - 30 * Math.Sin(Math.PI / 8 * 7), false);
                    points.Add(ela);
                    at.x16[7] = points.Count - 1;


                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI * 0), at.y + 30 * Math.Sin(Math.PI * 0), false);
                    points.Add(ela);
                    at.x16[8] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI / 8), at.y + 30 * Math.Sin(Math.PI / 8), false);
                    points.Add(ela);
                    at.x16[9] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI / 4), at.y + 30 * Math.Sin(Math.PI / 4), false);
                    points.Add(ela);
                    at.x16[10] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI / 8 * 3), at.y + 30 * Math.Sin(Math.PI / 8 * 3), false);
                    points.Add(ela);
                    at.x16[11] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI / 2), at.y + 30 * Math.Sin(Math.PI / 2), false);
                    points.Add(ela);
                    at.x16[12] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI / 8 * 5), at.y + 30 * Math.Sin(Math.PI / 8 * 5), false);
                    points.Add(ela);
                    at.x16[13] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI / 8 * 6), at.y + 30 * Math.Sin(Math.PI / 8 * 6), false);
                    points.Add(ela);
                    at.x16[14] = points.Count - 1;

                    ela = new El_ch(at.x + 30 * Math.Cos(Math.PI / 8 * 7), at.y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                    points.Add(ela);
                    at.x16[15] = points.Count - 1;
                    at.sta = 16;
                }

                label4.Text = "";
                paintka();
            }
            catch
            {
                label4.Text = "Error";
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            coor = checkBox1.Checked;
            paintka();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (selected >= 0)
            {
                try
                {
                    int x_old = atoms[selected].x;
                    int dx = int.Parse(textBox2.Text) - x_old;
                    atoms[selected].x = int.Parse(textBox2.Text);
                    if (atoms[selected].sta == 8)
                    {
                        points[atoms[selected].x8[0]].x += dx;
                        points[atoms[selected].x8[1]].x += dx;
                        points[atoms[selected].x8[2]].x += dx;
                        points[atoms[selected].x8[3]].x += dx;

                        points[atoms[selected].x8[4]].x += dx;
                        points[atoms[selected].x8[5]].x += dx;
                        points[atoms[selected].x8[6]].x += dx;
                        points[atoms[selected].x8[7]].x += dx;
                    }
                    else
                    {
                        Point el = new Point(atoms[selected].x, atoms[selected].y);
                        points[atoms[selected].x16[0]] = new El_ch(el.X - 30 * Math.Cos(Math.PI * 0), el.Y - 30 * Math.Sin(Math.PI * 0), false);

                        points[atoms[selected].x16[1]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8), el.Y - 30 * Math.Sin(Math.PI / 8), false);

                        points[atoms[selected].x16[2]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 4), el.Y - 30 * Math.Sin(Math.PI / 4), false);

                        points[atoms[selected].x16[3]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 3), el.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);

                        points[atoms[selected].x16[4]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 2), el.Y - 30 * Math.Sin(Math.PI / 2), false);

                        points[atoms[selected].x16[5]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 5), el.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);

                        points[atoms[selected].x16[6]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 6), el.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);

                        points[atoms[selected].x16[7]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 7), el.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);


                        points[atoms[selected].x16[8]] = new El_ch(el.X + 30 * Math.Cos(Math.PI * 0), el.Y + 30 * Math.Sin(Math.PI * 0), false);

                        points[atoms[selected].x16[9]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8), el.Y + 30 * Math.Sin(Math.PI / 8), false);

                        points[atoms[selected].x16[10]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 4), el.Y + 30 * Math.Sin(Math.PI / 4), false);

                        points[atoms[selected].x16[11]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 3), el.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);

                        points[atoms[selected].x16[12]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 2), el.Y + 30 * Math.Sin(Math.PI / 2), false);

                        points[atoms[selected].x16[13]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 5), el.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);

                        points[atoms[selected].x16[14]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 6), el.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);

                        points[atoms[selected].x16[15]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 7), el.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                    }
                    label4.Text = "";
                }
                catch 
                {
                    label4.Text = "Error";
                }
                label7.Text = string.Format("{0}", atoms[selected].x);
                label8.Text = string.Format("{0}", 750 - atoms[selected].y);
            }
            
            paintka();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (selected >= 0)
            {
                try
                {
                    int x_old = atoms[selected].y;
                    int dx = 750 - int.Parse(textBox3.Text) - x_old;
                    atoms[selected].y = 750 - int.Parse(textBox3.Text);
                    if (atoms[selected].sta == 8)
                    {
                        points[atoms[selected].x8[0]].y += dx;
                        points[atoms[selected].x8[1]].y += dx;
                        points[atoms[selected].x8[2]].y += dx;
                        points[atoms[selected].x8[3]].y += dx;

                        points[atoms[selected].x8[4]].y += dx;
                        points[atoms[selected].x8[5]].y += dx;
                        points[atoms[selected].x8[6]].y += dx;
                        points[atoms[selected].x8[7]].y += dx;
                    }
                    else
                    {
                        Point el = new Point(atoms[selected].x, atoms[selected].y);
                        points[atoms[selected].x16[0]] = new El_ch(el.X - 30 * Math.Cos(Math.PI * 0), el.Y - 30 * Math.Sin(Math.PI * 0), false);

                        points[atoms[selected].x16[1]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8), el.Y - 30 * Math.Sin(Math.PI / 8), false);

                        points[atoms[selected].x16[2]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 4), el.Y - 30 * Math.Sin(Math.PI / 4), false);

                        points[atoms[selected].x16[3]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 3), el.Y - 30 * Math.Sin(Math.PI / 8 * 3), false);

                        points[atoms[selected].x16[4]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 2), el.Y - 30 * Math.Sin(Math.PI / 2), false);

                        points[atoms[selected].x16[5]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 5), el.Y - 30 * Math.Sin(Math.PI / 8 * 5), false);

                        points[atoms[selected].x16[6]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 6), el.Y - 30 * Math.Sin(Math.PI / 8 * 6), false);

                        points[atoms[selected].x16[7]] = new El_ch(el.X - 30 * Math.Cos(Math.PI / 8 * 7), el.Y - 30 * Math.Sin(Math.PI / 8 * 7), false);


                        points[atoms[selected].x16[8]] = new El_ch(el.X + 30 * Math.Cos(Math.PI * 0), el.Y + 30 * Math.Sin(Math.PI * 0), false);

                        points[atoms[selected].x16[9]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8), el.Y + 30 * Math.Sin(Math.PI / 8), false);

                        points[atoms[selected].x16[10]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 4), el.Y + 30 * Math.Sin(Math.PI / 4), false);

                        points[atoms[selected].x16[11]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 3), el.Y + 30 * Math.Sin(Math.PI / 8 * 3), false);

                        points[atoms[selected].x16[12]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 2), el.Y + 30 * Math.Sin(Math.PI / 2), false);

                        points[atoms[selected].x16[13]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 5), el.Y + 30 * Math.Sin(Math.PI / 8 * 5), false);

                        points[atoms[selected].x16[14]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 6), el.Y + 30 * Math.Sin(Math.PI / 8 * 6), false);

                        points[atoms[selected].x16[15]] = new El_ch(el.X + 30 * Math.Cos(Math.PI / 8 * 7), el.Y + 30 * Math.Sin(Math.PI / 8 * 7), false);
                    }
                    label4.Text = "";
                }
                catch
                {
                    label4.Text = "Error";
                }
                label7.Text = string.Format("{0}", atoms[selected].x);
                label8.Text = string.Format("{0}", 750 - atoms[selected].y);
            }
            paintka();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            selected = -1;
            paintka();
        }
    }
    public partial class Form2 : Form
    {
        private TextBox textBox1;
        public Form2()
        {
            InitializeComponent();
        }

        internal Form2(int textBoxesCount)
        {
            InitializeComponent();
            TextBox[] textBoxes = new TextBox[textBoxesCount];
            this.AutoSize = true;
            for (int i = 0; i < textBoxesCount; i++)
            {
                textBoxes[i] = new TextBox();
                textBoxes[i].Top = 5 + i * 25;
                textBoxes[i].Left = 5;
                this.Controls.Add(textBoxes[i]);
            }
        }
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Multiline = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Enabled = false;
            this.textBox1.Text = 
                "Эта программа позволяет симулировать силовые линии в электростатическом поле " +
                Environment.NewLine +"Чтобы создать заряд, нажмите на кнопку New Charge и выберите тип заряда (+/-)"
                + Environment.NewLine + "У каждого созданного заряда изначально модуль заряда равен 1"
                + Environment.NewLine + "Для изменения необходимо перед созданием заряда указать его модуль заряда в поле для ввода"
                + Environment.NewLine + "Кнопка Test charge отвечает за создание пробного заряда и симуляцию его движения"
                + Environment.NewLine + "Зеленым цветом обозначены силовые линии"
                + Environment.NewLine + "Красным цветом - положительные заряды"
                + Environment.NewLine + "Синим цветом - отрицательные заряды"
                + Environment.NewLine + "Желтым - траектория движения пробного заряда"
                + Environment.NewLine + "Кнопка Clear очищает экран"
                + Environment.NewLine + "Кнопка Fill заполняет экран силовыми линиями: чем больше число, тем меньше линий"
                + Environment.NewLine + "Кнопка Move позволяет двигать заряд"
                + Environment.NewLine + "Кнопки 16 lines и 8 lines определяют, сколько линий будет ВЫХОДИТЬ из заряда при его создании"
                + Environment.NewLine + "При создании любой заряд можно двигать до тех пор, пока зажата ЛКМ"
                ;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(512, 256);
            this.Controls.Add(this.textBox1);
            this.Text = "TextBox Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
    public class Atom
    {
        public int x, y;
        public double charge = 0;
        public int[] x16 = new int[16];
        public int[] x8 = new int[8];
        public int sta = 8;
        double k = 9 * Math.Pow(10, 6);
        public Atom()
        {
            x = 0;
            y = 0;
        }
        public Atom(int x, int y, double charge)
        {
            this.x = x;
            this.y = y;
            this.charge = charge;
        }
        public Vector forcer(El_ch el)
        {
            double rad = Math.Sqrt((el.x - x) * (el.x - x) + (el.y - y) * (el.y - y));
            Vector once = new Vector((double)(el.x - x) / rad, (double)(el.y - y) / rad);
            double force = k * el.charge * charge / (rad * rad);
            //Console.WriteLine(" one-vec " + once);
            //Console.WriteLine(once + " " + rad + " " + force + " " + el.charge*charge*k);
            return new Vector(once.x * force, once.y * force);
        }
    }
    public class El_ch
    {
        public double x, y;
        public double charge = -1;
        public El_ch()
        {
            x = 0;
            y = 0;
        }
        public El_ch(double x, double y, bool plus)
        {
            this.x = x;
            this.y = y;
            if (plus) charge = -charge;
        }
    }
    public class Vector
    {
        public double x = 0;
        public double y = 0;
        public Vector()
        {
            x = 0;
            y = 0;
        }
        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return String.Format("{0:0.000000000000}, {1:0.000000000000}", x, y);
        }


    }
    public class Prob
    {
        public double x, y;
        public double charge = -1;
        public Prob()
        {
            x = 0;
            y = 0;
        }
        public Prob(double x, double y, bool plus)
        {
            this.x = x;
            this.y = y;
            if (plus) charge = -charge;
        }
        //public void move

    }
}
