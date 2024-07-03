using System;

public class Error
{
    public static void error(int line, string message, string? sourceCode)
    {
        report(line, message, sourceCode);

        Compiler.hadError = true;
    }

    private static void report(int line, string message, string? sourceCode)
    {
        Console.WriteLine("\nError in line {0}: {1}\n", line, message);
        Console.WriteLine('\t' + sourceCode + '\n');
    }
}