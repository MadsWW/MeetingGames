using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CheckCardDelegate(object sender, CheckCardEventArgs e);
public delegate void SetBoardSizeDelegate(object sender, SetBoardSizeEventArgs e);
public delegate void ChangeSetsTextDelegate(object sender, ChangeSetsTextEventArgs e);
public delegate void ChangeTurnTextDelegate(object sender, ChangeTurnTextEventArgs e);
public delegate void ChangeTimeTextDelegate(object sender, ChangeTimeTextEventArgs e);




