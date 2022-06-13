using System;
using UnityEngine;

namespace CardGame.Cards
{
    public interface ICard
    {
        YieldInstruction MoveToPlace();
        void SetPlace(Transform transform, Action<ICard> onChangedThisPlace);
    }
}
