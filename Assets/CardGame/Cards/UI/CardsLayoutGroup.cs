using UnityEngine;
using UnityEngine.UI;

namespace CardGame.Cards.UI
{
    public class CardsLayoutGroup : HorizontalLayoutGroup
    {
        int[][] _rotationOffsetfromCardCounts = new int[][] {
            new int[] { },
            new int[] { 0 }, 
            new int[] { 0, 0 }, 
            new int[] { -1, 0, 1 },
            new int[] { -1, 0, 0, 1 },
            new int[] { -2, -1, 0, 1, 2 }, 
            new int[] { -3, -2, -1, 1, 2, 3 } };

        int[][] _xOffsetfromCardCounts = new int[][] {
            new int[] { },
            new int[] { 0 },
            new int[] { 0, 0 },
            new int[] { 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 2, 1, 0, -1, -2 },
            new int[] { 3, 2, 1, 0, -1, -2 } };

        int[][] _yOffsetfromCardCounts = new int[][] {
            new int[] { },
            new int[] { 0 },
            new int[] { 0, 0 },
            new int[] { -1, 0, -1 },
            new int[] { -1, 0, 0, -1 },
            new int[] { -1, 0, 1, 0, -1 },
            new int[] { -2, 0, 1, 1, 0, -2 } };

        float _xOffset = 30f;
        float _yOffset = 30f;
        float _radius = 20f;

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();
            CalculateX();
        }

        public override void SetLayoutVertical()
        {
            base.SetLayoutVertical();
            CalculateY();
        }

        void CalculateX()
        {
            var rotation = _rotationOffsetfromCardCounts[rectChildren.Count];
            var x = _xOffsetfromCardCounts[rectChildren.Count];

            for (int i = 0; i < rectChildren.Count; i++)
            {
                var child = rectChildren[i];
                if (child)
                {
                    child.localRotation = Quaternion.FromToRotation(Vector3.up * _radius, rotation[i] * Vector3.right + Vector3.up * _radius);

                    var dx = x[i] * _xOffset;

                    child.localPosition += Vector3.right * dx;
                }
            }
        }

        void CalculateY()
        {
            var y = _yOffsetfromCardCounts[transform.childCount];

            for (int i = 0; i < rectChildren.Count; i++)
            {
                var child = rectChildren[i];
                if (child)
                {
                    var dy = y[i] * _yOffset;

                    child.localPosition += Vector3.up * dy;
                }
            }
        }
    }
}