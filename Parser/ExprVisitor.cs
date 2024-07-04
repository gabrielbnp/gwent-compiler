using System;

public interface ExprVisitor
{
    public void visit(Literal literalExpr);
    public void visit(Unary unaryExpr);
    public void visit(Binary binaryExpr);
    public void visit(Grouping groupExpr);
}