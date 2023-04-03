using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptsUtilities
{
    public class CoroutinesPool
    {
        private MonoBehaviour _ovner;
        private List<CoroutineController> _pool;

        public int size => _pool.Count;

        public CoroutinesPool(MonoBehaviour ovner, int number = 0)
        {
            _ovner = ovner;
            _pool = new List<CoroutineController>(number);
        }

        public void AddProcess(IEnumerator logic)
        {
            CoroutineController newProcess = new CoroutineController(_ovner);
            _pool.Add(newProcess);
            newProcess.Start(logic, () => _pool.Remove(newProcess));
        }
    }
}