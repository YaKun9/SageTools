using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        #region Linq拓展

        /// <summary>
        /// Where查询拓展，当condition为True时执行predicate条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">要对数据源执行的操作</param>
        /// <param name="condition">执行条件，为True才执行</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Where查询拓展，当condition为True时执行predicate条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">要对数据源执行的操作</param>
        /// <param name="condition">执行条件，为True才执行</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Where查询拓展，当condition为True时执行predicate条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">要对数据源执行的操作</param>
        /// <param name="condition">执行条件，为True才执行</param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Where查询拓展，当condition为True时执行predicate条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">要对数据源执行的操作</param>
        /// <param name="condition">执行条件，为True才执行</param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        #endregion

        public static bool IsNullOrEmpty<T>(IEnumerable<T> list)
        {
            if (list == null) return true;
            return !list.Any();
        }

        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> list)
        {
            return !IsNullOrEmpty(list);
        }
    }
}