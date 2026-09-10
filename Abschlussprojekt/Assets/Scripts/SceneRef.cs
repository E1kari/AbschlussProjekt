using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SceneRef", menuName = "Scenes/Scene Reference")]
public class SceneRef : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset;
#endif

    [SerializeField] public string sceneName;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
        else
        {
            sceneName = string.Empty;
            Debug.Log("Scene Asset is null");
        }
    }
#endif
}