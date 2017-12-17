using System;
using System.Collections.Generic;

namespace Mail2BigQuery
{
    internal static class EnumerableExtensions
    {
        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
                action(item);
        }

        internal static IEnumerable<IList<T>> Buffer<T>(this IEnumerable<T> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return source.Buffer_(count, count);
        }

        private static IEnumerable<IList<T>> Buffer_<T>(this IEnumerable<T> source, int count, int skip)
        {
            var buffers = new Queue<IList<T>>();
            var i = 0;
            foreach (var item in source)
            {
                if (i % skip == 0)
                    buffers.Enqueue(new List<T>(count));

                foreach (var buffer in buffers)
                    buffer.Add(item);

                if (buffers.Count > 0 && buffers.Peek().Count == count)
                    yield return buffers.Dequeue();

                i++;
            }

            while (buffers.Count > 0)
                yield return buffers.Dequeue();
        }
    }
}