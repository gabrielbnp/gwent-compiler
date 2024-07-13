using System;
using static TokenType;

public class Error
{
    public static void error(int line, int column, string? sourceCode, string errorMessage)
    {
        report(line, column, sourceCode, errorMessage);

        Compiler.hadError = true;
    }

    public static void error(Token token, string errorMessage)
    {
        if(token.type == EOF)
            report(token.numLine, 0, token.sourceCode, errorMessage);
        else
            report(token.numLine, token.numColumn, token.sourceCode, errorMessage);

        Compiler.hadError = true;
    }

    private static void report(int line, int column, string? sourceCode, string errorMessage)
    {
        if(column == 0)
            column = sourceCode == null ? 0 : sourceCode.Length - 1;

        int? length = sourceCode?.Length - (column == sourceCode?.Length - 1 ? 0 : 1);

        string here = "\t";

        for(int i = 0; i < length; i++)
        {
            if(i == column)
                here += "^";
            else
                here += ".";
        }

        Console.WriteLine("\nError in line {0}: {1}", line, errorMessage);
        Console.WriteLine('\t' + sourceCode);
        Console.WriteLine(here + '\n');
    }
}