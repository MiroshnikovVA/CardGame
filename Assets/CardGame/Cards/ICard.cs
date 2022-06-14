using System;
using UnityEngine;

namespace CardGame.Cards
{
    public interface ICard
    {
        YieldInstruction MoveToPlace();
        CustomYieldInstruction WaitingForInitialization();
        void SetPlace(Transform transform, Action<ICard> onChangedThisPlace);
    }
}
