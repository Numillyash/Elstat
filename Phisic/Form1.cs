using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/*
 * В программе используются переобозначения для упрощения обьяснения кода
 * "Атом" - аналог "заряд". Класс Atom
 * "Электрон" - точка генерации силовой линии. Класс El_ch. Привязана к атому
 * "Пробный электрон" - точка генерации маршрута заряда в поле. Класс Prob
*/

namespace Phisic
{
    /// <summary>
    /// Класс, отвечающий за основное окно приложения
    /// </summary>
    public partial class Form1 : Form
    {
        public Icon icon;

        //Флаги

        /// <summary>
        /// Флаг, отвечающий за отрисовку "напряженности" поля
        /// </summary>
        bool tent = false;

        /// <summary>
        /// Флаг, отвечающий за отрисовку точки "нулевой напряженности" поля
        /// </summary>
        bool E_0 = false;

        /// <summary>
        /// Флаг, отвечающий за темную тему
        /// </summary>
        bool black = false;

        /// <summary>
        /// Флаг, отвечающий за рисовку стрелок направлений СЛ
        /// </summary>
        bool arrows = false;

        /// <summary>
        /// Флаг, отвечающий за уменьшение кол-ва стрелок на линии
        /// </summary>
        bool rare = false;

        /// <summary>
        /// Флаг, отвечающий за отрисовку точек исхода СЛ
        /// </summary>
        bool drawer = false;

        /// <summary>
        /// Флаг, отвечающий за рисовку координатной сетки
        /// </summary>
        bool coor = false;

        /// <summary>
        /// Флаг, отвечающий за нажатие кнопки мыши
        /// </summary>
        bool dow = false;

        //Целочисленные переменные

        /// <summary>
        /// Толщина линий
        /// </summary>
        int width = 1;

        /// <summary>
        /// Переменная для подсчета времени рисования стрелок
        /// </summary>
        int kal = 1;

        /// <summary>
        /// Номер выбранного атома
        /// </summary>
        int selected = -1;

        /// <summary>
        /// Размер поля для функции заполнения линиями
        /// </summary>
        int fill_curr = 1;

        /// <summary>
        /// Тип заряда (+,-,0,пробный)
        /// </summary>
        Type type = Type.PLUS;
        enum Type
        {
            PLUS,
            MINUS,
            NEUTRAL,
            PROB,
            MOVE
        }

        /// <summary>
        /// Изначальный заряд
        /// </summary>
        int cha = 1;

        //Списки

        /// <summary>
        /// Список зарядов
        /// </summary>
        List<Atom> atoms = new List<Atom> { };

        /// <summary>
        /// Список пробных зарядов
        /// </summary>
        List<Prob> probs = new List<Prob> { };

        //Другие значения

        /// <summary>
        /// Переменная для вызова событий графики
        /// </summary>
        private readonly Graphics _graphics;
        private Bitmap _bitmap;


        /// <summary>
        /// Инициализация окна
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            icon = this.Icon;
            _bitmap = new Bitmap(1920, 1080);
            _graphics = Graphics.FromImage(_bitmap);
            pictureBox1.Image = _bitmap;
            _graphics.Clear(Color.White);
            paintka();
        }

