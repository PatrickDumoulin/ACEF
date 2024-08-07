using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Definitions
{
    public class ChildBols<T> : IChildBOL<T> where T : IBOL
    {
        private List<T> elementList;

        public ChildBols(List<T> elementList)
        {
            this.elementList = elementList;
            this.NewInstanceFunction = null;
            AddItemFunction = null;
            AddListFunction = null;

        }

        public Func<IChildBOL<T>, T> NewInstanceFunction { get; set; }
        public Action<IChildBOL<T>, T> AddItemFunction { get; set; }
        public Action<IChildBOL<T>, List<T>> AddListFunction { get; set; }

        public void Add(T item)
        {
            internalAddItem(item);
            AddItemFunction?.Invoke(this, item);
        }
        public void Add(List<T> items)
        {
            internalAddList(items);
            AddListFunction?.Invoke(this, items);
        }

        public bool Contains(T item)
        {
            if (item != null && item.State != ObjectState.DELETED)
                return elementList.Contains(item);

            return false;
        }
        public bool Any(Func<T, bool> predicate)
        {
            return elementList.Any(predicate);
        }

        public T FirstOrDefault()
        {
            return elementList.FirstOrDefault();
        }
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return elementList.FirstOrDefault(predicate);

        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return elementList.Where(predicate).ToList();
        }
        public IEnumerable<U> Select<U>(Func<T, U> selector)
        {
            return elementList.Select(selector);
        }

        public int Count
        {
            get { return elementList.Count(f => f.State != ObjectState.DELETED); }
        }

        public bool Remove(T item)
        {
            item.State = ObjectState.DELETED;
            return true;
        }
        public bool Remove(int index)
        {
            elementList[index].State = ObjectState.DELETED;
            return true;
        }
        public bool RemoveAll(Func<T, bool> predicate)
        {
            return RemoveAll(predicate, false);
        }

        public bool RemoveAll(Func<T, bool> predicate, bool removeFromList)
        {
            if (removeFromList)
            {
                Predicate<T> removePredicate = new Predicate<T>(predicate);
                elementList.RemoveAll(removePredicate);
            }
            else
            {
                elementList.Where(predicate).ToList().ForEach(x => x.State = ObjectState.DELETED);
            }

            return true;
        }

        public bool CancelAdd(T item)
        {
            if (item.State == ObjectState.ADDED)
                elementList.Remove(item);

            return true;
        }
        public bool CancelAdd(int index)
        {
            var item = elementList[index];
            return CancelAdd(item);
        }

        public T this[int index]
        {
            get
            {
                if (elementList[index] != null && elementList[index].State != ObjectState.DELETED)
                    return elementList[index];

                return default(T);
            }
        }

        public T New()
        {
            if (NewInstanceFunction != null)
                return NewInstanceFunction(this);

            return default(T);
        }

        public IEnumerable<T> GetModifiedElementList()
        {
            return GetElements(ObjectState.MODIFIED);
        }
        public IEnumerable<T> GetAddedElementList()
        {
            return GetElements(ObjectState.ADDED);
        }
        public IEnumerable<T> GetDeletedElementList()
        {
            return GetElements(ObjectState.DELETED);
        }
        public IEnumerable<T> GetAllElements(bool includeDeleted)
        {
            if (includeDeleted)
                return elementList.AsEnumerable<T>();
            else
                return elementList.Where(x => x.State != ObjectState.DELETED).AsEnumerable<T>();
        }
        public IEnumerable<T> GetAllElements()
        {
            return this.GetAllElements(true);
        }

        private void internalAddItem(T item)
        {
            elementList.Add(item);
            item.State = ObjectState.ADDED;
        }
        private void internalAddList(List<T> items)
        {
            elementList.AddRange(items);
            items.ForEach(x => x.State = ObjectState.ADDED);

        }

        private IEnumerable<T> GetElements(ObjectState state)
        {
            return elementList.Where(f => f.State == state).AsEnumerable<T>();
        }
        public IEnumerable<IBOL> GetBolList()
        {
            List<IBOL> bolList = new List<IBOL>();
            var list = this.GetAllElements(true);
            foreach (var item in list)
            {
                bolList.Add((IBOL)item);
            }
            return bolList;
        }
    }
}
