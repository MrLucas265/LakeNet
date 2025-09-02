using System;
using System.Threading;

/// <summary>
/// Defines methods for parallel for loops.
/// </summary>
public static class Parallel
{
    /// <summary>
    /// Executes a for loop in which iterations may run in parallel.
    /// </summary>
    /// <param name="fromInclusive">The start index, inclusive.</param>
    /// <param name="toExclusive">The end index, exclusive.</param>
    /// <param name="body">The delegate that is invoked once per iteration.</param>
    public static void For(int fromInclusive, int toExclusive, Action<int> body)
    {
        int threadCount = Environment.ProcessorCount;
        Thread[] threads = new Thread[threadCount];
        --fromInclusive;

        Action threadWork = () =>
        {
            int index;
            while (true)
            {
                index = Interlocked.Increment(ref fromInclusive);
                if (index >= toExclusive) return;
                body.Invoke(index);
            }
        };

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(new ThreadStart(threadWork));
            threads[i].Start();
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }
    }

    /// <summary>
    /// Executes a for loop in which iterations may run in parallel and the state of the loop may be monitored and manipulated.
    /// </summary>
    /// <param name="fromInclusive">The start index, inclusive.</param>
    /// <param name="toExclusive">The end index, exclusive.</param>
    /// <param name="body">The delegate that is invoked once per iteration.</param>
    public static void For(int fromInclusive, int toExclusive, Action<int, State> body)
    {
        int threadCount = Environment.ProcessorCount;
        Thread[] threads = new Thread[threadCount];
        State state = new State();
        --fromInclusive;

        Action threadWork = () =>
        {
            int index;
            while (true)
            {
                if (state.StopTriggered) return;
                index = Interlocked.Increment(ref fromInclusive);
                if (index >= toExclusive) return;
                body.Invoke(index, state);
            }
        };

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(new ThreadStart(threadWork));
            threads[i].Start();
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }
    }


    /// <summary>
    /// Executes a for loop with 64-bit indexes in which iterations may run in parallel.
    /// </summary>
    /// <param name="fromInclusive">The start index, inclusive.</param>
    /// <param name="toExclusive">The end index, exclusive.</param>
    /// <param name="body">The delegate that is invoked once per iteration.</param>
    public static void For(long fromInclusive, long toExclusive, Action<long> body)
    {
        int threadCount = Environment.ProcessorCount;
        Thread[] threads = new Thread[threadCount];
        --fromInclusive;

        Action threadWork = () =>
        {
            long index;
            while (true)
            {
                index = Interlocked.Increment(ref fromInclusive);
                if (index >= toExclusive) return;
                body.Invoke(index);
            }
        };

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(new ThreadStart(threadWork));
            threads[i].Start();
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }

    }





    /// <summary>
    /// Executes a for loop with 64-bit indexes in which iterations may run in parallel and the state of the loop may be monitored and manipulated.
    /// </summary>
    /// <param name="fromInclusive">The start index, inclusive.</param>
    /// <param name="toExclusive">The end index, exclusive.</param>
    /// <param name="body">The delegate that is invoked once per iteration.</param>
    public static void For(long fromInclusive, long toExclusive, Action<long, State> body)
    {
        int threadCount = Environment.ProcessorCount;
        Thread[] threads = new Thread[threadCount];
        State state = new State();
        --fromInclusive;

        Action threadWork = () =>
        {
            long index;
            while (true)
            {
                if (state.StopTriggered) return;
                index = Interlocked.Increment(ref fromInclusive);
                if (index >= toExclusive) return;
                body.Invoke(index, state);
            }
        };

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(new ThreadStart(threadWork));
            threads[i].Start();
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }
    }







    /// <summary>
    /// Executes a for loop with thread-local data in which iterations may run in parallel, and the state of the loop may be monitored and manipulated.
    /// </summary>
    /// <param name="fromInclusive">The start index, inclusive.</param>
    /// <param name="toExclusive">The end index, exclusive.</param>
    /// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
    /// <param name="body">The delegate that is invoked once per iteration.</param>
    /// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
    /// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
    public static void For<TLocal>(
        int fromInclusive,
        int toExclusive,
        Func<TLocal> localInit,
        Func<int, State, TLocal, TLocal> body,
        Action<TLocal> localFinally)
    {
        int threadCount = Environment.ProcessorCount;
        Thread[] threads = new Thread[threadCount];
        State state = new State();
        --fromInclusive;

        Action threadWork = () =>
        {
            TLocal localVars = localInit.Invoke();
            int index;
            while (true)
            {
                if (state.StopTriggered) break;
                index = Interlocked.Increment(ref fromInclusive);
                if (index >= toExclusive) break;
                localVars = body.Invoke(index, state, localVars);
            }
            localFinally.Invoke(localVars);
        };

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(new ThreadStart(threadWork));
            threads[i].Start(body);
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }
    }

    /// <summary>
    /// Executes a for loop with thread-local data in which iterations may run in parallel, and the state of the loop may be monitored and manipulated.
    /// </summary>
    /// <param name="fromInclusive">The start index, inclusive.</param>
    /// <param name="toExclusive">The end index, exclusive.</param>
    /// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
    /// <param name="body">The delegate that is invoked once per iteration.</param>
    /// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
    /// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
    public static void For<TLocal>(
        long fromInclusive,
        long toExclusive,
        Func<TLocal> localInit,
        Func<long, State, TLocal, TLocal> body,
        Action<TLocal> localFinally)
    {
        int threadCount = Environment.ProcessorCount;
        Thread[] threads = new Thread[threadCount];
        State state = new State();
        --fromInclusive;

        Action threadWork = () =>
        {
            TLocal localVars = localInit.Invoke();
            long index;
            while (true)
            {
                if (state.StopTriggered) break;
                index = Interlocked.Increment(ref fromInclusive);
                if (index >= toExclusive) break;
                localVars = body.Invoke(index, state, localVars);
            }
            localFinally.Invoke(localVars);
        };

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread(new ThreadStart(threadWork));
            threads[i].Start(body);
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }
    }


    /// <summary>
    /// Executes each action in parallel.
    /// </summary>
    /// <param name="actions">The set of delegates to invoke.</param>
    public static void Invoke(params Action[] actions)
    {
        int actionCount = actions.Length;
        Thread[] threads = new Thread[actionCount];

        for (int i = 0; i < actionCount; i++)
        {
            threads[i] = new Thread(new ThreadStart(actions[i]));
            threads[i].Start();
        }
        for (int i = 0; i < actionCount; i++)
        {
            threads[i].Join();
        }
    }







    /// <summary>
    /// Helper class to monitor and control the loop.
    /// </summary>
    public sealed class State
    {
        internal bool StopTriggered;

        /// <summary>
        /// Command the loop to exit as soon as possible.
        /// </summary>
        public void Stop()
        {
            StopTriggered = true;
        }
    }


}