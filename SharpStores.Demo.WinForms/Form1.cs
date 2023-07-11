namespace SharpStores.Demo.WinForms;

public partial class Form1 : Form
{
    private Writable<int> value = new(0);

    public Form1()
    {
        InitializeComponent();
        value.Subscribe(x =>
        {
            ValueLabel.Text = x.ToString();
        });
    }

    private void DecrementButton_Click(object sender, EventArgs e)
    {
        value.Update(x => x - 1);
    }

    private void IncrementButton_Click(object sender, EventArgs e)
    {
        value.Update(x => x + 1);
    }
}