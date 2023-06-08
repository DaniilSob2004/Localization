using System.Globalization;

namespace Localization
{
    public partial class Form1 : Form
    {
        private string lang;  // ��� ��������� ���������� (����� ���� ������������)

        public Form1()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                lang = Properties.Settings.Default.Language;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);  // ��������� �������� ��� �������� ���������� ������������
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);  // ��������� �������� ��� �������������� �����, ��� � �.�.
            }
            else lang = "en-US";

            Initialize();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            // ������� 2 ����� �������� MyData � MyData.en-US(��� ���������� ������)
            var answer = MessageBox.Show(Resources.MyData.ExitRequest, Resources.MyData.ExitTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes) Close();
        }

        private void LoadCombobox()
        {
            comboBox1.DataSource = new CultureInfo[]
            {
                CultureInfo.GetCultureInfo("ru-RU"),
                CultureInfo.GetCultureInfo("en-US")
            };
            comboBox1.DisplayMember = "NativeName";
            comboBox1.ValueMember = "Name";

            if (!String.IsNullOrEmpty(lang))
            {
                comboBox1.SelectedValue = lang;
            }
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedValueChanged;
        }

        private void Initialize()
        {
            InitializeComponent();
            BackgroundImage = Resources.MyData.image;  // ��������� ��������
            LoadCombobox();
        }

        private void ComboBox1_SelectedValueChanged(object? sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString() != lang)
            {
                lang = comboBox1.SelectedValue + "";
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);

                Controls.Clear();
                Initialize();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Language = lang;
            Properties.Settings.Default.Save();
        }
    }
}
