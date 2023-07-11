namespace SharpStores.Demo.WinForms;

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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        IncrementButton=new Button();
        DecrementButton=new Button();
        ValueLabel=new Label();
        SuspendLayout();
        // 
        // IncrementButton
        // 
        IncrementButton.Location=new Point(86, 39);
        IncrementButton.Name="IncrementButton";
        IncrementButton.Size=new Size(75, 23);
        IncrementButton.TabIndex=0;
        IncrementButton.Text="+";
        IncrementButton.UseVisualStyleBackColor=true;
        IncrementButton.Click+=IncrementButton_Click;
        // 
        // DecrementButton
        // 
        DecrementButton.Location=new Point(5, 39);
        DecrementButton.Name="DecrementButton";
        DecrementButton.Size=new Size(75, 23);
        DecrementButton.TabIndex=1;
        DecrementButton.Text="-";
        DecrementButton.UseVisualStyleBackColor=true;
        DecrementButton.Click+=DecrementButton_Click;
        // 
        // ValueLabel
        // 
        ValueLabel.AutoSize=true;
        ValueLabel.Location=new Point(65, 9);
        ValueLabel.Name="ValueLabel";
        ValueLabel.Size=new Size(35, 15);
        ValueLabel.TabIndex=2;
        ValueLabel.Text="Value";
        // 
        // Form1
        // 
        AutoScaleDimensions=new SizeF(7F, 15F);
        AutoScaleMode=AutoScaleMode.Font;
        ClientSize=new Size(181, 85);
        Controls.Add(ValueLabel);
        Controls.Add(DecrementButton);
        Controls.Add(IncrementButton);
        Name="Form1";
        Text="Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button IncrementButton;
    private Button DecrementButton;
    private Label ValueLabel;
}