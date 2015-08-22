﻿using UnityEngine;

public interface IControl
{
    Vector2 GetDirection();
    Vector2 GetAim();
    bool GetShoot();
}