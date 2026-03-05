using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Node 
    {

        public virtual Status Process()
        {
            return Status.Failed;
        }

        // there there need ot be on exit enter here soemhwere??
        // and where does the blackbaord come into oplay ?
    }
}
