using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Form = System.Windows.Forms.Form;

namespace NAME_CONSTRUCTOR
{
    public partial class NameConstructrorForm : Form
    {
        #region Private Fields
        static private string DataPath = "C:\\Users\\DEMENTEVMAX1\\OneDrive\\Programing\\Revit_NAME_CONSTRUCTOR\\NAME_CONSTRUCTOR\\Config\\";
        string _logsFolderPath = "C:\\Users\\DEMENTEVMAX1\\OneDrive\\Programing\\Revit_NAME_CONSTRUCTOR\\NAME_CONSTRUCTOR\\Config\\Logs";
        static private string _Control_buttonName1 = "Сформировать";
        static private string _Control_buttonName2 = "Очистить";
        private Logger _logger = Logger.Instance;
        #endregion

        #region Public Fields
        public static ExternalCommandData commandData { get;set;}
        #endregion


        #region Constructor 
        private static NameConstructrorForm Instance;
        private NameConstructrorForm()
        {
            InitializeComponent();
            InitComboBoxes();
            Control_button.Text = "Сформировать";

            this.FormClosed += Form_Closed;
        }
        #endregion
        public static NameConstructrorForm GetInstance(ExternalCommandData commandData)
        {
            if (Instance == null || Instance.IsDisposed)
            {
                Instance = new NameConstructrorForm();
                NameConstructrorForm.commandData = commandData;
            }
            return Instance;
        }

        //________ Buttons ________________________________________________________________
        private void Control_Click(object sender, EventArgs e)
        {
            try
            { 
                if (Control_button.Text == _Control_buttonName1)
                {
                    Join();
                }
                else
                {
                    ResultClear();
                }
                ChangeControlButtonName();
            }
            catch { }
        }

        private void ClearFields_Click(object sender, EventArgs e)
        {
            Field1_textBox.Clear();
            Field2_comboBox.Text = "";
            Field3_comboBox.Text = "";
            Field4_comboBox.Text = "";
            Field5_comboBox.Text = "";
            Field5_textBox.Clear();
        }

        //_______________________________________________________________________

        private void InitComboBoxes()
        {
            FillComboBoxes();

            Field2_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Field2_comboBox.DropDownWidth = 500;

            Field3_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Field3_comboBox.DropDownWidth = 500;

            Field4_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Field4_comboBox.DropDownWidth = 500;

        }

        private void FillComboBoxes()
        {
            #region Field #2
            List<ObjectDefinition> disciplines = XmlHandler.GetElementsAsObjectDefinitionFromRoot(
                $"{DataPath}Disciplines.xml");

            if (disciplines == null)
            {
                MessageBox.Show("Список дисциплин пуст или не был инициализирован. " +
                    "Обратитесь к BIM-специалисту");
            }
            disciplines.Insert(0, new ObjectDefinition { ShortName = "", FullName = "" });
            Field2_comboBox.DataSource = disciplines;
            Field2_comboBox.SelectedIndex = 0;
            Field2_comboBox.DisplayMember = "FullName";
            Field2_comboBox.ValueMember = "ShortName";
            #endregion

            #region Field #3
            List<ObjectDefinition> objectTypes = XmlHandler.GetElementsAsObjectDefinitionFromRoot(
                $"{DataPath}ObjectTypes.xml");
            if (objectTypes == null)
            {
                MessageBox.Show("Список типов объектов пуст или не был инициализирован. " +
                    "Обратитесь к BIM-специалисту");
            }
            objectTypes.Insert(0, new ObjectDefinition { ShortName = "", FullName = "" });

            Field3_comboBox.DataSource = objectTypes;
            Field3_comboBox.SelectedIndex = 0;
            Field3_comboBox.DisplayMember = "FullName";
            Field3_comboBox.ValueMember = "ShortName";
            #endregion

            #region Field #4
            List<ObjectDefinition> fileFunctions = XmlHandler.GetElementsAsObjectDefinitionFromRoot(
                $"{DataPath}FileFunctions.xml");

            if (fileFunctions == null)
            {
                MessageBox.Show("Список подразделов пуст или не был инициализирован. " +
                    "Обратитесь к BIM-специалисту");
            }
            fileFunctions.Insert(0, new ObjectDefinition { ShortName = "", FullName = "" });

            Field4_comboBox.DataSource = fileFunctions;
            Field4_comboBox.SelectedIndex = 0;
            Field4_comboBox.DisplayMember = "FullName";
            Field4_comboBox.ValueMember = "ShortName";
            #endregion

            #region Field #5
            string[] sectionType = XmlHandler.GetElementsNamesFromRoot(
                $"{DataPath}SectionType.xml")
                .ToArray();

            if (fileFunctions == null)
            {
                MessageBox.Show("Список типов секций пуст или не был инициализирован. " +
                    "Обратитесь к BIM-специалисту");
            }
            Field5_comboBox.Items.AddRange(sectionType);
            #endregion
        }

        private void ChangeControlButtonName()
        {
            if (Control_button.Text == _Control_buttonName1)
            {
                Control_button.Text = _Control_buttonName2;
                TextWasCopied_label.Text = "Имя файла было скопировано \nв буфер обмена";
            }
            else
            {
                Control_button.Text = _Control_buttonName1;
                TextWasCopied_label.Text = "";
            }
        }

        private void Join()
        {
            string Field1 = "";
            string Field2 = "";
            string Field3 = "";
            string Field4 = "";
            string Field5 = "";

            if (!string.IsNullOrEmpty(Field1_textBox.Text))
                Field1 = Field1_textBox.Text;

            if (!string.IsNullOrEmpty(Field2_comboBox.Text))
                Field2 = $"_{Field2_comboBox.SelectedValue.ToString()}";

            if (!string.IsNullOrEmpty(Field3_comboBox.Text))
                Field3 = $"_{Field3_comboBox.SelectedValue.ToString()}";

            if (!string.IsNullOrEmpty(Field4_comboBox.Text))
                Field4 = $"_{Field4_comboBox.SelectedValue.ToString()}";

            if (!string.IsNullOrEmpty(Field5_comboBox.Text) && !string.IsNullOrEmpty(Field5_textBox.Text))
                Field5 = $"_{Field5_comboBox.Text} {Field5_textBox.Text}";

            if (string.IsNullOrEmpty(Field1) ||
                string.IsNullOrEmpty(Field2) ||
                string.IsNullOrEmpty(Field3))
                throw new Exception("Поля пустые");

            string Result = Field1 + Field2 + Field3 + Field4 + Field5;

            Result_textBox.Text = Result;
            Result_textBox.Update();
            Clipboard.SetText(Result);

            _logger.Log(Result);
        }

        private void ResultClear()
        {
            Result_textBox.Clear();
            Result_textBox.Update();
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            string timeNow = DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss");
            string user = commandData.Application.Application.Username;
            string tempFilePath = Path.Combine
                ($"{_logsFolderPath}\\{user}_RVT_NAME_CONSTR{timeNow}.txt");

            string AllLogs = _logger.GetAllLogs();

            if (string.IsNullOrEmpty(AllLogs))
                return;

            if (Directory.Exists(_logsFolderPath))
                { File.WriteAllText(tempFilePath, AllLogs); } 
            _logger.Clear();

        }
    }
}
