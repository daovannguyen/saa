using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ====
 ==== Class này có chức năng lưu trữ lại các biến, văn bản, sử dụng tại nhiều vị trí
 ====
 */
public class Constrain 
{
    #region SceneName
    public const string SN_START = "Start";
    #endregion


    #region TagName
    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_DONTCONTROLITEM = "DontControlItem";
    #endregion

    #region Value game
    // Item/DestroyItem
    public const float ID_time = 0.1f;
    #endregion
}
