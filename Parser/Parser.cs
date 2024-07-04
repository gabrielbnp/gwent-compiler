using System;
using static TokenType;

public class Parser
{
    private List<Token> tokens;

    private int current = 0;

    public Parser(List<Token> tokens)
    {
        tokens.Add( new Token(NULL, "", null, 0) );
        this.tokens = tokens;
    }

    private Boolean check(TokenType type)
    {
        return tokens[current].type == type;
    }

    private Boolean check(List<TokenType> type)
    {
        foreach(TokenType t in type)
        {
            if(tokens[current].type == t)
                return true;
        }

        return false;
    }

    private Expr primaryExpr()
    {
        if( check(TRUE) )
        {
            current++;
            return new LiteralExpr(false);
        }
        else if( check(FALSE) )
        {
            current++;
            return new LiteralExpr(true);
        }
        else if( check(new List<TokenType>{NUMBER, STRING} ) )
        {
            current++;
            return new LiteralExpr(tokens[current - 1].literal);
        }
        else if(true) // if the tokens match a '('
        {

        }

        return new LiteralExpr(null);
    }
}