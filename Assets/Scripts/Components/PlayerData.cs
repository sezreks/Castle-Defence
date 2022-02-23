using Assets.Scripts.Bases;
using Components;
using Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    [SerializeField] private List<GameObject> arenas;
    private int diamonds
    {
        get
        {
            return GetData<int>("Diamonds", 0);
        }
        set
        {

            SetData("Diamonds", value.ToString());
        }
    }
    public int curArena
    {
        get
        {
            return GetData<int>("curArena", 0);
        }
        set
        {

            SetData("curArena", value.ToString());
        }
    }

    public int curUnlockable
    {
        get
        {
            return GetData<int>("curUnlockable", 0);
        }
        set
        {

            SetData("curUnlockable", value.ToString());
        }
    }
    public float unlockableFill
    {

        get
        {
            return GetData<float>("unlockableFill", 0);
        }
        set
        {

            SetData("unlockableFill", value.ToString());
        }

    }
    public int curLevel
    {
        get
        {
            return GetData<int>("curLevel", 0);
        }
        set
        {

            SetData("curLevel", value.ToString());
        }
    }

    #region Arena
    public void OpenArena(int i)
    {
        foreach (GameObject arena in arenas)
        {
            arena.SetActive(false);

        }


        arenas[i].SetActive(true);

    }


    #endregion

    #region Castle
    public Transform _castle;
    public RectTransform _castlePanel;
    public int _castleLevel
    {
        get
        {
            return GetData<int>("CastleLevel", 0);
        }
        set
        {
            CastleActions(value);
            SetData("CastleLevel", value.ToString());
        }
    }
    public List<StoreIndexData> _castleStoreState
    {
        get
        {
            var dataJson = GetData<string>("CastleStore", "");
            if (dataJson == "")
                return null;
            return JsonHelper.FromJson<StoreIndexData>(dataJson)?.ToList();
        }
        set
        {
            var dataJson = JsonHelper.ToJson(value.ToArray());
            SetData("CastleStore", dataJson);
        }
    }
    public void SetCastleLevel(int i)
    {
        _castleLevel = i;
        CastleActions(i);
    }
    private void CastleActions(int? value = null)
    {
        if (!value.HasValue)
            value = _castleLevel;

        CloseAllObjectVisibility(_castle);
        OpenSelectedObjectVisibility(_castle, value.Value);
    }
    public void SetCastleStoreState(int i, StoreStates state)
    {
        var data = _castleStoreState;
        if (state == StoreStates.Selected)
            data.ForEach(x =>
            {
                if (x.StoreStates == StoreStates.Selected)
                {
                    x.StoreStates = StoreStates.NothingElseMatter;
                }
            });


        if (data.Exists(x => x.Index == i))
        {
            if (state == StoreStates.Selected)
                SetCastleLevel(i);
            data.First(x => x.Index == i).StoreStates = state;
        }

        _castleStoreState = data;
    }
    public List<StoreIndexData> GetCastleStoreState() => Instance._castleStoreState;
    #endregion

    #region Weapon
    public Transform _weapon;
    public RectTransform _weaponPanel;
    private int _weaponLevel
    {
        get
        {
            return GetData<int>("weaponLevel", 0);
        }
        set
        {
            weaponActions(value);
            SetData("weaponLevel", value.ToString());
        }
    }
    public List<StoreIndexData> _weaponStoreState
    {
        get
        {
            var dataJson = GetData<string>("weaponStore", "");
            return JsonHelper.FromJson<StoreIndexData>(dataJson)?.ToList();
        }
        set
        {
            var dataJson = JsonHelper.ToJson(value.ToArray());
            SetData("weaponStore", dataJson);
        }
    }
    public void SetweaponLevel(int i)
    {
        _weaponLevel = i;
        weaponActions(i);
    }
    private void weaponActions(int? value = null)
    {
        if (!value.HasValue)
            value = _weaponLevel;

        CloseAllObjectVisibility(_weapon);
        OpenSelectedObjectVisibility(_weapon, value.Value);
    }
    public void SetweaponStoreState(int i, StoreStates state)
    {
        var data = _weaponStoreState;
        if (state == StoreStates.Selected)
            data.ForEach(x =>
            {
                if (x.StoreStates == StoreStates.Selected)
                {
                    x.StoreStates = StoreStates.NothingElseMatter;
                }
            });


        if (data.Exists(x => x.Index == i))
        {
            if (state == StoreStates.Selected)
                SetweaponLevel(i);
            data.First(x => x.Index == i).StoreStates = state;
        }

        _weaponStoreState = data;
    }
    public List<StoreIndexData> GetweaponStoreState() => Instance._weaponStoreState;
    #endregion

    #region Char
    public Transform _char;
    public RectTransform _charPanel;
    private int _charLevel
    {
        get
        {
            return GetData<int>("charLevel", 0);
        }
        set
        {
            charActions(value);
            SetData("charLevel", value.ToString());
        }
    }
    public List<StoreIndexData> _charStoreState
    {
        get
        {
            var dataJson = GetData<string>("charStore", "");
            return JsonHelper.FromJson<StoreIndexData>(dataJson)?.ToList();
        }
        set
        {
            var dataJson = JsonHelper.ToJson(value.ToArray());
            SetData("charStore", dataJson);
        }
    }
    public void SetcharLevel(int i)
    {
        _charLevel = i;
        charActions(i);
    }
    private void charActions(int? value = null)
    {
        if (!value.HasValue)
            value = _charLevel;

        CloseAllObjectVisibility(_char);
        OpenSelectedObjectVisibility(_char, value.Value);
    }
    public void SetcharStoreState(int i, StoreStates state)
    {
        var data = _charStoreState;
        if (state == StoreStates.Selected)
            data.ForEach(x =>
            {
                if (x.StoreStates == StoreStates.Selected)
                {
                    x.StoreStates = StoreStates.NothingElseMatter;
                }
            });


        if (data.Exists(x => x.Index == i))
        {
            if (state == StoreStates.Selected)
                SetcharLevel(i);
            data.First(x => x.Index == i).StoreStates = state;
        }

        _charStoreState = data;
    }
    public List<StoreIndexData> GetcharStoreState() => Instance._charStoreState;
    #endregion

    #region Utils

    private void CloseAllObjectVisibility(Transform _object)
    {
        foreach (Transform child in _object)
        {
            child.gameObject.SetActive(false);
        }
    }
    private void OpenSelectedObjectVisibility(Transform _object, int i)
    {
        if (_object.childCount >= i)
        {
            _object.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void SetData(string Key, string value)
    {

        PlayerPrefs.SetString(Key, EndlessEncryption.Encrypt(value.ToString()));
    }
    private T GetData<T>(string Key, T start)
    {
        var data = PlayerPrefs.GetString(Key);
        if (!string.IsNullOrEmpty(data))
            return EndlessEncryption.Decrypt(data).ConvertTo<T>();
        else PlayerPrefs.SetString(Key, EndlessEncryption.Encrypt(start.ToString()));
        return start;

    }

    #endregion

    #region Diamond
    public int Diamond { get => diamonds; }
    public void AddDiamond(int e)
    {
        diamonds += e;
    }
    public void RemoveDiamond(int e)
    {

        diamonds -= e;
        if (diamonds < 0)
            diamonds = 0;

    }

    #endregion

    protected override void Awake()
    {


        //ResetStore();
        //ResetUnlockables();


        //_castle = GameObject.Find("MainTower/TowerTop/Castles").transform;
        //_weapon = GameObject.Find("MainTower/TowerTop/WeaponsComplete/WeaponChair/WeaponDeck/Weapons").transform;
        //_char = GameObject.Find("Player/CharacterMesh").transform;


        if (PlayerPrefs.GetInt("HasPlayed") == 0)
        {
            //ResetStore();
            ResetUnlockables();
            PlayerPrefs.SetInt("HasPlayed", 1);
        }



        base.Awake();
    }
    public void Start()
    {
        OpenArena(curArena);
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    CastleActions();
        //    weaponActions();
        //    charActions();
        //}
    }

    public void ResetStore()
    {
        List<StoreIndexData> _temp = new List<StoreIndexData>();
        foreach (Transform item in _castlePanel)
        {
            if (item.name != "ActionButton")
            {
                if (item.GetSiblingIndex() == 0)
                    _temp.Add(new StoreIndexData() { Index = item.GetSiblingIndex(), StoreStates = StoreStates.Selected });
                else
                    _temp.Add(new StoreIndexData() { Index = item.GetSiblingIndex(), StoreStates = StoreStates.Locked });
            }
        }
        _castleStoreState = _temp;

        _temp = new List<StoreIndexData>();
        foreach (Transform item in _weaponPanel)
        {
            if (item.name != "ActionButton")
            {
                if (item.GetSiblingIndex() == 0)
                    _temp.Add(new StoreIndexData() { Index = item.GetSiblingIndex(), StoreStates = StoreStates.Selected });
                else
                    _temp.Add(new StoreIndexData() { Index = item.GetSiblingIndex(), StoreStates = StoreStates.Locked });
            }
        }
        _weaponStoreState = _temp;


        _temp = new List<StoreIndexData>();
        foreach (Transform item in _charPanel)
        {
            if (item.name != "ActionButton")
            {
                if (item.GetSiblingIndex() == 0)
                    _temp.Add(new StoreIndexData() { Index = item.GetSiblingIndex(), StoreStates = StoreStates.Selected });
                else
                    _temp.Add(new StoreIndexData() { Index = item.GetSiblingIndex(), StoreStates = StoreStates.Locked });
            }
        }
        _charStoreState = _temp;
    }

    public void ResetUnlockables()
    {

        curUnlockable = 0;

        unlockableFill = 0;

        curLevel = 0;

        curArena = 0;

        diamonds = 0;


    }
}

[Serializable]
public class StoreIndexData
{
    public int Index;
    public StoreStates StoreStates;
}

public enum StoreStates
{
    Locked,
    WillBuy,
    Selected,
    NothingElseMatter
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}