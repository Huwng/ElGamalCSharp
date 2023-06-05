namespace ElGamalCSharp
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
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerateKey = new System.Windows.Forms.Button();
            this.valAlpha = new System.Windows.Forms.TextBox();
            this.valBeta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.valP = new System.Windows.Forms.TextBox();
            this.valA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetKey = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblKeySet = new System.Windows.Forms.Label();
            this.btnSendToDecrypt = new System.Windows.Forms.Button();
            this.btnReadFromFile = new System.Windows.Forms.Button();
            this.btnSaveCiphertoFile = new System.Windows.Forms.Button();
            this.btnSavePlaintoFile = new System.Windows.Forms.Button();
            this.btnReadCipherfromFile = new System.Windows.Forms.Button();
            this.btnExportKeys = new System.Windows.Forms.Button();
            this.btnImportKeys = new System.Windows.Forms.Button();
            this.boxPlainTextInput = new System.Windows.Forms.RichTextBox();
            this.boxCipherTextInput = new System.Windows.Forms.RichTextBox();
            this.boxCipherTextOutput = new System.Windows.Forms.RichTextBox();
            this.boxPlainTextOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(183, 306);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(694, 306);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 1;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Alpha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Beta";
            // 
            // btnGenerateKey
            // 
            this.btnGenerateKey.Location = new System.Drawing.Point(12, 29);
            this.btnGenerateKey.Name = "btnGenerateKey";
            this.btnGenerateKey.Size = new System.Drawing.Size(107, 23);
            this.btnGenerateKey.TabIndex = 9;
            this.btnGenerateKey.Text = "Generate key";
            this.btnGenerateKey.UseVisualStyleBackColor = true;
            this.btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);
            // 
            // valAlpha
            // 
            this.valAlpha.Location = new System.Drawing.Point(125, 31);
            this.valAlpha.Name = "valAlpha";
            this.valAlpha.Size = new System.Drawing.Size(133, 20);
            this.valAlpha.TabIndex = 10;
            // 
            // valBeta
            // 
            this.valBeta.Location = new System.Drawing.Point(274, 30);
            this.valBeta.Name = "valBeta";
            this.valBeta.Size = new System.Drawing.Size(165, 20);
            this.valBeta.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "p";
            // 
            // valP
            // 
            this.valP.Location = new System.Drawing.Point(453, 30);
            this.valP.Name = "valP";
            this.valP.Size = new System.Drawing.Size(150, 20);
            this.valP.TabIndex = 13;
            // 
            // valA
            // 
            this.valA.Location = new System.Drawing.Point(625, 31);
            this.valA.Name = "valA";
            this.valA.Size = new System.Drawing.Size(144, 20);
            this.valA.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(622, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "a";
            // 
            // btnSetKey
            // 
            this.btnSetKey.Location = new System.Drawing.Point(785, 29);
            this.btnSetKey.Name = "btnSetKey";
            this.btnSetKey.Size = new System.Drawing.Size(75, 23);
            this.btnSetKey.TabIndex = 16;
            this.btnSetKey.Text = "Set key";
            this.btnSetKey.UseVisualStyleBackColor = true;
            this.btnSetKey.Click += new System.EventHandler(this.btnSetKey_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(453, 86);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblKeySet
            // 
            this.lblKeySet.AutoSize = true;
            this.lblKeySet.Location = new System.Drawing.Point(782, 9);
            this.lblKeySet.Name = "lblKeySet";
            this.lblKeySet.Size = new System.Drawing.Size(63, 13);
            this.lblKeySet.TabIndex = 18;
            this.lblKeySet.Text = "Key not set!";
            // 
            // btnSendToDecrypt
            // 
            this.btnSendToDecrypt.Location = new System.Drawing.Point(401, 464);
            this.btnSendToDecrypt.Name = "btnSendToDecrypt";
            this.btnSendToDecrypt.Size = new System.Drawing.Size(94, 23);
            this.btnSendToDecrypt.TabIndex = 19;
            this.btnSendToDecrypt.Text = "Send to Decrypt";
            this.btnSendToDecrypt.UseVisualStyleBackColor = true;
            this.btnSendToDecrypt.Click += new System.EventHandler(this.btnSendToDecrypt_Click);
            // 
            // btnReadFromFile
            // 
            this.btnReadFromFile.Location = new System.Drawing.Point(401, 145);
            this.btnReadFromFile.Name = "btnReadFromFile";
            this.btnReadFromFile.Size = new System.Drawing.Size(94, 23);
            this.btnReadFromFile.TabIndex = 20;
            this.btnReadFromFile.Text = "Read from File";
            this.btnReadFromFile.UseVisualStyleBackColor = true;
            this.btnReadFromFile.Click += new System.EventHandler(this.btnReadFromFile_Click);
            // 
            // btnSaveCiphertoFile
            // 
            this.btnSaveCiphertoFile.Location = new System.Drawing.Point(401, 493);
            this.btnSaveCiphertoFile.Name = "btnSaveCiphertoFile";
            this.btnSaveCiphertoFile.Size = new System.Drawing.Size(94, 23);
            this.btnSaveCiphertoFile.TabIndex = 21;
            this.btnSaveCiphertoFile.Text = "Save to File";
            this.btnSaveCiphertoFile.UseVisualStyleBackColor = true;
            this.btnSaveCiphertoFile.Click += new System.EventHandler(this.btnSaveCiphertoFile_Click);
            // 
            // btnSavePlaintoFile
            // 
            this.btnSavePlaintoFile.Location = new System.Drawing.Point(954, 493);
            this.btnSavePlaintoFile.Name = "btnSavePlaintoFile";
            this.btnSavePlaintoFile.Size = new System.Drawing.Size(94, 23);
            this.btnSavePlaintoFile.TabIndex = 22;
            this.btnSavePlaintoFile.Text = "Save to File";
            this.btnSavePlaintoFile.UseVisualStyleBackColor = true;
            this.btnSavePlaintoFile.Click += new System.EventHandler(this.btnSavePlaintoFile_Click);
            // 
            // btnReadCipherfromFile
            // 
            this.btnReadCipherfromFile.Location = new System.Drawing.Point(954, 145);
            this.btnReadCipherfromFile.Name = "btnReadCipherfromFile";
            this.btnReadCipherfromFile.Size = new System.Drawing.Size(94, 23);
            this.btnReadCipherfromFile.TabIndex = 23;
            this.btnReadCipherfromFile.Text = "Read from File";
            this.btnReadCipherfromFile.UseVisualStyleBackColor = true;
            this.btnReadCipherfromFile.Click += new System.EventHandler(this.btnReadCipherfromFile_Click);
            // 
            // btnExportKeys
            // 
            this.btnExportKeys.Location = new System.Drawing.Point(866, 31);
            this.btnExportKeys.Name = "btnExportKeys";
            this.btnExportKeys.Size = new System.Drawing.Size(75, 23);
            this.btnExportKeys.TabIndex = 24;
            this.btnExportKeys.Text = "Export keys";
            this.btnExportKeys.UseVisualStyleBackColor = true;
            this.btnExportKeys.Click += new System.EventHandler(this.btnExportKeys_Click);
            // 
            // btnImportKeys
            // 
            this.btnImportKeys.Location = new System.Drawing.Point(947, 30);
            this.btnImportKeys.Name = "btnImportKeys";
            this.btnImportKeys.Size = new System.Drawing.Size(75, 23);
            this.btnImportKeys.TabIndex = 25;
            this.btnImportKeys.Text = "Import keys";
            this.btnImportKeys.UseVisualStyleBackColor = true;
            this.btnImportKeys.Click += new System.EventHandler(this.btnImportKeys_Click);
            // 
            // boxPlainTextInput
            // 
            this.boxPlainTextInput.Location = new System.Drawing.Point(32, 88);
            this.boxPlainTextInput.Name = "boxPlainTextInput";
            this.boxPlainTextInput.Size = new System.Drawing.Size(351, 212);
            this.boxPlainTextInput.TabIndex = 26;
            this.boxPlainTextInput.Text = "";
            // 
            // boxCipherTextInput
            // 
            this.boxCipherTextInput.Location = new System.Drawing.Point(597, 76);
            this.boxCipherTextInput.Name = "boxCipherTextInput";
            this.boxCipherTextInput.Size = new System.Drawing.Size(351, 212);
            this.boxCipherTextInput.TabIndex = 26;
            this.boxCipherTextInput.Text = "";
            // 
            // boxCipherTextOutput
            // 
            this.boxCipherTextOutput.Location = new System.Drawing.Point(32, 345);
            this.boxCipherTextOutput.Name = "boxCipherTextOutput";
            this.boxCipherTextOutput.Size = new System.Drawing.Size(351, 212);
            this.boxCipherTextOutput.TabIndex = 26;
            this.boxCipherTextOutput.Text = "";
            // 
            // boxPlainTextOutput
            // 
            this.boxPlainTextOutput.Location = new System.Drawing.Point(597, 345);
            this.boxPlainTextOutput.Name = "boxPlainTextOutput";
            this.boxPlainTextOutput.Size = new System.Drawing.Size(351, 212);
            this.boxPlainTextOutput.TabIndex = 26;
            this.boxPlainTextOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 581);
            this.Controls.Add(this.boxPlainTextOutput);
            this.Controls.Add(this.boxCipherTextOutput);
            this.Controls.Add(this.boxCipherTextInput);
            this.Controls.Add(this.boxPlainTextInput);
            this.Controls.Add(this.btnImportKeys);
            this.Controls.Add(this.btnExportKeys);
            this.Controls.Add(this.btnReadCipherfromFile);
            this.Controls.Add(this.btnSavePlaintoFile);
            this.Controls.Add(this.btnSaveCiphertoFile);
            this.Controls.Add(this.btnReadFromFile);
            this.Controls.Add(this.btnSendToDecrypt);
            this.Controls.Add(this.lblKeySet);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSetKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.valA);
            this.Controls.Add(this.valP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.valBeta);
            this.Controls.Add(this.valAlpha);
            this.Controls.Add(this.btnGenerateKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Name = "Form1";
            this.Text = "ElGamal Encryption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerateKey;
        private System.Windows.Forms.TextBox valAlpha;
        private System.Windows.Forms.TextBox valBeta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox valP;
        private System.Windows.Forms.TextBox valA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetKey;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblKeySet;
        private System.Windows.Forms.Button btnSendToDecrypt;
        private System.Windows.Forms.Button btnReadFromFile;
        private System.Windows.Forms.Button btnSaveCiphertoFile;
        private System.Windows.Forms.Button btnSavePlaintoFile;
        private System.Windows.Forms.Button btnReadCipherfromFile;
        private System.Windows.Forms.Button btnExportKeys;
        private System.Windows.Forms.Button btnImportKeys;
        private System.Windows.Forms.RichTextBox boxPlainTextInput;
        private System.Windows.Forms.RichTextBox boxCipherTextInput;
        private System.Windows.Forms.RichTextBox boxCipherTextOutput;
        private System.Windows.Forms.RichTextBox boxPlainTextOutput;
    }
}

