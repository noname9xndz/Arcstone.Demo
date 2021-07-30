using System;
using System.Collections.Generic;
using System.Linq;

namespace Noname.UnitOfWork.Lib.Paging
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paginate<T> : IPaginate<T>
    {
        internal Paginate(IEnumerable<T> source, int index, int size, int from)
        {
            var enumerable = source as T[] ?? source.ToArray();

            if (from > index)
                throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

            if (source is IQueryable<T> querable)
            {
                Index = index;
                Size = size;
                From = from;
                Count = querable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                Items = querable.Skip((Index - From) * Size).Take(Size);
            }
            else
            {
                Index = index;
                Size = size;
                From = from;

                Count = enumerable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                Items = enumerable.Skip((Index - From) * Size).Take(Size);
            }
        }

        internal Paginate()
        {
            Items = new T[0];
        }

        /// <inheritdoc />
        public int From { get; set; }

        /// <inheritdoc />
        public int Index { get; set; }

        /// <inheritdoc />
        public int Size { get; set; }

        /// <inheritdoc />
        public int Count { get; set; }

        /// <inheritdoc />
        public int Pages { get; set; }

        /// <inheritdoc />
        public IEnumerable<T> Items { get; set; }

        /// <inheritdoc />
        public bool HasPrevious => Index - From > 0;

        /// <inheritdoc />
        public bool HasNext => Index - From + 1 < Pages;
    }

    internal class Paginate<TSource, TResult> : IPaginate<TResult>
    {
        public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
            int index, int size, int from)
        {
            var enumerable = source as TSource[] ?? source.ToArray();

            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            if (source is IQueryable<TSource> queryable)
            {
                Index = index;
                Size = size;
                From = from;
                Count = queryable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                var items = queryable.Skip((Index - From) * Size).Take(Size).ToArray();

                Items = (converter(items));
            }
            else
            {
                Index = index;
                Size = size;
                From = from;
                Count = enumerable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                var items = enumerable.Skip((Index - From) * Size).Take(Size).ToArray();

                Items = (converter(items));
            }
        }

        public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            Index = source.Index;
            Size = source.Size;
            From = source.From;
            Count = source.Count;
            Pages = source.Pages;

            Items = (converter(source.Items));
        }

        public int Index { get; }

        public int Size { get; }

        public int Count { get; }

        public int Pages { get; }

        public int From { get; }

        public IEnumerable<TResult> Items { get; }

        public bool HasPrevious => Index - From > 0;

        public bool HasNext => Index - From + 1 < Pages;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Paginate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPaginate<T> Empty<T>()
        {
            return new Paginate<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="converter"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static IPaginate<TResult> From<TResult, TSource>(IPaginate<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            return new Paginate<TSource, TResult>(source, converter);
        }
    }
}