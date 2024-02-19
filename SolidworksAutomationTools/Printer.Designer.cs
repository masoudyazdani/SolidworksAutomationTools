namespace SolidworksAutomationTools
{
    partial class Printer
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
            this.selectMainAssembly = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mainAssembly = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bomAssemblyTxt = new System.Windows.Forms.TextBox();
            this.bomAssemblySelect = new System.Windows.Forms.Button();
            this.printBtn = new System.Windows.Forms.Button();
            this.printChkbox = new System.Windows.Forms.CheckBox();
            this.ignorePrinterFileChkbox = new System.Windows.Forms.CheckBox();
            this.outputFolder = new System.Windows.Forms.TextBox();
            this.outputFolderSelectBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.vaultList = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeUsagelbl = new System.Windows.Forms.Label();
            this.bundlePDFsChkbox = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.includePictureChk = new System.Windows.Forms.CheckBox();
            this.generateBOMChkbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectMainAssembly
            // 
            this.selectMainAssembly.Location = new System.Drawing.Point(486, 15);
            this.selectMainAssembly.Name = "selectMainAssembly";
            this.selectMainAssembly.Size = new System.Drawing.Size(155, 50);
            this.selectMainAssembly.TabIndex = 0;
            this.selectMainAssembly.Text = "Select Main Assembly";
            this.selectMainAssembly.UseVisualStyleBackColor = true;
            this.selectMainAssembly.Visible = false;
            this.selectMainAssembly.Click += new System.EventHandler(this.selectMainAssembly_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Main Assembly";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // mainAssembly
            // 
            this.mainAssembly.Location = new System.Drawing.Point(105, 30);
            this.mainAssembly.Name = "mainAssembly";
            this.mainAssembly.ReadOnly = true;
            this.mainAssembly.Size = new System.Drawing.Size(362, 20);
            this.mainAssembly.TabIndex = 2;
            this.mainAssembly.Visible = false;
            this.mainAssembly.TextChanged += new System.EventHandler(this.mainAssembly_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "BOM Assembly";
            // 
            // bomAssemblyTxt
            // 
            this.bomAssemblyTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bomAssemblyTxt.Location = new System.Drawing.Point(105, 108);
            this.bomAssemblyTxt.Name = "bomAssemblyTxt";
            this.bomAssemblyTxt.ReadOnly = true;
            this.bomAssemblyTxt.Size = new System.Drawing.Size(362, 20);
            this.bomAssemblyTxt.TabIndex = 5;
            // 
            // bomAssemblySelect
            // 
            this.bomAssemblySelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bomAssemblySelect.Location = new System.Drawing.Point(486, 93);
            this.bomAssemblySelect.Name = "bomAssemblySelect";
            this.bomAssemblySelect.Size = new System.Drawing.Size(155, 50);
            this.bomAssemblySelect.TabIndex = 4;
            this.bomAssemblySelect.Text = "Select BOM Assembly";
            this.bomAssemblySelect.UseVisualStyleBackColor = true;
            this.bomAssemblySelect.Click += new System.EventHandler(this.toPrintSelect_Click);
            // 
            // printBtn
            // 
            this.printBtn.Location = new System.Drawing.Point(142, 436);
            this.printBtn.Name = "printBtn";
            this.printBtn.Size = new System.Drawing.Size(155, 50);
            this.printBtn.TabIndex = 6;
            this.printBtn.Text = "Run";
            this.printBtn.UseVisualStyleBackColor = true;
            this.printBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // printChkbox
            // 
            this.printChkbox.AutoSize = true;
            this.printChkbox.Location = new System.Drawing.Point(142, 242);
            this.printChkbox.Name = "printChkbox";
            this.printChkbox.Size = new System.Drawing.Size(47, 17);
            this.printChkbox.TabIndex = 7;
            this.printChkbox.Text = "Print";
            this.printChkbox.UseVisualStyleBackColor = true;
            this.printChkbox.CheckedChanged += new System.EventHandler(this.print_CheckedChanged);
            // 
            // ignorePrinterFileChkbox
            // 
            this.ignorePrinterFileChkbox.AutoSize = true;
            this.ignorePrinterFileChkbox.Enabled = false;
            this.ignorePrinterFileChkbox.Location = new System.Drawing.Point(142, 265);
            this.ignorePrinterFileChkbox.Name = "ignorePrinterFileChkbox";
            this.ignorePrinterFileChkbox.Size = new System.Drawing.Size(116, 17);
            this.ignorePrinterFileChkbox.TabIndex = 9;
            this.ignorePrinterFileChkbox.Text = "Ignore Printed Files";
            this.ignorePrinterFileChkbox.UseVisualStyleBackColor = true;
            // 
            // outputFolder
            // 
            this.outputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFolder.Location = new System.Drawing.Point(105, 185);
            this.outputFolder.Name = "outputFolder";
            this.outputFolder.Size = new System.Drawing.Size(362, 20);
            this.outputFolder.TabIndex = 13;
            this.outputFolder.TextChanged += new System.EventHandler(this.outputFolder_TextChanged);
            // 
            // outputFolderSelectBtn
            // 
            this.outputFolderSelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFolderSelectBtn.Location = new System.Drawing.Point(486, 170);
            this.outputFolderSelectBtn.Name = "outputFolderSelectBtn";
            this.outputFolderSelectBtn.Size = new System.Drawing.Size(155, 50);
            this.outputFolderSelectBtn.TabIndex = 12;
            this.outputFolderSelectBtn.Text = "Select Output Folder";
            this.outputFolderSelectBtn.UseVisualStyleBackColor = true;
            this.outputFolderSelectBtn.Click += new System.EventHandler(this.outputFolderSelectBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output Folder";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Vault";
            // 
            // vaultList
            // 
            this.vaultList.FormattingEnabled = true;
            this.vaultList.Location = new System.Drawing.Point(20, 242);
            this.vaultList.Name = "vaultList";
            this.vaultList.Size = new System.Drawing.Size(106, 173);
            this.vaultList.TabIndex = 17;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView1.Location = new System.Drawing.Point(315, 242);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.Size = new System.Drawing.Size(326, 337);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Item No.";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Part No.";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // treeUsagelbl
            // 
            this.treeUsagelbl.AutoSize = true;
            this.treeUsagelbl.ForeColor = System.Drawing.Color.Maroon;
            this.treeUsagelbl.Location = new System.Drawing.Point(397, 585);
            this.treeUsagelbl.Name = "treeUsagelbl";
            this.treeUsagelbl.Size = new System.Drawing.Size(244, 13);
            this.treeUsagelbl.TabIndex = 21;
            this.treeUsagelbl.Text = "\"tree.txt\" will be used for generating the BOM tree.";
            this.treeUsagelbl.Visible = false;
            // 
            // bundlePDFsChkbox
            // 
            this.bundlePDFsChkbox.AutoSize = true;
            this.bundlePDFsChkbox.Checked = true;
            this.bundlePDFsChkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bundlePDFsChkbox.Location = new System.Drawing.Point(142, 288);
            this.bundlePDFsChkbox.Name = "bundlePDFsChkbox";
            this.bundlePDFsChkbox.Size = new System.Drawing.Size(119, 17);
            this.bundlePDFsChkbox.TabIndex = 22;
            this.bundlePDFsChkbox.Text = "Bundle Printed Files";
            this.bundlePDFsChkbox.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(142, 344);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(138, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Add Where Used Table";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(142, 318);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(111, 17);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "Update Properties";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Visible = false;
            // 
            // includePictureChk
            // 
            this.includePictureChk.AutoSize = true;
            this.includePictureChk.Enabled = false;
            this.includePictureChk.Location = new System.Drawing.Point(142, 398);
            this.includePictureChk.Name = "includePictureChk";
            this.includePictureChk.Size = new System.Drawing.Size(129, 17);
            this.includePictureChk.TabIndex = 19;
            this.includePictureChk.Text = "Include Parts Pictures";
            this.includePictureChk.UseVisualStyleBackColor = true;
            this.includePictureChk.Visible = false;
            // 
            // generateBOMChkbox
            // 
            this.generateBOMChkbox.AutoSize = true;
            this.generateBOMChkbox.Location = new System.Drawing.Point(142, 375);
            this.generateBOMChkbox.Name = "generateBOMChkbox";
            this.generateBOMChkbox.Size = new System.Drawing.Size(97, 17);
            this.generateBOMChkbox.TabIndex = 23;
            this.generateBOMChkbox.Text = "Generate BOM";
            this.generateBOMChkbox.UseVisualStyleBackColor = true;
            this.generateBOMChkbox.Visible = false;
            this.generateBOMChkbox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // Printer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 615);
            this.Controls.Add(this.generateBOMChkbox);
            this.Controls.Add(this.bundlePDFsChkbox);
            this.Controls.Add(this.treeUsagelbl);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.includePictureChk);
            this.Controls.Add(this.vaultList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.outputFolder);
            this.Controls.Add(this.outputFolderSelectBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.ignorePrinterFileChkbox);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.printChkbox);
            this.Controls.Add(this.printBtn);
            this.Controls.Add(this.bomAssemblyTxt);
            this.Controls.Add(this.bomAssemblySelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mainAssembly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectMainAssembly);
            this.Name = "Printer";
            this.Text = "Solidworks Drawing Bundler";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectMainAssembly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mainAssembly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bomAssemblyTxt;
        private System.Windows.Forms.Button bomAssemblySelect;
        private System.Windows.Forms.Button printBtn;
        private System.Windows.Forms.CheckBox printChkbox;
        private System.Windows.Forms.CheckBox ignorePrinterFileChkbox;
        private System.Windows.Forms.TextBox outputFolder;
        private System.Windows.Forms.Button outputFolderSelectBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox vaultList;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label treeUsagelbl;
        private System.Windows.Forms.CheckBox bundlePDFsChkbox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox includePictureChk;
        private System.Windows.Forms.CheckBox generateBOMChkbox;
    }
}