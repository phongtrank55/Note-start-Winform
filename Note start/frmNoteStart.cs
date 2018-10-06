using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Note_start
{
    public partial class frmNoteStart : Form
    {
        private int number=-1;
        private int LenStory = 7;
        public frmNoteStart()
        {
            InitializeComponent();
            SetShape();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // MessageBox.Show(DateTime.Now.ToShortDateString());
            SetKeyRegistry();
            ShowStory();
            //richTextBox1.Text = "";
        }
        private void SetShape()
        {
            //Vẽ hình cho pictureBox
            GraphicsPath shape= new GraphicsPath();
            shape.AddEllipse(0,0,pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Region = new Region(shape);
            shape.Reset();
            //shape.CloseAllFigures();
            //Vẽ hình cho panel
            Point[] pointPanel = new Point[] { new Point(10, 2), new Point(panel1.Size.Width, 2), new Point(panel1.Size.Width, panel1.Size.Height), new Point(10, panel1.Size.Height), new Point(10, 55), new Point(0, 45), new Point(10, 35)};
            shape.AddPolygon(pointPanel);
            panel1.Region = new Region(shape);
            shape.CloseAllFigures();
        }
        private void SetKeyRegistry()
        {
            //Kiểm tra và tạo ra chương trình tự khởi động
            string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            //Registry.SetValue(key, "Anh", "abc");
            //RegistryKey cmdkey = key;
            string valueKey = "Ahihi";
            string path = Application.StartupPath+"Note start.exe";
            string cmd = (string)Registry.GetValue(key, "Ahihi", "No");
            if (cmd == "No")
            {
                RegistryKey HKMachine = Registry.LocalMachine;
                RegistryKey HKSoftware = HKMachine.OpenSubKey("SOFTWARE",RegistryKeyPermissionCheck.ReadWriteSubTree);
                HKSoftware.CreateSubKey("DateAhihi",RegistryKeyPermissionCheck.ReadWriteSubTree);
                Registry.SetValue(key, valueKey, path);
                SaveLastDay();
                Application.Exit();
            }
        }
        private void ShowStory()
        {
            RegistryKey HKmine = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("DateAhihi");
            //Nếu trong ngày đã mở rồi thì k mở nữa
            string dateNow = DateTime.Now.ToShortDateString();
            string date=(string)(HKmine.GetValue("LastDate","23"));
            if (dateNow == date)
            { Application.Exit(); return; }
            number = int.Parse((string)HKmine.GetValue("Number","100"));
            HKmine.Close();
            //Lấy câu chuyện
            string[] story = new string[LenStory];
            story[0] = "\n   Cuộc đời tôi vốn dĩ cô độc trong màn đêm mù mịt. Bỗng ngày bạn xuất hiện, như ánh nến chói lòa xua đi mọi tăm tối trong lòng. Tôi lại gần, nhăn răng mỉm cười và... thổi tắt nó :v. \n\r\n\rNguồn: Joker chế.";
            story[1] = "\n   Hai bố con mùa hè đi cắm trại. Giữa đêm tỉnh dậy ông bố hỏi: \n\rBố: Con có thấy gì không? \n\rCon: Là bầu trời đầy sao, là tình yêu bao la bố dành cho con! Ahihi.\n\rBố: Ngu vler! Lều bị thằng nào trộm mất rồi! \n\r\n\rNguồn: Anh hai phê cần.";
            story[2] = "\nSáng nay thầy Giáo ghé nhà\n\rBà già cắt tiết con gà đãi chơi\n\rThầy cười..:\"khách sáo..trời ơi..\"\n\rDứt câu thầy đớp tả tơi con gà\n\n Nguồn: Internet.";
            story[3] = "\nĐã 20 mùa lúa trổ bông\n\rChưa 1 lần sờ m**g ai đó.\n\rVà cũng qua rồi từng ấy mùa khoai sọ,\n\rChưa 1 lần này nọ với ai kia.\n\n\nNguồn: Nhật ký tuổi tank xuân.";
            story[4] = "\n Một anh chàng soái ca giơ tay lên trời thề với bạn gái: \"Nếu anh có ai khác, anh sẽ bị sét đánh\".\n\rBỗng trên trời vọng lại tiếng nói: \"Chờ tý tao đang sạc điện.\"\n\n\nNguồn: Internet.";
            story[5] = "\n   Tôi vẫn hay trầm tư suy nghĩ về cuộc đời: Có khi nào trên đường đời tấp nập. Ta vô tình vấp phải tập đô la :))\n\n\nNguồn: Internet.";
            story[6] = "\n    Hôm nay tôi sẽ ban cho bạn 1 điều ước: Là bạn sẽ không còn thấy cái này nữa :)) \nChúc bạn ngày càng xinh đẹp như mình. Ahihi :v \n\n\nPT đẹp zai không gay";
            
            
            richTextBox1.Text = story[number];
            //Nếu Đã đọc hết chuyện thì xóa toàn bộ registry
            if (number + 1 >= LenStory) RemoveAll();
            else SaveLastDay();
        }
        private void RemoveAll()
        {
            //string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            //Xóa bỏ khởi động
            RegistryKey HKSoftware = Registry.LocalMachine.OpenSubKey("SOFTWARE",RegistryKeyPermissionCheck.ReadWriteSubTree);
            RegistryKey Run = HKSoftware.OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Run", RegistryKeyPermissionCheck.ReadWriteSubTree);
            
            Run.DeleteValue("Ahihi");
            Run.Close();
            //Xóa bỏ thông tin đã lưu
            HKSoftware.DeleteSubKey("DateAhihi");
            HKSoftware.Close();
        }
        private void SaveLastDay()
        {
                RegistryKey HKmine = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("DateAhihi", true);
                string num = (number + 1).ToString();
                //HKmine.DeleteSubKey("LastDate");
                //HKmine.DeleteSubKey("Number");
                string date = DateTime.Now.ToShortDateString();
                HKmine.SetValue("LastDate", date, RegistryValueKind.String);
                HKmine.SetValue("Number", num, RegistryValueKind.String);
                HKmine.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Di chuyên form
        int XStart, YStart;
        bool ismove = false;
        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                ismove = true;
                XStart = e.X;
                YStart = e.Y;
            }
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if(ismove)
            {
                this.Left += e.X - XStart;
                this.Top += e.Y - YStart;
            }
        }

        private void toolStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            ismove = false;
        }



    }
}
