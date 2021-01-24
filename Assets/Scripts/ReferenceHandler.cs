using UnityEngine;

public class ReferenceHandler : MonoBehaviour
{
    private GameManager _gameManagerScriptReference;
    private SkinManager _skinManagerScriptReference;
    private void Awake()
    {
        AssignReferences();
    }

    void AssignReferences()
    {
        GameObject manager = GameObject.Find("Game Manager");
        _gameManagerScriptReference = manager.GetComponent<GameManager>();
        _skinManagerScriptReference = manager.GetComponent<SkinManager>();
    }

    public GameManager GetGameManagerReference()
    {
        return _gameManagerScriptReference;
    }
    public SkinManager GetSkinManagerReference()
    {
        return _skinManagerScriptReference;
    }
}
