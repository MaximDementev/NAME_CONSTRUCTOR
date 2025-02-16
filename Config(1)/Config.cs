using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace NAME_CONSTRUCTOR
{
    public class Config
    {
        #region Private Fields

        private static string _configPath = "C:\\Users\\DEMENTEVMAX1\\OneDrive\\Programing\\Revit_NAME_CONSTRUCTOR\\NAME_CONSTRUCTOR\\Data\\Config.xlsx";
        private FileInfo _fileInfo;
        private Dictionary<ConfigDataType, Dictionary<string, string>> _configDataTypeDict = new Dictionary<ConfigDataType, Dictionary<string, string>>
        {
            {ConfigDataType.Disciplines, Disciplines },
            {ConfigDataType.FileFunctions, FileFunctions },
            {ConfigDataType.ObjectTypes, ObjectTypes },
            {ConfigDataType.SectionType, SectionType },
        };

        private static Dictionary<string, string> Disciplines = new Dictionary<string, string>();
        private static Dictionary<string, string> FileFunctions = new Dictionary<string, string>();
        private static Dictionary<string, string> ObjectTypes = new Dictionary<string, string>();
        private static Dictionary<string, string> SectionType = new Dictionary<string, string>();
        #endregion

        //-------------- Methods --------------------------------------------
        public Dictionary<string, string> GetDisciplines()
        {
            if(Disciplines.Count != 0) return Disciplines;
            return LoadFromExcel(ConfigDataType.Disciplines);
        }

        public Dictionary<string, string> GetFileFunctions()
        {
            if (FileFunctions.Count != 0) return FileFunctions;
            return LoadFromExcel(ConfigDataType.FileFunctions);
        }

        public Dictionary<string, string> GetObjectTypes()
        {
            if (ObjectTypes.Count != 0) return ObjectTypes;
            return LoadFromExcel(ConfigDataType.ObjectTypes);
        }

        public Dictionary<string, string> GetSectionType()
        {
            if (SectionType.Count != 0) return SectionType;
            return LoadFromExcel(ConfigDataType.SectionType);
        }

        //------------------------------------------------------------------------
        private Dictionary<string, string> LoadFromExcel(ConfigDataType configDataType)
        {
            if (_fileInfo == null)
            {
                _fileInfo = new FileInfo(_configPath); 
            }
            List<ConfigDataType> keys = new List<ConfigDataType>(_configDataTypeDict.Keys);
            int index = keys.IndexOf(configDataType);
            Dictionary<string, string> currentDictionary = _configDataTypeDict[configDataType];

            using (var package = new ExcelPackage(_fileInfo))
            {
                var worksheet = package.Workbook.Worksheets[index];
                int row = 2;

                while (worksheet.Cells[row, 1].Text != string.Empty || worksheet.Cells[row, 2].Text != string.Empty)
                {
                    string key = worksheet.Cells[row, 1].Text;
                    string value = worksheet.Cells[row, 2].Text;
                    currentDictionary.Add(key, value);
                    row++;
                }
            }
            return currentDictionary;
        }
    }

    public enum ConfigDataType
    {
        Disciplines,
        FileFunctions,
        ObjectTypes,
        SectionType
    }
}
