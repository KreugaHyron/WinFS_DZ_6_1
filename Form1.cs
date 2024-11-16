using System.Windows.Forms;
using WinFS_DZ_6_1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFS_DZ_6_1
{
    public partial class EventPlannerForm : Form
    {
        private ListBox lstEvents;
        private DateTimePicker dtpEventDate;
        private System.Windows.Forms.TextBox txtEventName;
        private System.Windows.Forms.Button btnAddEvent;
        private System.Windows.Forms.Button btnDeleteEvent;
        private System.Windows.Forms.Button btnMarkAsCompleted;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolTip toolTip;
        public EventPlannerForm()
        {
            InitializeComponent();
            Text = "Планувальник подій";
            Width = 400;
            Height = 500;

            // Ініціалізація компонентів
            lstEvents = new ListBox { Top = 20, Left = 20, Width = 340, Height = 200 };
            dtpEventDate = new DateTimePicker { Top = 230, Left = 20, Width = 200 };
            txtEventName = new System.Windows.Forms.TextBox { Top = 260, Left = 20, Width = 200, PlaceholderText = "Назва події" };
            btnAddEvent = new System.Windows.Forms.Button { Top = 260, Left = 240, Width = 120, Text = "Додати подію" };
            btnDeleteEvent = new System.Windows.Forms.Button { Top = 300, Left = 240, Width = 120, Text = "Видалити подію" };
            btnMarkAsCompleted = new System.Windows.Forms.Button { Top = 340, Left = 240, Width = 120, Text = "Виконано" };
            progressBar = new System.Windows.Forms.ProgressBar { Top = 380, Left = 20, Width = 340, Height = 20 };

            toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnAddEvent, "Натисніть, щоб додати подію.");
            toolTip.SetToolTip(btnDeleteEvent, "Натисніть, щоб видалити вибрану подію.");
            toolTip.SetToolTip(btnMarkAsCompleted, "Натисніть, щоб позначити подію як виконану.");

            Controls.Add(lstEvents);
            Controls.Add(dtpEventDate);
            Controls.Add(txtEventName);
            Controls.Add(btnAddEvent);
            Controls.Add(btnDeleteEvent);
            Controls.Add(btnMarkAsCompleted);
            Controls.Add(progressBar);

            btnAddEvent.Click += BtnAddEvent_Click;
            btnDeleteEvent.Click += BtnDeleteEvent_Click;
            btnMarkAsCompleted.Click += BtnMarkAsCompleted_Click;
        }

        private void BtnAddEvent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEventName.Text))
            {
                MessageBox.Show("Введіть назву події.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string eventText = $"{dtpEventDate.Value.ToShortDateString()} - {txtEventName.Text}";
            lstEvents.Items.Add(eventText);
            UpdateProgressBar();
        }

        private void BtnDeleteEvent_Click(object sender, EventArgs e)
        {
            if (lstEvents.SelectedItem != null)
            {
                lstEvents.Items.Remove(lstEvents.SelectedItem);
                UpdateProgressBar();
            }
            else
            {
                MessageBox.Show("Виберіть подію для видалення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnMarkAsCompleted_Click(object sender, EventArgs e)
        {
            if (lstEvents.SelectedItem != null)
            {
                int index = lstEvents.SelectedIndex;
                string item = lstEvents.Items[index].ToString();
                if (!item.Contains("[Виконано]"))
                {
                    lstEvents.Items[index] = $"{item} [Виконано]";
                    UpdateProgressBar();
                }
                else
                {
                    MessageBox.Show("Ця подія вже позначена як виконана.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Виберіть подію для відмітки як виконану.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateProgressBar()
        {
            int totalEvents = lstEvents.Items.Count;
            int completedEvents = lstEvents.Items.Cast<string>().Count(item => item.Contains("[Виконано]"));

            progressBar.Maximum = totalEvents == 0 ? 1 : totalEvents;
            progressBar.Value = completedEvents;
        }
    }
}
    public static class Program
{
    [STAThread]
    public static void MainProgram()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new EventPlannerForm());
    }
}