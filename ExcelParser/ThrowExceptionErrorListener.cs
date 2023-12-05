using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace ExcelParser;
public class ThrowExceptionErrorListener : IAntlrErrorListener<int>
{
    public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        throw new ArgumentException("Invalid operation or argument: ", msg, e);
    }
}