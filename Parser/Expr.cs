using System;

// classes for defining the expressions
public abstract class Expr<T>
{
    public abstract T accept(IExprVisitor<T> v);
}

public class LiteralExpr<T> : Expr<T>
{
    public object? value;

    public LiteralExpr(object? value)
    {
        this.value = value;
    }

    public override T accept(IExprVisitor<T> v)
    {
        return v.visit(this);
    }
}


public class UnaryExpr<T> : Expr<T>
{
    public Token oper;
    public Expr<T> expr;

    public UnaryExpr(Token oper, Expr<T> expr)
    {
        this.oper = oper;
        this.expr = expr;
    }

    public override T accept(IExprVisitor<T> v)
    {
        return v.visit(this);
    }
}

public class BinaryExpr<T> : Expr<T>
{
    public Expr<T> left;
    public Expr<T> right;
    public Token oper;

    public BinaryExpr(Expr<T> left, Token oper, Expr<T> right)
    {
        this.left = left;
        this.oper = oper;
        this.right = right;
    }

    public override T accept(IExprVisitor<T> v)
    {
        return v.visit(this);
    }
}

public class GroupingExpr<T> : Expr<T>
{
    public Expr<T> expr;

    public GroupingExpr(Expr<T> expr)
    {
        this.expr = expr;
    }

    public override T accept(IExprVisitor<T> v)
    {
        return v.visit(this);
    }
}