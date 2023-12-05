namespace Excel;
public class Cell
{
    public double Value { get; set; }
    public string Expression { get; set; }
    public int CoordinateX { get; set; }
    public int CoordinateY { get; set; }
    public string Name { get; set; }
    public int ID { get; }
    public static int Count { get => count; set => count = value; }

    private static int count = 0;

    public Cell(double val, string exp, int coordX, int coordY, string jname, int id)
    {
        Value = val;
        Expression = exp;
        CoordinateX = coordX;
        CoordinateY = coordY;
        Name = jname;
        ID = id;
        Cell.count = Math.Max(Cell.count, ID + 1);
        if (Calculator.GlobalScope.ContainsKey(this.Name))
        {
            Calculator.GlobalScope[Name] = Value;
        }
        else
        {
            Calculator.GlobalScope.Add(Name, Value);
        }
    }

    public string GetExpression()
    {
        return Expression;
    }

    public Tuple<int, int> GetCoordinates()
    {
        Tuple<int, int> p = new Tuple<int, int>(this.CoordinateX, this.CoordinateY);
        return p;
    }

    public string GetName()
    {
        return this.Name;
    }

    public string CoordinatesToName()
    {
        string ans = string.Empty;
        int x = CoordinateX;
        while (x > 0)
        {
            ans = Convert.ToChar((x - 1) % 26 + 65) + ans;
            x /= 26;
        }
        ans = ans + Convert.ToString(CoordinateY);
        return ans;
    }
    public Cell(int x, int y)
    {
        CoordinateX = x;
        CoordinateY = y;
        Value = 0;
        Expression = string.Empty;
        Name = CoordinatesToName();
        ReCalculate();
        ID = Cell.count;
        Cell.count++;
    }
    private void ReCalculate()
    {
        Value = 0;
        if (Expression != "")
        {
            Value = Calculator.Evaluate(this.Expression);
        }
        if (Calculator.GlobalScope.ContainsKey(this.Name))
        {
            Calculator.GlobalScope.Remove(this.Name);
        }
        Calculator.GlobalScope.Add(this.Name, this.Value);
    }
    public void ChangeExpression(string exp)
    {
        Expression = exp;
        ReCalculate();
    }

    public Cell(int x, int y, string exp)
    {
        CoordinateX = x;
        CoordinateY = y;
        Expression = exp;
        Name = CoordinatesToName();
        ID = Cell.count;
        Cell.count++;
        ReCalculate();
    }

    public void Delete()
    {
        Expression = "0";
        Calculator.GlobalScope.Remove(this.Name);
    }

}