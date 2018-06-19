using Sourav.Utilities.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestScript : Singleton<TestScript>
{
    public Tilemap tilemap_Foreground;
    public Tilemap tilemap_Background;

    public GameObject intersectingSpriteTile;

	// Use this for initialization
	void Start ()
    {
        Debug.Log ( "width = " +tilemap_Background.size.x+" , height = "+tilemap_Background.size.y+"");
        Debug.Log ( "width = " + tilemap_Foreground.size.x + " , height = " + tilemap_Foreground.size.y + "" );

        //for ( int i = 0 ; i < tilemap_Foreground.size.x ; i++ )
        //{
        //    for ( int j = 0 ; j < tilemap_Foreground.size.y ; j++ )
        //    {
                
        //    }
        //}

        for ( int i = 0 ; i < tilemap_Background.size.x ; i++ )
        {
            for ( int j = 0 ; j < tilemap_Background.size.y ; j++ )
            {
                //Debug.Log ( "Tile name = " + tilemap_Background.GetSprite ( new Vector3Int ( i, j, 0 ) ) );

                if ( tilemap_Background.GetSprite( new Vector3Int(i, j, 0) )  != null )
                {
                    Debug.Log ( "Found at x = "+i+", y = "+j );
                    //TileBase tile;
                    
                    //tilemap_Foreground.SetTile( new Vector3Int ( i * 2, j * 2, 0 ) ), )
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

[Serializable]
public class IncludedTile
{
   public List<Vector2Int> IncludedTilesPos;

    public IncludedTile()
    {
        IncludedTilesPos = new List<Vector2Int> ( );
    }
}

public enum Tilemaps
{
    Foreground,
    Background
}
