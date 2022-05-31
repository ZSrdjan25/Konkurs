using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.DataAccessObject;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KandidatDao kandidatDao = new KandidatDao();
        Kandidat kandidat = new Kandidat();

        public MainWindow()
        {
            InitializeComponent();
        }

        //VALIDACIJA
        public bool Validation()
        {
            if (string.IsNullOrWhiteSpace(TextBoxIme.Text))
            {
                TextBlockErrorIme.Text = "Unesite ime kandidata.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxPrezime.Text))
            {
                TextBlockErrorPrezime.Text = "Unesite prezime kandidata.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxJMBG.Text))
            {
                TextBlockErrorJMBG.Text = "Unesite matični broj kandidata.";
                return false;
            }
            TextBlockErrorIme.Text = TextBlockErrorPrezime.Text = TextBlockErrorJMBG.Text = "";
            return true;
        }

        public void OcistiPolja()
        {
            TextBoxIme.Text = TextBoxPrezime.Text = TextBoxJMBG.Text = TextBoxTelefon.Text = TextBoxNapomena.Text = "";
            DataGridKandidat.SelectedIndex = -1;
        }

        //GET
        private void GET()
        {
            List<Kandidat> listaKandidata;
            listaKandidata = kandidatDao.GET();
            DataGridKandidat.ItemsSource = listaKandidata;
            ComboBoxKandidatZaposlen.Items.Add("DA");
            ComboBoxKandidatZaposlen.Items.Add("NE");
            ComboBoxKandidatZaposlen.SelectedIndex = 0;
            DatePickerGodinaRodjenja.SelectedDate = DateTime.Now;
            DatePickerDatumIzmjene.SelectedDate = DateTime.Now;
            OcistiPolja();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GET();
        }

        //SAVE
        private void SAVE()
        {
            if(Validation())
            {
                kandidat.Ime = TextBoxIme.Text;
                kandidat.Prezime = TextBoxPrezime.Text;
                kandidat.JMBG = TextBoxJMBG.Text;
                kandidat.GodinaRodjenja = (DateTime)DatePickerGodinaRodjenja.SelectedDate;
                kandidat.Telefon = TextBoxTelefon.Text;
                kandidat.Napomena = TextBoxNapomena.Text;
                kandidat.KandidatZaposlen = ComboBoxKandidatZaposlen.SelectedItem.ToString();
                kandidat.DatumIzmjenePodataka = DateTime.Now;
                _ = kandidatDao.SAVE(kandidat);
                MessageBox.Show("Sačuvano.");
                GET();
            }
        }

        //FILTRIRAJ 
        private List<Kandidat> PronadjiKandidate(string pretraga)
        {
            List<Kandidat> listaKandidata = null;
            listaKandidata = kandidatDao.GET();
            
            if (listaKandidata != null)
            {
                IEnumerable<Kandidat> filtriranaLista =
                listaKandidata.Select(p => p);

                pretraga = pretraga.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(pretraga))
                {
                    filtriranaLista = filtriranaLista
                        .Where(k => k.Ime.ToLower().Contains(pretraga) || k.Prezime.ToLower().Contains(pretraga) || k.JMBG.ToLower().Contains(pretraga));
                }

                return filtriranaLista.ToList();
            }
            else
            {
                return null;
            }
        }

        private void PrikaziKandidate()
        {
            string pretraga = TextBoxPretraga.Text.Trim().ToLower();
            DataGridKandidat.ItemsSource = PronadjiKandidate(pretraga);
        }

        private void DataGridKandidat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridKandidat.SelectedIndex > -1)
            {
                kandidat = kandidat = DataGridKandidat.SelectedItem as Kandidat;
                TextBoxIme.Text = kandidat.Ime;
                TextBoxPrezime.Text = kandidat.Prezime;
                TextBoxJMBG.Text = kandidat.JMBG;
                DatePickerGodinaRodjenja.SelectedDate = kandidat.GodinaRodjenja;
                TextBoxTelefon.Text = kandidat.Telefon;
                ComboBoxKandidatZaposlen.SelectedValue = kandidat.KandidatZaposlen;
                TextBoxNapomena.Text = kandidat.Napomena;
                DatePickerDatumIzmjene.SelectedDate = kandidat.DatumIzmjenePodataka;
            }
        }

        //SACUVAJ
        private void ButtonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            SAVE();
        }

        //OTKAZI
        private void ButtonOtkazi_Click(object sender, RoutedEventArgs e)
        {
            TextBoxIme.Text = TextBoxPrezime.Text = TextBoxJMBG.Text = TextBoxTelefon.Text = TextBoxNapomena.Text = "";
            DataGridKandidat.SelectedIndex = -1;
            ComboBoxKandidatZaposlen.SelectedIndex = 0;
            DatePickerGodinaRodjenja.SelectedDate = DateTime.Now;
            DatePickerDatumIzmjene.SelectedDate = DateTime.Now;
        }

        //DODAJ
        private void ButtonDodaj_Click(object sender, RoutedEventArgs e)
        {
            TextBoxIme.Text = TextBoxPrezime.Text = TextBoxJMBG.Text = TextBoxTelefon.Text = TextBoxNapomena.Text = "";
            DataGridKandidat.SelectedIndex = -1;
            ComboBoxKandidatZaposlen.SelectedIndex = 0;
            DatePickerGodinaRodjenja.SelectedDate = DateTime.Now;
            DatePickerDatumIzmjene.SelectedDate = DateTime.Now;
            TextBoxIme.Focus();
        }

        //IZBRISI
        private void ButtonIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridKandidat.SelectedIndex < 0)
                {
                    MessageBox.Show("Izaberite kandidata.");
                }
                else
                {
                    Kandidat kandidat = DataGridKandidat.SelectedItem as Kandidat;
                    MessageBoxResult mbr = MessageBox.Show($"Brisanje kandidata: {kandidat.Ime} {kandidat.Prezime}", "Brisanje", MessageBoxButton.YesNo);

                    if (mbr == MessageBoxResult.No)
                    {
                        return;
                    }

                    kandidatDao.DELETE(kandidat);
                    MessageBox.Show("Kandidat je obrisan.");

                    GET();
                }
               
            }
            catch (Exception xcp)
            {

                MessageBox.Show(xcp.Message);
            }
        }

        //PRETRAGA
        private void ButtonPretraga_Click(object sender, RoutedEventArgs e)
        {
            PrikaziKandidate();
        }
    }
}
