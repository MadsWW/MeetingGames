using System;

public delegate void CheckCardDelegate(CheckCardEventArgs e);
public delegate void SetBoardSizeDelegate(SetBoardSizeEventArgs e);
public delegate void ChangeSetsTextDelegate(ChangeSetsTextEventArgs e);
public delegate void ChangeTurnTextDelegate(ChangeTurnTextEventArgs e);
public delegate void ChangeTimeTextDelegate(ChangeTimeTextEventArgs e);
public delegate void SetAchievementDataDelegate( SetAchievementDataEventArgs e);
public delegate void PushCardBackInfoDelegate(PushCardBackInfoEventArgs e);
public delegate void SetCardInfoUnlockedDelegate(SetCardInfoUnlockedEventArgs e);
public delegate void SetAchievementOnCompletedDelegate(SetAchievementOnCompletedEventArgs e);
public delegate void PayOutOnCompletedDelegate(PayOutOnCompletedEventArgs e);
public delegate void OnPurchaseCompletedDelegate(OnPurchaseCompletedEventArgs e);
public delegate void CreateAchievementsDelegate(CreateAchievementEventArgs e);

public delegate void SaveGameDelegate();
public delegate void LoadGameDelegate();





