namespace Excel;
partial class Table
{
    public void DeleteCell(Tuple<int, int> coordinates)
    {
        Cell cell = CellByID[IDByCoordinates[coordinates]];
        foreach (var ID in BasisCells[cell.ID])
        {
            DependentCells[ID].Remove(cell.ID);
        }
        BasisCells[cell.ID].Clear();
        CellByID[cell.ID].ChangeExpression("0");
        Refresh(IDByCoordinates[coordinates]);
    }

    private void DeletePermanently(Tuple<int, int> coordinates)
    {
        int ID = IDByCoordinates[coordinates];
        if (DependentCells[ID].Count > 0)
        {
            //Cell cell = cellByID[ID];
            string s = "Від значення клітинки " + CellByID[ID].Name + " залежить значення таких клітинок:\n";
            foreach (var newID in DependentCells[ID])
            {
                s += CellByID[newID].Name + "; ";
            }
            s += "\nЗмініть вираз, записаний у даних клітинках перед видаленням цього";
            throw new ArgumentException(s);
        }
        else
        {
            Cell cell = CellByID[IDByCoordinates[coordinates]];
            foreach (var newID in BasisCells[cell.ID])
            {
                DependentCells[newID].Remove(cell.ID);
            }
            CellByID.Remove(cell.ID);
            IDByName.Remove(cell.Name);
            IDByCoordinates.Remove(cell.GetCoordinates());

            Color.Remove(cell.ID);
            DependentCells.Remove(cell.ID);
            BasisCells.Remove(cell.ID);
            cell.Delete();
        }
    }

    public void DeleteRow(int number)
    {
        try
        {
            List<Tuple<int, int>> DeleteQuery = new List<Tuple<int, int>>();
            foreach (var cell in CellByID.Values)
            {
                if (cell.GetCoordinates().Item2 == number)
                {
                    DeleteQuery.Add(cell.GetCoordinates());
                    if (DependentCells[cell.ID].Count > 0)
                    {
                        DeletePermanently(cell.GetCoordinates());
                    }
                }
            }
            foreach (var coordinates in DeleteQuery)
            {
                DeletePermanently(coordinates);
            }
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message + " рядка.");
        }
    }

    public void DeleteColumn(int number)
    {
        try
        {
            List<Tuple<int, int>> DeleteQuery = new List<Tuple<int, int>>();
            foreach (var cell in CellByID.Values)
            {
                if (cell.GetCoordinates().Item1 == number)
                {
                    DeleteQuery.Add(cell.GetCoordinates());
                    if (DependentCells[cell.ID].Count > 0)
                    {
                        DeletePermanently(cell.GetCoordinates());
                    }
                }
            }
            foreach (var coordinates in DeleteQuery)
            {
                DeletePermanently(coordinates);
            }
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message + " стовпця.");
        }

    }


}