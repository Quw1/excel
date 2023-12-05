namespace Excel;
partial class Table
{
    public double GetCellValue(Tuple<int, int> coordinates)
    {
        if (IDByCoordinates.ContainsKey(coordinates) && CellByID.ContainsKey(IDByCoordinates[coordinates]))
        {
            return CellByID[IDByCoordinates[coordinates]].Value;
        }
        else
        {
            return 0.0;
        }
    }

    public double GetCellValue(string name)
    {
        if (IDByName.ContainsKey(name) && CellByID.ContainsKey(IDByName[name]))
        {
            return CellByID[IDByName[name]].Value;
        }
        else
        {
            return 0.0;
        }
    }
    public bool CellExists(Tuple<int, int> coordinates)
    {
        if (IDByCoordinates.ContainsKey(coordinates) && CellByID.ContainsKey(IDByCoordinates[coordinates]))
        {
            return true;
        }
        return false;
    }

    public bool CellExists(string name)
    {
        if (IDByName.ContainsKey(name) && CellByID.ContainsKey(IDByName[name]))
        {
            return true;
        }
        return false;
    }

    public string GetExpression(Tuple<int, int> coordinates)
    {
        if (IDByCoordinates.ContainsKey(coordinates) && CellByID.ContainsKey(IDByCoordinates[coordinates]))
        {
            return CellByID[IDByCoordinates[coordinates]].GetExpression();
        }
        return "";
    }
}