using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public abstract class Node 
    {
        public abstract BehaviourResult Update();

        // there there need ot be on exit enter here soemhwere??
        // and where does the blackbaord come into oplay ?
    }
}
