using System;
using System.Collections;
using UnityEngine;

namespace ScriptsUtilities

{
    public class CoroutineController
    {
        private MonoBehaviour _ovner;
        private Coroutine _process;
        public bool inProcess => _process != null;
        public bool notInProcess => _process == null;

        public CoroutineController(MonoBehaviour ovner)
        {
            _ovner = ovner;
        }

        public void Stop()
        {
            if (inProcess)
            {
                _ovner.StopCoroutine(_process);
                _process = null;
            }
        }

        public void Start(IEnumerator logic, Action endCallback = null)
        {
            if (notInProcess)
                _process = _ovner.StartCoroutine(EndOfProcess(logic, endCallback));
        }

        private IEnumerator EndOfProcess(IEnumerator logic, Action endCallback = null)
        {
            yield return _ovner.StartCoroutine(logic);
            _process = null;
            endCallback?.Invoke();
        }
    }
}