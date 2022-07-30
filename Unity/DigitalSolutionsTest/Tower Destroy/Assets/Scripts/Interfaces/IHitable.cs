public interface IHitable
{
    void Accept(IHitVisitor hitVisitor);
}
