using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
public class MyPlayer : MonoBehaviour
{
   [SerializeField]
   private TMPro.TMP_Text playerName;
   public Player Player;

   public void setPlayerName(Player newPlayer){
      Debug.Log("in set player name");
   Player = newPlayer;
   playerName.text = newPlayer.NickName;
   }
}