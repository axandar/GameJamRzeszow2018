using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool DEBUG { get { return Application.platform == RuntimePlatform.WindowsEditor; } }
    public long points = 0;
    public bool inGame = false;
}