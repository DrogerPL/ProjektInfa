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
    public partial class Form1 : Form
    {    
        //Tutaj dane publiczne 
        OpenFileDialog ofd = new OpenFileDialog();
        public static  string droga,nazwa;         //Ścieżka do pliku i nazwa pliku       
        public static double maxprzedzial;
        public static  int[] ilewprzedziale = new int[2];
        //

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e) //Przycisk który wczytuje
        {
            bool pstryk = true;
            droga = null;
            for (int i = 0; i < Form1.ilewprzedziale.Length; i++) //Zeruje dane
            {
                Form1.ilewprzedziale[i] = 0;
            }                     
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                droga = ofd.FileName;
                using (StreamReader sr = File.OpenText(droga))
                {
                    string caly = File.ReadAllText(droga);                           
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano żadnego pliku!");
                pstryk = false;   //Ten bool po to żeby się 2 razy komunikat nie wyświetlał
            }
            try
            {
                if (droga.EndsWith(".txt")) //Odpala drugi przycisk
                {
                    button2.Enabled = true;
                    MessageBox.Show("Poprawnie wczytano plik!");
                    textBox1.Text = droga;
                }
                else 
                {
                    button2.Enabled = false;
                    textBox1.Text = null;
                    if (pstryk==true)
                    MessageBox.Show("Plik powinien posiadać rozszerzenie .txt");
                }
            }
            catch
            {
                //MessageBox.Show("Nie wybrano żadnego pliku!");
            }           
        }

        private void button2_Click(object sender, EventArgs e) //Przycisk który zlicza liczby
        {
            int ileliczb = 0;          
            droga = ofd.FileName;
            using (StreamReader sr = File.OpenText(droga))
            {
                string caly = File.ReadAllText(droga);
                int a, b;
               
                a = droga.LastIndexOf('\\');
                b = droga.LastIndexOf('.');
                nazwa = droga.Substring(a+1, b - a);           //Łapie nazwe
              

                for (int i = 0; i < caly.Length; i++)
                {
                    if (Char.IsDigit(caly[i]))
                    {
                        ileliczb++;
                        
                    }
                    else if (','==(caly[i]))
                    {
                            ileliczb--;
                    }
                }
                
                double[] tab1 = new double[ileliczb];
                int licznik=0;
                for (int i = 0; licznik<tab1.Length; i++)
                {
                    if (Char.IsDigit(caly[i]))
                    {
                        if(','==(caly[i+1]))
                        {
                            
                            tab1[licznik] = double.Parse(caly.Substring(i, 3));
                            i += 2;
                            licznik++;
                        }
                        else 
                        {
                            tab1[licznik] = double.Parse(Convert.ToString(caly[i]));
                            licznik++;                        
                        }
                    }
                    else
                    {
                        ;
                    }
                }                                
                Array.Sort(tab1);                    
                maxprzedzial = Math.Ceiling(tab1[tab1.Length-1]);         
                Array.Resize(ref ilewprzedziale, Convert.ToInt32(maxprzedzial));      
                int dolny = 0, gorny = 1;
                    for (int i = 0; i < ilewprzedziale.Length; i++)
                    {
                        for (int aa = 0; aa < tab1.Length; aa++)
                        {
                            if (tab1[aa] >= dolny && tab1[aa] < gorny)
                            {
                            ilewprzedziale[i]++;
                            }
                        }
                    dolny++;
                    gorny++;
                    }             
                Form2 frm2 = new Form2();
                frm2.Show();
            }
        }
    }
}
