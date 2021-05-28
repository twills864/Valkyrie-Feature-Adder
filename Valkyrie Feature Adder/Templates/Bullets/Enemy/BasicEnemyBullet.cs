﻿using System;
using System.Linq;
using Assets.Constants;
using Assets.GameTasks;
using Assets.ObjectPooling;
using Assets.Util;
using UnityEngine;

namespace Assets.Bullets.EnemyBullets
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicEnemyBullet : PermanentVelocityEnemyBullet
    {
        #region Prefabs

        [SerializeField]
        private float _Speed = GameConstants.PrefabNumber;

        #endregion Prefabs


        #region Prefab Properties

        public float Speed => _Speed;

        #endregion Prefab Properties



    }
}