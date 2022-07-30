using UnityEngine;

public class ShieldBackSide : MonoBehaviour, IHitable
{
    public void Accept(IHitVisitor hitVisitor)
    {
        hitVisitor.Visit(this);
    }
}
