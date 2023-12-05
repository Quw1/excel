using Antlr4.Runtime;

namespace ExcelParser;
public class ParserErrorListener : BaseErrorListener
{
    public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        throw new ArgumentException("Incorrect use of operation: ", msg, e);
    }
}