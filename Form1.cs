using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Utilities;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using GlobalLowLevelHooks;

namespace KeystrokesByErx
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Color HsvToRgb(float h, float s, float v)
        {
            int i;
            float f, p, q, t;

            if (s < float.Epsilon)
            {
                int c = (int)(v * 255);
                return Color.FromArgb(c, c, c);
            }

            h /= 60;
            i = (int)Math.Floor(h);
            f = h - i;
            p = v * (1 - s);
            q = v * (1 - s * f);
            t = v * (1 - s * (1 - f));

            float r, g, b;
            switch (i)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                default: r = v; g = p; b = q; break;
            }

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }
        float h = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            h++;
            if (h >= 360) h = 0;
            panelW.BorderColor = HsvToRgb(h, 1f, 1f);
            labelW.ForeColor = HsvToRgb(h, 1f, 1f);
            panelA.BorderColor = HsvToRgb(h, 0.8f, 1f);
            labelA.ForeColor = HsvToRgb(h, 0.8f, 1f);
            panelS.BorderColor = HsvToRgb(h, 0.8f, 1f);
            labelS.ForeColor = HsvToRgb(h, 0.8f, 1f);
            panelD.BorderColor = HsvToRgb(h, 0.8f, 1f);
            labelD.ForeColor = HsvToRgb(h, 0.8f, 1f);
            panelSpace.BorderColor = HsvToRgb(h, 0.6f, 1f);
            labelSpace.ForeColor = HsvToRgb(h, 0.6f, 1f);
            panelLMB.BorderColor = HsvToRgb(h, 0.4f, 1f);
            labelLMB.ForeColor = HsvToRgb(h, 0.4f, 1f);
            panelRMB.BorderColor = HsvToRgb(h, 0.4f, 1f);
            labelRMB.ForeColor = HsvToRgb(h, 0.4f, 1f);
        }
        KeyboardHook keyboardHook = new KeyboardHook();
        MouseHook mouseHook = new MouseHook();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
            mouseHook.LeftButtonDown += new MouseHook.MouseHookCallback(mouseHook_LeftButtonDown);
            mouseHook.LeftButtonUp += new MouseHook.MouseHookCallback(mouseHook_LeftButtonUp);
            mouseHook.RightButtonDown += new MouseHook.MouseHookCallback(mouseHook_RightButtonDown);
            mouseHook.RightButtonUp += new MouseHook.MouseHookCallback(mouseHook_RightButtonUp);
            mouseHook.Install();
            keyboardHook.KeyDown += new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyUp);
            keyboardHook.Install();
        }

        

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        /*private void mouseHook_MouseMove(MouseHook.)
        {
            Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] KeyDown Event {" + key.ToString() + "}");
        }*/
        private void keyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            if (key == KeyboardHook.VKeys.KEY_W)
            {
                panelW.BackgroundColor = System.Drawing.Color.White;
            }
            if (key == KeyboardHook.VKeys.KEY_A)
            {
                panelA.BackgroundColor = System.Drawing.Color.White;
            }
            if (key == KeyboardHook.VKeys.KEY_S)
            {
                panelS.BackgroundColor = System.Drawing.Color.White;
            }
            if (key == KeyboardHook.VKeys.KEY_D)
            {
                panelD.BackgroundColor = System.Drawing.Color.White;
            }
            if (key == KeyboardHook.VKeys.SPACE)
            {
                panelSpace.BackgroundColor = System.Drawing.Color.White;
            }
        }
        private void keyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            if (key == KeyboardHook.VKeys.KEY_W)
            {
                panelW.BackgroundColor = System.Drawing.Color.Black;
            }
            if (key == KeyboardHook.VKeys.KEY_A)
            {
                panelA.BackgroundColor = System.Drawing.Color.Black;
            }
            if (key == KeyboardHook.VKeys.KEY_S)
            {
                panelS.BackgroundColor = System.Drawing.Color.Black;
            }
            if (key == KeyboardHook.VKeys.KEY_D)
            {
                panelD.BackgroundColor = System.Drawing.Color.Black;
            }
            if (key == KeyboardHook.VKeys.SPACE)
            {
                panelSpace.BackgroundColor = System.Drawing.Color.Black;
            }
        }
        private void mouseHook_LeftButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            panelLMB.BackgroundColor = System.Drawing.Color.White;
        }
        private void mouseHook_LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            panelLMB.BackgroundColor = System.Drawing.Color.Black;
        }
        private void mouseHook_RightButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            panelRMB.BackgroundColor = System.Drawing.Color.White;
        }
        private void mouseHook_RightButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            panelRMB.BackgroundColor = System.Drawing.Color.Black;
        }

        private void OnApplicationExit(object sender, FormClosingEventArgs e)
        {

            keyboardHook.KeyDown -= new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            keyboardHook.KeyUp -= new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyUp);
            keyboardHook.Uninstall();
            mouseHook.LeftButtonDown -= new MouseHook.MouseHookCallback(mouseHook_LeftButtonDown);
            mouseHook.LeftButtonUp -= new MouseHook.MouseHookCallback(mouseHook_LeftButtonUp);
            mouseHook.RightButtonDown -= new MouseHook.MouseHookCallback(mouseHook_RightButtonDown);
            mouseHook.RightButtonUp -= new MouseHook.MouseHookCallback(mouseHook_RightButtonUp);
            mouseHook.Uninstall();
        }
    }
}
