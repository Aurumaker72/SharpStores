using System.Globalization;
using Timer = System.Windows.Forms.Timer;

namespace SharpStores.Demo.WinForms;

public partial class Form1 : Form
{
    private readonly Writable<int> value = new(0);
    private readonly Readable<DateTime> time;

    public Form1()
    {
        InitializeComponent();
        value.Subscribe(x => { ValueLabel.Text = x.ToString(); });
        time = new Readable<DateTime>(DateTime.Now, set =>
        {
            void Tick(object sender, EventArgs e)
            {
                set(DateTime.Now);
            }

            Timer timer = new();
            timer.Tick += Tick;
            timer.Start();

            return () =>
            {
                timer.Tick -= Tick;
                timer.Stop();
            };
        });
        time.Subscribe(x => { Text = x.ToString(CultureInfo.InvariantCulture); });
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