        /// <summary>
        /// События, происходящие при загрузке окна (не используется)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Рисование линии в сторону отрицательного заряда
        /// </summary>
        /// <param name="dk">Кол-во итераций в цикле</param>
        /// <param name="k">Коеффицент погрешности</param>
        /// <param name="kekl">Продолжать/нет рисование</param>
        /// <param name="x_now">Текущая координата по Х</param>
        /// <param name="y_now">Текущая координата по Y</param>
        private void drawer_line1(int dk, float k, bool kekl, float x_now, float y_now)
        {
            for (int i = 0; i < dk; i++)
            {
                if (!kekl)
                {
                    Vector new_pos = new Vector();
                    know_new_pos(ref kekl, ref new_pos, x_now, y_now);
                    if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) break;
                    Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) break;

                    draw_arrow(true, new PointF(x_now, y_now), new PointF(x_now + (float)(rv.x * k), y_now + (float)(rv.y * k)));
                    kal++;
                    x_now += (float)(rv.x * k);
                    y_now += (float)(rv.y * k);
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Рисование линии в сторону положительного заряда
        /// </summary>
        /// <param name="dk">Кол-во итераций в цикле</param>
        /// <param name="k">Коеффицент погрешности</param>
        /// <param name="kekl">Продолжать/нет рисование</param>
        /// <param name="x_now">Текущая координата по Х</param>
        /// <param name="y_now">Текущая координата по Y</param>
        private void drawer_line2(int dk, float k, bool kekl, float x_now, float y_now)
        {
            for (int i = 0; i < dk; i++)
            {
                if (!kekl)
                {
                    Vector new_pos = new Vector();
                    know_new_pos(ref kekl, ref new_pos, x_now, y_now);
                    if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) break;
                    Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) break;

                    draw_arrow(false, new PointF(x_now, y_now), new PointF(x_now - (float)(rv.x * k), y_now - (float)(rv.y * k)));
                    kal++;
                    x_now -= (float)(rv.x * k);
                    y_now -= (float)(rv.y * k);
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Расчет силовых линий
        /// </summary>
        private void phisica()
        {
            int k = 1;
            kal = 1;
            int dk = 5000;

            foreach (Atom at in atoms)
            {
                foreach (El_ch el in at.electrons)
                {
                    float x_now = (float)el.x;
                    float y_now = (float)el.y;
                    bool kekl = false;
                    drawer_line1(dk, k, kekl, x_now, y_now);
                    x_now = (float)el.x;
                    y_now = (float)el.y;
                    kekl = false;
                    drawer_line2(dk, k, kekl, x_now, y_now);
                    draw_at();
                }

            }
        }

        /// <summary>
        /// Расчет нового положения
        /// </summary>
        /// <param name="kekl">Продолжать/нет рисование</param>
        /// <param name="new_pos">Вектор новых координат</param>
        /// <param name="x_now">Текущая координата по Х</param>
        /// <param name="y_now">Текущая координата по Y</param>
        private void know_new_pos(ref bool kekl, ref Vector new_pos, float x_now, float y_now)
        {
            foreach (Atom at in atoms)
            {
                new_pos += at.forcer(new El_ch(x_now, y_now, false));
                double rad = Math.Sqrt((at.x - x_now) * (at.x - x_now) + (at.y - y_now) * (at.y - y_now));
                if (rad < 5 && at.charge != 0) kekl = true;
            }
            if ((Math.Abs(x_now) >= 1500) || (Math.Abs(y_now) >= 1500)) kekl = true;
        }

        /// <summary>
        /// Отрисовка траектории полета пробного заряда (вспомогательная функция)
        /// </summary>
        /// <param name="dk">Кол-во итераций в цикле</param>
        /// <param name="k2">Коеффицент погрешности</param>
        /// <param name="x_now">Текущая координата по Х</param>
        /// <param name="y_now">Текущая координата по Y</param>
        /// <param name="kekl">Продолжать/нет рисование</param>
        /// <param name="x_old">Старая координата по Х</param>
        /// <param name="y_old">Старая координата по Y</param>
        private void check(int dk, float k2, float x_now, float y_now, bool kekl, float x_old, float y_old)
        {
            Brush brush_bl = new SolidBrush(Color.Black);
            Brush brush_y = new SolidBrush(Color.Yellow);

            for (int i = 1; i < dk; i++)
            {
                if (!kekl)
                {
                    Vector new_pos = new Vector();
                    bool d = false;
                    know_new_pos(ref d, ref new_pos, x_now, y_now);

                    if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) break;
                    Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) break;

                    float a = x_now, b = y_now;
                    x_now = (float)(2 * x_now - x_old + rv.x * k2 * k2);
                    y_now = (float)(2 * y_now - y_old + rv.y * k2 * k2);
                    x_old = a;
                    y_old = b;

                    _graphics.FillEllipse(brush_bl, new RectangleF(x_now - 2, y_now - 2, 4, 4));
                    _graphics.DrawLine(new Pen(brush_y), new PointF(x_old, y_old), new PointF(x_now, y_now));
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Отрисовка траектории полета пробного заряда
        /// </summary>
        private void phisica2()
        {
            float k2 = 1;
            int dk = 300;
            Brush brush_bl = new SolidBrush(Color.Black);
            Brush brush_y = new SolidBrush(Color.Yellow);

            foreach (Prob el in probs)
            {
                float x_now = (float)el.x;
                float y_now = (float)el.y;
                float x_old = x_now;
                float y_old = y_now;
                bool kekl = false;
                if (!kekl)
                {
                    Vector new_pos = new Vector();
                    bool d = false;
                    know_new_pos(ref d, ref new_pos, x_now, y_now);
                    if (double.IsNaN(new_pos.x) || double.IsNaN(new_pos.y)) continue;
                    Vector rv = new Vector(new_pos.x / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y), new_pos.y / Math.Sqrt(new_pos.x * new_pos.x + new_pos.y * new_pos.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) continue;

                    x_now += (float)(rv.x * k2 * k2);
                    y_now += (float)(rv.y * k2 * k2);
                    _graphics.FillEllipse(brush_bl, new RectangleF(x_now - 2, y_now - 2, 4, 4));
                    _graphics.DrawLine(new Pen(brush_y), new PointF(x_old, y_old), new PointF(x_now, y_now));
                }
                check(dk, k2, x_now, y_now, kekl, x_old, y_old);
            }
        }

        /// <summary>
        /// Расчет напряженности поля
        /// </summary>
        private void phisica3()
        {
            double k = 1.045;
            Brush brush = new SolidBrush(Color.Red);

            Brush brush_bl = new SolidBrush(Color.Black);
            Brush brush_b = new SolidBrush(Color.Blue);
            Brush brush_y = new SolidBrush(Color.Yellow);
            Brush brush_g = new SolidBrush(Color.Green);

            if (tent)
            {
                for (int i = 0; i < 800; i++)
                {
                    for (int j = 0; j < 800; j++)
                    {
                        El_ch el = new El_ch(i, j, false);

                        Vector new_pos = new Vector();

                        foreach (Atom at in atoms)
                        {
                            new_pos += at.forcer(el);
                            //double rad = Math.Sqrt((at.x - x_now) * (at.x - x_now) + (at.y - y_now) * (at.y - y_now));
                        }
                        double rad = Math.Sqrt(Math.Pow(new_pos.x, 2) + Math.Pow(new_pos.y, 2));
                        if (!double.IsNaN(rad) && !double.IsInfinity(rad))
                        {
                            if (Math.Log(rad, k) >= 255)
                            {
                                _graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(i, j, 1, 1));

                            }
                            else if (Math.Log(rad, k) <= 0)
                            {
                                _graphics.FillRectangle(new SolidBrush(Color.FromArgb((int)rad, (int)rad, (int)rad)), new Rectangle(i, j, 1, 1));
                            }
                            else
                            {
                                _graphics.FillRectangle(new SolidBrush(Color.FromArgb((int)(Math.Log(rad, k)), (int)(Math.Log(rad, k)), (int)(Math.Log(rad, k)))), new Rectangle(i, j, 1, 1));
                            }
                        }
                    }
                }

            }
            foreach (Atom at in atoms)
            {
                if (at.charge > 0)
                    _graphics.FillEllipse(brush, new Rectangle(at.x - at.radius / 2, at.y - at.radius / 2, at.radius, at.radius));
                if (at.charge == 0)
                    _graphics.FillEllipse(brush_bl, new Rectangle(at.x - at.radius / 2, at.y - at.radius / 2, at.radius, at.radius));
                if (at.charge < 0)
                    _graphics.FillEllipse(brush_b, new Rectangle(at.x - at.radius / 2, at.y - at.radius / 2, at.radius, at.radius));
            }
        }

        /// <summary>
        /// Расчет точки нулевой напряженности поля (не используется)
        /// </summary>
        private void phisica_E_0()
        {
            if (E_0)
            {
                int all_one_charge = 0;
                foreach (Atom atom in atoms)
                {
                    if (atom.charge < 0)
                    {
                        all_one_charge += 0;
                    }
                    else
                    {
                        all_one_charge += 1;
                    }
                }

                // Eix = (x-a) * Q / ((x-a)^2+(y-b)^2)^2
                // Eiy = (y-b) * Q / ((x-a)^2+(y-b)^2)^2
                // if (Ex^2+Ey^2 < 1e-6) : cool

                int kv_min_x = 800, kv_min_y = 800, kv_max_x = -1, kv_max_y = -1;
                foreach (Atom at in atoms)
                {
                    if (at.x > kv_max_x)
                        kv_max_x = at.x;
                    if (at.x < kv_min_x)
                        kv_min_x = at.x;
                    if (at.y > kv_max_y)
                        kv_max_y = at.y;
                    if (at.y < kv_min_y)
                        kv_min_y = at.y;
                }

                if (all_one_charge == 0 || all_one_charge == atoms.Count)
                {
                    double e_x = -10, e_y = -10;
                    int e_0_rad = 10;
                    for (e_x = kv_min_x; e_x < kv_max_x; e_x += 0.1)
                    {
                        for (e_y = kv_min_y; e_y < kv_max_y; e_y += 0.1)
                        {
                            El_ch el_Ch = new El_ch(e_x, e_y, false);
                            Vector summ = new Vector(0, 0);
                            foreach (Atom at in atoms)
                            {
                                summ += at.forcer(el_Ch);
                            }
                            if (Math.Abs(summ.GetLong()) <= 0.1)
                            {
                                if (atoms.Count > 1)
                                {
                                    Brush brush = new SolidBrush(Color.Purple);
                                    _graphics.FillEllipse(brush, new Rectangle((int)e_x - e_0_rad / 2, (int)e_y - e_0_rad / 2, e_0_rad, e_0_rad));
                                }
                                Console.WriteLine(e_x + " " + e_y + " " + summ);
                            }
                        }
                    }


                }
            }
        }

        /// <summary>
        /// Выбор в каком конце рисовать стрелку
        /// </summary>
        /// <param name="tr">На конце или на начале</param>
        /// <param name="pen">Кисть</param>
        private void start_end(bool tr, ref Pen pen)
        {
            if (tr)
                pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            else
                pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }

        /// <summary>
        /// Рисование линий и стрелок на них
        /// </summary>
        /// <param name="tr">На конце или на начале</param>
        /// <param name="p1">Точка начала</param>
        /// <param name="p2">Точка конца</param>
        private void draw_arrow(bool tr, PointF p1, PointF p2)
        {
            Pen pen = new Pen(Color.Green, width);
            if (arrows)
            {
                if (rare && kal == 75)
                {
                    start_end(tr, ref pen);
                    kal = 1;
                }
                else if (!rare && kal % 15 == 0)
                {
                    start_end(tr, ref pen);
                    kal = 1;
                }
            }
            _graphics.DrawLine(pen, p1, p2);
        }

        /// <summary>
        /// Отрисовка всех атомов
        /// </summary>
        private void draw_at()
        {
            Brush brush = new SolidBrush(Color.Red);
            Brush brush_b = new SolidBrush(Color.Blue);
            Brush brush_bl = new SolidBrush(Color.Black);
            foreach (Atom at in atoms)
            {
                if (at.charge > 0)
                    _graphics.FillEllipse(brush, new Rectangle(at.x - at.radius / 2, at.y - at.radius / 2, at.radius, at.radius));
                if (at.charge == 0)
                    _graphics.FillEllipse(brush_bl, new Rectangle(at.x - at.radius / 2, at.y - at.radius / 2, at.radius, at.radius));
                if (at.charge < 0)
                    _graphics.FillEllipse(brush_b, new Rectangle(at.x - at.radius / 2, at.y - at.radius / 2, at.radius, at.radius));
            }
        }

        /// <summary>
        /// Отрисовка всех электронов
        /// </summary>
        private void draw_el()
        {
            if (drawer)
            {
                Brush brush_b = new SolidBrush(Color.Blue);
                foreach (Atom at in atoms)
                {
                    foreach (El_ch el in at.electrons)
                        _graphics.FillEllipse(brush_b, new Rectangle((int)(el.x - 2), (int)(el.y - 2), 4, 4));

                }
            }
        }

        /// <summary>
        /// Отрисовка координатной сетки
        /// </summary>
        private void draw_coor()
        {
            Brush brush_bl = new SolidBrush(Color.Black);
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
        }

        /// <summary>
        /// Перерисовка поля
        /// </summary>
        private void paintka()
        {
            _graphics.Clear(Color.White);

            draw_at();
            draw_el();
            draw_coor();
            if (selected >= 0)
            {
                _graphics.DrawEllipse(new Pen(Color.Purple, 2), new Rectangle(atoms[selected].x - atoms[selected].radius / 2 - 2, atoms[selected].y - atoms[selected].radius / 2 - 2, atoms[selected].radius + 4, atoms[selected].radius + 4));
            }

            if (tent)
                phisica3();
            else
            {
                phisica();
                phisica2();
            }
            phisica_E_0();
            Refresh();
        }

        /// <summary>
        /// Создание атома с параметрами координат и заряда
        /// </summary>
        /// <param name="x">Координата по Х</param>
        /// <param name="y">Координата по Y</param>
        /// <param name="ch">Модуль заряда</param>
        private void create_atom(int x, int y, int ch)
        {
            Atom at = new Atom(x, y, ch, fill_curr);
            atoms.Add(at);
        }

        /// <summary>
        /// Очистка поля
        /// </summary>
        private void clear()
        {
            type = Type.PLUS;
            atoms = new List<Atom> { };
            probs = new List<Prob> { };
            selected = -1;
            paintka();
        }

        /// <summary>
        /// Обработка клика (не используется)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обработка клика (не используется)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// Выбор отрицательного заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            paintka();
            type = Type.MINUS;
        }

        /// <summary>
        /// Выбор положительного заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            type = Type.PLUS;
            paintka();
        }

