using System;
using static TokenType;

public class Lexer
{
    private string sourceCode;
    private int numline;
    public List<Token> tokens = new List<Token>();

    private int start = 0;
    private int end = 0;

    public Lexer(string sourceCode, int numline)
    {
        this.sourceCode = sourceCode;
        this.numline = numline;

        tokenizeCode();
    }

    private void tokenizeCode()
    {
        sourceCode += " ";

        while( end <= sourceCode.Length - 2 )
        {
            if(sourceCode[end] == ' ')
            {
                end++;
                start =  end;
                continue;
            }

            string possibleToken = readWord();

            identToken(possibleToken);

            start = end;
        }
    }

    // functions to help tokenizeCode()

    private char  nextChar { get { return sourceCode[end]; } }

    private bool isAlpha(char c)
    {
        if( ('a' <= c) && (c <= 'z') )
            return true;

        if( ('A' <= c) && (c <= 'Z') )
            return true;

        if(c == '_')
            return true;

        return false;
    }

    private bool isDigit(char c)
    {
        return ('0' <= c) && (c <= '9');
    }

    private string readWord()
    {
        string word = "" + sourceCode?[end];
        end++;

        #pragma warning disable CS8602 // Dereference of a possibly null reference.

        if ( isAlpha(sourceCode[start]) ) // possible identifier or keyword detected
        {
            while( (end <= sourceCode.Length - 2) && ( isAlpha(sourceCode[end]) ) )
            {
                word += sourceCode[end];
                end++;
            }
        }
        else if( isDigit(sourceCode[start]) ) // possible number detected
        {
            while( (end <= sourceCode.Length - 2) && ( isDigit(sourceCode[end]) ) )
            {
                word += sourceCode[end];
                end++;
            }
        }
        
        return word;
    }

    private void addToken(TokenType type, string lexeme, object? literal)
    {
        tokens.Add( new Token(type, lexeme, literal, numline) );
    }

    private bool isKeyword(string possibleKeyWord)
    {
        switch(possibleKeyWord)
        {
            case "bool":
                addToken(BOOL, "bool", null);
                return true;
            case "true":
                addToken(TRUE, "true", null);
                return true;
            case "false":
                addToken(FALSE, "false", null);
                return true;
            case "if":
                addToken(IF, "if", null);
                return true;
            case "else":
                addToken(ELSE, "else", null);
                return true;
            case "for":
                addToken(FOR, "for", null);
                return true;
            case "in":
                addToken(IN, "in", null);
                return true;
            case "while":
                addToken(WHILE, "while", null);
                return true;

            default:
                return false;
        }
    }

    private void identToken(string possibleToken)
    {
        switch(possibleToken)
        {
            case "(":
                addToken(LEFT_PAREN, "(", null);
                break;
            case ")":
                addToken(RIGHT_PAREN, ")", null);
                break;
            case ";":
                addToken(SEMICOLON, ";", null);
                break;
            case "*":
                addToken(STAR, "*", null);
                break;
            case "+":
                addToken(PLUS, "+", null);
                break;
            case "-":
                addToken(MINUS, "-", null);
                break;
            case "@":
                addToken(PLUS_STR, "@", null);
                break;
            
            case "<":
                if(nextChar == '=')
                {
                    addToken(LESS_EQUAL, "<=", null);
                    end++;
                }
                else
                {
                    addToken(LESS, "<", null);
                }
                break;

            case ">":
                if(nextChar == '=')
                {
                    addToken(GREATER_EQUAL, ">=", null);
                    end++;
                }
                else
                {
                    addToken(GREATER, ">", null);
                }
                break;

            case "=":
                if(nextChar == '=')
                {
                    addToken(EQUAL_EQUAL, "==", null);
                    end++;
                }
                else
                {
                    addToken(EQUAL, "=", null);
                }
                break;

            case "!":
                if(nextChar == '=')
                {
                    addToken(BANG_EQUAL, "!=", null);
                    end++;
                }
                else
                {
                    addToken(BANG, "!", null);
                }
                break;

            case "&":
                if(nextChar == '&')
                {
                    addToken(AND, "&&", null);
                    end++;
                }
                else
                {
                    Error.error(numline, "Expected &.", sourceCode);
                }
                break;

            case "|":
                if(nextChar == '|')
                {
                    addToken(OR, "||", null);
                    end++;
                }
                else
                {
                    Error.error(numline, "Expected ||.", sourceCode);
                }
                break;

            case "/":
                if(nextChar == '/') // comment detected. ignore the whole line
                    end = sourceCode.Length;
                else
                    addToken(SLASH, "/", null);
                break;
            case "\"":
                bool foundQuote = false;

                while( (foundQuote == false) && (end <= sourceCode.Length - 2) )
                {
                    if(sourceCode[end] == '\"')
                        foundQuote = true;
                    else
                        end++;
                }

                if(foundQuote == false)
                {
                    Error.error(numline, "Expected \" character. Unterminated string.", sourceCode);
                }
                else
                {
                    string str = sourceCode.Substring(start + 1, end - start - 1);

                    addToken(STRING, str, str);
                    end++;
                }

                break;

            default:

                if( isDigit(possibleToken[0]) )
                {
                    addToken(NUMBER, possibleToken, Int64.Parse(possibleToken) );
                }
                else if( isAlpha(possibleToken[0]) )
                {
                    if( isKeyword(possibleToken) == false )
                        addToken(IDENTIFIER, possibleToken, null);
                }
                else
                {
                    Error.error(numline, "Unexpected character.", sourceCode);
                }

                break;
        }
    }
}