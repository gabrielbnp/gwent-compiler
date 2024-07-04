using System;
using static TokenType;

public class Parser
{
    private List<Token> tokens;

    public Parser(List<Token> tokens)
    {
        tokens.Add( new Token(NULL, "", null, 0) );
        this.tokens = tokens;
    }
}