namespace KeyFunctionsWinfowsFormsApp
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxSetCursorPosition = new System.Windows.Forms.CheckBox();
            this.checkBoxMaintainClipHistory = new System.Windows.Forms.CheckBox();
            this.checkBoxCleanSpecialCharacters = new System.Windows.Forms.CheckBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // checkBoxSetCursorPosition
            // 
            this.checkBoxSetCursorPosition.AutoSize = true;
            this.checkBoxSetCursorPosition.Location = new System.Drawing.Point(12, 12);
            this.checkBoxSetCursorPosition.Name = "checkBoxSetCursorPosition";
            this.checkBoxSetCursorPosition.Size = new System.Drawing.Size(457, 19);
            this.checkBoxSetCursorPosition.TabIndex = 0;
            this.checkBoxSetCursorPosition.Text = "Set cursor position to one of the following corners of Primary Screen while typin" +
    "g.";
            this.checkBoxSetCursorPosition.UseVisualStyleBackColor = true;
            this.checkBoxSetCursorPosition.CheckedChanged += new System.EventHandler(this.CheckBox_SetCursorPosition_CheckedChanged);
            // 
            // checkBoxMaintainClipHistory
            // 
            this.checkBoxMaintainClipHistory.AutoSize = true;
            this.checkBoxMaintainClipHistory.Location = new System.Drawing.Point(12, 37);
            this.checkBoxMaintainClipHistory.Name = "checkBoxMaintainClipHistory";
            this.checkBoxMaintainClipHistory.Size = new System.Drawing.Size(169, 19);
            this.checkBoxMaintainClipHistory.TabIndex = 1;
            this.checkBoxMaintainClipHistory.Text = "Maintain Clipboard History";
            this.checkBoxMaintainClipHistory.UseVisualStyleBackColor = true;
            this.checkBoxMaintainClipHistory.CheckedChanged += new System.EventHandler(this.CheckBox_MaintainClipHistory_CheckedChanged);
            // 
            // checkBoxCleanSpecialCharacters
            // 
            this.checkBoxCleanSpecialCharacters.AutoSize = true;
            this.checkBoxCleanSpecialCharacters.Location = new System.Drawing.Point(12, 62);
            this.checkBoxCleanSpecialCharacters.Name = "checkBoxCleanSpecialCharacters";
            this.checkBoxCleanSpecialCharacters.Size = new System.Drawing.Size(375, 19);
            this.checkBoxCleanSpecialCharacters.TabIndex = 2;
            this.checkBoxCleanSpecialCharacters.Text = "Clean special characters from clipboard-text which prepend: \'mis \'";
            this.checkBoxCleanSpecialCharacters.UseVisualStyleBackColor = true;
            this.checkBoxCleanSpecialCharacters.CheckedChanged += new System.EventHandler(this.CheckBox_CleanSpecialCharacters_CheckedChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(12, 87);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(657, 351);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.checkBoxCleanSpecialCharacters);
            this.Controls.Add(this.checkBoxMaintainClipHistory);
            this.Controls.Add(this.checkBoxSetCursorPosition);
            this.Name = "MainWindow";
            this.Text = "Key Functions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_OnClosing);
            this.Load += new System.EventHandler(this.MainWindow_OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox checkBoxSetCursorPosition;
        private CheckBox checkBoxMaintainClipHistory;
        private CheckBox checkBoxCleanSpecialCharacters;
        private ListView listView1;
    }
}