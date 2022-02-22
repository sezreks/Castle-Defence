using Assets.Scripts.Bases;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : Singleton<StoreManager>
{


    public void Start()
    {
        Invoke("OpenStore", .5f);
    }
    public void OpenStore()
    {
        ChangeCastleStoreStates();
        ChangecharStoreStates();
        ChangeweaponStoreStates();
    }
    public void SetIndexZero(RectTransform _canvas)
    {
        _canvas.transform.SetAsLastSibling();
    }


    #region Castle
    public RectTransform _castlePanel;
    public int _selectedCastleIndex;
    public void CastleAction()
    {
        var _castleData = PlayerData.Instance.GetCastleStoreState();
        var i = _selectedCastleIndex;
        var _tempObj = _castleData.FirstOrDefault(x => x.Index == i);
        if (_tempObj != null)
        {
            switch (_tempObj.StoreStates)
            {
                case StoreStates.Locked:
                    PlayerData.Instance.SetCastleStoreState(i, StoreStates.WillBuy);
                    break;

                case StoreStates.WillBuy:
                    if (PlayerData.Instance.Diamond >= 200)
                    {
                        PlayerData.Instance.SetCastleStoreState(i, StoreStates.Selected);
                        PlayerData.Instance.RemoveDiamond(200);

                    }
                    else
                    {

                        Debug.Log("Paran yok");
                    }
                    break;

                case StoreStates.NothingElseMatter:
                    PlayerData.Instance.SetCastleStoreState(i, StoreStates.Selected);
                    break;

            }
            ChangeCastleStoreStates();
        }
    }
    public void SetSelectedCastle(int i)
    {

        _selectedCastleIndex = i;
        var ItemList = _castlePanel.GetComponentsInChildren<Outline>();
        foreach (Outline item in ItemList)
        {
            item.enabled = false;
        }

        _castlePanel.transform.GetChild(i).GetComponent<Outline>().enabled = true;
        #region ActionButton configuration
        var actionButton = _castlePanel.transform.GetChild(_castlePanel.childCount - 1).gameObject;
        if (PlayerData.Instance._castleStoreState[i].StoreStates == StoreStates.Locked)
        {

            actionButton.transform.GetComponent<Button>().enabled = false;
            actionButton.transform.GetChild(0).gameObject.SetActive(false);
            actionButton.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (PlayerData.Instance._castleStoreState[i].StoreStates == StoreStates.WillBuy)
        {
            actionButton.transform.GetComponent<Button>().enabled = true;
            actionButton.transform.GetChild(0).gameObject.SetActive(true);
            actionButton.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (PlayerData.Instance._castleStoreState[i].StoreStates == StoreStates.NothingElseMatter || PlayerData.Instance._castleStoreState[i].StoreStates == StoreStates.Selected)
        {
            actionButton.transform.GetComponent<Button>().enabled = true;
            actionButton.transform.GetChild(0).gameObject.SetActive(false);
            actionButton.transform.GetChild(1).gameObject.SetActive(true);

        }
        #endregion

    }
    public void ChangeCastleStoreStates()
    {
        var _castleData = PlayerData.Instance.GetCastleStoreState();


        foreach (var _state in _castleData)
        {
            var _trans = _castlePanel.transform.GetChild(_state.Index);
            foreach (Transform _child in _trans)
            {
                _child.gameObject.SetActive(false);
            }

            int i = -1;
            switch (_state.StoreStates)
            {
                case StoreStates.Locked: i = 2; break;
                case StoreStates.WillBuy: i = 1; break;
                case StoreStates.Selected: i = 0; break;
                case StoreStates.NothingElseMatter: i = -1; break;
                default: i = -1; break;
            }
            if (i >= 0)
            {
                _castlePanel.transform.GetChild(_state.Index).GetChild(i).gameObject.SetActive(true);
            }
        }

    }
    #endregion 

    #region Weapon
    [Space(20)]
    public RectTransform _weaponPanel;
    public int _selectedweaponIndex;
    public void weaponAction()
    {
        var _weaponData = PlayerData.Instance.GetweaponStoreState();
        var i = _selectedweaponIndex;
        var _tempObj = _weaponData.FirstOrDefault(x => x.Index == i);
        if (_tempObj != null)
        {
            switch (_tempObj.StoreStates)
            {
                case StoreStates.Locked: PlayerData.Instance.SetweaponStoreState(i, StoreStates.WillBuy); break;
                case StoreStates.WillBuy:
                    if (PlayerData.Instance.Diamond >= 200)
                    {
                        PlayerData.Instance.SetweaponStoreState(i, StoreStates.Selected);
                        PlayerData.Instance.RemoveDiamond(200);

                    }
                    else
                    {

                        Debug.Log("Paran yok");
                    }
                    break;
                case StoreStates.NothingElseMatter: PlayerData.Instance.SetweaponStoreState(i, StoreStates.Selected); break;
            }
            ChangeweaponStoreStates();
        }
    }
    public void SetSelectedweapon(int i)
    {
        _selectedweaponIndex = i;
        var ItemList = _weaponPanel.GetComponentsInChildren<Outline>();
        foreach (Outline item in ItemList)
        {
            item.enabled = false;
        }

        _weaponPanel.transform.GetChild(i).GetComponent<Outline>().enabled = true;

        #region ActionButton configuration
        var actionButton = _weaponPanel.transform.GetChild(_weaponPanel.childCount - 1).gameObject;
        if (PlayerData.Instance._weaponStoreState[i].StoreStates == StoreStates.Locked)
        {

            actionButton.transform.GetComponent<Button>().enabled = false;
            actionButton.transform.GetChild(0).gameObject.SetActive(false);
            actionButton.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (PlayerData.Instance._weaponStoreState[i].StoreStates == StoreStates.WillBuy)
        {
            actionButton.transform.GetComponent<Button>().enabled = true;
            actionButton.transform.GetChild(0).gameObject.SetActive(true);
            actionButton.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (PlayerData.Instance._weaponStoreState[i].StoreStates == StoreStates.NothingElseMatter || PlayerData.Instance._weaponStoreState[i].StoreStates == StoreStates.Selected)
        {
            actionButton.transform.GetComponent<Button>().enabled = true;
            actionButton.transform.GetChild(0).gameObject.SetActive(false);
            actionButton.transform.GetChild(1).gameObject.SetActive(true);

        }
        #endregion
    }
    public void ChangeweaponStoreStates()
    {
        var _weaponData = PlayerData.Instance.GetweaponStoreState();


        foreach (var _state in _weaponData)
        {
            var _trans = _weaponPanel.transform.GetChild(_state.Index);
            foreach (Transform _child in _trans)
            {
                _child.gameObject.SetActive(false);
            }

            int i = -1;
            switch (_state.StoreStates)
            {
                case StoreStates.Locked: i = 2; break;
                case StoreStates.WillBuy: i = 1; break;
                case StoreStates.Selected: i = 0; break;
                case StoreStates.NothingElseMatter: i = -1; break;
                default: i = -1; break;
            }
            if (i >= 0)
            {
                _weaponPanel.transform.GetChild(_state.Index).GetChild(i).gameObject.SetActive(true);
            }
        }

    }
    #endregion

    #region Char
    [Space(20)]
    public RectTransform _charPanel;
    public int _selectedcharIndex;
    public void charAction()
    {
        var _charData = PlayerData.Instance.GetcharStoreState();
        var i = _selectedcharIndex;
        var _tempObj = _charData.FirstOrDefault(x => x.Index == i);
        if (_tempObj != null)
        {
            switch (_tempObj.StoreStates)
            {
                case StoreStates.Locked: PlayerData.Instance.SetcharStoreState(i, StoreStates.WillBuy); break;
                case StoreStates.WillBuy:
                    if (PlayerData.Instance.Diamond >= 200)
                    {
                        PlayerData.Instance.SetcharStoreState(i, StoreStates.Selected);
                        PlayerData.Instance.RemoveDiamond(200);

                    }
                    else
                    {

                        Debug.Log("Paran yok");
                    }

                    break;
                case StoreStates.NothingElseMatter: PlayerData.Instance.SetcharStoreState(i, StoreStates.Selected); break;
            }
            ChangecharStoreStates();
        }
    }
    public void SetSelectedchar(int i)
    {
        _selectedcharIndex = i;
        var ItemList = _charPanel.GetComponentsInChildren<Outline>();
        foreach (Outline item in ItemList)
        {
            item.enabled = false;
        }

        _charPanel.transform.GetChild(i).GetComponent<Outline>().enabled = true;

        #region ActionButton configuration
        var actionButton = _charPanel.transform.GetChild(_charPanel.childCount - 1).gameObject;
        if (PlayerData.Instance._charStoreState[i].StoreStates == StoreStates.Locked)
        {

            actionButton.transform.GetComponent<Button>().enabled = false;
            actionButton.transform.GetChild(0).gameObject.SetActive(false);
            actionButton.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (PlayerData.Instance._charStoreState[i].StoreStates == StoreStates.WillBuy)
        {
            actionButton.transform.GetComponent<Button>().enabled = true;
            actionButton.transform.GetChild(0).gameObject.SetActive(true);
            actionButton.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (PlayerData.Instance._charStoreState[i].StoreStates == StoreStates.NothingElseMatter || PlayerData.Instance._weaponStoreState[i].StoreStates == StoreStates.Selected)
        {
            actionButton.transform.GetComponent<Button>().enabled = true;
            actionButton.transform.GetChild(0).gameObject.SetActive(false);
            actionButton.transform.GetChild(1).gameObject.SetActive(true);

        }
        #endregion
    }
    public void ChangecharStoreStates()
    {
        var _charData = PlayerData.Instance.GetcharStoreState();


        foreach (var _state in _charData)
        {
            var _trans = _charPanel.transform.GetChild(_state.Index);
            foreach (Transform _child in _trans)
            {
                _child.gameObject.SetActive(false);
            }

            int i = -1;
            switch (_state.StoreStates)
            {
                case StoreStates.Locked: i = 2; break;
                case StoreStates.WillBuy: i = 1; break;
                case StoreStates.Selected: i = 0; break;
                case StoreStates.NothingElseMatter: i = -1; break;
                default: i = -1; break;
            }
            if (i >= 0)
            {
                _charPanel.transform.GetChild(_state.Index).GetChild(i).gameObject.SetActive(true);
            }
        }

    }
    #endregion

}
