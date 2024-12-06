using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] Image heartShard;
    [SerializeField] Image manaShard;
    [SerializeField] GameObject upCast, sideCast, downCast;
    [SerializeField] GameObject dash, varJump, wallJump;

    private void OnEnable()
    {
        //heart shard
        heartShard.fillAmount = PlayerController.Instance.heartShards * 0.25f;

        //mana shards
        manaShard.fillAmount = PlayerController.Instance.orbShard * 0.34f;


        //spells
        if(PlayerController.Instance.unlockedUpCast)
        {
            upCast.SetActive(true);
        }
        else
        {
            upCast.SetActive(false);
        }
        if (PlayerController.Instance.unlockedSideCast)
        {
            sideCast.SetActive(true);
        }
        else
        {
            sideCast.SetActive(false);
        }
        if (PlayerController.Instance.unlockedDownCast)
        {
            downCast.SetActive(true);
        }
        else
        {
            downCast.SetActive(false);
        }

        //abilities
        if (PlayerController.Instance.unlockedDash)
        {
            dash.SetActive(true);
        }
        else
        {
            dash.SetActive(false);
        }
        if (PlayerController.Instance.unlockedVarJump)
        {
            varJump.SetActive(true);
        }
        else
        {
            varJump.SetActive(false);
        }
        if (PlayerController.Instance.unlockedWallJump)
        {
            wallJump.SetActive(true);
        }
        else
        {
            wallJump.SetActive(false);
        }
    }
}
