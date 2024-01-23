using System;
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

    public RectTransform considerTimeMenu;
    public List<Button> considerTimeOptions;

    public GridLayoutGroup promotionMenu;
    public GameObject promotionOptionPrefab;
    public Dictionary<chess.chessType, UiPromotionInfo> promotionOptions;
    


    [Tooltip("DebugSection")]
    public bool debugMode;
    public TextMeshProUGUI debugMoveHistory;

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

        promotionOptions = new Dictionary<chess.chessType, UiPromotionInfo>();

        debugMoveHistory.text = "";
    }

    private void setUiActive(GameObject menu, bool isActive)
    {
        if(menu == null) return;
        menu.SetActive(isActive);
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

    public void setConsiderTimeMenuActive(bool isActive)
    {
        setUiActive(considerTimeMenu.gameObject, isActive);
    }

    public void setConsiderTimeOptions(int index, string display, Action<int> onClick)
    {
        if(index < 0 
        || considerTimeOptions.Count <= index
        || considerTimeOptions[index] == null
        ) return;
        
        considerTimeOptions[index].GetComponentInChildren<TextMeshProUGUI>().text = display;
        considerTimeOptions[index].onClick.RemoveAllListeners();
        considerTimeOptions[index].onClick.AddListener(delegate() {onClick?.Invoke(index);});
    }

    public void setPromotionMenuActive(bool isActive, chess piece)
    {
        if(isActive && piece != null)
        {
            Vector2Int grid2 = Gamedev.instance.ba.getGrid2(piece);
            Vector3 position = new Vector3(chessBoardIdsX[grid2.x].position.x, chessBoardIdsY[grid2.y].position.y, chessBoardIdsX[grid2.x].position.z);
            promotionMenu.transform.position = position;
            if(grid2.y < 4)
            {
                promotionMenu.startCorner = GridLayoutGroup.Corner.LowerLeft;
                promotionMenu.childAlignment = TextAnchor.LowerCenter;
            }
            else
            {                    
                promotionMenu.startCorner = GridLayoutGroup.Corner.UpperLeft;
                promotionMenu.childAlignment = TextAnchor.UpperCenter;
            }
        }
        setUiActive(promotionMenu.gameObject, isActive);
    }

    public void setPromotionOptions(chess.chessType chessType, string display, Action<chess.chessType> onClick)
    {
        if(promotionOptions == null
        || promotionMenu == null
        || promotionOptionPrefab == null
        ) return;
        
        UiPromotionInfo promotionInfo = null;
        if(!promotionOptions.ContainsKey(chessType))
        {
            GameObject tmp = Instantiate(promotionOptionPrefab, Vector3.zero, Quaternion.identity, promotionMenu.transform);
            promotionInfo = new UiPromotionInfo().bindComponents(tmp.GetComponent<RectTransform>());
            promotionOptions.Add(chessType, promotionInfo);
        }
        else promotionOptions.TryGetValue(chessType, out promotionInfo);
            
        promotionInfo.name.text = display;
        promotionInfo.button.onClick.RemoveAllListeners();
        promotionInfo.button.onClick.AddListener(delegate() {onClick?.Invoke(chessType);});
    }
}

[System.Serializable]
public class UiPlayerInfo
{
    public RectTransform rectTrans;
    public TextMeshProUGUI name {get; private set;}
    public Image turnIndicator {get; private set;}
    public TextMeshProUGUI considerTime {get; private set;}

    public void bindComponents()
    {
        if(!rectTrans) return;

        name = rectTrans.Find("DisplayName").GetComponent<TextMeshProUGUI>();
        turnIndicator = rectTrans.Find("ImageTurn").GetComponent<Image>();
        considerTime = rectTrans.Find("considerTime").GetComponent<TextMeshProUGUI>();
    }
}


[System.Serializable]
public class UiPromotionInfo
{
    public RectTransform rectTrans;
    public Button button {get; private set;}
    public TextMeshProUGUI name {get; private set;}

    public UiPromotionInfo bindComponents(RectTransform rect)
    {
        rectTrans = rect;
        if(!rectTrans) return this;

        button = rectTrans.GetComponent<Button>();
        name = rectTrans.Find("DisplayName").GetComponent<TextMeshProUGUI>();

        return this;
    }
}