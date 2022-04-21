public class TargetDieTransition : Transition
{
    void Update()
    {
        if (Target == null)
            NeedTransit = true;
    }
}
