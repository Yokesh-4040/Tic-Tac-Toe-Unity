using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyType : MonoBehaviour
{
public enum mySelection
{
   Default,
   x,
   o
}

public Sprite x;
public Sprite o;
public Image xo_Image;
public mySelection Selection;

private void Start()
{
    GetComponent<Button>().onClick.AddListener(Clicked);
}

private void Clicked()
{
   GetComponent<Button>().interactable = false;
  

   if (GameManager.Instance.currentPlayer == GameManager.Players.Player_1)
   {
       xo_Image.sprite = x;
       Selection = mySelection.x;
   }
   else
   {
       xo_Image.sprite = o;
       Selection = mySelection.o;

   }
   GameManager.Instance.SetPlayerTurn();
}
}
