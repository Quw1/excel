using ExcelParser;
using System.Diagnostics;

namespace ExcelParser;

#pragma warning disable 0436

public class LabCalculatorVisitor : LabCalculatorBaseVisitor<double>
{

    private readonly IDictionary<string, double> tableIdentifier;
    public LabCalculatorVisitor(IDictionary<string, double> tableIdentifier)
    {
        ArgumentNullException.ThrowIfNull(tableIdentifier);
        this.tableIdentifier = tableIdentifier;
    }

    public override double VisitCompileUnit(LabCalculatorParser.CompileUnitContext context)
    {
        return Visit(context.expression());
    }

    public override double VisitNumberExpr(LabCalculatorParser.NumberExprContext context)
    {
        var result = double.Parse(context.GetText());
        Debug.WriteLine(result);

        return result;
    }

    //IdentifierExpr
    public override double VisitIdentifierExpr(LabCalculatorParser.IdentifierExprContext context)
    {
        var result = context.GetText();
        //видобути значення змінної з таблиці
        if (tableIdentifier.TryGetValue(result.ToString(), out double value))
        {
            return value;
        }
        else
        {
            return 0.0;
        }
    }

    public override double VisitParenthesizedExpr(LabCalculatorParser.ParenthesizedExprContext context)
    {
        return Visit(context.expression());
    }

    public override double VisitUnaryExpr(LabCalculatorParser.UnaryExprContext context)
    {
        var left = WalkLeft(context);
        if (context.operatorToken.Type == LabCalculatorLexer.SUBTRACT)
        {
            left *= (-1);
        }
        return left;
    }

    public override double VisitExponentialExpr(LabCalculatorParser.ExponentialExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);

        Debug.WriteLine("{0} ^ {1}", left, right);
        return System.Math.Pow(left, right);
    }

    public override double VisitAdditiveExpr(LabCalculatorParser.AdditiveExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);

        if (context.operatorToken.Type == LabCalculatorLexer.ADD)
        {
            Debug.WriteLine("{0} + {1}", left, right);
            return left + right;
        }
        else //LabCalculatorLexer.SUBTRACT
        {
            Debug.WriteLine("{0} - {1}", left, right);
            return left - right;
        }
    }

    public override double VisitMultiplicativeExpr(LabCalculatorParser.MultiplicativeExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);

        if (context.operatorToken.Type == LabCalculatorLexer.MULTIPLY)
        {
            Debug.WriteLine("{0} * {1}", left, right);
            return left * right;
        }
        else //LabCalculatorLexer.DIVIDE
        {
            Debug.WriteLine("{0} / {1}", left, right);
            if (right != 0.0)
            {
                return left / right;
            }
            else
            {
                throw new DivideByZeroException("Спроба поділити на 0.");
            }
        }
    }

    public override double VisitMminExpr(LabCalculatorParser.MminExprContext context)
    {
        double minValue = Double.PositiveInfinity;
        foreach (var child in context.paramlist.children.OfType<LabCalculatorParser.ExpressionContext>())
        {
            double childValue = this.Visit(child);
            if (childValue < minValue)
            {
                minValue = childValue;
            }
        }
        return minValue;
    }

    public override double VisitMmaxExpr(LabCalculatorParser.MmaxExprContext context)
    {
        double maxValue = Double.NegativeInfinity;
        foreach (var child in context.paramlist.children.OfType<LabCalculatorParser.ExpressionContext>())
        {
            double childValue = this.Visit(child);
            if (childValue > maxValue)
            {
                maxValue = childValue;
            }
        }
        return maxValue;
    }

    public override double VisitMinExpr(LabCalculatorParser.MinExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);
        if (left < right)
        {
            return left;
        }
        return right;
    }

    public override double VisitMaxExpr(LabCalculatorParser.MaxExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);
        if (left > right)
        {
            return left;
        }
        return right;
    }

    public override double VisitGLTExpr(LabCalculatorParser.GLTExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);
        if (context.operatorToken.Type == LabCalculatorLexer.GT)
        {
            if (left > right)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (left < right)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public override double VisitGLEExpr(LabCalculatorParser.GLEExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);
        if (context.operatorToken.Type == LabCalculatorLexer.GE)
        {
            if (left >= right)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (left <= right)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public override double VisitENQExpr(LabCalculatorParser.ENQExprContext context)
    {
        var left = WalkLeft(context);
        var right = WalkRight(context);
        if (context.operatorToken.Type == LabCalculatorLexer.EQ)
        {
            if (left == right)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (left != right)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    private double WalkLeft(LabCalculatorParser.ExpressionContext context)
    {
        return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(0));
    }

    private double WalkRight(LabCalculatorParser.ExpressionContext context)
    {
        return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(1));
    }
}