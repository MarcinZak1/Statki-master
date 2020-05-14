using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Statki
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        Random random = new Random((int)DateTime.Now.Ticks);
        int plaszczyzna, pozycja, proby = 0, licznik1=0;
        int licznik4=0, licznik4zestrzelonych=0;
        int licznik3=0, licznik3zestrzelonych=0;
        int licznik2=0, licznik2zestrzelonych=0;


        private void Form1_Load(object sender, EventArgs e)
        {
            int index = 1;
            List<Control> controls = this.Controls.OfType<Button>().Cast<Control>().ToList();
            controls.Reverse();
            foreach (var item in controls)
            {
                Button btn = (Button)item;
                btn.Tag = String.Format("Pudło!"); // ustawiam Tag przycisku wg schematu: BUTTONx, gdzie x to numer przycisku
                btn.Text = string.Empty;                 //index.ToString();
                btn.Click += Btn_Click;  //podpięcie do klikniecia na przycisk metody 
                index++;
            }

            FindShipButtonByTag(101);
            CreatBoat4Masztowiec(controls);
            CreatBoat3Masztowiec(controls);
            CreatBoat2Masztowiec(controls);
            CreatBoat1Masztowiec(controls);
        }

        /// <summary>
        /// Pobiera obiekt przycisku matrycy statków na podstawie jego numeru
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private Button FindShipButtonByTag(int number)
        {
            String pattern = String.Format("BUTTON{0}", number);
            var item = this.Controls.Cast<Control>().FirstOrDefault(control => String.Equals(control.Tag, pattern));
            return (item == null) ? null : (Button)item;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            MessageBox.Show(btn.Tag.ToString());
            btn.Enabled = false;
            if(btn.Tag.Equals("Trafiłeś Czteromasztowiec"))
            {
                btn.Text = "4";
            }
            else if (btn.Tag.Equals("Trafiłeś Trzymasztowiec"))
            {
                btn.Text = "3";
            }
            else if (btn.Tag.Equals("Trafiłeś Dwumasztowiec"))
            {
                btn.Text = "2";                
            }
            else if (btn.Tag.Equals("Trafiłeś Jednomasztowca"))
            {
                btn.Text = "1";
            }
            else
            {
                btn.Text = string.Empty;
            }
            Wyniki4(btn);
            Wyniki3(btn);
            Wyniki2(btn);
            Wyniki1(btn);
            proby++;
            Proby.Text = proby.ToString();
        }
        private void CreatBoat4Masztowiec(List<Control> controls)
        {
            plaszczyzna = random.Next(0, 2);
            if (plaszczyzna == 1)   // jeśli płaszczyzna==1 ustawiamy statek pionowo
            {
                pozycja = random.Next(1, 71); //wybieramy guzik z danej puli 
                for (int i = 0; i < 4; i++)
                {
                    controls[pozycja + i*10-1].Tag = ("Trafiłeś Czteromasztowiec");
                }
            }
            else
            {
                pozycja = random.Next(1, 98);
                
                    while((pozycja > 7 && pozycja < 11) || (pozycja > 17 && pozycja < 21) || (pozycja > 27 && pozycja < 31) ||
                    (pozycja > 37 && pozycja < 41) || (pozycja > 47 && pozycja < 51) || (pozycja > 57 && pozycja < 61) ||
                    (pozycja > 67 && pozycja < 71) || (pozycja > 77 && pozycja < 81) || (pozycja > 87 && pozycja < 91))
                    {
                        pozycja = random.Next(1, 98);
                    }
                for (int i = 0; i < 4; i++)
                {
                    controls[pozycja + i-1].Tag = ("Trafiłeś Czteromasztowiec");
                }
            }
            
        }
        private void CreatBoat3Masztowiec(List<Control> controls)
        {
            for (int i = 0; i < 2; i++)     // liczba 3masztowców
            {
                plaszczyzna = random.Next(0, 2);
                if (plaszczyzna == 1)   // jeśli płaszczyzna==1 ustawiamy statek pionowo
                {
                    pozycja = random.Next(1, 81); //wybieramy guzik z danej puli
                    while (!controls[pozycja - 1].Tag.Equals("Pudło!") || !controls[pozycja - 1 + 10].Tag.Equals("Pudło!")
                        || !controls[pozycja - 1 + 20].Tag.Equals("Pudło!"))
                    {
                        pozycja = random.Next(1, 81);
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        controls[pozycja + j * 10 - 1].Tag = ("Trafiłeś Trzymasztowiec");
                    }
                }
                else
                {
                    pozycja = random.Next(1, 99);

                    while (((pozycja > 8 && pozycja < 11) || (pozycja > 18 && pozycja < 21) || (pozycja > 28 && pozycja < 31) ||
                    (pozycja > 38 && pozycja < 41) || (pozycja > 48 && pozycja < 51) || (pozycja > 58 && pozycja < 61) ||
                    (pozycja > 68 && pozycja < 71) || (pozycja > 78 && pozycja < 81) || (pozycja > 88 && pozycja < 91))
                    || (!controls[pozycja - 1].Tag.Equals("Pudło!") || !controls[pozycja - 1 + 1].Tag.Equals("Pudło!")
                        || !controls[pozycja - 1 + 2].Tag.Equals("Pudło!")))
                    {
                        pozycja = random.Next(1, 99);
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        controls[pozycja + j - 1].Tag = ("Trafiłeś Trzymasztowiec");
                    }
                }
            }
        }
        private void CreatBoat2Masztowiec(List<Control>controls)
        {
            for (int i = 0; i < 3; i++)     // liczba 2masztowców
            {
                plaszczyzna = random.Next(0, 2);
                if (plaszczyzna == 1)   // jeśli płaszczyzna==1 ustawiamy statek pionowo
                {
                    pozycja = random.Next(1, 91); //wybieramy guzik z danej puli
                    while (!controls[pozycja - 1].Tag.Equals("Pudło!") ||
                        !controls[pozycja - 1 + 10].Tag.Equals("Pudło!"))
                    {
                        pozycja = random.Next(1, 91);
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        controls[pozycja + j * 10 - 1].Tag = ("Trafiłeś Dwumasztowiec");
                    }
                }
                else
                {
                    pozycja = random.Next(1, 100);

                    while (((pozycja > 9 && pozycja < 11) || (pozycja > 19 && pozycja < 21) || (pozycja > 29 && pozycja < 31) ||
                    (pozycja > 39 && pozycja < 41) || (pozycja > 49 && pozycja < 51) || (pozycja > 59 && pozycja < 61) ||
                    (pozycja > 69 && pozycja < 71) || (pozycja > 79 && pozycja < 81) || (pozycja > 89 && pozycja < 91))
                    || (!controls[pozycja - 1].Tag.Equals("Pudło!") || !controls[pozycja - 1 + 1].Tag.Equals("Pudło!")))
                    {
                        pozycja = random.Next(1, 100);
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        controls[pozycja + j - 1].Tag = ("Trafiłeś Dwumasztowiec");
                    }
                }
            }
        }
        private void CreatBoat1Masztowiec(List<Control> controls)
        {
            for (int i = 0; i < 4; i++)     // liczba 1masztowców
            {
                    pozycja = random.Next(1, 101); //wybieramy guzik z danej puli
                    while (!controls[pozycja - 1].Tag.Equals("Pudło!"))
                    {
                        pozycja = random.Next(1, 101);
                    }
                    for (int j = 0; j < 1; j++)
                    {
                        controls[pozycja - 1].Tag = ("Trafiłeś Jednomasztowca");
                    }
            }
        }
        private void Wyniki4(Button btn)
        {

            if (btn.Tag.Equals("Trafiłeś Czteromasztowiec"))
            {
                licznik4++;
                if(licznik4==4)
                {
                    CzteroMasztowce.Text = (++licznik4zestrzelonych).ToString();
                }
            }
        }
        private void Wyniki3(Button btn)
        {
            if (btn.Tag.Equals("Trafiłeś Trzymasztowiec"))
            {
                licznik3++;
                if (licznik3 == 3)
                {
                    licznik3 = 0;
                    TrzyMasztowce.Text = (++licznik3zestrzelonych).ToString();
                }
            }
        }
        private void Wyniki2(Button btn)
        {
            if (btn.Tag.Equals("Trafiłeś Dwumasztowiec"))
            {
                licznik2++;
                if (licznik2 == 2)
                {
                    licznik2 = 0;
                    DwuMasztowce.Text = (++licznik2zestrzelonych).ToString();
                }
            }
        }
        private void Wyniki1(Button btn)
        {
            if (btn.Tag.Equals("Trafiłeś Jednomasztowca"))
            {
                    JednoMasztowce.Text = (++licznik1).ToString();
            }
        }
    }
}
