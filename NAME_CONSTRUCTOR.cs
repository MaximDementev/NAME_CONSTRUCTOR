using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
namespace NAME_CONSTRUCTOR
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]

    public class NAME_CONSTRUCTOR : IExternalCommand
    {
        public static ExternalCommandData currentCommandData { get; private set; }

        NameConstructrorForm _nameConstructrorForm = NameConstructrorForm.GetInstance();
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            currentCommandData = commandData;

            _nameConstructrorForm.Show();

            return Result.Succeeded;
        }
        
    }
}

