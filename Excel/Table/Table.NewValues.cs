using ExtensionMethods;

namespace Excel;
partial class Table
{
    public void EditCell(Tuple<int, int> coordinates, string expression)
    {
        List<string> str = MyExtension.ParseName(expression);
        List<int> OldBasis = new List<int>();
        OldBasis = BasisCells[IDByCoordinates[coordinates]];
        foreach (var oldID in BasisCells[IDByCoordinates[coordinates]])
        {
            DependentCells[oldID].Remove(IDByCoordinates[coordinates]);
        }
        BasisCells[IDByCoordinates[coordinates]].Clear();

        foreach (string s in str)
        {
            Tuple<int, int> p = MyExtension.NameToCoordinates(s);
            if (!CellExists(p))
            {
                AddCell(p);
            }
            DependentCells[IDByCoordinates[p]].Add(IDByCoordinates[coordinates]);
            BasisCells[IDByCoordinates[coordinates]].Add(IDByCoordinates[p]);
        }

        if (FindCycles(coordinates) == true)
        {
            foreach (var ID in BasisCells[IDByCoordinates[coordinates]])
            {
                DependentCells[ID].Remove(IDByCoordinates[coordinates]);
            }

            BasisCells[IDByCoordinates[coordinates]] = OldBasis;
            foreach (var ID in BasisCells[IDByCoordinates[coordinates]])
            {
                DependentCells[ID].Add(IDByCoordinates[coordinates]);
            }

            throw new ArgumentException("Введений вираз призвів до утворення циклу.");
        }
        else
        {
            CellByID[IDByCoordinates[coordinates]].ChangeExpression(expression);
            Refresh(IDByCoordinates[coordinates]);
        }
    }

    public void AddCell(Tuple<int, int> coordinates)
    {
        Cell cell = new Cell(coordinates.Item1, coordinates.Item2);
        CellByID.Add(cell.ID, cell);
        IDByName.Add(cell.Name, cell.ID);
        IDByCoordinates.Add(coordinates, cell.ID);

        Color.Add(cell.ID, 0);
        DependentCells.Add(cell.ID, new List<int>());
        BasisCells.Add(cell.ID, new List<int>());
    }

    public void AddCell(Tuple<int, int> coordinates, string expression)
    {
        Cell cell = new Cell(coordinates.Item1, coordinates.Item2, expression);
        CellByID.Add(cell.ID, cell);
        IDByName.Add(cell.Name, cell.ID);
        IDByCoordinates.Add(coordinates, cell.ID);

        Color.Add(cell.ID, 0);
        DependentCells.Add(cell.ID, new List<int>());
        BasisCells.Add(cell.ID, new List<int>());

        List<string> str = MyExtension.ParseName(expression);
        foreach (string s in str)
        {
            Tuple<int, int> p = MyExtension.NameToCoordinates(s);
            if (!CellExists(p))
            {
                AddCell(p);
            }
            DependentCells[IDByCoordinates[p]].Add(IDByCoordinates[coordinates]);
            BasisCells[IDByCoordinates[coordinates]].Add(IDByCoordinates[p]);
        }

        if (FindCycles(coordinates) == true)
        {
            DeleteCell(coordinates);
            throw new ArgumentException("Введений вираз призвів до утворення циклу.");
        }
    }
}