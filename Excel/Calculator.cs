using Antlr4.Runtime;
using ExcelParser;
namespace Excel;
#pragma warning disable 0436
public static class Calculator
{
    public static IDictionary<string, double> GlobalScope { get; } = new Dictionary<string, double> { };
    public static double Evaluate(string expression)
    {
        var lexer = new LabCalculatorLexer(new AntlrInputStream(expression));
        lexer.RemoveErrorListeners();
        lexer.AddErrorListener(new ThrowExceptionErrorListener());

        var tokens = new CommonTokenStream(lexer);
        var parser = new LabCalculatorParser(tokens);
        parser.RemoveErrorListeners();
        parser.AddErrorListener(new ParserErrorListener());

        var tree = parser.compileUnit();

        var visitor = new LabCalculatorVisitor(GlobalScope);

        return visitor.Visit(tree);
    }
}