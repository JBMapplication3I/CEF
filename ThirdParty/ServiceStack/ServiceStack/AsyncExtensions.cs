namespace ServiceStack
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class AsyncExtensions
    {
        // http://bradwilson.typepad.com/blog/2012/04/tpl-and-servers-pt3.html
        public static Task<TOut> Continue<TOut>(
            this Task task,
            Func<Task, TOut> next)
        {
            if (!task.IsCompleted)
            {
                return ContinueClosure(task, next);
            }
            var tcs = new TaskCompletionSource<TOut>();
            try
            {
                var res = next(task);
                tcs.TrySetResult(res);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
            return tcs.Task;
        }

        private static Task<TOut> ContinueClosure<TOut>(
            Task task,
            Func<Task, TOut> next)
        {
            var context = SynchronizationContext.Current;
            return HostContext.Async.ContinueWith(
                task,
                innerTask =>
                {
                    var tcs = new TaskCompletionSource<TOut>();
                    try
                    {
                        if (context != null)
                        {
                            context.Post(
                                _ =>
                                {
                                    try
                                    {
                                        var res = next(innerTask);
                                        tcs.TrySetResult(res);
                                    }
                                    catch (Exception ex)
                                    {
                                        tcs.TrySetException(ex);
                                    }
                                },
                                state: null);
                        }
                        else
                        {
                            var res = next(innerTask);
                            var t = res as Task;
                            if (t?.IsFaulted == true)
                            {
                                tcs.TrySetException(t.Exception);
                            }
                            else
                            {
                                tcs.TrySetResult(res);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                    return tcs.Task;
                })
                .Unwrap();
        }
    }
}
