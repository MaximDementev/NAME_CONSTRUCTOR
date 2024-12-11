using System;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;


namespace NAME_CONSTRUCTOR 
{
    internal class Logger
    {

        #region Конструктор Singletone
        internal static Logger Instance { get; set; } = new Logger();
        private Logger()
        { }
        #endregion

        private List <string> logs = new List<string>();
        internal void Log(string log)
        {
            if (string.IsNullOrEmpty(log)) return;
            logs.Add($"{log}");
        }
        internal string GetAllLogs()
        {
            string time = DateTime.Now.ToString();
            string user = NAME_CONSTRUCTOR.currentCommandData.Application.Application.Username;

            string logsAsString = ($"==== {time} == {user} ====");
            
            foreach (string log in logs)
            {
                logsAsString += ($"\n{log}");
            }
            return logsAsString;
        }

        internal void Clear()
        {
            logs.Clear();
        }
    }
}
