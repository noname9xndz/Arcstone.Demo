using System.Collections.Generic;

namespace Noname.UnitOfWork.Lib.Paging
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPaginate<T>
    {
        /// <summary>
        /// 
        /// </summary>
        int From { get; }

        /// <summary>
        /// 
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        int Pages { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<T> Items { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasNext { get; }
    }
}