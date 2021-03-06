﻿using System;

public delegate void CheckCardDelegate(CheckCardEventArgs e);
public delegate void SetBoardSizeDelegate(SetBoardSizeEventArgs e);

public delegate void ChangeSetsTextDelegate(ChangeSetsTextEventArgs e);
public delegate void ChangeTurnTextDelegate(ChangeTurnTextEventArgs e);
public delegate void ChangeTimeTextDelegate(ChangeTimeTextEventArgs e);
public delegate void ChangeCoinTextDelegate(ChangeCoinTextEventArgs e);

public delegate void SetAchievementDataDelegate( SetAchievementDataEventArgs e);
public delegate void SetAchievementOnCompletedDelegate(SetAchievementOnCompletedEventArgs e);
public delegate void AchievedAmountForAchievementDelegate(AchievedAmountForAchievementEventArgs e);

public delegate void PayOutOnCompletedDelegate(PayOutOnCompletedEventArgs e);
public delegate void OnPurchaseCompletedDelegate(OnPurchaseCompletedEventArgs e);
public delegate void OnUpdatedCardInfoDelegate(SendUpdatedCardInfoEventArgs e);

public delegate void SaveGameDelegate();
public delegate void LoadGameDelegate();





