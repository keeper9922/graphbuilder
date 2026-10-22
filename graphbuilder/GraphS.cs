namespace graphbuilder;

public class GraphS
{
    public string Name { get; set; } = "graph";
    public Color Color { get; set; } = Color.Red;
    public List<PointF> Points {get; set;} = new List<PointF>();
    public bool ShowPoints { get; set; } = true;
    public float PointsSize { get; set; } = 1;
    
}