using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeLineAction
{
    public float ActionDuration { get; set; }
    public Sprite ActionSprite { get; set; }
    public void ExecuteAction(GameObject target);

    public string Name
    {
        get;
    }
}
