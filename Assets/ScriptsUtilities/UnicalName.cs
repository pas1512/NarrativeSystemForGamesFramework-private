using System.Collections.Generic;

namespace ScriptsUtilities
{
    public class UnicalName
    {
        private static List<string> _existingNames;

        private string _name;
        public string name => _name;

        public void SetName(string name)
        {
            if(_existingNames == null)
                _existingNames = new List<string>();

            if (_existingNames.Contains(name))
            {
                _name = Validate(name);
                _existingNames.Add(_name);
                return;
            }

            if (_name != "")
                _existingNames.Remove(_name);

            _name = name;
            _existingNames.Add(_name);
        }

        public void UnsetName()
        {
            if (_name != "")
                _existingNames.Remove(_name);
        }

        private string Validate(string name)
        {
            int nameNumber = DetermineNameNumber(name);
            string newName = SetNameNumber(name, nameNumber + 1);

            if (_existingNames.Contains(newName))
                return Validate(name);

            return newName;
        }

        private int DetermineNameNumber(string name)
        {
            string[] strings = name.Split('#');

            if (strings.Length > 1)
            {
                string number = strings[strings.Length - 1];
                return int.Parse(number);
            }

            return 1;
        }

        private string SetNameNumber(string name, int number)
        {
            string[] strings = name.Split('#');

            if (strings.Length > 1)
            {
                strings[strings.Length - 1] = number.ToString();
                return string.Join("#", strings);
            }

            return $"{name}#{number}";
        }
    }
}