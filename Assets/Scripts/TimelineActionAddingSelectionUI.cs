using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TimelineActionAddingSelectionUI : MonoBehaviour
{
    [SerializeField] string _filePathToActionsFolder;
    
    [SerializeField] GameObject _categorySelector;
    [SerializeField] GameObject _actionSelector;

    [SerializeField] GameObject _actionListObject;

    List<GameObject> _categorySelectors = new List<GameObject>();
    Dictionary<int,List<Object>> _categories = new Dictionary<int, List<Object>>();

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
            Sprite categorySprite = Sprite.Create(
                 tex,
                new Rect(0,0,tex.width,tex.height),
                Vector2.zero);

            newCategorySelector.GetComponentsInChildren<Image>()[1].sprite = categorySprite;
            int index = count;
            newCategorySelector.GetComponent<Button>().onClick.AddListener(() => OpenCategory(index));
            _categorySelectors.Add(newCategorySelector);

            List<Object> categoryActions = Resources.LoadAll($"{_filePathToActionsFolder}/{folderName}").ToList();
            _categories.Add(count, categoryActions);

        }
    }

    void OpenCategory(int index)
    {
        Debug.Log(index);
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
            Object action = _categories[index][i];

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
    }
}
