using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimelineActionAddingSelectionUI : MonoBehaviour
{
    [SerializeField] string _filePathToActionsFolder;
    [Header("UI Component Prefabs")]
    [SerializeField] GameObject _categorySelector;
    [SerializeField] GameObject _actionSelector;

    [SerializeField] GameObject _actionListObject;

    [Header("Fallback UI Visuals")]
    [SerializeField] Sprite _defaultCategoryIcon;

    public event Action<ITimeLineAction> onActionSelectedToAdd;

    List<GameObject> _categorySelectors = new List<GameObject>();
    Dictionary<int,List<UnityEngine.Object>> _categories = new Dictionary<int, List<UnityEngine.Object>>();

    ITimeLineAction _selectedAction;
    int _selectedCategory = 0;
    void Start()
    {
        string[] folderPaths = System.IO.Directory.GetDirectories($"Assets/Resources/{_filePathToActionsFolder}");
        int count = 0;
        
        foreach(string path in folderPaths)
        {
            count++;

            string folderName = System.IO.Path.GetFileNameWithoutExtension(path);
            
            GameObject newCategorySelector = Instantiate(_categorySelector, transform);

            Texture2D tex = (Texture2D)Resources.Load($"CategoryIcons/{folderName}_CategoryIcon");
            Sprite categorySprite = tex? Sprite.Create(
                 tex,
                new Rect(0,0,tex.width,tex.height),
                Vector2.zero) : _defaultCategoryIcon;

            newCategorySelector.GetComponentsInChildren<Image>()[1].sprite = categorySprite;
            int index = count;
            newCategorySelector.GetComponent<Button>().onClick.AddListener(() => OpenCategory(index));
            _categorySelectors.Add(newCategorySelector);

            List<UnityEngine.Object> categoryActions = Resources.LoadAll($"{_filePathToActionsFolder}/{folderName}").ToList();
            _categories.Add(count, categoryActions);

        }
    }

    void OpenCategory(int index)
    {
        if (_categories[index] == null) Debug.LogError($"Tried to access non existent TimelineAction Category on `{name}`");

        if (_selectedCategory == index) return;

        _selectedCategory = index;

        int currentListElementCount = _actionListObject.transform.childCount;
        
        for (int i = 0; i < currentListElementCount; i++)
        {
            _actionListObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        for(int i = 0; i< _categories[index].Count; i++)
        {
            UnityEngine.Object action = _categories[index][i];

            GameObject newAction = currentListElementCount <= i ? Instantiate(_actionSelector,_actionListObject.transform) : _actionListObject.transform.GetChild(i).gameObject;
            newAction.SetActive(true);
            ITimeLineAction actionObject = (ITimeLineAction)action;
            newAction.GetComponentInChildren<TMPro.TMP_Text>().text     = actionObject.Name;
            newAction.GetComponentsInChildren<Image>()[1].sprite            = actionObject.ActionSprite;
            newAction.GetComponentInChildren<Button>().onClick.AddListener(() => SelectAction(actionObject));
        }
    }

    void SelectAction(ITimeLineAction action)
    {
        _selectedAction = action;
        onActionSelectedToAdd?.Invoke(action);
    }
}
