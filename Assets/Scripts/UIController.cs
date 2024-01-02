using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Image chessBoardBg;
    public List<RectTransform> chessBoardIdsX;
    public List<RectTransform> chessBoardIdsY;

    [SerializeField]
    public UiPlayerInfo[] playersInfo;
    
    public Vector2 boardIdsAnchorOrigin;
    public Vector2 boardIdsAnchorSize;

    [Tooltip("DebugSection")]
    public bool debugMode;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        chessBoardBg.enabled = debugMode;
        if(debugMode)
        {
            chessBoardIdsInit();
        }
    }
    
    
    public void Initialize()
    {
        chessBoardIdsInit();

        foreach(var info in playersInfo) 
        {
            info.bindComponents();
        }
    }


    private void chessBoardIdsInit()
    {
        for(int i = 0; i < chessBoardIdsX.Count; i++) 
        {
            chessBoardIdsX[i].anchorMin = new Vector2(boardIdsAnchorOrigin.x, 0) + new Vector2(boardIdsAnchorSize.x * i, 0);
            chessBoardIdsX[i].anchorMax = new Vector2(boardIdsAnchorOrigin.x, 0) + new Vector2(boardIdsAnchorSize.x * i, 0);
        }
        for(int i = 0; i < chessBoardIdsY.Count; i++)
        {
            chessBoardIdsY[i].anchorMin = new Vector2(0, boardIdsAnchorOrigin.y) + new Vector2(0, boardIdsAnchorSize.y * i);   
            chessBoardIdsY[i].anchorMax = new Vector2(0, boardIdsAnchorOrigin.y) + new Vector2(0, boardIdsAnchorSize.y * i);   
        }
    }

    public UiPlayerInfo getSelfPlayer(int index)
    {
        if(!(playersInfo != null) && !(0 <= index && index < playersInfo.Length)) return null;

        return playersInfo[index];
    }
}

[System.Serializable]
public class UiPlayerInfo
{
    public RectTransform rectTrans;
    public TextMeshProUGUI name {get; private set;}
    public Image turnIndicator {get; private set;}

    public void bindComponents()
    {
        if(!rectTrans) return;

        name = rectTrans.Find("DisplayName").GetComponent<TextMeshProUGUI>();
        turnIndicator = rectTrans.Find("ImageTurn").GetComponent<Image>();
    }
}