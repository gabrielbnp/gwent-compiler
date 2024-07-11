using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

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
    
    // to locate the token in the source code
    public int numLine { get; private set; }
    public int numColumn { get; private set; }
    public string sourceCode { get; private set; }

    public Token(TokenType type, string lexeme, object? literal, int numLine, int numColumn, string sourceCode)
    {
        this.type = type;
        this.lexeme = lexeme;
        this.literal = literal;

        this.numLine = numLine;
        this.numColumn = numColumn;
        this.sourceCode = sourceCode;
    }

    public override string ToString()
    {
        if(literal == null)
            return "[" + type + "] \'" + lexeme + "\'";

        return "[" + type + "] \'" + lexeme + "\' " + literal;
    }
}