        /// <summary>
        /// Выбор електрона (не используется)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            type = Type.PROB;
            paintka();
        }

        /// <summary>
        /// Обработка нажатия кнопки Clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear();
        }

        /// <summary>
        /// Клик мышью(она только нажата)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            probs = new List<Prob> { };
            dow = true;
            selected = -1;
            switch (type)
            {
                case Type.PLUS:
                    dow = true;
                    create_atom(e.X, e.Y, Math.Abs(cha));
                    break;
                case Type.MINUS:
                    dow = true;
                    create_atom(e.X, e.Y, -Math.Abs(cha));
                    break;
                case Type.PROB:
                    Prob el = new Prob(e.X, e.Y, false);
                    probs.Add(el);
                    break;
                case Type.MOVE:
                    foreach (Atom atom in atoms)
                    {
                        double rad = Math.Sqrt((atom.x - e.X) * (atom.x - e.X) + (atom.y - e.Y) * (atom.y - e.Y));
                        if (rad < 10)
                        {
                            selected = atoms.IndexOf(atom);
                            textBox11.Text = string.Format("{0}", atom.x);
                            textBox10.Text = string.Format("{0}", 750 - atom.y);
                            textBox13.Text = string.Format("{0}", atoms[selected].charge);
                        }
                    }
                    break;
                case Type.NEUTRAL:
                    dow = true;
                    create_atom(e.X, e.Y, 0);
                    break;
                default: break;
            }
            paintka();
        }

        /// <summary>
        /// Смена положения атома и его електронов
        /// </summary>
        /// <param name="e"></param>
        private void changer_atom(MouseEventArgs e)
        {
            atoms[atoms.Count - 1].x = e.X;
            atoms[atoms.Count - 1].y = e.Y;
            atoms[atoms.Count - 1].create_at_els();
        }

        /// <summary>
        /// Смена положения выделенного атома и его електронов
        /// </summary>
        /// <param name="e"></param>
        private void selected_changa(MouseEventArgs e, int selected)
        {
            atoms[selected].x = e.X;
            atoms[selected].y = e.Y;

            atoms[selected].create_at_els();
        }

        /// <summary>
        /// Клик мышью(она зажата)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dow)
            {
                switch (type)
                {
                    case Type.PLUS:
                        changer_atom(e);
                        break;
                    case Type.MINUS:
                        changer_atom(e);
                        break;
                    case Type.PROB:
                        probs[probs.Count - 1] = new Prob(e.X, e.Y, false);
                        break;
                    case Type.MOVE:
                        selected = -1;
                        foreach (Atom atom in atoms)
                        {
                            double rad = Math.Sqrt((atom.x - e.X) * (atom.x - e.X) + (atom.y - e.Y) * (atom.y - e.Y));
                            if (rad < 10)
                                selected = atoms.IndexOf(atom);
                        }
                        if (selected >= 0)
                        {
                            textBox11.Text = string.Format("{0}", atoms[selected].x);
                            textBox10.Text = string.Format("{0}", 750 - atoms[selected].y);
                            textBox13.Text = string.Format("{0}", atoms[selected].charge);
                            selected_changa(e, selected);
                        }
                        break;
                    case Type.NEUTRAL:
                        changer_atom(e);
                        break;
                    default: break;
                }
                paintka();
            }
        }

        /// <summary>
        /// Мышь отпущена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dow = false;
        }

        /// <summary>
        /// Выбор параметров заполнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Выбор параметров заполнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (fill_curr < 16)
            {
                foreach (Atom at in atoms)
                {
                    at.sta_koeff *= 2;
                    at.create_at_els();
                }
                fill_curr *= 2;
                paintka();
            }
        }

        /// <summary>
        /// Выбор параметров заполнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Выбор параметров заполнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Назначение 8 линий исходящих из заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //dalk = 8;
        }

        /// <summary>
        /// Назначение 16 линий исходящих из заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //dalk = 16;
        }

        /// <summary>
        /// Назначение 0 линий исходящих из заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //dalk = 0;
        }

        /// <summary>
        /// Очистка от пробных зарядов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            probs = new List<Prob> { };
            paintka();
        }

        /// <summary>
        /// Выбор на перемещение заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = Type.MOVE;
        }

        /// <summary>
        /// Изменение заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                cha = (int.Parse(textBox1.Text));
                if (cha > 16) cha = 16;
                if (cha < -16) cha = -16;
                if (selected >= 0)
                {
                    atoms[selected].charge = cha;
                    atoms[selected].sta = 8 * (int)Math.Sqrt(Math.Abs(cha));
                    atoms[selected].create_at_els();
                    atoms[selected].radius = Math.Abs((int)(atoms[selected].charge)) / 2 + 10;
                    textBox13.Text = string.Format("{0}", atoms[selected].charge);
                    paintka();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Показ инструкции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            (new Form2()).Show();
        }

        /// <summary>
        /// Выбор нейтрального заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nullChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = -1;
            type = Type.NEUTRAL;
            paintka();
        }

        /// <summary>
        /// Удаление атома
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (selected >= 0)
            {
                atoms.RemoveAt(selected);
            }
            selected = -1;
            paintka();
        }

        /// <summary>
        /// Создание атома по координатам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            probs = new List<Prob> { };
            selected = -1;
            try
            {
                int x = int.Parse(textBox2.Text);
                int y = 750 - int.Parse(textBox3.Text);

                create_atom(x, y, cha);

                label4.Text = "";
                paintka();
            }
            catch
            {
                label4.Text = "Error";
            }
        }

        /// <summary>
        /// Рисовать/не рисовать коор. сетку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            coor = checkBox1.Checked;
            paintka();
        }

        /// <summary>
        /// Изменение/задание координаты по Х
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (selected >= 0)
            {
                try
                {
                    atoms[selected].x = int.Parse(textBox2.Text);
                    atoms[selected].create_at_els();
                    label4.Text = "";
                }
                catch
                {
                    label4.Text = "Error";
                }
                textBox11.Text = string.Format("{0}", atoms[selected].x);
                textBox10.Text = string.Format("{0}", 750 - atoms[selected].y);
                textBox13.Text = string.Format("{0}", atoms[selected].charge);
            }
            paintka();
        }

        /// <summary>
        /// Изменение/задание координаты по У
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (selected >= 0)
            {
                try
                {
                    atoms[selected].y = 750 - int.Parse(textBox3.Text);
                    atoms[selected].create_at_els();
                    label4.Text = "";
                }
                catch
                {
                    label4.Text = "Error";
                }
                textBox11.Text = string.Format("{0}", atoms[selected].x);
                textBox10.Text = string.Format("{0}", 750 - atoms[selected].y);
                textBox13.Text = string.Format("{0}", atoms[selected].charge);
            }
            paintka();
        }

        /// <summary>
        /// Снятие выделения заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            selected = -1;
            paintka();
        }

        /// <summary>
        /// Показ направления СЛ  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            arrows = checkBox2.Checked;
            paintka();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            width = 1;
            paintka();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            width = 2;
            paintka();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            width = 3;
            paintka();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            width = 4;
            paintka();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            width = 5;
            paintka();
        }

        /// <summary>
        /// Уредчение кол-ва стрелок СЛ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            rare = checkBox3.Checked;
            if (!arrows && rare)
            {
                arrows = true;
                checkBox2.Checked = true;
            }
            paintka();
        }

        /// <summary>
        /// Включить темную тему
        /// </summary>
        private void go_black()
        {
            /*
            this.BackColor = Control.DefaultForeColor;

            this.menuStrip1.BackColor = Control.DefaultForeColor;
            this.menuStrip1.ForeColor = Color.White;

            this.label1.ForeColor = Color.White;
            this.label2.ForeColor = Color.White;
            //this.label3.ForeColor = Color.White;
            this.label4.ForeColor = Color.White;
            this.label5.ForeColor = Color.White;
            this.label6.ForeColor = Color.White;
            this.label7.ForeColor = Color.White;
            this.label8.ForeColor = Color.White;

            this.checkBox1.ForeColor = Color.White;
            this.checkBox2.ForeColor = Color.White;
            this.checkBox3.ForeColor = Color.White;
            this.checkBox4.ForeColor = Color.White;

            this.button1.BackColor = Control.DefaultForeColor;
            this.button1.ForeColor = Color.White;
            this.button3.BackColor = Control.DefaultForeColor;
            this.button3.ForeColor = Color.White;
            this.button5.BackColor = Control.DefaultForeColor;
            this.button5.ForeColor = Color.White;
            this.button6.BackColor = Control.DefaultForeColor;
            this.button6.ForeColor = Color.White;
            this.button7.BackColor = Control.DefaultForeColor;
            this.button7.ForeColor = Color.White;

            this.blackThemeToolStripMenuItem.Text = "White theme";*/
        }

        /// <summary>
        /// Включить светлую тему
        /// </summary>
        private void go_white()
        {
            /*
            this.BackColor = Control.DefaultBackColor;

            this.menuStrip1.BackColor = Control.DefaultBackColor;
            this.menuStrip1.ForeColor = Control.DefaultForeColor;

            this.label1.ForeColor = Control.DefaultForeColor;
            this.label2.ForeColor = Control.DefaultForeColor;
            this.label3.ForeColor = Control.DefaultForeColor;
            this.label4.ForeColor = Control.DefaultForeColor;
            this.label5.ForeColor = Control.DefaultForeColor;
            this.label6.ForeColor = Control.DefaultForeColor;
            this.label7.ForeColor = Control.DefaultForeColor;
            this.label8.ForeColor = Control.DefaultForeColor;

            this.checkBox1.ForeColor = Control.DefaultForeColor;
            this.checkBox2.ForeColor = Control.DefaultForeColor;
            this.checkBox3.ForeColor = Control.DefaultForeColor;
            this.checkBox4.ForeColor = Control.DefaultForeColor;

            this.button1.BackColor = Control.DefaultBackColor;
            this.button1.ForeColor = Control.DefaultForeColor;
            this.button3.BackColor = Control.DefaultBackColor;
            this.button3.ForeColor = Control.DefaultForeColor;
            this.button5.BackColor = Control.DefaultBackColor;
            this.button5.ForeColor = Control.DefaultForeColor;
            this.button6.BackColor = Control.DefaultBackColor;
            this.button6.ForeColor = Control.DefaultForeColor;
            this.button7.BackColor = Control.DefaultBackColor;
            this.button7.ForeColor = Control.DefaultForeColor;

            this.blackThemeToolStripMenuItem.Text = "Dark theme";*/
        }

        /// <summary>
        /// Включить/выключить темную тему
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blackThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (black)
            {
                go_white();
            }
            else
            {
                go_black();
            }
            black = !black;

            paintka();
        }

        /// <summary>
        /// Показ/не показ "напряженности" (не используется)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            tent = checkBox4.Checked;
            paintka();
        }

        /// <summary>
        /// (не используется)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Сохранение в файл по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("save.txt");

            writer.WriteLine("Atoms: \n" + atoms.Count);
            foreach (Atom point in atoms)
            {
                writer.WriteLine(" " + point.x + " " + point.y + " " + point.charge);
            }

            writer.WriteLine("Test: \n" + probs.Count);
            foreach (Prob point in probs)
            {
                writer.WriteLine(point.x + " " + point.y);
            }

            writer.Close();
        }

        /// <summary>
        /// Загрузка из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                clear();
                StreamReader reader = new StreamReader(openFileDialog1.FileName);

                reader.ReadLine();

                string str = reader.ReadLine();
                String[] words = str.Split(new char[] { ' ' });
                int kolvo = int.Parse(words[0]);
                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });
                    Point en = new Point(int.Parse(words[1]), int.Parse(words[2]));
                    create_atom(en.X, en.Y, int.Parse(words[3]));
                }
                str = reader.ReadLine();
                str = reader.ReadLine();
                words = str.Split(new char[] { ' ' });
                kolvo = int.Parse(words[0]);
                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });

                    probs.Add(new Prob(int.Parse(words[0]), int.Parse(words[1]), true));

                }
                paintka();
                reader.Close();
            }
        }

        /// <summary>
        /// Загрузка из файла по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("save.txt"))
            {
                clear();
                StreamReader reader = new StreamReader("save.txt");

                reader.ReadLine();

                string str = reader.ReadLine();
                String[] words = str.Split(new char[] { ' ' });
                int kolvo = int.Parse(words[0]);
                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });
                    Point en = new Point(int.Parse(words[1]), int.Parse(words[2]));
                    create_atom(en.X, en.Y, int.Parse(words[3]));

                }
                str = reader.ReadLine();
                str = reader.ReadLine();
                words = str.Split(new char[] { ' ' });
                kolvo = int.Parse(words[0]);
                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });

                    probs.Add(new Prob(int.Parse(words[0]), int.Parse(words[1]), true));

                }
                paintka();
                reader.Close();
            }
        }

        /// <summary>
        /// Сохранение в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);

            writer.WriteLine("Atoms: \n" + atoms.Count);
            foreach (Atom point in atoms)
            {
                writer.WriteLine(point.x + " " + point.y + " " + point.charge);
            }

            writer.WriteLine("Test: \n" + probs.Count);
            foreach (Prob point in probs)
            {
                writer.WriteLine(point.x + " " + point.y);
            }

            writer.Close();
        }//Сохранить как

        /// <summary>
        /// Вывод точки нулевой напряженности поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            E_0 = checkBox5.Checked;
            paintka();
        }

        private void clearFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Atom at in atoms)
            {
                at.sta_koeff = 1;
                at.create_at_els();
            }
            fill_curr = 1;
            paintka();
        }

        private void deMultiplyLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fill_curr > 1)
            {
                foreach (Atom at in atoms)
                {
                    at.sta_koeff /= 2;
                    at.create_at_els();
                }
                fill_curr /= 2;
                paintka();
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }

    /// <summary>
    /// Окно с инструкциями
    /// </summary>
    public partial class Form2 : Form
    {
        private TextBox textBox1;

        /// <summary>
        /// Инициализация формы
        /// </summary>
        public Form2()
        {
            this.Icon = Properties.Resources.spider2;
            InitializeComponent();
        }

        /// <summary>
        /// Инициализация компонентов формы
        /// </summary>
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
            this.textBox1.Enabled = true;
            this.textBox1.ReadOnly = true;
            this.textBox1.Text =
                "Эта программа позволяет симулировать силовые линии в электростатическом поле "
                + Environment.NewLine + "Значения кнопок:"
                + Environment.NewLine + "Создать заряд - выбор создаваемого элемента или его настройки:"
                + Environment.NewLine + "\tПоложительный - создает положительный заряд (модуль указан в поле Charge), при зажатии ЛКМ можно перемещать по полю до постановки. Заряд по умолчанию +1"
                + Environment.NewLine + "\tОтрицательный - создает отрицательный заряд (модуль указан в поле Charge), при зажатии ЛКМ можно перемещать по полю до постановки. Заряд по умолчанию -1"
                + Environment.NewLine + "\tНейтральный - создает незаряженное тело, при зажатии ЛКМ можно перемещать по полю до постановки"
                + Environment.NewLine + "\tПробный - создает элементарный отрицательный заряд, при зажатии ЛКМ можно перемещать по полю до постановки, также показывает траекторию его полета"
                + Environment.NewLine + "\tИзменить - при клике на заряд он выделяется. Выделенный заряд можно перетаскивать по полю и изменять его координаты"
                + Environment.NewLine + "Очистка поля - очищает все поле"
                + Environment.NewLine + "Увеличение кол-ва линий - увеличивает кол-во линий, исходящих из заряда"
                + Environment.NewLine + "Толщина линий - изменяет толщину силовых линий"
                //+ Environment.NewLine + "Dark theme - меняет цвет окна и элементов"
                + Environment.NewLine + "Удалить тестовый заряд - очищает поле от тестогого заряда, если таковой существует"
                + Environment.NewLine + "Удалить выбранный заряд - удаляет выбранный заряд, если таковой существует"
                + Environment.NewLine + "Создать заряд - создает заряд с заданными зарядом и координатами"
                + Environment.NewLine + "Координатная сетка - показывать/не показывать координатную сетку"
                + Environment.NewLine + "Напряженность поля - показывать/не показывать напряженность поля"
                + Environment.NewLine + "Снять выделение - снимает выделение с заряда"
                + Environment.NewLine + "Стрелки - добавляет стрелки магнитным линиям"
                + Environment.NewLine + "Редкие стрелки - добавляет редкие стрелки магнитным линиям"
                + Environment.NewLine + "\nСоздатель:"
                + Environment.NewLine + "Ученик 10-4 класса Президентского ФМЛ №239"
                + Environment.NewLine + "Улановский Георгий"
                + Environment.NewLine + "\nНаучный руководитель:"
                + Environment.NewLine + "Учитель физики Президентского ФМЛ №239"
                + Environment.NewLine + "Гурьянов Иван Анатольевич"
                ;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1024, 512);
            this.Controls.Add(this.textBox1);
            this.Text = "Инструкция";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

    /// <summary>
    /// Класс для обьявления зарядов на поле
    /// </summary>
    public class Atom
    {
        /// <summary>
        /// Коеффицент заполнения
        /// </summary>
        public int sta_koeff { get; set; } = 1;

        /// <summary>
        /// Координата по Х
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// Координата по Y
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// Модуль заряда атома
        /// </summary>
        public int charge { get; set; } = 0;

        /// <summary>
        /// Массив, содержащий привязанные к атому 16 электронов
        /// </summary>
        public List<El_ch> electrons { get; set; } = new List<El_ch> { };

        /// <summary>
        /// Кол-во электронов, привязанных к атому
        /// </summary>
        public int sta { get; set; } = 8;

        /// <summary>
        /// Радиус атома
        /// </summary>
        public int radius { get; set; } = 10;

        const int k = 9000000;

        /// <summary>
        /// Инициализация пустого атома
        /// </summary>
        public Atom()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Инициализация заряда с параметрами координат и заряда
        /// </summary>
        /// <param name="x">Координата по Х</param>
        /// <param name="y">Координата по Y</param>
        /// <param name="charge">Модуль заряда</param>
        public Atom(int _x, int _y, int _charge, int _sta_koeff)
        {
            this.x = _x;
            this.y = _y;
            this.charge = _charge;
            this.sta_koeff = _sta_koeff;
            this.sta = 8 * (int)Math.Sqrt(Math.Abs(_charge));
            radius = Math.Abs(this.charge) / 2 + 10;
            create_at_els();
        }

        /// <summary>
        /// Расчет вектора силы, действующего на данный пробный заряд от данного заряда
        /// </summary>
        /// <param name="el">Обьект пробного заряда</param>
        /// <returns>Итоговый вектор силы</returns>
        public Vector forcer(El_ch el)
        {
            double rad = Math.Sqrt((el.x - x) * (el.x - x) + (el.y - y) * (el.y - y));
            Vector once = new Vector((double)(el.x - x) / rad, (double)(el.y - y) / rad);
            double force = k * el.charge * charge / (rad * rad);
            return new Vector(once.x * force, once.y * force);
        }

        /// <summary>
        /// Добавление электронов атому
        /// </summary>
        public void create_at_els()
        {
            this.electrons = new List<El_ch> { };
            double pi = 2 * Math.PI / (sta*sta_koeff);
            List<double> coeff = new List<double> { };
            for (int i = 0; i < (sta * sta_koeff); i++)
            {
                coeff.Add(Math.Cos(pi * i));
                coeff.Add(Math.Sin(pi * i));
            }
            for (int i = 0; i < (sta * sta_koeff); i++)
            {
                El_ch ela = new El_ch(this.x + 10 * coeff[2 * i], this.y + 10 * coeff[2 * i + 1], false);
                this.electrons.Add(ela);
            }
        }
    }

    /// <summary>
    /// Класс описывающий электрон на поле
    /// </summary>
    public class El_ch
    {
        /// <summary>
        /// Координата по Х
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Координата по Y
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// Модуль заряда електрона. По умолчанию 1
        /// </summary>
        public double charge { get; set; } = 1;

        /// <summary>
        /// Инициализация пустого электрона
        /// </summary>
        public El_ch()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Инициализация електрона с параметрами координат и знаком заряда
        /// </summary>
        /// <param name="x">Координата по Х</param>
        /// <param name="y">Координата по Y</param>
        /// <param name="plus">Знак заряда</param>
        public El_ch(double x, double y, bool plus)
        {
            this.x = x;
            this.y = y;
            if (plus) charge = -charge;
        }
    }

    /// <summary>
    /// Вектор (имеет только длину проекций на оси X и Y)
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// Координата по Х
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Координата по Y
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// Инициализация нуль-вектора
        /// </summary>
        public Vector()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Инициализация вектора с заданной длиной вдоль осей X и Y
        /// </summary>
        /// <param name="x">Длина вдоль (или против) оси Х</param>
        /// <param name="y">Длина вдоль (или против) оси Y</param>
        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Вывод значений длины вектора вдоль осей
        /// </summary>
        /// <returns>Строка с 2-мя занчениями - длина вдоль осей Х и Y</returns>
        public override string ToString()
        {
            return String.Format("{0:0.000000000000}, {1:0.000000000000}", x, y);
        }

        /// <summary>
        /// Расчет векторной суммы
        /// </summary>
        /// <param name="a">Первый вектор</param>
        /// <param name="b">Второй вектор</param>
        /// <returns>Итоговый вектор</returns>
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

        public double GetLong()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }

    /// <summary>
    /// Класс описывающий пробный електрон на поле
    /// </summary>
    public class Prob
    {
        /// <summary>
        /// Координата по Х
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Координата по Y
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// Модуль заряда електрона. По умолчанию -1.
        /// При создании меняется на +1.
        /// </summary>
        public double charge { get; set; } = -1;

        /// <summary>
        /// Инициализация пустого пробного электрона
        /// </summary>
        public Prob()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Инициализация електрона с параметрами координат и знаком заряда
        /// </summary>
        /// <param name="x">Координата по Х</param>
        /// <param name="y">Координата по Y</param>
        /// <param name="plus">Знак заряда</param>
        public Prob(double x, double y, bool plus)
        {
            this.x = x;
            this.y = y;
            if (plus) charge = -charge;
        }
    }
}