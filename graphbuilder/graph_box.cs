using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace graphbuilder;

public enum GridType
{
    Linear,
    Logarithmic,
}

public sealed partial class GraphBox : UserControl
{
    public List<GraphS> GraphList { get; } = [];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridType CurrentGridType { get; set; } = GridType.Linear;
    
    private float minX = -1000f, maxX = 1000f, minY = -1000f, maxY = 1000f;
    
    private bool isDragging = false;
    private Point lastMousePosition;
    public GraphBox()
    {
        InitializeComponent();
        DoubleBuffered = true;
        BackColor = Color.White;
        MouseDown += graphBox_MouseDown;
        MouseMove += graphBox_MouseMove;
        MouseUp += graphBox_MouseUp;
        MouseWheel += graphBox_MouseWheel;
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer, true);
        UpdateStyles();
    }

    private PointF WorldToScreen(PointF point)
    {
        var x = point.X;
        var y = point.Y;
        if (CurrentGridType == GridType.Logarithmic)
        {
            x = (x > 0) ? (float)Math.Log10(x) : -5f;
        }
        
        var effectiveMinX = (CurrentGridType == GridType.Logarithmic && minX > 0) 
            ? (float)Math.Log10(minX) : minX;
        var effectiveMaxX = (CurrentGridType == GridType.Logarithmic && maxX > 0) 
            ? (float)Math.Log10(maxX) : maxX;
        var sx = (x - effectiveMinX) / (effectiveMaxX - effectiveMinX) * Width;
        var sy = Height - (y - minY) / (maxY - minY) * Height;
        return new PointF(sx, sy);
    }
    // Из пикселей экрана в мировые координаты
    private PointF ScreenToWorld(Point screenPoint)
    {
        float effectiveMinX = (CurrentGridType == GridType.Logarithmic && minX > 0) ? (float)Math.Log10(minX) : minX;
        float effectiveMaxX = (CurrentGridType == GridType.Logarithmic && maxX > 0) ? (float)Math.Log10(maxX) : maxX;

        float wx = effectiveMinX + (screenPoint.X / (float)Width) * (effectiveMaxX - effectiveMinX);
        float wy = minY + ((Height - screenPoint.Y) / (float)Height) * (maxY - minY);

        if (CurrentGridType == GridType.Logarithmic)
        {
            wx = (float)Math.Pow(10, wx);
        }

        return new PointF(wx, wy);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.HighQuality;

        DrawGrid(g);
        DrawAxes(g);

        foreach (var graph in GraphList)
        {
            DrawSeries(g, graph);
        }
    }
    
    private void DrawGrid(Graphics g)
    {
        using (Pen gridPen = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash })
        {
            // Обычная линейная сетка (10 делений)
            int divisions = 10;
            for (int i = 0; i <= divisions; i++)
            {
                float xPx = i * (Width / (float)divisions);
                float yPx = i * (Height / (float)divisions);

                g.DrawLine(gridPen, xPx, 0, xPx, Height);
                g.DrawLine(gridPen, 0, yPx, Width, yPx);
            }
        }
    }

    private void DrawAxes(Graphics g)
    {
        using (Pen axisPen = new Pen(Color.Black, 2))
        {
            PointF origin = WorldToScreen(new PointF(0, 0));

            // Ось X
            if (origin.Y >= 0 && origin.Y <= Height)
                g.DrawLine(axisPen, 0, origin.Y, Width, origin.Y);

            // Ось Y
            if (origin.X >= 0 && origin.X <= Width)
                g.DrawLine(axisPen, origin.X, 0, origin.X, Height);
        }
    }

    private void DrawSeries(Graphics g, GraphS graph)
    {
        if (graph.Points.Count == 0) return;

        using (Pen pen = new Pen(graph.Color, 2))
        using (Brush pointBrush = new SolidBrush(graph.Color))
        using (Font font = new Font("Arial", 8))
        using (Brush textBrush = new SolidBrush(Color.DarkSlateGray))
        {
            PointF? prevPx = null;

            foreach (var pt in graph.Points)
            {
                // Пропуск точек вне логарифмической области
                if (CurrentGridType == GridType.Logarithmic && pt.X <= 0) continue;

                PointF currentPx = WorldToScreen(pt);

                // Рисуем линию
                if (prevPx.HasValue)
                {
                    // Отсечение очевидных вылетов за пределы экрана
                    if (Math.Abs(currentPx.Y - prevPx.Value.Y) < Height * 10)
                        g.DrawLine(pen, prevPx.Value, currentPx);
                }

                // Рисуем маркер точки
                float r = graph.PointsSize;
                g.FillEllipse(pointBrush, currentPx.X - r, currentPx.Y - r, r * 2, r * 2);

                // Рисуем адекватные текстовые подписи
                if (graph.ShowPoints)
                {
                    string label = $"({pt.X:0.##}; {pt.Y:0.##})";
                    g.DrawString(label, font, textBrush, currentPx.X + 5, currentPx.Y - 12);
                }

                prevPx = currentPx;
            }
        }
    }

    private void graphBox_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;
        isDragging = true;
        lastMousePosition = e.Location;
        Cursor = Cursors.Hand;
    }

    private void graphBox_MouseMove(object? sender, MouseEventArgs e)
    {
        if (!isDragging) return;
        // Вычисляем смещение мыши
        float dx = e.X - lastMousePosition.X;
        float dy = e.Y - lastMousePosition.Y;

        // Переводим пиксельное смещение в мировой масштаб
        var worldWidth = maxX - minX;
        var worldHeight = maxY - minY;

        var deltaX = (dx / Width) * worldWidth;
        var deltaY = (dy / Height) * worldHeight;

        minX -= deltaX;
        maxX -= deltaX;
        minY += deltaY; // Y инвертирован
        maxY += deltaY;

        lastMousePosition = e.Location;
        Invalidate(); // Перерисовка
    }

    private void graphBox_MouseUp(object? sender, MouseEventArgs e)
    {
        if(e.Button != MouseButtons.Left) return;
        isDragging = false;
        Cursor = Cursors.Default;
    }
    
    private void graphBox_MouseWheel(object? sender, MouseEventArgs e)
    {
        // Коэффициент зума (10%)
        float zoomFactor = (e.Delta > 0) ? 0.9f : 1.1f;

        // Масштабирование относительно текущего положения курсора
        PointF mouseWorldBefore = ScreenToWorld(e.Location);

        minX = mouseWorldBefore.X + (minX - mouseWorldBefore.X) * zoomFactor;
        maxX = mouseWorldBefore.X + (maxX - mouseWorldBefore.X) * zoomFactor;
        minY = mouseWorldBefore.Y + (minY - mouseWorldBefore.Y) * zoomFactor;
        maxY = mouseWorldBefore.Y + (maxY - mouseWorldBefore.Y) * zoomFactor;

        Invalidate();
    }
    
    public GraphS AddFormulaGraph(string name, Color color, Func<double, double> formula, double step = 0.1)
    {
        var graph = new GraphS { Name = name, Color = color, ShowPoints = false };
        for (double x = minX - 50; x <= maxX + 50; x += step)
        {
            try
            {
                var y = formula(x);
                if (!double.IsNaN(y) && !double.IsInfinity(y))
                {
                    graph.Points.Add(new PointF((float)x, (float)y));
                }
            }
            catch
            {
                // ignored
            }
        }
        GraphList.Add(graph);
        Invalidate();
        return graph;
    }

    // 2. Чтение точек из текстового файла (Формат строки: X;Y или X,Y)
    public GraphS LoadFromFile(string filePath, Color color, string graphName = "File Series")
    {
        var graph = new GraphS { Name = graphName, Color = color, ShowPoints = true };
        
        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split([';', ',', '\t', ' '], StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) continue;
            if (!float.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float x) ||
                !float.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float y)) continue;
            Console.WriteLine($"points: {x}; {y}");
            graph.Points.Add(new PointF(x, y));
        }
        GraphList.Add(graph);
        Invalidate();
        return graph;
    }

    public void ChangeColors(GraphS graph, Color color)
    {
        graph.Color = color;
        Invalidate();
    }

    public void ChangePointsVisibility(GraphS graph, bool visible)
    {
        graph.ShowPoints = visible;
        Invalidate();
    }
    
}