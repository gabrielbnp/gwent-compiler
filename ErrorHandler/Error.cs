using System;

public class Error
{
    public static void error(int line, int column, string message, string? sourceCode)
    {
        report(line, column, message, sourceCode);

        Compiler.hadError = true;
    }

    private static void report(int line, int column,string message, string? sourceCode)
    {
        Console.WriteLine("\nError in line {0}: {1}", line, message);
        Console.WriteLine('\t' + sourceCode);
        
        string here = "\t";
        for(int i = 0; i < sourceCode?.Length; i++)
        {
            if(i == column)
                here += "^";
            else
                here += ".";
        }

        Console.WriteLine(here + '\n');
    }
}