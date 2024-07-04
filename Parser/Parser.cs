using System;
using System.Security.Cryptography;
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
        else if( check(LEFT_PAREN) ) // if the tokens match a '('
        {
            Expr expr = expression();
            // throw an error if no right parenthesis is found
            // ...
            
            return new GroupingExpr(expr);
        }

        return new LiteralExpr(null);
    }

    private Expr unaryExpr()
    {
        if( check( new List<TokenType>{BANG, MINUS} ) )
        {
            Token oper = tokens[current];
            current++;
            Expr right = unaryExpr();

            return new UnaryExpr(oper, right);
        }

        return primaryExpr();
    }

    private Expr factorExpr()
    {
        Expr left = unaryExpr();

        while( check( new List<TokenType>{SLASH, STAR} ) )
        {
            current++;
            Token oper = tokens[current - 1];
            Expr right = unaryExpr();

            left = new BinaryExpr(left, oper, right);
        }

        return left;
    }

    private Expr sumExpr()
    {
        Expr left = factorExpr();

        while( check( new List<TokenType>{PLUS, MINUS} ) )
        {
            current++;
            Token oper = tokens[current - 1];
            Expr right = factorExpr();

            left = new BinaryExpr(left, oper, right);
        }

        return left;
    }

    private Expr comparisonExpr()
    {
        Expr left = sumExpr();

        while( check( new List<TokenType>{LESS, LESS_EQUAL, GREATER, GREATER_EQUAL} ) )
        {
            current++;
            Token oper = tokens[current - 1];
            Expr right = sumExpr();

            left = new BinaryExpr(left, oper, right);
        }

        return left;
    }

    private Expr equalityExpr()
    {
        Expr left = comparisonExpr();

        while( check( new List<TokenType>{BANG_EQUAL, EQUAL_EQUAL} ) )
        {
            current++;
            Token oper = tokens[current - 1];
            Expr right = comparisonExpr();

            left = new BinaryExpr(left, oper, right);
        }

        return left;
    }

    private Expr expression()
    {
        return equalityExpr();
    }
}