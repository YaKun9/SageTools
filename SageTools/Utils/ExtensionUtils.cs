using System;
using System.Threading.Tasks;

namespace SageTools.Utils
{
    /// <summary>
    /// 工具类扩展方法类
    /// </summary>
    public class ExtensionUtils
    {
        /// <summary>
        /// 同步等待重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeFunc">执行方法</param>
        /// <param name="breakConditionFunc">跳出重试条件，当为true时，终止重试</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="milliseconds">重试间隔时间（ms）</param>
        /// <exception cref="Exception">执行出错</exception>
        public static T DelayRetry<T>(Func<T> executeFunc, Func<T, bool> breakConditionFunc, int retryCount = 1, int milliseconds = 1000)
        {
            var res = default(T);
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    res = executeFunc.Invoke();
                    if (breakConditionFunc.Invoke(res)) break;
                    Task.Delay(milliseconds).Wait();
                }
                catch
                {
                    Task.Delay(milliseconds).Wait();
                }
            }

            return res;
        }

        /// <summary>
        /// 异步等待重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeFunc">执行方法</param>
        /// <param name="breakConditionFunc">跳出重试条件，当为true时，终止重试</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="milliseconds">重试间隔时间（ms）</param>
        /// <exception cref="Exception">执行出错</exception>
        public static async Task<T> DelayRetryAsync<T>(Func<Task<T>> executeFunc, Func<T, bool> breakConditionFunc, int retryCount = 1, int milliseconds = 1000)
        {
            var res = default(T);
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    res = await executeFunc.Invoke();
                    if (breakConditionFunc.Invoke(res)) break;
                    await Task.Delay(milliseconds);
                }
                catch
                {
                    Task.Delay(milliseconds).Wait();
                }
            }

            return res;
        }

        /// <summary>
        ///同步方法重试（异常情况也重试）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">执行的方法</param>
        /// <param name="breakConditionFunc">跳出重试条件，当为true时，终止重试</param>
        /// <param name="retryCount">重试次数</param>
        /// <returns></returns>
        public static T ActionRetry<T>(Func<T> action, Func<T, bool> breakConditionFunc, int retryCount = 1)
        {
            var res = default(T);
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    res = action.Invoke();
                    if (breakConditionFunc.Invoke(res)) break;
                }
                catch
                {
                    // ignore
                }
            }

            return res;
        }

        /// <summary>
        /// 异步方法重试（异常情况也重试）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">执行的方法</param>
        /// <param name="breakConditionFunc">跳出重试条件，当为true时，终止重试</param>
        /// <param name="retryCount">重试次数</param>
        /// <returns></returns>
        public static async Task<T> ActionRetryAsync<T>(Func<Task<T>> action, Func<T, bool> breakConditionFunc, int retryCount = 1)
        {
            var res = default(T);
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    res = await action.Invoke();
                    if (breakConditionFunc.Invoke(res)) break;
                }
                catch
                {
                    // ignore
                }
            }

            return res;
        }
    }
}