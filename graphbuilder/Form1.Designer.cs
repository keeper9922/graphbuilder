namespace graphbuilder;

partial class Form1
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        groupBox2 = new System.Windows.Forms.GroupBox();
        graphBox1 = new graphbuilder.GraphBox();
        tabControl1 = new System.Windows.Forms.TabControl();
        tabPage1 = new System.Windows.Forms.TabPage();
        flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        add_formula_btn = new System.Windows.Forms.Button();
        tabPage2 = new System.Windows.Forms.TabPage();
        loadDataButton = new System.Windows.Forms.Button();
        grid_type_change_btn = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        groupBox2.SuspendLayout();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        tabPage2.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(graphBox1);
        groupBox2.Location = new System.Drawing.Point(259, 12);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(712, 710);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "groupBox2";
        // 
        // graphBox1
        // 
        graphBox1.BackColor = System.Drawing.Color.White;
        graphBox1.Location = new System.Drawing.Point(6, 30);
        graphBox1.Name = "graphBox1";
        graphBox1.Size = new System.Drawing.Size(696, 670);
        graphBox1.TabIndex = 0;
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Location = new System.Drawing.Point(12, 12);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new System.Drawing.Size(241, 700);
        tabControl1.TabIndex = 2;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(flowLayoutPanel1);
        tabPage1.Location = new System.Drawing.Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new System.Windows.Forms.Padding(3);
        tabPage1.Size = new System.Drawing.Size(233, 672);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Формулы";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(add_formula_btn);
        flowLayoutPanel1.Location = new System.Drawing.Point(6, 6);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new System.Drawing.Size(221, 713);
        flowLayoutPanel1.TabIndex = 3;
        // 
        // add_formula_btn
        // 
        add_formula_btn.Location = new System.Drawing.Point(3, 3);
        add_formula_btn.Name = "add_formula_btn";
        add_formula_btn.Size = new System.Drawing.Size(40, 40);
        add_formula_btn.TabIndex = 2;
        add_formula_btn.Text = "+";
        add_formula_btn.UseVisualStyleBackColor = true;
        add_formula_btn.Click += add_formula_btn_Click;
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(loadDataButton);
        tabPage2.Location = new System.Drawing.Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new System.Windows.Forms.Padding(3);
        tabPage2.Size = new System.Drawing.Size(233, 672);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Точки";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // loadDataButton
        // 
        loadDataButton.Location = new System.Drawing.Point(6, 6);
        loadDataButton.Name = "loadDataButton";
        loadDataButton.Size = new System.Drawing.Size(221, 29);
        loadDataButton.TabIndex = 0;
        loadDataButton.Text = "Загрузить с файла";
        loadDataButton.UseVisualStyleBackColor = true;
        loadDataButton.Click += loadDataButton_Click;
        // 
        // grid_type_change_btn
        // 
        grid_type_change_btn.Location = new System.Drawing.Point(332, 726);
        grid_type_change_btn.Name = "grid_type_change_btn";
        grid_type_change_btn.Size = new System.Drawing.Size(133, 30);
        grid_type_change_btn.TabIndex = 3;
        grid_type_change_btn.Text = "Linear";
        grid_type_change_btn.UseVisualStyleBackColor = true;
        grid_type_change_btn.Click += grid_type_change_btn_Click;
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(259, 730);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(67, 26);
        label1.TabIndex = 4;
        label1.Text = "Сменить:";
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(983, 765);
        Controls.Add(label1);
        Controls.Add(grid_type_change_btn);
        Controls.Add(tabControl1);
        Controls.Add(groupBox2);
        Text = "График";
        groupBox2.ResumeLayout(false);
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        flowLayoutPanel1.ResumeLayout(false);
        tabPage2.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button grid_type_change_btn;
    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Button loadDataButton;

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

    private graphbuilder.GraphBox graphBox1;

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button add_formula_btn;

    private System.Windows.Forms.GroupBox groupBox2;

    #endregion
}