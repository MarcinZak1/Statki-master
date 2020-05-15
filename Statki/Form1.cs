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
        int plaszczyzna, pozycja, proby = 0, trafione=0, licznik1 = 0;
        int licznik4 = 0, licznik4zestrzelonych = 0;
        int licznik3 = 0, licznik3zestrzelonych = 0;
        int licznik2 = 0, licznik2zestrzelonych = 0;
        int liczba1masztowcow = 0, liczba2masztowcow = 0, liczba3masztowcow = 0;
        string pusty = "Pudło!";
        string trzymasztowiec = "Trafiłeś Trzymasztowiec";
        string dwumasztowiec = "Trafiłeś Dwumasztowiec";
        string jednomasztowiec = "Trafiłeś Jednomasztowiec";
        string czteromasztowiec = "Trafiłeś Czteromasztowiec";

        public void Form1_Load(object sender, EventArgs e)
        {
            int index = 1;
            List<Control> controls = this.Controls.OfType<Button>().Cast<Control>().ToList();
            controls.Reverse();
            foreach (var item in controls)
            {
                Button btn = (Button)item;
                btn.Tag = String.Format(pusty); // ustawiam Tag przycisku wg schematu: BUTTONx, gdzie x to numer przycisku
                btn.Text = string.Empty;                 //index.ToString();
                btn.Click += Btn_Click;  //podpięcie do klikniecia na przycisk metody 
                index++;
            }
            FindShipButtonByTag(101);
            CreatBoat4Masztowiec(controls);
            WarunekDla3(controls);
            WarunekDla2(controls);
            WarunekDla1(controls);
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
            if (btn.Tag.Equals(czteromasztowiec))
            {
                btn.Text = "4";
            }
            else if (btn.Tag.Equals(trzymasztowiec))
            {
                btn.Text = "3";
            }
            else if (btn.Tag.Equals(dwumasztowiec))
            {
                btn.Text = "2";
            }
            else if (btn.Tag.Equals(jednomasztowiec))
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
            CelneStrzaly(btn);
            Proby(btn);
            KoniecGry();
        }

        private void CreatBoat4Masztowiec(List<Control> controls)
        {
            plaszczyzna = random.Next(0, 2);
            if (plaszczyzna == 1)   // jeśli płaszczyzna==1 ustawiamy statek pionowo
            {
                pozycja = random.Next(1, 71) - 1; //wybieramy guzik z danej puli 
                for (int i = 0; i < 4; i++)
                {
                    controls[pozycja + i * 10].Tag = czteromasztowiec;
                }
            }
            else
            {
                pozycja = random.Next(1, 98) - 1;

                while ((pozycja > 6 && pozycja < 10) || (pozycja > 16 && pozycja < 20) || (pozycja > 26 && pozycja < 30) ||
                (pozycja > 36 && pozycja < 40) || (pozycja > 46 && pozycja < 50) || (pozycja > 56 && pozycja < 60) ||
                (pozycja > 66 && pozycja < 70) || (pozycja > 76 && pozycja < 80) || (pozycja > 86 && pozycja < 90))
                {
                    pozycja = random.Next(1, 98) - 1;
                }
                for (int i = 0; i < 4; i++)
                {
                    controls[pozycja + i].Tag = czteromasztowiec;
                }
            }
        }

        private void Wyniki4(Button btn)
        {

            if (btn.Tag.Equals(czteromasztowiec))
            {
                licznik4++;
                if (licznik4 == 4)
                {
                    CzteroMasztowce.Text = (++licznik4zestrzelonych).ToString();
                }
            }
        }
        private void Wyniki3(Button btn)
        {
            if (btn.Tag.Equals(trzymasztowiec))
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
            if (btn.Tag.Equals(dwumasztowiec))
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
            if (btn.Tag.Equals(jednomasztowiec))
            {
                JednoMasztowce.Text = (++licznik1).ToString();
            }
        }

        private void WarunekDla1(List<Control> controls)
        {
            while (liczba1masztowcow != 4)     // liczba 1masztowców
            {
                pozycja = random.Next(0, 100); //wybieramy guzik z danej puli

                // jeśli jest na górnym boku
                if ((pozycja > 0 && pozycja < 9))
                {
                    if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                   && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                   && controls[pozycja + 11].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                // jesli jest na dolnym boku
                else if ((pozycja > 90 && pozycja < 99))
                {
                    if (controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                       && controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty)
                       && controls[pozycja - 9].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                // jesli jest na lewym boku
                else if ((pozycja == 10 || pozycja == 20 || pozycja == 30 || pozycja == 40 || pozycja == 50 || pozycja == 60
                        || pozycja == 70 || pozycja == 80))
                {
                    if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                       && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                       && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                //jesli jest na prawym boku
                else if ((pozycja == 19 || pozycja == 29 || pozycja == 39 || pozycja == 49 || pozycja == 59 || pozycja == 69
                        || pozycja == 79 || pozycja == 89))
                {
                    if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                        && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty)
                        && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                // jesli jest w lewym gornym rogu
                else if (pozycja == 0)
                {
                    if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                       && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                // jesli jest w prawym gornym rogu
                else if (pozycja == 9)
                {
                    if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                       && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                // jesli jest w lewym dolnym rogu
                else if (pozycja == 90)
                {
                    if
                           (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                // jesli jest w prawym dolnym rogu
                else if (pozycja == 99)
                {
                    if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                       && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
                // jesli pozycja nie przylega do zadnej ze ścian
                else
                {
                    if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                      && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty)
                      && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                      && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty)
                      && controls[pozycja + 9].Tag.Equals(pusty))
                    {
                        Dodawanie1Masztowca(controls);
                    }
                }
            }
        }
        private void WarunekDla3(List<Control> controls)
        {
            while (liczba3masztowcow != 2)     // liczba 1masztowców
            {
                plaszczyzna = random.Next(0, 2);
                if (plaszczyzna == 1)   // jeśli płaszczyzna==1 ustawiamy statek pionowo
                {
                    pozycja = random.Next(1, 81) - 1; //wybieramy guzik z danej puli

                    if ((pozycja > 0 && pozycja < 9))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty)
                           && controls[pozycja + 21].Tag.Equals(pusty) && controls[pozycja + 29].Tag.Equals(pusty) && controls[pozycja + 30].Tag.Equals(pusty)
                           && controls[pozycja + 31].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else if ((pozycja > 70 && pozycja < 79))
                    {
                        if (controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                               && controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty)
                               && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty)
                               && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 21].Tag.Equals(pusty)
                               && controls[pozycja + 19].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else if ((pozycja == 10 || pozycja == 20 || pozycja == 30 || pozycja == 40 || pozycja == 50 || pozycja == 60))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja + 21].Tag.Equals(pusty)
                       && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                       && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 30].Tag.Equals(pusty)
                       && controls[pozycja + 31].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else if ((pozycja == 19 || pozycja == 29 || pozycja == 39 || pozycja == 49 || pozycja == 59 || pozycja == 69))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja + 30].Tag.Equals(pusty)
                           && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty)
                           && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 29].Tag.Equals(pusty)
                           && controls[pozycja - 11].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 0)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty)
                           && controls[pozycja + 21].Tag.Equals(pusty) && controls[pozycja + 30].Tag.Equals(pusty) && controls[pozycja + 31].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 9)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty)
                           && controls[pozycja + 30].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty) && controls[pozycja + 29].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 70)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty)
                           && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja + 21].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 79)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                    else
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                          && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                          && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty)
                          && controls[pozycja + 21].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty)
                          && controls[pozycja + 31].Tag.Equals(pusty) && controls[pozycja + 30].Tag.Equals(pusty) && controls[pozycja + 29].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPionowo(controls);
                        }
                    }
                }
                else
                {
                    pozycja = random.Next(1, 99) - 1;

                    while ((pozycja > 7 && pozycja < 10) || (pozycja > 17 && pozycja < 20) || (pozycja > 27 && pozycja < 30) ||
                    (pozycja > 37 && pozycja < 40) || (pozycja > 47 && pozycja < 50) || (pozycja > 57 && pozycja < 60) ||
                    (pozycja > 67 && pozycja < 70) || (pozycja > 77 && pozycja < 80) || (pozycja > 87 && pozycja < 90) || (pozycja > 97))
                    {
                        pozycja = random.Next(1, 99) - 1;
                    }

                    if ((pozycja > 0 && pozycja < 7))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                           && controls[pozycja + 2].Tag.Equals(pusty) && controls[pozycja + 3].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                           && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty)
                           && controls[pozycja + 12].Tag.Equals(pusty) && controls[pozycja + 13].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else if ((pozycja > 90 && pozycja < 97))
                    {
                        if (controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                           && controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty)
                           && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja - 8].Tag.Equals(pusty) && controls[pozycja - 7].Tag.Equals(pusty)
                           && controls[pozycja + 2].Tag.Equals(pusty) && controls[pozycja + 3].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else if ((pozycja == 10 || pozycja == 20 || pozycja == 30 || pozycja == 40 || pozycja == 50 || pozycja == 60 || pozycja == 70 || pozycja == 80))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                       && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja - 8].Tag.Equals(pusty)
                       && controls[pozycja + 12].Tag.Equals(pusty) && controls[pozycja + 13].Tag.Equals(pusty) && controls[pozycja - 7].Tag.Equals(pusty)
                       && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty) && controls[pozycja + 3].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else if ((pozycja == 17 || pozycja == 27 || pozycja == 37 || pozycja == 47 || pozycja == 57 || pozycja == 67 || pozycja == 77 || pozycja == 87))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty)
                           && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 12].Tag.Equals(pusty)
                           && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja - 8].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 0)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 12].Tag.Equals(pusty)
                           && controls[pozycja + 13].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty) && controls[pozycja + 3].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 7)
                    {
                        if
                               (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty)
                           && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 12].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 90)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja - 8].Tag.Equals(pusty)
                           && controls[pozycja + 2].Tag.Equals(pusty) && controls[pozycja + 3].Tag.Equals(pusty) && controls[pozycja - 7].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 97)
                    {
                        if
                               (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                           && controls[pozycja - 8].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                    else
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                          && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                          && controls[pozycja - 8].Tag.Equals(pusty) && controls[pozycja - 7].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty)
                          && controls[pozycja + 3].Tag.Equals(pusty) && controls[pozycja + 12].Tag.Equals(pusty) && controls[pozycja + 13].Tag.Equals(pusty)
                          && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty))
                        {
                            Dodawanie3MasztowcaPoziomo(controls);
                        }
                    }
                }
            }
        }
        private void WarunekDla2(List<Control> controls)
        {
            while (liczba2masztowcow != 3)     // liczba 1masztowców
            {
                plaszczyzna = random.Next(0, 2);
                if (plaszczyzna == 1)   // jeśli płaszczyzna==1 ustawiamy statek pionowo
                {
                    pozycja = random.Next(1, 91) - 1; //wybieramy guzik z danej puli

                    if ((pozycja > 0 && pozycja < 9))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty)
                           && controls[pozycja + 21].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else if ((pozycja > 80 && pozycja < 89))
                    {
                        if (controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                      && controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty)
                      && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty)
                      && controls[pozycja + 9].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else if ((pozycja == 10 || pozycja == 20 || pozycja == 30 || pozycja == 40 || pozycja == 50 || pozycja == 60 || pozycja == 70))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja + 21].Tag.Equals(pusty)
                        && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                        && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else if ((pozycja == 19 || pozycja == 29 || pozycja == 39 || pozycja == 49 || pozycja == 59 || pozycja == 69))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                        && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty)
                        && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 0)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                          && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty)
                          && controls[pozycja + 21].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 9)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty)
                           && controls[pozycja + 19].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 80)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                        && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty)
                        && controls[pozycja + 10].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else if (pozycja == 89)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                        && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                        && controls[pozycja + 9].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                    else
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                          && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                          && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty)
                          && controls[pozycja + 21].Tag.Equals(pusty) && controls[pozycja + 20].Tag.Equals(pusty) && controls[pozycja + 19].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPionowo(controls);
                        }
                    }
                }
                else
                {
                    pozycja = random.Next(1, 100) - 1;

                    while ((pozycja > 8 && pozycja < 10) || (pozycja > 18 && pozycja < 20) || (pozycja > 28 && pozycja < 30) ||
                    (pozycja > 38 && pozycja < 40) || (pozycja > 48 && pozycja < 50) || (pozycja > 58 && pozycja < 60) ||
                    (pozycja > 68 && pozycja < 70) || (pozycja > 78 && pozycja < 80) || (pozycja > 88 && pozycja < 90) || (pozycja > 98 && pozycja < 100))
                    {
                        pozycja = random.Next(1, 100) - 1;
                    }

                    if ((pozycja > 0 && pozycja < 8))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                           && controls[pozycja + 2].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty)
                           && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja + 12].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                    else if ((pozycja > 90 && pozycja < 98))
                    {
                        if (controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                          && controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty)
                          && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja - 8].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                    else if ((pozycja == 10 || pozycja == 20 || pozycja == 30 || pozycja == 40 || pozycja == 50 || pozycja == 60 || pozycja == 70 || pozycja == 80))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                           && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja - 8].Tag.Equals(pusty)
                           && controls[pozycja + 12].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                    else if ((pozycja == 18 || pozycja == 28 || pozycja == 38 || pozycja == 48 || pozycja == 58 || pozycja == 68 || pozycja == 78 || pozycja == 88))
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty)
                           && controls[pozycja + 11].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 0)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty)
                           && controls[pozycja + 12].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 8)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty)
                           && controls[pozycja + 9].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 90)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                           && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                           && controls[pozycja - 8].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                    else if (pozycja == 98)
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja - 10].Tag.Equals(pusty)
                           && controls[pozycja - 1].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty)
                           && controls[pozycja - 9].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }

                    else
                    {
                        if (controls[pozycja].Tag.Equals(pusty) && controls[pozycja + 1].Tag.Equals(pusty) && controls[pozycja - 1].Tag.Equals(pusty)
                          && controls[pozycja - 10].Tag.Equals(pusty) && controls[pozycja - 11].Tag.Equals(pusty) && controls[pozycja - 9].Tag.Equals(pusty)
                          && controls[pozycja - 8].Tag.Equals(pusty) && controls[pozycja + 2].Tag.Equals(pusty)
                          && controls[pozycja + 12].Tag.Equals(pusty) && controls[pozycja + 11].Tag.Equals(pusty)
                          && controls[pozycja + 10].Tag.Equals(pusty) && controls[pozycja + 9].Tag.Equals(pusty))
                        {
                            Dodawanie2MasztowcaPoziomo(controls);
                        }
                    }
                }
            }
        }
        private void Dodawanie2MasztowcaPoziomo(List<Control> controls)
        {
            for (int j = 0; j < 2; j++)
            {
                controls[pozycja + j].Tag = dwumasztowiec;
            }
            liczba2masztowcow++;
        }
        private void Dodawanie2MasztowcaPionowo(List<Control> controls)
        {
            for (int j = 0; j < 2; j++)
            {
                controls[pozycja + j * 10].Tag = dwumasztowiec;
            }
            liczba2masztowcow++;
        }
        private void Dodawanie3MasztowcaPoziomo(List<Control> controls)
        {
            for (int j = 0; j < 3; j++)
            {
                controls[pozycja + j].Tag = trzymasztowiec;
            }
            liczba3masztowcow++;
        }
        private void Dodawanie3MasztowcaPionowo(List<Control> controls)
        {
            for (int j = 0; j < 3; j++)
            {
                controls[pozycja + j * 10].Tag = trzymasztowiec;
            }
            liczba3masztowcow++;
        }
        private void Dodawanie1Masztowca(List<Control> controls)
        {
            controls[pozycja].Tag = (jednomasztowiec);
            liczba1masztowcow++;
        }
        private void CelneStrzaly(Button btn)
        {
            if (!btn.Tag.Equals(pusty))
            {
                tbTrafioneStatki.Text = (++trafione).ToString();
            }
        }
        private void Proby(Button btn)
        {
                tbProby.Text = (++proby).ToString();
        }
        private void KoniecGry()
        {
            if(trafione==20)
            {
                string koniec = string.Format("Wygrałeś, po {0} próbach.", proby);
                MessageBox.Show(koniec);
            }
        }
    }
}