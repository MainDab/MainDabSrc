using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace ProjectMainDab
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("main_ex"))
            {
                MessageBox.Show("DONT YOU DARE TRY FUCKING IMPERSONATE ME BITCH", "Main_EX says no");
            }
            else if (textBox1.Text == (""))
            {
                MessageBox.Show("Please type in a username");
            }
            else if (textBox1.Text == (" "))
            {
                MessageBox.Show("Please type in a proper username");
            }
            else
            {
                string username = textBox1.Text;
                string dusername = textBox2.Text;
                using (dWebHook dcWeb = new dWebHook())
                {
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\OurSettings");
                    key.SetValue("Setting1", (username));
                    key.Close();
                    dcWeb.ProfilePicture = "https://www.kindpng.com/picc/m/287-2874533_discord-server-icon-logo-discord-png-transparent-png.png";
                    dcWeb.UserName = "Sign up bot";
                    dcWeb.WebHook = ("https://discordapp.com/api/webhooks/692356553138765914/e6U3hmqWbgrrXix036OIcU_Fm5QHAd7HtA8i-4qGhiJv8YMPhwbL7HSaXb6J37Jnuc34");
                    dcWeb.SendMessage("```" + "A new user signed up! YAY!\nUsername : " + (username) + ("\nDiscord Username :") + (dusername) + "```");
                    Form3 form = new Form3();
                    form.Show();

                    base.Hide();
                }
            }
        }

        private void TitleDraggable_Click(object sender, EventArgs e)
        {
        }
    }
}