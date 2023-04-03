namespace ScriptsUtilities
{
    public class ValueMemorizer<T>
    {
        private T _oldValue;
        public T value => _oldValue;

        public ValueMemorizer(T startValue)
        {
            _oldValue = startValue;
        }

        public bool Changed(T value)
        {
            bool changed = !_oldValue.Equals(value);

            if (changed)
                _oldValue = value;

            return changed;
        }
    }
}