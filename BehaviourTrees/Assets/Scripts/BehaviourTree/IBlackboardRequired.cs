namespace BehaviourTrees
{
    public interface IBlackboardRequired
    {
        Blackboard Blackboard { get; set; }
        void AssignBlackboard(Blackboard blackboard);
    }
}
