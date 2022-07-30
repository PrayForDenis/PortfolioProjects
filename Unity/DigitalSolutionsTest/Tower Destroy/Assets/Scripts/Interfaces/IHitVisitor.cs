public interface IHitVisitor
{
    void Visit(Tower tower);
    void Visit(Bullet bullet);
    void Visit(Shield shield);
    void Visit(ShieldBackSide shieldBackSide);
}
