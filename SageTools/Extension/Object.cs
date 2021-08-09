using System;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        public static bool IsNull(this object obj) => obj == null;

        public static bool IsNotNull(this object obj) => obj != null;

        public static void IfFalse(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (!@this)
                trueAction();
            else if (falseAction != null)
                falseAction();
        }

        public static void IfTrue(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (@this)
                trueAction();
            else if (falseAction != null)
                falseAction();
        }

    }
}
