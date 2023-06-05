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
    public static int RailCount { get { return _Instance._railCount; } set { _Instance._railCount = value; } }
    public static int BridgeCount { get { return _Instance._bridgeCount; } set { _Instance._bridgeCount = value; } }
    public static int TrainCount { get { return _Instance._trainCount; } set { _Instance._trainCount = value; } }

    private static ResourcesData m_instance;
    
    /// <summary>
    /// Singletone instance of resources data
    /// </summary>
    private static ResourcesData _Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = Resources.Load<ResourcesData>("ResourcesData");
                if (m_instance == null)
                {
                    Debug.LogError("ResourcesData asset not found. Create a ResourcesData asset using the Create menu.");
                }
                else
                {
                    m_instance._railCount = m_instance._railCountSerialize;
                    m_instance._bridgeCount = m_instance._bridgeCountSerialize;
                    m_instance._trainCount = m_instance._trainCountSerialize;
                }
            }
            return m_instance;
        }
    }

    public static void ResetCounts() => m_instance = null;
}