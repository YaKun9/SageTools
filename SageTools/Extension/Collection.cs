﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        /// 是否为null或空集合
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> list)
        {
            if (list == null) return true;
            return !list.Any();
        }

        /// <summary>
        /// 是否不为null且不为空集合
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> list)
        {
            return !IsNullOrEmpty(list);
        }

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
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 分页查询拓展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allItems"></param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static IList<T> ToPagedList<T>(this ICollection<T> allItems, int pageIndex, int pageSize)
        {
            var itemList = allItems.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return itemList;
        }

        /// <summary>
        /// 分页执行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">源数据集合</param>
        /// <param name="action">要进行的操作</param>
        /// <param name="pageSize">每页大小</param>
        public static void PagingToOperate<T>(this ICollection<T> list, Action<ICollection<T>> action, int pageSize = 1000)
        {
            if (list.IsNullOrEmpty())
            {
                action(list);
            }
            else
            {
                var pageCount = Math.Ceiling(list.Count * 1d / pageSize);
                for (var pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    var items = list.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    action(items);
                }
            }
        }
    }
}