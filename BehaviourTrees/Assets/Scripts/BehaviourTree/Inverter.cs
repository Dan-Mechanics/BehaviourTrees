namespace BehaviourTrees
{
    public class Inverter : Node
    {
        private readonly Node node;

        public Inverter(Node node)
        {
            this.node = node;
        }

        public override BehaviourResult Update()
        {
            BehaviourResult result = node.Update();
            if (result == BehaviourResult.Failed)
                result = BehaviourResult.Success;

            if (result == BehaviourResult.Success)
                result = BehaviourResult.Failed;

            return result;
        }

    }
}
