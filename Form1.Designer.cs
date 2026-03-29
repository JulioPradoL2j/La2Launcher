using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace La2Laucher
{
    partial class Form1
    {
        private IContainer components = null;

        private PictureBox backgroundBox;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private Panel btnPlayArea;
        private Panel btnFullCheckArea;
        private Panel btnCancelArea;

        // barra de arquivos
        private OrangeProgressBar arquivosBar;
        private System.Windows.Forms.Label lblStatus;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                if (trayIcon != null)
                    trayIcon.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.backgroundBox = new System.Windows.Forms.PictureBox();
            this.btnPlayArea = new System.Windows.Forms.Panel();
            this.btnFullCheckArea = new System.Windows.Forms.Panel();
            this.btnCancelArea = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.arquivosBar = new La2Laucher.OrangeProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundBox)).BeginInit();
            this.backgroundBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundBox
            // 
            this.backgroundBox.Controls.Add(this.btnPlayArea);
            this.backgroundBox.Controls.Add(this.btnFullCheckArea);
            this.backgroundBox.Controls.Add(this.btnCancelArea);
            this.backgroundBox.Controls.Add(this.arquivosBar);
            this.backgroundBox.Controls.Add(this.lblStatus);
            this.backgroundBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundBox.Image = global::La2Laucher.Properties.Resources.backgroundStart;
            this.backgroundBox.Location = new System.Drawing.Point(0, 0);
            this.backgroundBox.Name = "backgroundBox";
            this.backgroundBox.Size = new System.Drawing.Size(900, 500);
            this.backgroundBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.backgroundBox.TabIndex = 0;
            this.backgroundBox.TabStop = false;
            // 
            // btnPlayArea
            // 
            this.btnPlayArea.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayArea.Location = new System.Drawing.Point(349, 353);
            this.btnPlayArea.Name = "btnPlayArea";
            this.btnPlayArea.Size = new System.Drawing.Size(150, 27);
            this.btnPlayArea.TabIndex = 1;
            this.btnPlayArea.Click += new System.EventHandler(this.BtnPlay_Click);

            // 
            // btnFullCheckArea
            // 
            this.btnFullCheckArea.BackColor = System.Drawing.Color.Transparent;
            this.btnFullCheckArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFullCheckArea.Location = new System.Drawing.Point(345, 435);
            this.btnFullCheckArea.Name = "btnFullCheckArea";
            this.btnFullCheckArea.Size = new System.Drawing.Size(150, 25);
            this.btnFullCheckArea.TabIndex = 2;
            this.btnFullCheckArea.Click += new System.EventHandler(this.BtnCheckFull_Click);
            // 
            // btnCancelArea
            // 
            this.btnCancelArea.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelArea.Location = new System.Drawing.Point(340, 473);
            this.btnCancelArea.Name = "btnCancelArea";
            this.btnCancelArea.Size = new System.Drawing.Size(150, 20);
            this.btnCancelArea.TabIndex = 3;
            this.btnCancelArea.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(115, 377);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(620, 20);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Inicializando...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trayMenu
            // 
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Icon = global::La2Laucher.Properties.Resources.favicon;
            this.trayIcon.Text = "L2 Launcher";
            this.trayIcon.Visible = true;
            // 
            // arquivosBar
            // 
            this.arquivosBar.BackColorBar = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.arquivosBar.EndColor = System.Drawing.Color.DarkOrange;
            this.arquivosBar.Location = new System.Drawing.Point(107, 399);
            this.arquivosBar.Name = "arquivosBar";
            this.arquivosBar.Size = new System.Drawing.Size(637, 7);
            this.arquivosBar.StartColor = System.Drawing.Color.Orange;
            this.arquivosBar.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.backgroundBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::La2Laucher.Properties.Resources.favicon;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "L2 Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.backgroundBox)).EndInit();
            this.backgroundBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}