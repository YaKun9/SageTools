namespace SageTools.Extension
{
    public static partial class Extension
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
    }
}
