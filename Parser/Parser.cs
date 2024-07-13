using static TokenType;

public class Parser
{
    private List<Token> tokens;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    private int current = 0; // token currently being read from List<Token> tokens

    private Boolean match(TokenType t)
    {
        return tokens[current].type == t;
    }

    private Boolean match(TokenType[] types)
    {
        foreach(TokenType t in types)
        {
            if(tokens[current].type == t)
                return true;
        }

        return false;
    }

    private Expr<object> primary()
    {
        if( match(TRUE) )
        {
            current++;
            return new LiteralExpr<object>(true);
        }
        else if( match(FALSE) )
        {
            current++;
            return new LiteralExpr<object>(false);
        }
        else if( match( new TokenType[] {NUMBER, STRING} ) )
        {
            current++;
            return new LiteralExpr<object>( tokens[current - 1].literal );
        }
        else if( match(LEFT_PAREN) ) // if the tokens match a '('
        {
            current++;
            Expr<object> expr = expression();

            // throw an error if no right parenthesis is found

            return new GroupingExpr<object>(expr);
        }

        return new LiteralExpr<object>(null);
    }

    private Expr<object> unary()
    {
        if( match( new TokenType[] {BANG, MINUS} ) )
        {
            Token oper = tokens[current];
            current++;
            Expr<object> right = unary();

            return new UnaryExpr<object>(oper, right);
        }

        return primary();
    }

    private Expr<object> factor()
    {
        Expr<object> left = unary();

        while( match( new TokenType[] {SLASH, STAR} ) )
        {
            Token oper = tokens[current];
            current++;

            Expr<object> right = unary();

            left = new BinaryExpr<object>(left, oper, right);
        }

        return left;
    }

    private Expr<object> sum()
    {
        Expr<object> left = factor();

        while( match( new TokenType[] {PLUS, MINUS, PLUS_STR} ) )
        {
            Token oper = tokens[current];
            current++;

            Expr<object> right = factor();

            left = new BinaryExpr<object>(left, oper, right);
        }

        return left;
    }

    private Expr<object> comparison()
    {
        Expr<object> left = sum();

        while( match( new TokenType[] {EQUAL_EQUAL, BANG_EQUAL, LESS, LESS_EQUAL, GREATER, GREATER_EQUAL} ) )
        {
            Token oper = tokens[current];
            current++;

            Expr<object> right = sum();

            left = new BinaryExpr<object>(left, oper, right);
        }

        return left;
    }

    private Expr<object> expression()
    {
        return comparison();
    }

    // Panic mode error recovery

    private void check(TokenType type, string errorMessage)
    {
        if( match(type) )
            current++;
        else
            Error.error(tokens[current], errorMessage);
    }
}