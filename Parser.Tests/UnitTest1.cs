using Excel;

namespace DemoParser.Tests;

[TestClass]
public class BasicArithmeticTests
{
    [TestMethod]
    public void NumbersAddition()
    {
        Assert.AreEqual(Calculator.Evaluate("1+2"), 3.0);
    }

    [TestMethod]
    public void VariablesAddition()
    {
        Calculator.GlobalScope.Add("A1", 70);
        Calculator.GlobalScope.Add("B1", 18);
        Assert.AreEqual(Calculator.Evaluate("A1+B1"), 88);
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("B1");
    }

    [TestMethod]
    public void NumbersSubtraction()
    {
        Assert.AreEqual(Calculator.Evaluate("12-5"), 7);
    }

    [TestMethod]
    public void VariablesSubtraction()
    {
        Calculator.GlobalScope.Add("A1", 70);
        Calculator.GlobalScope.Add("B1", 1);
        Assert.AreEqual(Calculator.Evaluate("A1-B1"), 69);
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("B1");
    }

    [TestMethod]
    public void NumbersMultiplication()
    {
        Assert.AreEqual(Calculator.Evaluate("5*8"), 40);
    }

    [TestMethod]
    public void VariablesMultiplication()
    {
        Calculator.GlobalScope.Add("A1", 11);
        Calculator.GlobalScope.Add("B1", 15);
        Assert.AreEqual(Calculator.Evaluate("A1*B1"), 165);
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("B1");
    }

    [TestMethod]
    public void NumbersDivision()
    {
        Assert.AreEqual(Calculator.Evaluate("10/40"), 0.25);
    }

    [TestMethod]
    public void VariablesDivision()
    {
        Calculator.GlobalScope.Add("A1", 7);
        Calculator.GlobalScope.Add("B1", 16);
        Assert.AreEqual(Calculator.Evaluate("A1/B1"), 0.4375);
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("B1");
    }

    [TestMethod]
    public void PowerTest()
    {
        Assert.AreEqual(Calculator.Evaluate("2^10"), 1024);
    }

    [TestMethod]
    public void maxTest()
    {
        Assert.AreEqual(Calculator.Evaluate("max(1+10, 2*6)"), 12);
    }

    [TestMethod]
    public void minTest()
    {
        Assert.AreEqual(Calculator.Evaluate("min(1/100, -1/10)"), -0.1);
    }

    [TestMethod]
    public void mmaxTest()
    {
        Assert.AreEqual(Calculator.Evaluate("mmax(1, 2, 3, 4, 10)"), 10);
    }

    [TestMethod]
    public void mminTest()
    {
        Assert.AreEqual(Calculator.Evaluate("mmin(-11, 12, -3*5, 19, 10)"), -15);
    }

    [TestMethod]
    public void UnaryTest()
    {
        Calculator.GlobalScope.Add("A1", 10);
        double a = Calculator.Evaluate("-A1");
        double b = Calculator.Evaluate("+A1");
        Calculator.GlobalScope.Remove("A1");
        Assert.AreEqual(a, -10);
        Assert.AreEqual(b, 10);
    }

    public void GreaterLessTest()
    {
        Calculator.GlobalScope.Add("A1", 5);
        Calculator.GlobalScope.Add("A2", 2);
        double a = Calculator.Evaluate("A1>A2");
        double b = Calculator.Evaluate("A1<A2");
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("A2");
        Assert.AreEqual(a, 1);
        Assert.AreEqual(b, 0);
    }

    public void GreaterLessEqualsTest()
    {
        Calculator.GlobalScope.Add("A1", 5);
        Calculator.GlobalScope.Add("A2", 2);
        double a = Calculator.Evaluate("A1>=A2");
        double b = Calculator.Evaluate("A2<=2");
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("A2");
        Assert.AreEqual(a, 1);
        Assert.AreEqual(b, 1);
    }

    public void EqNotEqTest()
    {
        Calculator.GlobalScope.Add("A1", 5);
        Calculator.GlobalScope.Add("A2", 2);
        double a = Calculator.Evaluate("A1=5");
        double b = Calculator.Evaluate("A1<>A2");
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("A2");
        Assert.AreEqual(a, 1);
        Assert.AreEqual(b, 1);
    }
}

