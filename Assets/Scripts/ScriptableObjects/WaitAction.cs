using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaitAction", fileName = "new WaitAction", order = 1)]
public class WaitAction : ScriptableObject, ITimeLineAction
{
    public float ActionDuration { get { return _actionDuration; } set { _actionDuration = value; } }
    public Sprite ActionSprite { get { return _actionSprite; } set { } }

    public string Name => _name;

    [SerializeField] string _name;
    [SerializeField] float _actionDuration;
    [SerializeField] Sprite _actionSprite;

    public void ExecuteAction(GameObject target)
    {
        //wait
    }
}
