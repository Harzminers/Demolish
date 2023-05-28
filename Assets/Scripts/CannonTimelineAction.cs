using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTimelineAction 
{
    public ITimeLineAction Action { get; private set; }
    public float Duration { get; private set; }

    public CannonTimelineAction(ITimeLineAction action, float actionDuration = 0)
    {
        this.Action = action;
        Duration = actionDuration > 0 ? actionDuration : action.ActionDuration;
    }

    public void Execute(GameObject targetCannon)
    {
        Action.ExecuteAction(targetCannon);
    }
}