[TestClass]
public class ArithmeticLawsTests
{
    [TestMethod]
    public void CommutativelawTest()
    {

        Calculator.GlobalScope.Add("A1", 3);
        Calculator.GlobalScope.Add("B1", 11);
        double a = Calculator.Evaluate("A1+B1"), b = Calculator.Evaluate("B1+A1"), c = Calculator.Evaluate("A1*B1"), d = Calculator.Evaluate("B1*A1");
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("B1");
        Assert.AreEqual(a, b);
        Assert.AreEqual(c, d);
    }

    [TestMethod]
    public void DistributiveLawTest()
    {
        Calculator.GlobalScope.Add("A1", 70);
        Calculator.GlobalScope.Add("B1", 18);
        Calculator.GlobalScope.Add("C1", 12);
        double a = Calculator.Evaluate("A1*(B1+C1)");
        double b = Calculator.Evaluate("A1*B1+A1*C1");

        double c = Calculator.Evaluate("(A1+B1)*C1");
        double d = Calculator.Evaluate("A1*C1+B1*C1");
        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("B1");
        Calculator.GlobalScope.Remove("C1");
        Assert.AreEqual(a, b);
        Assert.AreEqual(c, d);
    }

    [TestMethod]
    public void AssociativeLawTest()
    {
        Calculator.GlobalScope.Add("A1", 70);
        Calculator.GlobalScope.Add("B1", 18);
        Calculator.GlobalScope.Add("C1", 12);

        double a = Calculator.Evaluate("A1+(B1+C1)");
        double b = Calculator.Evaluate("(A1+B1)+C1");

        double c = Calculator.Evaluate("A1*(B1*C1)");
        double d = Calculator.Evaluate("(A1*B1)*C1");

        Calculator.GlobalScope.Remove("A1");
        Calculator.GlobalScope.Remove("B1");
        Calculator.GlobalScope.Remove("C1");
        Assert.AreEqual(a, b);
        Assert.AreEqual(c, d);
    }

}

[TestClass]
public class ComplexTests
{

    [TestMethod]
    public void HardTest1()
    {
        Assert.AreEqual(Calculator.Evaluate("(2 + 3 * 4) / (5 - 1)"), 3.5);
    }

    [TestMethod]
    public void HardTest2()
    {
        Assert.AreEqual(Calculator.Evaluate("-3*(2--1)++4*-(1+2+3)"), -33);
    }
    [TestMethod]
    public void HardTest3()
    {
        Assert.AreEqual(Calculator.Evaluate("2^3 + max(4, 5) - min(6, 7)"), 7);
    }
    [TestMethod]
    public void HardTest4()
    {
        Assert.AreEqual(Calculator.Evaluate("3 * mmax(4, 5, 6) - mmin(7, 8, 9)"), 11);
    }
    [TestMethod]
    public void HardTest5()
    {
        Assert.AreEqual(Calculator.Evaluate("3^2 * 4^2 / mmax(3, 2) - mmin(4, 5)"), 44);
    }
    [TestMethod]
    public void HardTest6()
    {
        Assert.AreEqual(Calculator.Evaluate("(mmax(10/7, 3/2) + mmin(11, 45/(45/10))) * (mmax(12, 11, 10, 9) - mmin(8, 9*10, 115))"), 46);
    }
    [TestMethod]
    public void HardTest7()
    {
        Assert.AreEqual(Calculator.Evaluate("-mmax(1, 2) * mmin(3, 4) - mmax(5, 6, 7, 1/5)^mmin(8, 9, 11, 144)"), -5764807);
    }
    [TestMethod]
    public void ExceptionTest()
    {
        Exception expectedException = null;
        try
        {
            Calculator.Evaluate("1/0");
        }
        catch (Exception e)
        {
            expectedException = e;
        }
        Assert.AreEqual(expectedException.Message, "Спроба поділити на 0.");
    }
}