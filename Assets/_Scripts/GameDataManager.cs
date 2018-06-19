using Sourav.Utilities.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [SerializeField]
    private GameData data;
    private bool hasDataChanged;

    public int Life
    {
        get
        {
            return data.Life;
        }
        set
        {
            data.Life = value;
            if ( data.Life < 0 )
            {
                data.Life = 0;
                GameManager.Instance.GameOver ( );
            }
        }
    }

    public int Coins
    {
        get
        {
            return data.Coins;
        }
        set
        {
            data.Coins = value;
            GameManager.Instance.CoinsChanged ( );
        }
    } 

    #region Methods
    #region MONO Methods

    private void Awake()
    {
        if ( !RetrieveData ( ) )
        {
            ResetData ( );
            SaveData ( );
        }
    }

    private void OnApplicationPause( bool pause )
    {
        if ( !pause )
        {
            if ( hasDataChanged )
            {
                SaveData ( );
                hasDataChanged = false;
            }
        }
    }

    #endregion

    #region SAVE, RETRIEVE and RESET DATA

    void DataChanged()
    {
        hasDataChanged = true;
    }

    private void SaveData()
    {
        if ( data == null )
        {
            Debug.Log ( "DATA IS NULL" );
            ResetData ( );
        }
        string str = JsonUtility.ToJson ( data );
        FileIO.WriteData ( str );
    }

    private bool RetrieveData()
    {
        if ( FileIO.FileExists ( ) )
        {
            Debug.Log ( "FILE EXISTS" );
            string str = FileIO.ReadData ( );
            data = JsonUtility.FromJson<GameData> ( str );
            return true;
        }

        return false;
    }

    private void ResetData()
    {
        data = new GameData ( );
    }

    private void StateChanged()
    {
        DataChanged ( );

        SaveData ( );
    }

    #endregion

    #endregion
}

public class GameData
{
    public int Life;
    public int Coins;

    public GameData()
    {
        Life = 3;
        Coins = 0;
    }
}
