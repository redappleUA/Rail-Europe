using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesData", menuName = "Rail Europe/Resources Data", order = 1)]
public class ResourcesData : ScriptableObject
{
    [SerializeField] private int _railCountSerialize;
    [SerializeField] private int _bridgeCountSerialize;
    [SerializeField] private int _trainCountSerialize;

    private int _railCount;
    private int _bridgeCount;
    private int _trainCount;
    public static int RailCount { get { return Instance._railCount; } set { Instance._railCount = value; } }
    public static int BridgeCount { get { return Instance._bridgeCount; } set { Instance._bridgeCount = value; } }
    public static int TrainCount { get { return Instance._trainCount; } set { Instance._trainCount = value; } }

    private static ResourcesData _instance;
    private static ResourcesData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ResourcesData>("ResourcesData");
                if (_instance == null)
                {
                    Debug.LogError("ResourcesData asset not found. Create a ResourcesData asset using the Create menu.");
                }
                else
                {
                    _instance._railCount = _instance._railCountSerialize;
                    _instance._bridgeCount = _instance._bridgeCountSerialize;
                    _instance._trainCount = _instance._trainCountSerialize;
                }
            }
            return _instance;
        }
    }
}