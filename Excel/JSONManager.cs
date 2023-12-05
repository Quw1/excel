using Excel;
using System.Text.Json.Serialization;
using System.Text.Json;

public class JsonSerializable_
{
    [JsonPropertyName("A")]
    public List<CellDeserializer> A { get; set; }

    [JsonPropertyName("CountColumn")]
    public int CountColumn { get; set; }

    [JsonPropertyName("CountRow")]
    public int CountRow { get; set; }

    public JsonSerializable_(Table tableInput, int col, int row)
    {
        A = new List<CellDeserializer>();
        foreach (var cell in tableInput.CellByID.Values)
        {
            A.Add(new CellDeserializer(cell.Value,
                                         cell.Expression,
                                         cell.CoordinateX,
                                         cell.CoordinateY,
                                         cell.Name,
                                         cell.ID,
                                         tableInput.DependentCells[cell.ID],
                                         tableInput.BasisCells[cell.ID]));
        }
        CountColumn = col;
        CountRow = row;
    }

    [JsonConstructor]
    public JsonSerializable_(List<CellDeserializer> A, int CountColumn, int CountRow)
    {
        this.A = A;
        this.CountColumn = CountColumn;
        this.CountRow = CountRow;
    }

}

public class CellDeserializer
{
    [JsonPropertyName("value")]
    public double value { get; set; }

    [JsonPropertyName("expression")]
    public string expression { get; set; }

    [JsonPropertyName("coordinateX")]
    public int coordinateX { get; set; }

    [JsonPropertyName("coordinateY")]
    public int coordinateY { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("ID")]
    public int ID { get; set; }

    [JsonPropertyName("DependentCells")]
    public List<int> DependentCells { get; set; }

    [JsonPropertyName("BasisCells")]
    public List<int> BasisCells { get; set; }

    [JsonConstructor]
    public CellDeserializer(double value, string expression, int coordinateX, int coordinateY, string name, int ID, List<int> DependentCells, List<int> BasisCells)
    {
        this.value = value;
        this.expression = expression;
        this.coordinateX = coordinateX;
        this.coordinateY = coordinateY;
        this.name = name;
        this.ID = ID;
        this.DependentCells = DependentCells;
        this.BasisCells = BasisCells;
    }


}
public static class JSONManager
{
    public static void SaveFile(string path, JsonSerializable_ objToSave)
    {
        var jsonString = JsonSerializer.Serialize(objToSave, new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            PropertyNameCaseInsensitive = true
        });
        File.WriteAllText(@path, jsonString);
    }

    public static JsonSerializable_ ReadFile(string path)
    {
        string content = File.ReadAllText(@path);
        JsonSerializable_ data = JsonSerializer.Deserialize<JsonSerializable_>(content, new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            PropertyNameCaseInsensitive = true

        });
        return data;
    }


}