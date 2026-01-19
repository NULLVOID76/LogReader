using System.Runtime.InteropServices;
using System.Text;

namespace LogReader
{
    public class IniFile
    {
        private readonly string _path;

        public IniFile(string path)
        {
            _path = path;
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section,
            string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section,
            string key, string defaultValue, StringBuilder returnValue,
            int size, string filePath);

        public void Write(string section, string key, string value)
        {
            try
            {
                WritePrivateProfileString(section, key, value, _path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to write INI file [{section}][{key}]: {ex.Message}");
                throw;
            }
        }

        public string Read(string section, string key, string def = "")
        {
            try
            {
                var retVal = new StringBuilder(255);
                int charsRead = GetPrivateProfileString(section, key, def, retVal, retVal.Capacity, _path);
                return retVal.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to read INI file [{section}][{key}]: {ex.Message}");
                return def;
            }
        }
    }
}
