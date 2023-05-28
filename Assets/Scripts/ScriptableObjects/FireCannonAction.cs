using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="FireCannonAction",fileName ="new FireCannonAction",order = 1)]
public class FireCannonAction : ScriptableObject,ITimeLineAction
{
    public float ActionDuration { get { return _actionDuration; } set { } }
    public Sprite ActionSprite { get { return _actionSprite; } set { } }

    public string Name => _name;

    [SerializeField] string _name;
    [SerializeField] float _actionDuration;
    [SerializeField] Sprite _actionSprite;
    [SerializeField] GameObject _projectile;

    public void ExecuteAction(GameObject target)
    {
        TimelineCannon cannon = target.GetComponent<TimelineCannon>();
        if(cannon == null) 
        {
            Debug.LogError("target object for Action " + this.name + " was invalid");
            return;
        }
        cannon.Launch(_projectile);
    }

    #if UNITY_EDITOR
    void OnValidate()
    {
        if (_projectile == null) return;
        string newName = $"FireCannon_{_projectile.name}";
        if (_name != newName) {
            string assetPath = AssetDatabase.GetAssetPath(this);
            string newPath = assetPath.Replace(name, newName);

            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.ImportAsset(newPath);
        }
    }
    #endif
}
