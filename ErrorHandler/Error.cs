using System;

public class Error
{
    public static void error(int line, int column, string? sourceCode, string errorMessage)
    {
        report(line, column, sourceCode, errorMessage);

        Compiler.hadError = true;
    }

    public static void error(Token token, string errorMessage)
    {
        report(token.numLine, token.numColumn, token.sourceCode, errorMessage);

        Compiler.hadError = true;
    }

    private static void report(int line, int column, string? sourceCode, string errorMessage)
    {
        Console.WriteLine("\nError in line {0}: {1}", line, errorMessage);
        Console.WriteLine('\t' + sourceCode);
        
        string here = "\t";

        int? length = sourceCode?.Length - (column == sourceCode?.Length - 1 ? 0 : 1);

        for(int i = 0; i < length; i++)
        {
            if(i == column)
                here += "^";
            else
                here += ".";
        }

        Console.WriteLine(here + '\n');
    }
}