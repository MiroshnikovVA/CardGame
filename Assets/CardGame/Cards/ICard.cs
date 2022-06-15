using DG.Tweening;
using System;
using UnityEngine;

namespace CardGame.Cards
{
    public interface ICard
    {
        int Attack { get; set; }
        int Mana { get; set; }
        int HP { get; set; }

        public Tween CurrentCounterAnimation { get; }

        Tween MoveToPlace();
        CustomYieldInstruction WaitingForInitialization();

        void SetPlace(Transform transform, Func<ICard, Tween> onRemoveFromSetCallback);

        Tween Destroy();
        void ToForeground();
    }
}
