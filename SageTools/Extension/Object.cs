using System;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        /// ==null 拓展
        /// </summary>
        public static bool IsNull(this object obj) => obj == null;

        /// <summary>
        /// !=null 拓展
        /// </summary>
        public static bool IsNotNull(this object obj) => obj != null;

        /// <summary>
        /// True/False时分别执行不同的操作
        /// </summary>
        public static void IfFalse(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (!@this)
            {
                trueAction();
            }
            else
            {
                falseAction?.Invoke();
            }
        }

        /// <summary>
        /// True/False时分别执行不同的操作
        /// </summary>
        public static void IfTrue(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (@this)
            {
                trueAction();
            }
            else
            {
                falseAction?.Invoke();
            }
        }

    }
}
