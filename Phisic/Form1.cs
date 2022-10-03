using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing.Drawing2D;

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

        public delegate void MyDelegate(Atom at);

        //Флаги

        /// <summary>
        /// Флаг, отвечающий за отрисовку "напряженности" поля
        /// </summary>
        bool isNeedToCountTension = false;

        /// <summary>
        /// Флаг, отвечающий за отрисовку точки "нулевой напряженности" поля
        /// </summary>
        bool isCountingZeroTension = false;

        /// <summary>
        /// Флаг, отвечающий за темную тему
        /// </summary>
        bool black = false;

        /// <summary>
        /// Флаг, отвечающий за рисовку стрелок направлений СЛ
        /// </summary>
        static bool isDrawingArrows = false;

        /// <summary>
        /// Флаг, отвечающий за уменьшение кол-ва стрелок на линии
        /// </summary>
        static bool areArrowsRare = false;

        /// <summary>
        /// Флаг, отвечающий за отрисовку точек исхода СЛ
        /// </summary>
        bool drawer = false;

        /// <summary>
        /// Флаг, отвечающий за рисовку координатной сетки
        /// </summary>
        bool isDrawingCoordinateGrid = false;

        /// <summary>
        /// Флаг, отвечающий за нажатие кнопки мыши
        /// </summary>
        bool isMouseDowned = false;

        //Целочисленные переменные

        /// <summary>
        /// Толщина линий
        /// </summary>
        static int width = 1;

        /// <summary>
        /// Номер выбранного атома
        /// </summary>
        int selectedAtomIndex = -1;

        /// <summary>
        /// Размер поля для функции заполнения линиями
        /// </summary>
        int linesMultiplier = 1;

        /// <summary>
        /// Тип заряда (+,-,0,пробный)
        /// </summary>
        Type currentTypeOfFunction = Type.Positive;
        enum Type
        {
            Positive,
            Negative,
            Neutral,
            Trial,
            Moving,
            TensityVector
        }

        /// <summary>
        /// Изначальный заряд
        /// </summary>
        int currentCharge = 1;

        //Списки

        /// <summary>
        /// Список зарядов
        /// </summary>
        static List<Atom> atoms = new List<Atom> { };

        /// <summary>
        /// Список пробных зарядов
        /// </summary>
        List<Trial> trialAtoms = new List<Trial> { };

        //Другие значения

        /// <summary>
        /// Переменная для вызова событий графики
        /// </summary>
        static private Bitmap _bitmap = new Bitmap(1920, 1080);
        static private readonly Graphics _graphics = Graphics.FromImage(_bitmap);

        PointF tensityTrialVectorCoordinates = new PointF();

        static object locker = new object();
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            icon = this.Icon;
            pictureBox1.Image = _bitmap;
            _graphics.Clear(Color.White);
            RenderPicture();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

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
        /// <param name="iterationsNumber">Кол-во итераций в цикле</param>
        /// <param name="inaccuracy">Коеффицент погрешности</param>
        /// <param name="isKeepDrawing">Продолжать/нет рисование</param>
        /// <param name="xNow">Текущая координата по Х</param>
        /// <param name="yNow">Текущая координата по Y</param>
        static private void GetToNegativeLine(int iterationsNumber, float inaccuracy, bool isKeepDrawing, float xNow, float yNow, List<Tuple<PointF, PointF, Pen>> line)
        {
            int numberNotArrowed = 0;
            for (int i = 0; i < iterationsNumber; i++)
            {
                if (!isKeepDrawing)
                {
                    Vector2 newPosition = new Vector2();
                    GetNewPosition(ref isKeepDrawing, ref newPosition, xNow, yNow);
                    if (double.IsNaN(newPosition.x) || double.IsNaN(newPosition.y)) break;
                    Vector2 rv = new Vector2(
                        newPosition.x / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y), // единичный вектор направления
                        newPosition.y / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y)
                        );
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) break;

                    AddArrowToLine(true, new PointF(xNow, yNow), new PointF(xNow + (float)(rv.x * inaccuracy), yNow + (float)(rv.y * inaccuracy)), ref numberNotArrowed, line);
                    numberNotArrowed++;
                    xNow += (float)(rv.x * inaccuracy);
                    yNow += (float)(rv.y * inaccuracy);
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Рисование линии в сторону положительного заряда
        /// </summary>
        /// <param name="iterationsNumber">Кол-во итераций в цикле</param>
        /// <param name="inaccuracy">Коеффицент погрешности</param>
        /// <param name="isKeepDrawing">Продолжать/нет рисование</param>
        /// <param name="xNow">Текущая координата по Х</param>
        /// <param name="yNow">Текущая координата по Y</param>
        static private void GetToPositiveLine(int iterationsNumber, float inaccuracy, bool isKeepDrawing, float xNow, float yNow, List<Tuple<PointF, PointF, Pen>> line)
        {
            int numberNotArrowed = 0;
            for (int i = 0; i < iterationsNumber; i++)
            {
                if (!isKeepDrawing)
                {
                    Vector2 newPosition = new Vector2();
                    GetNewPosition(ref isKeepDrawing, ref newPosition, xNow, yNow);
                    if (double.IsNaN(newPosition.x) || double.IsNaN(newPosition.y)) break;
                    Vector2 rv = new Vector2(newPosition.x / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y), newPosition.y / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) break;

                    AddArrowToLine(false, new PointF(xNow, yNow), new PointF(xNow - (float)(rv.x * inaccuracy), yNow - (float)(rv.y * inaccuracy)), ref numberNotArrowed, line);
                    numberNotArrowed++;
                    xNow -= (float)(rv.x * inaccuracy);
                    yNow -= (float)(rv.y * inaccuracy);
                }
                else
                    break;
            }
        }

        // TODO: вынести "линии" в привязке к атому, чтобы отрисовывать их уже позже. ПИЗДЕЦ ПОЗЖЕ.
        static private void AtomPhisicsThread(Object obj)
        {
            Atom at = (Atom)obj;
            int inaccuracy = 1;
            int iterationsNumber = 5000;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (ElementaryCharge el in at.electrons)
            {
                List<Tuple<PointF, PointF, Pen>> firstLine = new List<Tuple<PointF, PointF, Pen>> { };
                List<Tuple<PointF, PointF, Pen>> secondLine = new List<Tuple<PointF, PointF, Pen>> { };

                float xNow = (float)el.x;
                float yNow = (float)el.y;
                bool isKeepDrawing = false;
                GetToNegativeLine(iterationsNumber, inaccuracy, isKeepDrawing, xNow, yNow, firstLine);
                xNow = (float)el.x;
                yNow = (float)el.y;
                isKeepDrawing = false;
                GetToPositiveLine(iterationsNumber, inaccuracy, isKeepDrawing, xNow, yNow, secondLine);

                lock (locker)
                {
                    GraphicsPath path = new GraphicsPath();
                    Pen pen = new Pen(Color.Green, width);

                    path.StartFigure();

                    foreach (var pair in firstLine)
                    {
                        path.AddLine(pair.Item1, pair.Item2);
                    }
                    _graphics.DrawPath(pen, path);
                    path = new GraphicsPath();
                    foreach (var pair in secondLine)
                    {
                        path.AddLine(pair.Item1, pair.Item2);
                    }
                    _graphics.DrawPath(pen, path);
                }
            }
        }

        /// <summary>
        /// Расчет силовых линий
        /// </summary>
        private void CountMainLinesPhisics()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // 300-500
            foreach (Atom atom in atoms)
            {
                AtomPhisicsThread(atom);
            }

            // TODO: поменять отрисовку. возможно быстрее ставить один пиксель

            // 400-500
            //Parallel.ForEach(atoms, item => atomPhisicsThread(item));

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("RunTime for atoms calculations " + String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds));
            DrawAtoms();
        }

        /// <summary>
        /// Расчет нового положения
        /// </summary>
        /// <param name="isKeepDrawing">Продолжать/нет рисование</param>
        /// <param name="newPosition">Вектор новых координат</param>
        /// <param name="xNow">Текущая координата по Х</param>
        /// <param name="yNow">Текущая координата по Y</param>
        static private void GetNewPosition(ref bool isKeepDrawing, ref Vector2 newPosition, float xNow, float yNow)
        {
            foreach (Atom at in atoms)
            {
                newPosition += at.getForceInPosition(new ElementaryCharge(xNow, yNow, false));
                double distanceToAtom = Math.Sqrt((at.x - xNow) * (at.x - xNow) + (at.y - yNow) * (at.y - yNow));
                if (distanceToAtom < 5 && at.charge != 0) isKeepDrawing = true;
            }
            if ((Math.Abs(xNow) >= 1500) || (Math.Abs(yNow) >= 1500)) isKeepDrawing = true;
        }

        /// <summary>
        /// Отрисовка траектории полета пробного заряда (вспомогательная функция)
        /// </summary>
        /// <param name="iterationsNumber">Кол-во итераций в цикле</param>
        /// <param name="inaccuracy">Коеффицент погрешности</param>
        /// <param name="xNow">Текущая координата по Х</param>
        /// <param name="yNow">Текущая координата по Y</param>
        /// <param name="isKeepDrawing">Продолжать/нет рисование</param>
        /// <param name="xPrev">Старая координата по Х</param>
        /// <param name="yPrev">Старая координата по Y</param>
        private void GetTrialLine(int iterationsNumber, float inaccuracy, float xNow, float yNow, bool isKeepDrawing, float xPrev, float yPrev)
        {
            Brush brushBlack = new SolidBrush(Color.Black);
            Brush brushYellow = new SolidBrush(Color.Yellow);

            for (int i = 1; i < iterationsNumber; i++)
            {
                if (!isKeepDrawing)
                {
                    Vector2 newPosition = new Vector2();
                    bool d = false; // just need to work
                    GetNewPosition(ref d, ref newPosition, xNow, yNow);

                    if (double.IsNaN(newPosition.x) || double.IsNaN(newPosition.y)) break;
                    Vector2 rv = new Vector2(newPosition.x / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y), newPosition.y / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) break;

                    float a = xNow, b = yNow;
                    xNow = (float)(2 * xNow - xPrev + rv.x * inaccuracy * inaccuracy);
                    yNow = (float)(2 * yNow - yPrev + rv.y * inaccuracy * inaccuracy);
                    xPrev = a;
                    yPrev = b;

                    _graphics.FillEllipse(brushBlack, new RectangleF(xNow - 2, yNow - 2, 4, 4));
                    _graphics.DrawLine(new Pen(brushYellow), new PointF(xPrev, yPrev), new PointF(xNow, yNow));

                }
                else
                    break;
            }
        }

        /// <summary>
        /// Отрисовка траектории полета пробного заряда
        /// </summary>
        private void CountTrialLinePhisics()
        {
            float inaccuracy = 1;
            int iterarionsNumber = 300;
            Brush brushBlack = new SolidBrush(Color.Black);
            Brush brushYellow = new SolidBrush(Color.Yellow);

            foreach (Trial el in trialAtoms)
            {
                float xNow = (float)el.x;
                float yNow = (float)el.y;
                float xPrev = xNow;
                float yPrev = yNow;
                bool isKeepDrawing = false;
                if (!isKeepDrawing)
                {
                    Vector2 newPosition = new Vector2();
                    bool d = false;
                    GetNewPosition(ref d, ref newPosition, xNow, yNow);
                    if (double.IsNaN(newPosition.x) || double.IsNaN(newPosition.y)) continue;
                    Vector2 rv = new Vector2(newPosition.x / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y), newPosition.y / Math.Sqrt(newPosition.x * newPosition.x + newPosition.y * newPosition.y));
                    if (double.IsNaN(rv.x) || double.IsNaN(rv.y)) continue;

                    xNow += (float)(rv.x * inaccuracy * inaccuracy);
                    yNow += (float)(rv.y * inaccuracy * inaccuracy);
                    _graphics.FillEllipse(brushBlack, new RectangleF(xNow - 2, yNow - 2, 4, 4));
                    _graphics.DrawLine(new Pen(brushYellow), new PointF(xPrev, yPrev), new PointF(xNow, yNow));
                }
                GetTrialLine(iterarionsNumber, inaccuracy, xNow, yNow, isKeepDrawing, xPrev, yPrev);
            }
        }

        /// <summary>
        /// Расчет напряженности поля
        /// </summary>
        private void CountFieldTension()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            double logariphmDegree = 1.043;
            if (isNeedToCountTension)
            {
                Color currentColor;
                for (int x = 0; x < 800; x += 1)
                {
                    for (int y = 0; y < 800; y += 1)
                    {
                        ElementaryCharge el = new ElementaryCharge(x, y, false);

                        Vector2 newPosition = new Vector2();
                        // TODO: засечь время здесь и посчитать что долгое
                        foreach (Atom at in atoms)
                        {
                            newPosition += at.getForceInPosition(el);
                            //double rad = Math.Sqrt((at.x - x_now) * (at.x - x_now) + (at.y - y_now) * (at.y - y_now));
                        }
                        double rad = newPosition.GetLong() * 1000;
                        if (!double.IsNaN(rad) && !double.IsInfinity(rad))
                        {
                            double lg = Math.Log(rad, logariphmDegree);
                            if (lg >= 255)
                            {
                                currentColor = Color.FromArgb(255, 255, 255);
                            }
                            else if (lg <= 0)
                            {
                                currentColor = Color.FromArgb((int)rad, (int)rad, (int)rad);
                            }
                            else
                            {
                                currentColor = Color.FromArgb((int)lg, (int)lg, (int)lg);
                            }
                            //_graphics.FillRectangle(new SolidBrush(nowColor), new Rectangle(i, j, 1, 1));

                            _bitmap.SetPixel(x, y, currentColor);
                        }
                    }
                }

            }

            pictureBox1.Image = _bitmap;

            DrawAtoms();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("RunTime napr " + String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds));
        }

        /// <summary>
        /// Расчет точки нулевой напряженности поля
        /// </summary>
        private void CountZeroTension()
        {
            if (isCountingZeroTension)
            {
                int atomsChargeState = 0;
                foreach (Atom atom in atoms)
                {
                    if (atom.charge < 0)
                    {
                        atomsChargeState += 0;
                    }
                    else if (atom.charge > 0)
                    {
                        atomsChargeState += 1;
                    }
                    else
                    {
                        atomsChargeState += 2;
                    }
                }

                // Eix = (x-a) * Q / ((x-a)^2+(y-b)^2)^2
                // Eiy = (y-b) * Q / ((x-a)^2+(y-b)^2)^2
                // if (Ex^2+Ey^2 < 1e-6) : cool
                Functions2 f = new Functions2();
                if (atomsChargeState == 0 || atomsChargeState == atoms.Count)
                {
                    if (atoms.Count == 2)
                    {
                        double squareLeftUpX = Math.Min(atoms[0].x, atoms[1].x), squareLeftUpY = Math.Min(atoms[0].y, atoms[1].y);
                        double squareRightDownX = Math.Max(atoms[0].x, atoms[1].x), squareRightDownY = Math.Max(atoms[0].y, atoms[1].y);
                        double e_x = -10, e_y = -10;
                        int e_0_rad = 10;
                        Line2 line2 = new Line2(atoms[0].x, atoms[0].y, atoms[1].x, atoms[1].y);
                        for (e_x = squareLeftUpX; e_x < squareRightDownX; e_x += 0.1)
                        {
                            for (e_y = squareLeftUpY; e_y < squareRightDownY; e_y += 0.1)
                            {
                                Point2 currentPoint = new Point2(e_x, e_y);
                                if (f.DisplacementDotLine(line2, currentPoint) < 5)
                                {
                                    ElementaryCharge el_Ch = new ElementaryCharge(e_x, e_y, false);
                                    Vector2 summaryForceVector = new Vector2(0, 0);
                                    foreach (Atom at in atoms)
                                    {
                                        summaryForceVector += at.getForceInPosition(el_Ch);
                                    }
                                    if (Math.Abs(summaryForceVector.GetLong()) <= 1e-9)
                                    {
                                        if (atoms.Count > 1)
                                        {
                                            Brush brush = new SolidBrush(Color.Purple);
                                            _graphics.FillEllipse(brush, new Rectangle((int)e_x - e_0_rad / 2, (int)e_y - e_0_rad / 2, e_0_rad, e_0_rad));
                                        }
                                        Console.WriteLine(e_x + " " + e_y + " " + summaryForceVector);
                                    }
                                }
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
        static private void ChooseArrowDirection(bool tr, ref Pen pen)
        {
            if (tr)
                pen.EndCap = LineCap.ArrowAnchor;
            else
                pen.StartCap = LineCap.ArrowAnchor;
        }

        /// <summary>
        /// Рисование линий и стрелок на них
        /// </summary>
        /// <param name="isStartArrow">На конце или на начале</param>
        /// <param name="startPoint">Точка начала</param>
        /// <param name="endPoint">Точка конца</param>
        static private void AddArrowToLine(bool isStartArrow, PointF startPoint, PointF endPoint, ref int numberNotArrowed, List<Tuple<PointF, PointF, Pen>> resultLine)
        {
            Pen pen = new Pen(Color.Green, width);
            if (isDrawingArrows)
            {
                if (areArrowsRare && numberNotArrowed == 75)
                {
                    ChooseArrowDirection(isStartArrow, ref pen);
                    numberNotArrowed = 1;
                }
                else if (!areArrowsRare && numberNotArrowed % 15 == 0)
                {
                    ChooseArrowDirection(isStartArrow, ref pen);
                    numberNotArrowed = 1;
                }
            }
            resultLine.Add(Tuple.Create(startPoint, endPoint, pen));
            //_graphics.DrawLine(pen, p1, p2);

        }

        /// <summary>
        /// Отрисовка всех атомов
        /// </summary>
        static private void DrawAtoms()
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
        private void DrawElementaryCharges()
        {
            if (drawer)
            {
                Brush brush_b = new SolidBrush(Color.Blue);
                foreach (Atom at in atoms)
                {
                    foreach (ElementaryCharge el in at.electrons)
                        _graphics.FillEllipse(brush_b, new Rectangle((int)(el.x - 2), (int)(el.y - 2), 4, 4));
                }
            }
        }

        /// <summary>
        /// Отрисовка координатной сетки
        /// </summary>
        private void DrawCoordinateGrid()
        {
            Brush brushBlue = new SolidBrush(Color.Black);
            if (isDrawingCoordinateGrid)
            {
                _graphics.FillRectangle(brushBlue, new Rectangle(0, 748, 750, 2));
                _graphics.FillRectangle(brushBlue, new Rectangle(0, 0, 2, 750));
                for (int i = 0; i < 750; i += 50)
                {
                    for (int j = 0; j < 750; j += 50)
                    {
                        _graphics.FillRectangle(brushBlue, new Rectangle(i - 1, j - 1, 2, 2));
                        _graphics.FillRectangle(brushBlue, new Rectangle(i - 1, j - 1, 2, 2));
                    }
                    _graphics.FillRectangle(brushBlue, new Rectangle(i - 1, 745, 2, 5));
                    if (i == 50)
                    {
                        _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brushBlue, new Point(i - 9, 732));
                    }
                    else _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brushBlue, new Point(i - 13, 732));
                }
                for (int i = 700; i > 0; i -= 50)
                {
                    _graphics.FillRectangle(brushBlue, new Rectangle(0, i - 1, 5, 2));
                    if (i == 700)
                    {
                        _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brushBlue, new Point(4, (750 - i) - 6));
                    }
                    else _graphics.DrawString(string.Format("{0}", i), new Font(this.Font, FontStyle.Bold), brushBlue, new Point(4, (750 - i) - 6));

                }
                _graphics.DrawString(string.Format("x"), new Font(this.Font, FontStyle.Bold), brushBlue, new Point(737, 732));
                _graphics.DrawString(string.Format("y"), new Font(this.Font, FontStyle.Bold), brushBlue, new Point(6, 4));
                _graphics.DrawLine(new Pen(brushBlue), new Point(0, 750), new Point(5, 745));
                _graphics.DrawLine(new Pen(brushBlue), new Point(1, 750), new Point(6, 745));
                _graphics.DrawLine(new Pen(brushBlue), new Point(2, 750), new Point(7, 745));
                _graphics.DrawLine(new Pen(brushBlue), new Point(0, 749), new Point(5, 744));
                _graphics.DrawLine(new Pen(brushBlue), new Point(0, 748), new Point(5, 743));
                _graphics.DrawString(string.Format("0"), new Font(this.Font, FontStyle.Bold), brushBlue, new Point(4, 732));
            }
        }

        /// <summary>
        /// Перерисовка поля
        /// </summary>
        private void RenderPicture()
        {
            _graphics.Clear(Color.White);

            DrawAtoms();
            DrawElementaryCharges();
            DrawCoordinateGrid();
            if (selectedAtomIndex >= 0)
            {
                _graphics.DrawEllipse(new Pen(Color.Purple, 2), new Rectangle(atoms[selectedAtomIndex].x - atoms[selectedAtomIndex].radius / 2 - 2, atoms[selectedAtomIndex].y - atoms[selectedAtomIndex].radius / 2 - 2, atoms[selectedAtomIndex].radius + 4, atoms[selectedAtomIndex].radius + 4));
            }

            if (isNeedToCountTension)
                CountFieldTension();
            else
            {
                CountMainLinesPhisics();
                CountTrialLinePhisics();
            }
            CountZeroTension();
            if (currentTypeOfFunction == Type.TensityVector)
                DrawTensityVector(tensityTrialVectorCoordinates);


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Refresh();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("RunTime for refresh " + String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds));
        }

        /// <summary>
        /// Создание атома с параметрами координат и заряда
        /// </summary>
        /// <param name="x">Координата по Х</param>
        /// <param name="y">Координата по Y</param>
        /// <param name="atomCharge">Модуль заряда</param>
        private void CreateAtom(int x, int y, int atomCharge)
        {
            Atom at = new Atom(x, y, atomCharge, linesMultiplier);
            atoms.Add(at);
        }

        /// <summary>
        /// Очистка поля
        /// </summary>
        private void ClearField()
        {
            currentTypeOfFunction = Type.Positive;
            atoms = new List<Atom> { };
            trialAtoms = new List<Trial> { };
            selectedAtomIndex = -1;
            RenderPicture();
        }

        /// <summary>
        /// Выбор отрицательного заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedAtomIndex = -1;
            RenderPicture();
            currentTypeOfFunction = Type.Negative;
        }

        /// <summary>
        /// Выбор положительного заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedAtomIndex = -1;
            currentTypeOfFunction = Type.Positive;
            RenderPicture();
        }

        /// <summary>
        /// Выбор пробного заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedAtomIndex = -1;
            currentTypeOfFunction = Type.Trial;
            RenderPicture();
        }

        /// <summary>
        /// Обработка нажатия кнопки Clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        /// <summary>
        /// Клик мышью(она только нажата)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            trialAtoms = new List<Trial> { };
            isMouseDowned = true;
            selectedAtomIndex = -1;
            switch (currentTypeOfFunction)
            {
                case Type.Positive:
                    isMouseDowned = true;
                    CreateAtom(e.X, e.Y, Math.Abs(currentCharge));
                    break;
                case Type.Negative:
                    isMouseDowned = true;
                    CreateAtom(e.X, e.Y, -Math.Abs(currentCharge));
                    break;
                case Type.Trial:
                    Trial el = new Trial(e.X, e.Y, false);
                    trialAtoms.Add(el);
                    break;
                case Type.Moving:
                    foreach (Atom atom in atoms)
                    {
                        double rad = Math.Sqrt((atom.x - e.X) * (atom.x - e.X) + (atom.y - e.Y) * (atom.y - e.Y));
                        if (rad < 10)
                        {
                            selectedAtomIndex = atoms.IndexOf(atom);
                            XCoordinateOutput.Text = string.Format("{0}", atom.x);
                            YCoordinateOutput.Text = string.Format("{0}", 750 - atom.y);
                            ChargeModuleOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].charge);
                        }
                    }
                    break;
                case Type.Neutral:
                    isMouseDowned = true;
                    CreateAtom(e.X, e.Y, 0);
                    break;
                case Type.TensityVector:
                    isMouseDowned = true;
                    tensityTrialVectorCoordinates = new PointF(e.X, e.Y);
                    break;
                default: break;
            }
            RenderPicture();
        }

        private void DrawTensityVector(PointF e)
        {
            ElementaryCharge elemCharge = new ElementaryCharge(e.X, e.Y, false);
            Vector2 summaryForce = new Vector2(0, 0);
            foreach (Atom at in atoms)
            {
                summaryForce += at.getForceInPosition(elemCharge);
            }
            TensityOutput.Text = string.Format("{0}", summaryForce.GetLong());
            summaryForce *= 100;
            Pen pen = new Pen(Color.Purple, 3);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            PointF p1 = new PointF(e.X, e.Y);
            PointF p2 = new PointF(e.X + (float)summaryForce.x, e.Y + (float)summaryForce.y);
            //Console.WriteLine(p1 + " " + p2);
            _graphics.DrawLine(pen, p1, p2);
        }

        /// <summary>
        /// Смена положения атома и его електронов
        /// </summary>
        /// <param name="e"></param>
        private void changeAtomPosition(MouseEventArgs e)
        {
            atoms[atoms.Count - 1].x = e.X;
            atoms[atoms.Count - 1].y = e.Y;
            atoms[atoms.Count - 1].createAtomElemCharges();
        }

        /// <summary>
        /// Смена положения выделенного атома и его електронов
        /// </summary>
        /// <param name="e"></param>
        private void changeSelectedAtomPosition(MouseEventArgs e, int selectedAtomIndex)
        {
            atoms[selectedAtomIndex].x = e.X;
            atoms[selectedAtomIndex].y = e.Y;

            atoms[selectedAtomIndex].createAtomElemCharges();
        }

        /// <summary>
        /// Клик мышью(она зажата)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDowned)
            {
                switch (currentTypeOfFunction)
                {
                    case Type.Positive:
                        changeAtomPosition(e);
                        break;
                    case Type.Negative:
                        changeAtomPosition(e);
                        break;
                    case Type.Trial:
                        trialAtoms[trialAtoms.Count - 1] = new Trial(e.X, e.Y, false);
                        break;
                    case Type.Moving:
                        selectedAtomIndex = -1;
                        foreach (Atom atom in atoms)
                        {
                            double rad = Math.Sqrt((atom.x - e.X) * (atom.x - e.X) + (atom.y - e.Y) * (atom.y - e.Y));
                            if (rad < 10)
                                selectedAtomIndex = atoms.IndexOf(atom);
                        }
                        if (selectedAtomIndex >= 0)
                        {
                            XCoordinateOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].x);
                            YCoordinateOutput.Text = string.Format("{0}", 750 - atoms[selectedAtomIndex].y);
                            ChargeModuleOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].charge);
                            changeSelectedAtomPosition(e, selectedAtomIndex);
                        }
                        break;
                    case Type.Neutral:
                        changeAtomPosition(e);
                        break;
                    case Type.TensityVector:
                        tensityTrialVectorCoordinates = new PointF(e.X, e.Y);
                        break;
                    default: break;
                }
                RenderPicture();
            }
        }

        /// <summary>
        /// Мышь отпущена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDowned = false;
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
            if (linesMultiplier < 16)
            {
                foreach (Atom at in atoms)
                {
                    at.linesMultiplierCounter *= 2;
                    at.createAtomElemCharges();
                }
                linesMultiplier *= 2;
                RenderPicture();
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
        /// Очистка от пробных зарядов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAtomButton_Click(object sender, EventArgs e)
        {
            trialAtoms = new List<Trial> { };
            RenderPicture();
        }

        /// <summary>
        /// Выбор на перемещение заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTypeOfFunction = Type.Moving;
        }

        /// <summary>
        /// Изменение заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChargeModuleInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                currentCharge = (int.Parse(ChargeModuleInput.Text));
                if (currentCharge > 16) currentCharge = 16;
                if (currentCharge < -16) currentCharge = -16;
                if (selectedAtomIndex >= 0)
                {
                    atoms[selectedAtomIndex].charge = currentCharge;
                    atoms[selectedAtomIndex].elemChargesCount = 8 * (int)Math.Sqrt(Math.Abs(currentCharge));
                    atoms[selectedAtomIndex].createAtomElemCharges();
                    atoms[selectedAtomIndex].radius = Math.Abs((int)(atoms[selectedAtomIndex].charge)) / 2 + 10;
                    ChargeModuleOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].charge);
                    RenderPicture();
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
        private void InstructionButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.isInstructionFromAddaed == false)
            {
                InstructionForm instructionForm = new InstructionForm();
                instructionForm.Show();
            }
        }

        /// <summary>
        /// Выбор нейтрального заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nullChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedAtomIndex = -1;
            currentTypeOfFunction = Type.Neutral;
            RenderPicture();
        }

        /// <summary>
        /// Удаление атома
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSelectedAtomButton_Click(object sender, EventArgs e)
        {
            if (selectedAtomIndex >= 0)
            {
                atoms.RemoveAt(selectedAtomIndex);
            }
            selectedAtomIndex = -1;
            RenderPicture();
        }

        /// <summary>
        /// Создание атома по координатам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateAtomButton_Click(object sender, EventArgs e)
        {
            trialAtoms = new List<Trial> { };
            selectedAtomIndex = -1;
            try
            {
                int x = int.Parse(XCoordinateInput.Text);
                int y = 750 - int.Parse(YCoordinateInput.Text);

                CreateAtom(x, y, currentCharge);

                label4.Text = "";
                RenderPicture();
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
        private void CoordinateGrid_CheckedChanged(object sender, EventArgs e)
        {
            isDrawingCoordinateGrid = CoordinateGrid.Checked;
            RenderPicture();
        }

        /// <summary>
        /// Изменение/задание координаты по Х
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XCoordinateInput_TextChanged(object sender, EventArgs e)
        {
            if (selectedAtomIndex >= 0)
            {
                try
                {
                    atoms[selectedAtomIndex].x = int.Parse(XCoordinateInput.Text);
                    atoms[selectedAtomIndex].createAtomElemCharges();
                    label4.Text = "";
                }
                catch
                {
                    label4.Text = "Error";
                }
                XCoordinateOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].x);
                YCoordinateOutput.Text = string.Format("{0}", 750 - atoms[selectedAtomIndex].y);
                ChargeModuleOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].charge);
            }
            RenderPicture();
        }

        /// <summary>
        /// Изменение/задание координаты по У
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YCoordinateInput_TextChanged(object sender, EventArgs e)
        {
            if (selectedAtomIndex >= 0)
            {
                try
                {
                    atoms[selectedAtomIndex].y = 750 - int.Parse(YCoordinateInput.Text);
                    atoms[selectedAtomIndex].createAtomElemCharges();
                    label4.Text = "";
                }
                catch
                {
                    label4.Text = "Error";
                }
                XCoordinateOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].x);
                YCoordinateOutput.Text = string.Format("{0}", 750 - atoms[selectedAtomIndex].y);
                ChargeModuleOutput.Text = string.Format("{0}", atoms[selectedAtomIndex].charge);
            }
            RenderPicture();
        }

        /// <summary>
        /// Снятие выделения заряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeselectButton_Click(object sender, EventArgs e)
        {
            selectedAtomIndex = -1;
            RenderPicture();
        }

        /// <summary>
        /// Показ направления СЛ  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowArrowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDrawingArrows = ShowArrowsCheckBox.Checked;
            RenderPicture();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            width = 1;
            RenderPicture();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            width = 2;
            RenderPicture();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            width = 3;
            RenderPicture();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            width = 4;
            RenderPicture();
        }

        /// <summary>
        /// Изменение толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            width = 5;
            RenderPicture();
        }

        /// <summary>
        /// Уредчение кол-ва стрелок СЛ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowRareArrowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            areArrowsRare = ShowRareArrowsCheckBox.Checked;
            if (!isDrawingArrows && areArrowsRare)
            {
                isDrawingArrows = true;
                ShowArrowsCheckBox.Checked = true;
            }
            RenderPicture();
        }

        /// <summary>
        /// Показ/не показ "напряженности"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTensityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isNeedToCountTension = ShowTensityCheckBox.Checked;
            RenderPicture();
        }

        // TODO: Исправить формат сохранения на JSON

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

            writer.WriteLine("Test: \n" + trialAtoms.Count);
            foreach (Trial point in trialAtoms)
            {
                writer.WriteLine(point.x + " " + point.y);
            }

            writer.Close();
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
                writer.WriteLine(point.x + " " + point.y + " " + point.charge);
            }

            writer.WriteLine("Test: \n" + trialAtoms.Count);
            foreach (Trial point in trialAtoms)
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
                ClearField();
                StreamReader reader = new StreamReader(openFileDialog1.FileName);

                reader.ReadLine();

                string str = reader.ReadLine();
                String[] words = str.Split(new char[] { ' ' });
                int kolvo = int.Parse(words[0]);
                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });
                    Point en = new Point(int.Parse(words[0]), int.Parse(words[1]));
                    CreateAtom(en.X, en.Y, int.Parse(words[2]));
                    Console.WriteLine(atoms.Count);

                }
                str = reader.ReadLine();
                str = reader.ReadLine();
                words = str.Split(new char[] { ' ' });
                kolvo = int.Parse(words[0]);
                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });

                    trialAtoms.Add(new Trial(int.Parse(words[0]), int.Parse(words[1]), true));

                }
                RenderPicture();
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
                ClearField();
                StreamReader reader = new StreamReader("save.txt");

                reader.ReadLine();

                string str = reader.ReadLine();
                String[] words = str.Split(new char[] { ' ' });
                int kolvo = int.Parse(words[0]);

                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });
                    Point en = new Point(int.Parse(words[0]), int.Parse(words[1]));
                    CreateAtom(en.X, en.Y, int.Parse(words[2]));
                }
                str = reader.ReadLine();
                str = reader.ReadLine();
                words = str.Split(new char[] { ' ' });
                kolvo = int.Parse(words[0]);
                for (int i = 0; i < kolvo; i++)
                {
                    str = reader.ReadLine();
                    words = str.Split(new char[] { ' ' });

                    trialAtoms.Add(new Trial(int.Parse(words[0]), int.Parse(words[1]), true));

                }

                RenderPicture();
                reader.Close();
            }
        }

        /// <summary>
        /// Вывод точки нулевой напряженности поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowE0CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isCountingZeroTension = ShowE0CheckBox.Checked;
            RenderPicture();
        }

        private void clearFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Atom at in atoms)
            {
                at.linesMultiplierCounter = 1;
                at.createAtomElemCharges();
            }
            linesMultiplier = 1;
            RenderPicture();
        }

        private void deMultiplyLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (linesMultiplier > 1)
            {
                foreach (Atom at in atoms)
                {
                    at.linesMultiplierCounter /= 2;
                    at.createAtomElemCharges();
                }
                linesMultiplier /= 2;
                RenderPicture();
            }
        }

        private void TensityVectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTypeOfFunction = Type.TensityVector;
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
        public int linesMultiplierCounter { get; set; } = 1;

        /// <summary>
        /// Координата по Х
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// Координата по Y
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// Модуль заряда атома, в Кулонах
        /// </summary>
        public int charge { get; set; } = 0;

        /// <summary>
        /// Массив, содержащий привязанные к атому электроны
        /// </summary>
        public List<ElementaryCharge> electrons { get; set; } = new List<ElementaryCharge> { };

        /// <summary>
        /// Кол-во электронов, привязанных к атому
        /// </summary>
        public int elemChargesCount { get; set; } = 8;

        /// <summary>
        /// Радиус атома
        /// </summary>
        public int radius { get; set; } = 10;

        /// <summary>
        /// Коеффицент пропорциональности
        /// </summary>
        const long proportionalCoefficent = 9000;// * (long)(1e9);

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
            this.linesMultiplierCounter = _sta_koeff;
            this.elemChargesCount = 8 * (int)Math.Sqrt(Math.Abs(_charge));
            radius = Math.Abs(this.charge) / 2 + 10;
            createAtomElemCharges();
        }

        /// <summary>
        /// Расчет вектора силы, действующего на данный пробный заряд от данного заряда
        /// </summary>
        /// <param name="el">Обьект пробного заряда</param>
        /// <returns>Итоговый вектор силы</returns>
        public Vector2 getForceInPosition(ElementaryCharge el)
        {
            // расстояние до атома
            double distanceToAtom = Math.Sqrt((el.x - x) * (el.x - x) + (el.y - y) * (el.y - y));
            // Единичный вектор напрвления "атом - электрон"
            Vector2 unitForceVector = new Vector2((double)(el.x - x) / distanceToAtom, (double)(el.y - y) / distanceToAtom);
            // Сила: заряд атома * заряд электрона * коеффицент / расстояние^2
            double force = proportionalCoefficent * el.charge * charge / (distanceToAtom * distanceToAtom);

            return new Vector2(unitForceVector.x * force, unitForceVector.y * force);
        }

        /// <summary>
        /// Добавление электронов атому
        /// </summary>
        public void createAtomElemCharges()
        {
            this.electrons = new List<ElementaryCharge> { };
            double pi = 2 * Math.PI / (elemChargesCount * linesMultiplierCounter);
            List<double> triginometryAngleCoefficents = new List<double> { };
            for (int i = 0; i < (elemChargesCount * linesMultiplierCounter); i++)
            {
                triginometryAngleCoefficents.Add(Math.Cos(pi * i));
                triginometryAngleCoefficents.Add(Math.Sin(pi * i));
            }
            for (int i = 0; i < (elemChargesCount * linesMultiplierCounter); i++)
            {
                ElementaryCharge ela = new ElementaryCharge(this.x + 10 * triginometryAngleCoefficents[2 * i], this.y + 10 * triginometryAngleCoefficents[2 * i + 1], false);
                this.electrons.Add(ela);
            }
        }
    }

    /// <summary>
    /// Класс описывающий электрон на поле
    /// </summary>
    public class ElementaryCharge
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
        /// Модуль заряда електрона в кулонах. По умолчанию 1
        /// </summary>
        public double charge { get; set; } = 1;

        /// <summary>
        /// Инициализация пустого электрона
        /// </summary>
        public ElementaryCharge()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Инициализация електрона с параметрами координат и знаком заряда
        /// </summary>
        /// <param name="x">Координата по Х</param>
        /// <param name="y">Координата по Y</param>
        /// <param name="isPositive">Знак заряда</param>
        public ElementaryCharge(double x, double y, bool isPositive)
        {
            this.x = x;
            this.y = y;
            if (isPositive) charge = -charge;
        }
    }

    /// <summary>
    /// Класс описывающий пробный електрон на поле
    /// </summary>
    public class Trial
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
        public Trial()
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
        public Trial(double x, double y, bool plus)
        {
            this.x = x;
            this.y = y;
            if (plus) charge = -charge;
        }
    }

    public class Point2
    {
        public double x, y;

        public Point2()
        {
            x = 0;
            y = 0;
        }

        public Point2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return String.Format("({0:0.000}, {1:0.000})", x, y);
        }
    }
    public class Vector2
    {
        public double x1 { get; set; }

        public double y1 { get; set; }

        public double x2 { get; set; }

        public double y2 { get; set; }

        public double x { get; set; }

        public double y { get; set; }

        public Vector2()
        {
            x1 = 0;
            x2 = 0;
            y1 = 0;
            y2 = 0;
            x = 0;
            y = 0;
        }

        public Vector2(Point2 p1, Point2 p2)
        {
            this.x1 = p1.x;
            this.y1 = p1.y;
            this.x2 = p2.x;
            this.y2 = p2.y;
            x = x2 - x1;
            y = y2 - y1;
        }

        public Vector2(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            x = x2 - x1;
            y = y2 - y1;
        }

        public Vector2(double x, double y)
        {
            x1 = 0;
            x2 = 0;
            x2 = x;
            y2 = y;
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return String.Format("{0:0.000}, {1:0.000}", x, y);
        }

        public double GetLong()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x1, a.y1, a.x2 + b.x, a.y2 + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x1 + b.x, a.y1 + b.y, a.x2, a.y2);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(a.x2, a.y2, a.x1, a.y1);
        }

        public static Vector2 operator *(Vector2 a, int b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static double operator *(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public double Angle2(Vector2 vector, Vector2 vector2)
        {
            return vector * vector2 / (vector.GetLong() * vector2.GetLong());
        }
    }
    public class Line2
    {
        public double a, b, c;

        public Vector2 vector;

        public Line2()
        {
            a = 0;
            b = 0;
            c = 0;
            vector = new Vector2();
        }

        public Line2(Point2 point, Point2 point2)
        {
            a = point.y - point2.y;
            b = point2.x - point.x;
            c = point.x * point2.y - point2.x * point.y;
            vector = new Vector2(point, point2);
        }

        public Line2(double x1, double y1, double x2, double y2)
        {
            a = y1 - y2;
            b = x2 - x1;
            c = x1 * y2 - x2 * y1;
            vector = new Vector2(x1, y1, x2, y2);
        }

        public Line2(double ask, double bsk, double csk)
        {
            a = ask;
            b = bsk;
            c = csk;
            vector = new Vector2(b, -a);
        }

        public double DistanceToPoint(Point2 alpha)
        {
            double res = 0;
            res = Math.Abs(a * alpha.x + b * alpha.y + c) / Math.Sqrt(a * a + b * b);
            return res;
        }

        public bool IsParallel(Line2 alpha)
        {
            if (Math.Abs(alpha.a * b - alpha.b * a) < 0.001)
                return true;
            else
                return false;
        }

        public Line2 ParallelLine(Point2 alpha)
        {
            Line2 ans = null;
            ans = new Line2(a, b, -a * alpha.x - b * alpha.y);
            return ans;
        }

        public override string ToString()
        {
            string answer = "";
            answer = String.Format("{0:0.00}x", a);
            if (b >= 0)
                answer += String.Format(" + {0:0.00}y", b);
            else
                answer += String.Format(" - {0:0.00}y", -b);
            if (c >= 0)
                answer += String.Format(" + {0:0.00}", c);
            else
                answer += String.Format(" - {0:0.00}", -c);
            answer += " = 0";
            return answer;
        }

        public bool Intersection(Line2 Line2, out Point2 Point2)
        {
            Point2 = null;
            bool res = IsParallel(Line2);
            if (res)
                return false;
            else
            {
                Point2 = new Point2();
                Point2.y = (Line2.c * a - c * Line2.a) / (b * Line2.a - Line2.b * a);
                Point2.x = (-c - b * Point2.y) / a;
                return true;
            }
        }

        public Point2 NearPoint(Point2 Point0)
        {
            Point2 Point2 = new Point2();
            Line2 Line2 = new Line2(b, -a, a * Point0.y - b * Point0.x);
            Intersection(Line2, out Point2);
            return Point2;
        }

        public Line2 PerpendicularLine(Point2 Point20)
        {
            return new Line2(b, -a, a * Point20.y - b * Point20.x); ;
        }

        public void Normalize()
        {
            if (c != 0)
            {
                a /= c;
                b /= c;
                c /= c;
            }
            else if (a != 0)
            {
                b /= a;
                a /= a;
            }
            else
                b = 1;
        }
    }
    public class Functions2
    {
        public double DisplacementDotLine(Line2 a, Point2 M)
        {
            return (Math.Abs(a.a * M.x + a.b * M.y + a.c)) / (Math.Sqrt(a.a * a.a + a.b * a.b));
        }
    }
}