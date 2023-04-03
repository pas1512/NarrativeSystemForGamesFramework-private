using System;
using System.Collections.Generic;

namespace ScriptsUtilities
{
    public class ObjectsPool<objectT>
    {
        private List<objectT> _pool;

        public int length => _pool.Count;
        public objectT[] pool => _pool.ToArray();
        public objectT this[int id] => _pool[id];

        public ObjectsPool()
        {
            _pool = new List<objectT>();
        }

        public void Init(int number, Func<int, objectT> initAction)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("Number of init objects in pool can not be less zero!");

            if (number == length)
                return;

            if (number == 0)
                _pool.Clear();

            if (length < number)
            {
                int wantage = number - length;

                for (int i = 0; i < wantage; i++)
                    _pool.Add(initAction.Invoke(i));
            }
            else
            {
                int overfolow = length - number;

                for (int i = overfolow - 1; i >= 0; i--)
                    _pool.RemoveAt(i);
            }
        }

        public void Init<objectTnew>(int number) where objectTnew : objectT, new()
        {
            Init(number, (int id) => new objectTnew());
        }
    }
}