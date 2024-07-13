using System;
using System.IO;

public class Compiler
{
    public static bool hadError = false;

    public static void Main(string[] args)
    {
        if(args.Length == 0) // no path to find the source code
        {
            throw new Exception("A path to the source code must be provided.");
        }
        else if(args.Length > 1) // not a valid path, too many things or whitespaces
        {
            throw new Exception("Not a valid path. Check whitespaces.");
        }

        runFile(args[0]);
    }

    private static void runFile(string path)
    {
        StreamReader file = new StreamReader(path);

        string? line = file.ReadLine();
        int numline = 1;

        while(line != null)
        {
            // process the line...
            scanLine(line, numline);

            if(hadError)
                throw new Exception("Correct the compilation errors and then execute the project compiler.");

            line = file.ReadLine();
            numline++;
        }
    }

    private static void scanLine(string sourceCode, int numline)
    {
        Lexer Scanner = new Lexer(sourceCode, numline);

        List<Token> tokens = Scanner.tokens;

        foreach(Token token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}