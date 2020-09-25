
using System;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UserFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int selectedIndex;
        string MS_key;
        int i = 0;
        string key;
        

        private void button1_Click(object sender, EventArgs e)
        {
            selectedIndex = comboBox1.SelectedIndex;
            if(selectedIndex == 0)
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                foreach (ManagementObject hdd in searcher.Get())
                {
                    MS_key += i.ToString() + ". " + hdd["SerialNumber"].ToString() + " \n";
                    i++;
                }
                MessageBox.Show(MS_key);
                i = 0;
                FileStream fstream = new FileStream(@"E:\Desktop\RAMIL\UserFiles\HDDs.txt", FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(fstream);
                writer.WriteLine(MS_key);
                writer.Close();
            }
            else if (selectedIndex == 1)
            {
                FileStream fstream = new FileStream(@"E:\Desktop\RAMIL\UserFiles\Porol.txt", FileMode.OpenOrCreate);
                ushort secretKey = 0x0088; // Секретный ключ (длина - 16 bit).
                string str1 = "Hello World"; //это строка которую мы зашифруем
                string str2;
                //MessageBox.Show(Convert.ToInt32(secretKey).ToString());
                int enteredSecretKey = Convert.ToInt32(textBox1.Text);
                ushort bit16Key = Convert.ToUInt16(enteredSecretKey & 0xffff);

                str1 = EncodeDecrypt(str1, secretKey); //производим шифрование

                str2 = EncodeDecrypt(str1, bit16Key); //производим рассшифровку
                StreamWriter writer = new StreamWriter(fstream);
                writer.WriteLine(str1);
                writer.Close();

                textBox2.Text = str2;
                
            }
            else if (selectedIndex == 2)
            {
                //Directory.CreateDirectory(@"E:\Desktop\RAMIL\UserFiles\1");
                //Directory.CreateDirectory(@"E:\Desktop\RAMIL\UserFiles\2");
                //File.WriteAllText(@"E:\Desktop\RAMIL\UserFiles\1\1.txt", "текст");
                File.Move(@"E:\Desktop\RAMIL\UserFiles\1\1.txt", @"E:\Desktop\RAMIL\UserFiles\2\2.txt");
                File.Copy(@"E:\Desktop\RAMIL\UserFiles\2\3.txt", @"E:\Desktop\RAMIL\UserFiles\1\3.txt");
            }
        }

        public static string EncodeDecrypt(string str, ushort secretKey)
        {
            var ch = str.ToArray(); //преобразуем строку в символы
            string newStr = "";      //переменная которая будет содержать зашифрованную строку
            foreach (var c in ch)  //выбираем каждый элемент из массива символов нашей строки
                newStr += TopSecret(c, secretKey);  //производим шифрование каждого отдельного элемента и сохраняем его в строку
            return newStr;
        }

        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey); //Производим XOR операцию
            return character;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            try
            {
                Directory.Delete(@"E:\Desktop\RAMIL\UserFiles\1", true);
                Directory.Delete(@"E:\Desktop\RAMIL\UserFiles\2", true);
            }
            catch (Exception ex)
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 2)
            {
                button4.Visible = true;
            }
            else
            {
                button4.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"E:\Desktop\RAMIL\UserFiles\1");
            Directory.CreateDirectory(@"E:\Desktop\RAMIL\UserFiles\2");
            File.WriteAllText(@"E:\Desktop\RAMIL\UserFiles\1\1.txt", "текст1");
            File.WriteAllText(@"E:\Desktop\RAMIL\UserFiles\2\3.txt", "текст3");
        }
    }
}
