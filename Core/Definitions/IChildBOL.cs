using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public interface IChildBOL<T> : IBOLCollection where T : IBOL
    {
        IEnumerable<T> GetModifiedElementList();
        IEnumerable<T> GetAddedElementList();
        IEnumerable<T> GetDeletedElementList();

        IEnumerable<T> GetAllElements();
        IEnumerable<T> GetAllElements(bool includeDeleted);

        void Add(T item);
        void Add(List<T> items);

        bool Any(Func<T, bool> predicate);

        T FirstOrDefault();
        T FirstOrDefault(Func<T, bool> predicate);

        IEnumerable<T> Where(Func<T, bool> predicate);
        IEnumerable<U> Select<U>(Func<T, U> selector);

        bool Remove(T item);
        bool RemoveAll(Func<T, bool> predicate, bool removeFromList);

        bool RemoveAll(Func<T, bool> predicate);

        int Count { get; }

        T New();
    }
}
