using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogReader
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.txtDestPath = new System.Windows.Forms.TextBox();
            this.txtTopRes = new System.Windows.Forms.TextBox();
            this.txtTopUser = new System.Windows.Forms.TextBox();
            this.txtPassTop = new System.Windows.Forms.TextBox();
            this.txtPassBot = new System.Windows.Forms.TextBox();
            this.txtBotUser = new System.Windows.Forms.TextBox();
            this.txtBotRes = new System.Windows.Forms.TextBox();
            this.SourcePath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.BackColor = System.Drawing.Color.White;
            this.txtSourcePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourcePath.Enabled = false;
            this.txtSourcePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSourcePath.Location = new System.Drawing.Point(150, 90);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.ReadOnly = true;
            this.txtSourcePath.Size = new System.Drawing.Size(300, 25);
            this.txtSourcePath.TabIndex = 1;
            this.txtSourcePath.TabStop = false;
            // 
            // txtDestPath
            // 
            this.txtDestPath.BackColor = System.Drawing.Color.White;
            this.txtDestPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestPath.Enabled = false;
            this.txtDestPath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDestPath.Location = new System.Drawing.Point(150, 130);
            this.txtDestPath.Name = "txtDestPath";
            this.txtDestPath.ReadOnly = true;
            this.txtDestPath.Size = new System.Drawing.Size(300, 25);
            this.txtDestPath.TabIndex = 2;
            this.txtDestPath.TabStop = false;
            // 
            // txtTopRes
            // 
            this.txtTopRes.BackColor = System.Drawing.Color.White;
            this.txtTopRes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTopRes.Enabled = false;
            this.txtTopRes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTopRes.Location = new System.Drawing.Point(150, 180);
            this.txtTopRes.Name = "txtTopRes";
            this.txtTopRes.ReadOnly = true;
            this.txtTopRes.Size = new System.Drawing.Size(200, 25);
            this.txtTopRes.TabIndex = 3;
            this.txtTopRes.TabStop = false;
            // 
            // txtTopUser
            // 
            this.txtTopUser.BackColor = System.Drawing.Color.White;
            this.txtTopUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTopUser.Enabled = false;
            this.txtTopUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTopUser.Location = new System.Drawing.Point(150, 220);
            this.txtTopUser.Name = "txtTopUser";
            this.txtTopUser.ReadOnly = true;
            this.txtTopUser.Size = new System.Drawing.Size(200, 25);
            this.txtTopUser.TabIndex = 4;
            this.txtTopUser.TabStop = false;
            // 
            // txtPassTop
            // 
            this.txtPassTop.BackColor = System.Drawing.Color.White;
            this.txtPassTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassTop.Enabled = false;
            this.txtPassTop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassTop.Location = new System.Drawing.Point(150, 260);
            this.txtPassTop.Name = "txtPassTop";
            this.txtPassTop.ReadOnly = true;
            this.txtPassTop.Size = new System.Drawing.Size(200, 25);
            this.txtPassTop.TabIndex = 5;
            this.txtPassTop.TabStop = false;
            // 
            // txtPassBot
            // 
            this.txtPassBot.BackColor = System.Drawing.Color.White;
            this.txtPassBot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassBot.Enabled = false;
            this.txtPassBot.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassBot.Location = new System.Drawing.Point(600, 260);
            this.txtPassBot.Name = "txtPassBot";
            this.txtPassBot.ReadOnly = true;
            this.txtPassBot.Size = new System.Drawing.Size(200, 25);
            this.txtPassBot.TabIndex = 8;
            this.txtPassBot.TabStop = false;
            // 
            // txtBotUser
            // 
            this.txtBotUser.BackColor = System.Drawing.Color.White;
            this.txtBotUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBotUser.Enabled = false;
            this.txtBotUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBotUser.Location = new System.Drawing.Point(600, 220);
            this.txtBotUser.Name = "txtBotUser";
            this.txtBotUser.ReadOnly = true;
            this.txtBotUser.Size = new System.Drawing.Size(200, 25);
            this.txtBotUser.TabIndex = 7;
            this.txtBotUser.TabStop = false;
            // 
            // txtBotRes
            // 
            this.txtBotRes.BackColor = System.Drawing.Color.White;
            this.txtBotRes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBotRes.Enabled = false;
            this.txtBotRes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBotRes.Location = new System.Drawing.Point(600, 180);
            this.txtBotRes.Name = "txtBotRes";
            this.txtBotRes.ReadOnly = true;
            this.txtBotRes.Size = new System.Drawing.Size(200, 25);
            this.txtBotRes.TabIndex = 6;
            this.txtBotRes.TabStop = false;
            // 
            // SourcePath
            // 
            this.SourcePath.AutoSize = true;
            this.SourcePath.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.SourcePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.SourcePath.Location = new System.Drawing.Point(50, 93);
            this.SourcePath.Name = "SourcePath";
            this.SourcePath.Size = new System.Drawing.Size(84, 19);
            this.SourcePath.TabIndex = 11;
            this.SourcePath.Text = "Source Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label1.Location = new System.Drawing.Point(50, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Target Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label2.Location = new System.Drawing.Point(50, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Top Resource";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label3.Location = new System.Drawing.Point(50, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 19);
            this.label3.TabIndex = 14;
            this.label3.Text = "Top User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label4.Location = new System.Drawing.Point(50, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "Top Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label5.Location = new System.Drawing.Point(470, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 19);
            this.label5.TabIndex = 16;
            this.label5.Text = "Bottom Resource";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label6.Location = new System.Drawing.Point(470, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 19);
            this.label6.TabIndex = 17;
            this.label6.Text = "Bottom User";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label7.Location = new System.Drawing.Point(470, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 19);
            this.label7.TabIndex = 18;
            this.label7.Text = "Bottom Password";
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMsg.Enabled = false;
            this.txtMsg.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtMsg.Location = new System.Drawing.Point(54, 25);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.Size = new System.Drawing.Size(746, 60);
            this.txtMsg.TabIndex = 9;
            this.txtMsg.TabStop = false;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.Snow;
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTime.Enabled = false;
            this.txtTime.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtTime.Location = new System.Drawing.Point(474, 90);
            this.txtTime.Multiline = true;
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(326, 65);
            this.txtTime.TabIndex = 10;
            this.txtTime.TabStop = false;
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmMain
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(900, 350);
            this.Controls.Add(this.txtSourcePath);
            this.Controls.Add(this.txtDestPath);
            this.Controls.Add(this.txtTopRes);
            this.Controls.Add(this.txtTopUser);
            this.Controls.Add(this.txtPassTop);
            this.Controls.Add(this.txtBotRes);
            this.Controls.Add(this.txtBotUser);
            this.Controls.Add(this.txtPassBot);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.SourcePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Reader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.TextBox txtDestPath;
        private System.Windows.Forms.TextBox txtTopRes;
        private System.Windows.Forms.TextBox txtTopUser;
        private System.Windows.Forms.TextBox txtPassTop;
        private System.Windows.Forms.TextBox txtPassBot;
        private System.Windows.Forms.TextBox txtBotUser;
        private System.Windows.Forms.TextBox txtBotRes;
        private System.Windows.Forms.Label SourcePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Timer timer;
    }
}