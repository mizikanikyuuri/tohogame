using System.Windows.Forms;

class Appmain : Form
{
    private Label lb1;

    public static void Main()
    {
        Application.Run(new Appmain());
    }

    public Appmain()
    {
        this.Text = "Test!!!";
        this.Height = 300; this.Width = 400;

        lb1 = new Label();

        lb1.Width = 400; lb1.Height = lb1.Bottom * 3;
        lb1.Top = 0;
        lb1.Text = "This is a test program.\nWe'll develop this game soon.\n";
        lb1.Parent = this;
    }
}