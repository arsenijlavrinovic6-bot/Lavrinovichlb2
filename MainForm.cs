using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace Lavrinovich
{
    public class MainForm : Form
    {
        private readonly TextBox filter = new TextBox();
        private readonly ComboBox filterBy = new ComboBox();
        private readonly Button apply = new Button();
        private readonly Button previous = new Button();
        private readonly Button next = new Button();
        private readonly Label pageInfo = new Label();
        private readonly FlowLayoutPanel tiles = new FlowLayoutPanel();
        private List<Motorbike> all = new List<Motorbike>();
        private List<Motorbike> shown = new List<Motorbike>();
        private int page = 0;
        private const int PageSize = 4;
        public MainForm()
        {
            Text = "Lavrinovich — мотоциклы из MotorbikeDB"; StartPosition = FormStartPosition.CenterScreen;
            Width = 1120; Height = 850; MinimumSize = new Size(900, 650); BackColor = Color.FromArgb(245,247,250);
            var top = new Panel { Dock = DockStyle.Top, Height = 62, Padding = new Padding(12), BackColor = Color.White };
            filterBy.DropDownStyle = ComboBoxStyle.DropDownList; filterBy.Items.AddRange(new object[] { "Все поля", "Модель", "Бренд", "Цена", "Мощность", "Пробег" }); filterBy.SelectedIndex = 0; filterBy.Width = 140;
            filter.SetBounds(165, 15, 260, 30); filterBy.SetBounds(12, 15, 140, 30); apply.Text = "Фильтровать"; apply.SetBounds(435, 14, 110, 32); apply.Click += (s,e) => ApplyFilter();
            top.Controls.Add(filterBy); top.Controls.Add(filter); top.Controls.Add(apply);
            tiles.Dock = DockStyle.Fill; tiles.AutoScroll = true; tiles.Padding = new Padding(15); tiles.WrapContents = true;
            var bottom = new Panel { Dock = DockStyle.Bottom, Height = 58, BackColor = Color.White };
            previous.Text = "◀ Назад"; previous.SetBounds(330, 12, 110, 34); previous.Click += (s,e) => { if(page > 0) { page--; RenderPage(); } };
            next.Text = "Вперёд ▶"; next.SetBounds(670, 12, 110, 34); next.Click += (s,e) => { if((page+1)*PageSize < shown.Count) { page++; RenderPage(); } };
            pageInfo.TextAlign = ContentAlignment.MiddleCenter; pageInfo.SetBounds(450, 12, 210, 34); bottom.Controls.Add(previous); bottom.Controls.Add(next); bottom.Controls.Add(pageInfo);
            Controls.Add(tiles); Controls.Add(bottom); Controls.Add(top); Load += LoadData;
        }
        private void LoadData(object sender, EventArgs e)
        {
            try { using (var db = new ModelDB()) { all = db.Table_Motorbike.OrderBy(x => x.ID).ToList(); } shown = all.ToList(); RenderPage(); }
            catch (Exception ex) { MessageBox.Show("Не удалось подключиться к MotorbikeDB. Сначала выполните CreateDatabase.sql и проверьте строку подключения в App.config.

Подробности: " + ex.Message, "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void ApplyFilter()
        {
            string q = filter.Text.Trim();
            if (q.Length == 0) shown = all.ToList();
            else switch (filterBy.SelectedItem.ToString())
            {
                case "Модель": shown = all.Where(x => (x.Model ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0).ToList(); break;
                case "Бренд": shown = all.Where(x => (x.Brand ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0).ToList(); break;
                case "Цена": shown = all.Where(x => x.Price.ToString().Contains(q)).ToList(); break;
                case "Мощность": shown = all.Where(x => x.Power.ToString().Contains(q)).ToList(); break;
                case "Пробег": shown = all.Where(x => x.Mileage.ToString().Contains(q)).ToList(); break;
                default: shown = all.Where(x => (x.Brand + " " + x.Model).IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 || x.Price.ToString().Contains(q) || x.Power.ToString().Contains(q) || x.Mileage.ToString().Contains(q)).ToList(); break;
            }
            page = 0; RenderPage();
        }
        private void RenderPage()
        {
            tiles.SuspendLayout();
            foreach (Control c in tiles.Controls) c.Dispose(); tiles.Controls.Clear();
            foreach (var bike in shown.Skip(page * PageSize).Take(PageSize)) { var tile = new MotorbikeTile(); tile.SetData(bike); tiles.Controls.Add(tile); }
            int pages = Math.Max(1, (int)Math.Ceiling(shown.Count / (double)PageSize)); pageInfo.Text = "Страница " + (page+1) + " из " + pages + " · записей: " + shown.Count;
            previous.Enabled = page > 0; next.Enabled = (page+1)*PageSize < shown.Count; tiles.ResumeLayout();
        }
    }
}