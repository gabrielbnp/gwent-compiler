public interface IExprVisitor
{
    public void visit(LiteralExpr literalExpr);
    public void visit(UnaryExpr unaryExpr);
    public void visit(BinaryExpr binaryExpr);
    public void visit(GroupingExpr groupExpr);
}