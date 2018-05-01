using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSizeButton : MonoBehaviour {

    public static event SetBoardSizeDelegate BoardSize;

    public int width;
    public int height;

    public void SetBoardSize()
    {
        SetBoardSizeEventArgs BoardSizeArgs = new SetBoardSizeEventArgs();
        BoardSizeArgs.BoardHeight = height;
        BoardSizeArgs.BoardWidth = width;
        BoardSize(gameObject, BoardSizeArgs);
    }
}
