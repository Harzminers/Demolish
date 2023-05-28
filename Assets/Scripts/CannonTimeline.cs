using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonTimeline : MonoBehaviour
{
    TimelineCannon _cannon;
    List<GameObject> _actionsInTimeLine = new List<GameObject>();

    public CannonTimelineData LocalCannonTimeline;
    public UnityEvent<CannonTimelineData> onTimelineChange;   

    bool _currentlyExecutingTimeline = false;
    void Start()
    {
        LocalCannonTimeline = new CannonTimelineData();
        _cannon = GetComponent<TimelineCannon>();
        FindObjectOfType<BomberGameManager>().OnAttackCommence += ExecuteTimeline;
    }
    
    public void ExecuteTimeline() 
    {
        StartCoroutine(ExecuteTimeLineActions());
    }

    public void AddActionToTimeline(ITimeLineAction action,float duration = 0) 
    {
        if (_currentlyExecutingTimeline)
        {
            return;
        }
        CannonTimelineAction newAction = new CannonTimelineAction(action, duration);
        LocalCannonTimeline._timelineActions.Add(newAction);
        onTimelineChange?.Invoke(LocalCannonTimeline);
    }

    public void RemoveAction()
    {
        if (_currentlyExecutingTimeline) 
        {
            return;
        }

        if (LocalCannonTimeline._timelineActions.Count > 0)
        {
            LocalCannonTimeline._timelineActions.RemoveAt(LocalCannonTimeline._timelineActions.Count - 1);
            onTimelineChange?.Invoke(LocalCannonTimeline);
        }
    }

    public IEnumerator ExecuteTimeLineActions()
    {
        if (LocalCannonTimeline._timelineActions.Count <= 0) yield break;

        _currentlyExecutingTimeline = true;
        foreach (CannonTimelineAction action in LocalCannonTimeline._timelineActions) 
        {
            action.Execute(gameObject);
            yield return new WaitForSeconds(action.Duration);
        }
        _currentlyExecutingTimeline = false;
    }

    public CannonTimelineData GetTimeLine() 
    {
        return LocalCannonTimeline;
    }
}
