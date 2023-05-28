using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonTimelineUI : MonoBehaviour
{
    [SerializeField] GameObject _timelineActionObject;
    [SerializeField] Transform _timelineActionUILayoutGroup;
    [SerializeField] Text _timelineHeadText;

    CannonTimeline currentlyDisplayedTimeline;
    void Awake()
    {
        SelectableObject.OnObjectSelectionComplete += OnSelection;
    }
    void OnSelection()
    {
        CannonTimeline selectedCannonTimeline = SelectableObject.SelectedObject.GetComponent<CannonTimeline>();
        if (selectedCannonTimeline != null && selectedCannonTimeline != currentlyDisplayedTimeline) {
            currentlyDisplayedTimeline?.onTimelineChange.RemoveListener(UpdateTimelineUI);
            currentlyDisplayedTimeline = selectedCannonTimeline;
            selectedCannonTimeline.onTimelineChange.AddListener(UpdateTimelineUI);
        }
    }

    public void UpdateTimelineUI(CannonTimelineData data)
    {
        for (int i = 0; i<_timelineActionUILayoutGroup.childCount;i++)
        {
            Destroy(_timelineActionUILayoutGroup.GetChild(i).gameObject);
        }
        if (data == null)
        {
            _timelineHeadText.text = "Timeline: no data";
            return;
        }

        int actions = data._timelineActions.Count;
        int projectilesLoaded = 0;
        if (actions > 0)
        {
            
            foreach (CannonTimelineAction action in data._timelineActions)
            {
                GameObject newAction = Instantiate(_timelineActionObject, _timelineActionUILayoutGroup);
                newAction.GetComponentInChildren<Image>().sprite = action.Action.ActionSprite;
                newAction.GetComponentInChildren<Text>().text = action.Duration.ToString() + "s";

                if(action.Action is FireCannonAction) 
                {
                    projectilesLoaded++;
                }
            }
            _timelineHeadText.text = "Timeline: " + projectilesLoaded.ToString() + " Projectiles loaded";
        }
        else 
        {
            _timelineHeadText.text = "Timeline: no data";
        }
    }

    void OnDestroy()
    {
        currentlyDisplayedTimeline?.onTimelineChange.RemoveListener(UpdateTimelineUI);    
    }
}
