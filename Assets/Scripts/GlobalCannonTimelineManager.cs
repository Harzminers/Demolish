using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCannonTimelineManager : MonoBehaviour
{
    GameObject SelectedObject => SelectableObject.SelectedObject;
    [SerializeField] FireCannonAction _cannonFireAction;
    [SerializeField] WaitAction _waitAction;
    [SerializeField] InputField _delayDurationInputField;
    [SerializeField] float _maxDelayTime = 20;
    CannonTimelineUI _ui;

    float _currentDelayActionDuration => float.TryParse(_delayDurationInputField.text.Replace(".",","),out float result)? Mathf.Clamp(result,0,_maxDelayTime) : 0;
    CannonTimeline selectedCannonTimeline;
    void Start()
    {
        SelectableObject.OnObjectSelectionComplete += UpdateUIifTargetChanged;
        _ui = GetComponent<CannonTimelineUI>();

    }
    public void AddCannonFireAction() 
    {
        selectedCannonTimeline?.AddActionToTimeline(_cannonFireAction,_cannonFireAction.ActionDuration);
    }
    public void AddWaitAction() 
    {
        if (_currentDelayActionDuration > 0)
        {
            selectedCannonTimeline?.AddActionToTimeline(_waitAction,_currentDelayActionDuration);
        }
    }
    public void RemoveLastAction() 
    {
        selectedCannonTimeline?.RemoveAction();
    }
    public void UpdateUIifTargetChanged()
    {
        if (SelectedObject == null) { return; }
        
        selectedCannonTimeline = SelectedObject.GetComponentInChildren<CannonTimeline>();
        if (selectedCannonTimeline != null)
        {
            _ui.UpdateTimelineUI(selectedCannonTimeline.GetTimeLine());
        }
        else 
        {
            _ui.UpdateTimelineUI(null);
        }
    }

}
