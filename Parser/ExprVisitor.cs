public interface IExprVisitor<T>
{
    public T visit(LiteralExpr<T> literalExpr);
    public T visit(UnaryExpr<T> unaryExpr);
    public T visit(BinaryExpr<T> binaryExpr);
    public T visit(GroupingExpr<T> groupExpr);
}