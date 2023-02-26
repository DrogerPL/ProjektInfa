using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProjektInfraSem2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
       
        private void Form2_Load_1(object sender, EventArgs e)
        {
            int gorny = 1, dolny = 0;
            for (int i = 0; i < Convert.ToInt32(Form1.maxprzedzial); i++)
            {
                this.chart1.Series["Oceny"].Points.AddXY(dolny+"-"+gorny, Form1.ilewprzedziale[i]);
                gorny++;
                dolny++;
            }         
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            int  liczbazapisu = 1;
            int wskaznik = Form1.droga.LastIndexOf("\\");
            string zwrot = Form1.droga.Substring(0, wskaznik);

            zwrot = zwrot +""+ Form1.nazwa +"---"+ "ZapisWyników.txt";
            while (File.Exists(zwrot))                                   //Zapis wyników tutaj
            {
                zwrot = Form1.droga.Substring(0, wskaznik);
                zwrot = zwrot +""+Form1.nazwa+"---"+ "ZapisWyników"+liczbazapisu+".txt";
                liczbazapisu++;
            }
            try
            {
                using (StreamWriter sw = File.CreateText(zwrot))
                {

                    int dolny = 0, gorny = 1;
                    for (int i = 0; i < Form1.maxprzedzial; i++)
                    {
                        sw.WriteLine("Ocen w przedziale od " + dolny + " do " + gorny + " było : " + Form1.ilewprzedziale[i]);
                        dolny++;
                        gorny++;
                    }
                    MessageBox.Show("Udało się zapisać plik w podanej ścieżce : "+zwrot);
                }
            }
            catch
            {
                MessageBox.Show("Nie udało się zapisać wyników!");
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < Form1.ilewprzedziale.Length; i++) //Zeruje dane
            {
                Form1.ilewprzedziale[i] = 0;
            }          
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            else
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            chart1.Series["Oceny"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            else
            chart1.Series["Oceny"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            if(checkBox3.Checked)
            chart1.Series["Oceny"].IsVisibleInLegend = false;
            else
            chart1.Series["Oceny"].IsVisibleInLegend = true;
        }
    }
}
