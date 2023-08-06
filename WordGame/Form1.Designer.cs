namespace WordGame
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
			this.Word00 = new System.Windows.Forms.Button();
			this.Word01 = new System.Windows.Forms.Button();
			this.Word02 = new System.Windows.Forms.Button();
			this.Word03 = new System.Windows.Forms.Button();
			this.Word13 = new System.Windows.Forms.Button();
			this.Word12 = new System.Windows.Forms.Button();
			this.Word11 = new System.Windows.Forms.Button();
			this.Word10 = new System.Windows.Forms.Button();
			this.TextInput = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Word00
			// 
			this.Word00.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word00.Location = new System.Drawing.Point(1, 121);
			this.Word00.Name = "Word00";
			this.Word00.Size = new System.Drawing.Size(55, 55);
			this.Word00.TabIndex = 0;
			this.Word00.UseVisualStyleBackColor = true;
			// 
			// Word01
			// 
			this.Word01.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word01.Location = new System.Drawing.Point(61, 121);
			this.Word01.Name = "Word01";
			this.Word01.Size = new System.Drawing.Size(55, 55);
			this.Word01.TabIndex = 1;
			this.Word01.UseVisualStyleBackColor = true;
			// 
			// Word02
			// 
			this.Word02.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word02.Location = new System.Drawing.Point(121, 121);
			this.Word02.Name = "Word02";
			this.Word02.Size = new System.Drawing.Size(55, 55);
			this.Word02.TabIndex = 2;
			this.Word02.UseVisualStyleBackColor = true;
			// 
			// Word03
			// 
			this.Word03.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word03.Location = new System.Drawing.Point(181, 121);
			this.Word03.Name = "Word03";
			this.Word03.Size = new System.Drawing.Size(55, 55);
			this.Word03.TabIndex = 3;
			this.Word03.UseVisualStyleBackColor = true;
			// 
			// Word13
			// 
			this.Word13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word13.Location = new System.Drawing.Point(181, 181);
			this.Word13.Name = "Word13";
			this.Word13.Size = new System.Drawing.Size(55, 55);
			this.Word13.TabIndex = 7;
			this.Word13.UseVisualStyleBackColor = true;
			// 
			// Word12
			// 
			this.Word12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word12.Location = new System.Drawing.Point(121, 181);
			this.Word12.Name = "Word12";
			this.Word12.Size = new System.Drawing.Size(55, 55);
			this.Word12.TabIndex = 6;
			this.Word12.UseVisualStyleBackColor = true;
			// 
			// Word11
			// 
			this.Word11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word11.Location = new System.Drawing.Point(61, 181);
			this.Word11.Name = "Word11";
			this.Word11.Size = new System.Drawing.Size(55, 55);
			this.Word11.TabIndex = 5;
			this.Word11.UseVisualStyleBackColor = true;
			// 
			// Word10
			// 
			this.Word10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Word10.Location = new System.Drawing.Point(1, 181);
			this.Word10.Name = "Word10";
			this.Word10.Size = new System.Drawing.Size(55, 55);
			this.Word10.TabIndex = 4;
			this.Word10.UseVisualStyleBackColor = true;
			// 
			// TextInput
			// 
			this.TextInput.BackColor = System.Drawing.SystemColors.Control;
			this.TextInput.Font = new System.Drawing.Font("Roboto", 30F);
			this.TextInput.Location = new System.Drawing.Point(1, 2);
			this.TextInput.Name = "TextInput";
			this.TextInput.Size = new System.Drawing.Size(235, 56);
			this.TextInput.TabIndex = 8;
			this.TextInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(238, 481);
			this.Controls.Add(this.TextInput);
			this.Controls.Add(this.Word13);
			this.Controls.Add(this.Word12);
			this.Controls.Add(this.Word11);
			this.Controls.Add(this.Word10);
			this.Controls.Add(this.Word03);
			this.Controls.Add(this.Word02);
			this.Controls.Add(this.Word01);
			this.Controls.Add(this.Word00);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Word00;
		private System.Windows.Forms.Button Word01;
		private System.Windows.Forms.Button Word02;
		private System.Windows.Forms.Button Word03;
		private System.Windows.Forms.Button Word13;
		private System.Windows.Forms.Button Word12;
		private System.Windows.Forms.Button Word11;
		private System.Windows.Forms.Button Word10;
		private System.Windows.Forms.TextBox TextInput;
	}
}

