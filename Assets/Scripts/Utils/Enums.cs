using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class Enums
    {
        public enum KindsOfDeath
        {
            Normal,
            InFire,
            Squeezed
        }

        public enum Edges
        {
            LEFT,
            RIGHT,
            TOP,
            BOTTOM,
            NONE
        };

        public enum Direction
        {
            HORIZONTAL,
            VERTICAL
        };
    }
}
