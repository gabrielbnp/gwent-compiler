using System;

public abstract class Expr
{

}

public class Literal : Expr
{
    public required object value;

    public Literal(object value)
    {
        this.value = value;
    }
}

public class Unary : Expr
{
    public required Token oper;
    public required Expr expr;

    public Unary(Token oper, Expr expr)
    {
        this.oper = oper;
        this.expr = expr;
    }
}

public class Binary : Expr
{
    public required Expr left;
    public required Expr right;
    public required Token oper;

    public Binary(Expr left, Token oper, Expr right)
    {
        this.left = left;
        this.right = right;
        this.oper = oper;
    }
}

public class Grouping : Expr
{
    public required Expr expr;

    public Grouping(Expr expr)
    {
        this.expr = expr;
    }
}