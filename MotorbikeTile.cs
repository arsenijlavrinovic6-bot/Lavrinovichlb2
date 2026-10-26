using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Lavrinovich
{
    public class MotorbikeTile : UserControl
    {
        private readonly PictureBox picture = new PictureBox();
        private readonly Label details = new Label();
        public MotorbikeTile()
        {
            BorderStyle = BorderStyle.FixedSingle; BackColor = Color.White; Size = new Size(350, 310); Margin = new Padding(10);
            picture.Dock = DockStyle.Top; picture.Height = 190; picture.SizeMode = PictureBoxSizeMode.Zoom; picture.BackColor = Color.WhiteSmoke;
            details.Dock = DockStyle.Fill; details.Padding = new Padding(10); details.Font = new Font("Segoe UI", 10F); details.AutoSize = false;
            Controls.Add(details); Controls.Add(picture);
        }
        public void SetData(Motorbike bike)
        {
            details.Text = bike.Brand + " " + bike.Model + Environment.NewLine +
                "Цена: " + bike.Price.ToString("N0") + " руб." + Environment.NewLine +
                "Мощность: " + bike.Power + " л.с." + Environment.NewLine +
                "Пробег: " + bike.Mileage.ToString("N0") + " км";
            string path = Path.Combine(Application.StartupPath, "Pictures", bike.Image ?? "");
            if (picture.Image != null) { var old = picture.Image; picture.Image = null; old.Dispose(); }
            if (File.Exists(path)) { using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) using (var img = Image.FromStream(stream)) picture.Image = new Bitmap(img); }
        }
        protected override void Dispose(bool disposing) { if (disposing && picture.Image != null) picture.Image.Dispose(); base.Dispose(disposing); }
    }
}