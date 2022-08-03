using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace admin
{
    public partial class status : Form
    {
        public int mode;
        public static Color SUCCESS = Color.FromArgb(0,255,0);
        public static Color ERROR = Color.FromArgb(255,0,0);

        // Add functions that generate success / error during a given delay
        public void ShowMessage(bool success, string message)
        {
            // Setting the color
            if (success) this.msg.ForeColor = status.SUCCESS;
            else this.msg.ForeColor = status.ERROR;

            // Setting the message
            if (message == "")
            {
                if (success) this.msg.Text = "Réussite !";
                else this.msg.Text = "Echec ! ";
            }
            else
            {
                this.msg.Text = message;
            }

            // Triggering the delay
            Task t = Task.Run(() => {
                this.Show();
                // Add the delay here
                Task.Delay(5000); // 5 seconds
            });
            t.Wait();
            this.Hide();
        }

        public status()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
