using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public interface IBlackboardRequired
    {
        Blackboard Blackboard { get; set; }
        //void Assign(Blackboard blackboard);
    }
}
