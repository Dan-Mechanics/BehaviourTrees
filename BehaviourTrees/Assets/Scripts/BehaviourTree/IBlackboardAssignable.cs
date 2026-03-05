using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTrees
{
    public interface IBlackboardAssignable
    {
        void Assign(Blackboard blackboard);
    }
}
