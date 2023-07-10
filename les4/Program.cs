using System;
using System.Diagnostics;

public interface IIntegrationMethod
{
    double CalculateIntegral(Func<double, double> function, double lowerBound, double upperBound, double accuracy);
    string MethodName { get; }
    int IterationCount { get; }
    TimeSpan ElapsedTime { get; }
}

public class LeftRectangleMethod : IIntegrationMethod
{
    public int IterationCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }

    public double CalculateIntegral(Func<double, double> function, double lowerBound, double upperBound, double accuracy)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        double integral = 0;
        double step = (upperBound - lowerBound) / accuracy;

        for (double x = lowerBound; x < upperBound; x += step)
        {
            integral += function(x) * step;
            IterationCount++;
        }

        stopwatch.Stop();
        ElapsedTime = stopwatch.Elapsed;

        return integral;
    }

    public string MethodName => "Left Rectangle Method";
}

public class RightRectangleMethod : IIntegrationMethod
{
    public int IterationCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }

    public double CalculateIntegral(Func<double, double> function, double lowerBound, double upperBound, double accuracy)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        double integral = 0;
        double step = (upperBound - lowerBound) / accuracy;

        for (double x = lowerBound + step; x <= upperBound; x += step)
        {
            integral += function(x) * step;
            IterationCount++;
        }

        stopwatch.Stop();
        ElapsedTime = stopwatch.Elapsed;

        return integral;
    }

    public string MethodName => "Right Rectangle Method";
}

public class MidpointRectangleMethod : IIntegrationMethod
{
    public int IterationCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }

    public double CalculateIntegral(Func<double, double> function, double lowerBound, double upperBound, double accuracy)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        double integral = 0;
        double step = (upperBound - lowerBound) / accuracy;

        for (double x = lowerBound + step / 2; x < upperBound; x += step)
        {
            integral += function(x) * step;
            IterationCount++;
        }

        stopwatch.Stop();
        ElapsedTime = stopwatch.Elapsed;

        return integral;
    }

    public string MethodName => "Midpoint Rectangle Method";
}

public class TrapezoidalMethod : IIntegrationMethod
{
    public int IterationCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }

    public double CalculateIntegral(Func<double, double> function, double lowerBound, double upperBound, double accuracy)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        double integral = 0;
        double step = (upperBound - lowerBound) / accuracy;

        for (double x = lowerBound; x < upperBound; x += step)
        {
            integral += (function(x) + function(x + step)) * step / 2;
            IterationCount++;
        }

        stopwatch.Stop();
        ElapsedTime = stopwatch.Elapsed;

        return integral;
    }

    public string MethodName => "Trapezoidal Method";
}

public class SimpsonMethod : IIntegrationMethod
{
    public int IterationCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }

    public double CalculateIntegral(Func<double, double> function, double lowerBound, double upperBound, double accuracy)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        double integral = 0;
        double step = (upperBound - lowerBound) / accuracy;

        for (double x = lowerBound; x < upperBound; x += 2 * step)
        {
            integral += (function(x) + 4 * function(x + step) + function(x + 2 * step)) * step / 3;
            IterationCount++;
        }

        stopwatch.Stop();
        ElapsedTime = stopwatch.Elapsed;

        return integral;
    }

    public string MethodName => "Simpson Method";
}

class Program
{
    static void Main(string[] args)
    {
        IIntegrationMethod leftRectangleMethod = new LeftRectangleMethod();
        IIntegrationMethod rightRectangleMethod = new RightRectangleMethod();
        IIntegrationMethod midpointRectangleMethod = new MidpointRectangleMethod();
        IIntegrationMethod trapezoidalMethod = new TrapezoidalMethod();
        IIntegrationMethod simpsonMethod = new SimpsonMethod();

        double result1 = leftRectangleMethod.CalculateIntegral(x => x * x, 0, 1, 1000);
        double result2 = rightRectangleMethod.CalculateIntegral(x => x * x, 0, 1, 1000);
        double result3 = midpointRectangleMethod.CalculateIntegral(x => x * x, 0, 1, 1000);
        double result4 = trapezoidalMethod.CalculateIntegral(x => x * x, 0, 1, 1000);
        double result5 = simpsonMethod.CalculateIntegral(x => x * x, 0, 1, 1000);

        Console.WriteLine($"Левые прямоугольники: {result1}, Iteration Count: {leftRectangleMethod.IterationCount}, Elapsed Time: {leftRectangleMethod.ElapsedTime}");
        Console.WriteLine($"Правые прямоугольники: {result2}, Iteration Count: {rightRectangleMethod.IterationCount}, Elapsed Time: {rightRectangleMethod.ElapsedTime}");
        Console.WriteLine($"Средние прямоугольники: {result3}, Iteration Count: {midpointRectangleMethod.IterationCount}, Elapsed Time: {midpointRectangleMethod.ElapsedTime}");
        Console.WriteLine($"Трапеции: {result4}, Iteration Count: {trapezoidalMethod.IterationCount}, Elapsed Time: {trapezoidalMethod.ElapsedTime}");
        Console.WriteLine($"Параболы: {result5}, Iteration Count: {simpsonMethod.IterationCount}, Elapsed Time: {simpsonMethod.ElapsedTime}");
    }
}
