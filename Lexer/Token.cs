public enum TokenType
{
    // single-character tokens
    LEFT_PAREN, RIGHT_PAREN,
    SEMICOLON, STAR, PLUS, MINUS, PLUS_STR,

    // one or two character tokens
    SLASH, BANG, BANG_EQUAL,
    LESS, LESS_EQUAL, GREATER, GREATER_EQUAL, EQUAL, EQUAL_EQUAL,
    AND, OR,

    // literals
    IDENTIFIER, NUMBER, STRING,

    // keywords
    BOOL, TRUE, FALSE, IF, ELSE, FOR, IN, WHILE,

    NULL
};

public class Token
{
    public TokenType type;
    public string lexeme;
    public object? literal;
    
    protected int numline;

    public Token(TokenType type, string lexeme, object? literal, int numline)
    {
        this.type = type;
        this.lexeme = lexeme;
        this.literal = literal;
        this.numline = numline;
    }

    public override string ToString()
    {
        if(literal == null)
            return "[" + type + "] \'" + lexeme + "\'";

        return "[" + type + "] \'" + lexeme + "\' " + literal;
    }
}