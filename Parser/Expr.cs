using System;


// classes for defining the expressions
public abstract class Expr
{
    public abstract void accept(ExprVisitor v);
}

public class LiteralExpr : Expr
{
    public object? value;

    public LiteralExpr(object? value)
    {
        this.value = value;
    }

    public override void accept(ExprVisitor v)
    {
        v.visit(this);
    }
}

public class UnaryExpr : Expr
{
    public Token oper;
    public Expr expr;

    public UnaryExpr(Token oper, Expr expr)
    {
        this.oper = oper;
        this.expr = expr;
    }

    public override void accept(ExprVisitor v)
    {
        v.visit(this);
    }
}

public class BinaryExpr : Expr
{
    public Expr left;
    public Expr right;
    public Token oper;

    public BinaryExpr(Expr left, Token oper, Expr right)
    {
        this.left = left;
        this.oper = oper;
        this.right = right;
    }

    public override void accept(ExprVisitor v)
    {
        v.visit(this);
    }
}

public class GroupingExpr : Expr
{
    public Expr expr;

    public GroupingExpr(Expr expr)
    {
        this.expr = expr;
    }

    public override void accept(ExprVisitor v)
    {
        v.visit(this);
    }
}