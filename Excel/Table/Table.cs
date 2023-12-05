using System.Collections.ObjectModel;

namespace Excel;
public partial class Table
{
    public Dictionary<int, Cell> CellByID { get; set; }
    public Dictionary<string, int> IDByName { get; set; }
    public Dictionary<Tuple<int, int>, int> IDByCoordinates { get; set; }
    public Dictionary<int, int> Color { get; set; }
    public Dictionary<int, List<int>> DependentCells { get; set; }
    public Dictionary<int, List<int>> BasisCells { get; set; }

    public Table()
    {
        CellByID = new Dictionary<int, Cell>();
        IDByName = new Dictionary<string, int>();
        IDByCoordinates = new Dictionary<Tuple<int, int>, int>();
        Color = new Dictionary<int, int>();
        DependentCells = new Dictionary<int, List<int>>();
        BasisCells = new Dictionary<int, List<int>>();
    }

    public void Refresh(int ID)
    {
        Queue<int> Q = new();
        Q.Enqueue(ID);
        while (Q.Count > 0)
        {
            int newID = Q.First();
            string exp = CellByID[newID].GetExpression();
            CellByID[newID].ChangeExpression(exp);
            foreach (var i in DependentCells[newID])
            {
                Q.Enqueue(i);
            }
            Q.Dequeue();
        }
    }

    private bool DFS(int ID)
    {
        Color[ID] = 1;
        foreach (var newCellID in DependentCells[ID])
        {
            if (Color[newCellID] == 0)
            {
                if (DFS(newCellID) == true)
                {
                    return true;
                }
            }
            if (Color[newCellID] == 1)
            {
                return true;
            }
        }
        Color[ID] = 2;
        return false;
    }

    public bool FindCycles(Tuple<int, int> coordinates)
    {
        foreach (var key in Color.Keys)
        {
            Color[key] = 0;
        }
        return DFS(IDByCoordinates[coordinates]);
    }
}