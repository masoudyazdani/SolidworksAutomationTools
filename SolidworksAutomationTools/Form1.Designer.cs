namespace SWPrintAndMerge
{
    partial class Form1
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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.ignorePrinterFileChkbox = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.outputFolder = new System.Windows.Forms.TextBox();
            this.outputFolderSelectBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.vaultList = new System.Windows.Forms.ListBox();
            this.includePictureChk = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeUsagelbl = new System.Windows.Forms.Label();
            this.bundlePDFsChkbox = new System.Windows.Forms.CheckBox();
            this.generateBOMChkbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectMainAssembly
            // 
            this.selectMainAssembly.Location = new System.Drawing.Point(972, 29);
            this.selectMainAssembly.Margin = new System.Windows.Forms.Padding(6);
            this.selectMainAssembly.Name = "selectMainAssembly";
            this.selectMainAssembly.Size = new System.Drawing.Size(310, 96);
            this.selectMainAssembly.TabIndex = 0;
            this.selectMainAssembly.Text = "Select Main Assembly";
            this.selectMainAssembly.UseVisualStyleBackColor = true;
            this.selectMainAssembly.Click += new System.EventHandler(this.selectMainAssembly_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Main Assembly";
            // 
            // mainAssembly
            // 
            this.mainAssembly.Location = new System.Drawing.Point(210, 58);
            this.mainAssembly.Margin = new System.Windows.Forms.Padding(6);
            this.mainAssembly.Name = "mainAssembly";
            this.mainAssembly.ReadOnly = true;
            this.mainAssembly.Size = new System.Drawing.Size(720, 31);
            this.mainAssembly.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 215);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "BOM Assembly";
            // 
            // bomAssemblyTxt
            // 
            this.bomAssemblyTxt.Location = new System.Drawing.Point(210, 208);
            this.bomAssemblyTxt.Margin = new System.Windows.Forms.Padding(6);
            this.bomAssemblyTxt.Name = "bomAssemblyTxt";
            this.bomAssemblyTxt.ReadOnly = true;
            this.bomAssemblyTxt.Size = new System.Drawing.Size(720, 31);
            this.bomAssemblyTxt.TabIndex = 5;
            // 
            // bomAssemblySelect
            // 
            this.bomAssemblySelect.Location = new System.Drawing.Point(972, 179);
            this.bomAssemblySelect.Margin = new System.Windows.Forms.Padding(6);
            this.bomAssemblySelect.Name = "bomAssemblySelect";
            this.bomAssemblySelect.Size = new System.Drawing.Size(310, 96);
            this.bomAssemblySelect.TabIndex = 4;
            this.bomAssemblySelect.Text = "Select BOM Assembly";
            this.bomAssemblySelect.UseVisualStyleBackColor = true;
            this.bomAssemblySelect.Click += new System.EventHandler(this.toPrintSelect_Click);
            // 
            // printBtn
            // 
            this.printBtn.Location = new System.Drawing.Point(284, 838);
            this.printBtn.Margin = new System.Windows.Forms.Padding(6);
            this.printBtn.Name = "printBtn";
            this.printBtn.Size = new System.Drawing.Size(310, 96);
            this.printBtn.TabIndex = 6;
            this.printBtn.Text = "Run";
            this.printBtn.UseVisualStyleBackColor = true;
            this.printBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // printChkbox
            // 
            this.printChkbox.AutoSize = true;
            this.printChkbox.Location = new System.Drawing.Point(284, 465);
            this.printChkbox.Margin = new System.Windows.Forms.Padding(6);
            this.printChkbox.Name = "printChkbox";
            this.printChkbox.Size = new System.Drawing.Size(88, 29);
            this.printChkbox.TabIndex = 7;
            this.printChkbox.Text = "Print";
            this.printChkbox.UseVisualStyleBackColor = true;
            this.printChkbox.CheckedChanged += new System.EventHandler(this.print_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(284, 661);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(6);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(267, 29);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Add Where Used Table";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // ignorePrinterFileChkbox
            // 
            this.ignorePrinterFileChkbox.AutoSize = true;
            this.ignorePrinterFileChkbox.Enabled = false;
            this.ignorePrinterFileChkbox.Location = new System.Drawing.Point(284, 510);
            this.ignorePrinterFileChkbox.Margin = new System.Windows.Forms.Padding(6);
            this.ignorePrinterFileChkbox.Name = "ignorePrinterFileChkbox";
            this.ignorePrinterFileChkbox.Size = new System.Drawing.Size(230, 29);
            this.ignorePrinterFileChkbox.TabIndex = 9;
            this.ignorePrinterFileChkbox.Text = "Ignore Printed Files";
            this.ignorePrinterFileChkbox.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(284, 611);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(6);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(217, 29);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "Update Properties";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // outputFolder
            // 
            this.outputFolder.Location = new System.Drawing.Point(210, 356);
            this.outputFolder.Margin = new System.Windows.Forms.Padding(6);
            this.outputFolder.Name = "outputFolder";
            this.outputFolder.ReadOnly = true;
            this.outputFolder.Size = new System.Drawing.Size(720, 31);
            this.outputFolder.TabIndex = 13;
            // 
            // outputFolderSelectBtn
            // 
            this.outputFolderSelectBtn.Location = new System.Drawing.Point(972, 327);
            this.outputFolderSelectBtn.Margin = new System.Windows.Forms.Padding(6);
            this.outputFolderSelectBtn.Name = "outputFolderSelectBtn";
            this.outputFolderSelectBtn.Size = new System.Drawing.Size(310, 96);
            this.outputFolderSelectBtn.TabIndex = 12;
            this.outputFolderSelectBtn.Text = "Select Output Folder";
            this.outputFolderSelectBtn.UseVisualStyleBackColor = true;
            this.outputFolderSelectBtn.Click += new System.EventHandler(this.outputFolderSelectBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 363);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output Folder";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 427);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 25);
            this.label4.TabIndex = 16;
            this.label4.Text = "Vault";
            // 
            // vaultList
            // 
            this.vaultList.FormattingEnabled = true;
            this.vaultList.ItemHeight = 25;
            this.vaultList.Location = new System.Drawing.Point(40, 465);
            this.vaultList.Margin = new System.Windows.Forms.Padding(6);
            this.vaultList.Name = "vaultList";
            this.vaultList.Size = new System.Drawing.Size(208, 329);
            this.vaultList.TabIndex = 17;
            // 
            // includePictureChk
            // 
            this.includePictureChk.AutoSize = true;
            this.includePictureChk.Enabled = false;
            this.includePictureChk.Location = new System.Drawing.Point(284, 765);
            this.includePictureChk.Margin = new System.Windows.Forms.Padding(6);
            this.includePictureChk.Name = "includePictureChk";
            this.includePictureChk.Size = new System.Drawing.Size(253, 29);
            this.includePictureChk.TabIndex = 19;
            this.includePictureChk.Text = "Include Parts Pictures";
            this.includePictureChk.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView1.Location = new System.Drawing.Point(630, 465);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.Size = new System.Drawing.Size(652, 648);
            this.dataGridView1.TabIndex = 20;
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
            this.treeUsagelbl.Location = new System.Drawing.Point(794, 1125);
            this.treeUsagelbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.treeUsagelbl.Name = "treeUsagelbl";
            this.treeUsagelbl.Size = new System.Drawing.Size(489, 25);
            this.treeUsagelbl.TabIndex = 21;
            this.treeUsagelbl.Text = "\"tree.txt\" will be used for generating the BOM tree.";
            this.treeUsagelbl.Visible = false;
            // 
            // bundlePDFsChkbox
            // 
            this.bundlePDFsChkbox.AutoSize = true;
            this.bundlePDFsChkbox.Checked = true;
            this.bundlePDFsChkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bundlePDFsChkbox.Location = new System.Drawing.Point(284, 554);
            this.bundlePDFsChkbox.Margin = new System.Windows.Forms.Padding(6);
            this.bundlePDFsChkbox.Name = "bundlePDFsChkbox";
            this.bundlePDFsChkbox.Size = new System.Drawing.Size(237, 29);
            this.bundlePDFsChkbox.TabIndex = 22;
            this.bundlePDFsChkbox.Text = "Bundle Printed Files";
            this.bundlePDFsChkbox.UseVisualStyleBackColor = true;
            // 
            // generateBOMChkbox
            // 
            this.generateBOMChkbox.AutoSize = true;
            this.generateBOMChkbox.Location = new System.Drawing.Point(284, 721);
            this.generateBOMChkbox.Margin = new System.Windows.Forms.Padding(6);
            this.generateBOMChkbox.Name = "generateBOMChkbox";
            this.generateBOMChkbox.Size = new System.Drawing.Size(187, 29);
            this.generateBOMChkbox.TabIndex = 23;
            this.generateBOMChkbox.Text = "Generate BOM";
            this.generateBOMChkbox.UseVisualStyleBackColor = true;
            this.generateBOMChkbox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 1173);
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
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox ignorePrinterFileChkbox;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox outputFolder;
        private System.Windows.Forms.Button outputFolderSelectBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox vaultList;
        private System.Windows.Forms.CheckBox includePictureChk;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label treeUsagelbl;
        private System.Windows.Forms.CheckBox bundlePDFsChkbox;
        private System.Windows.Forms.CheckBox generateBOMChkbox;
    }
}