using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GloryHole
{
    public partial class GloryHoleWPF : Window
    {
        ObservableCollection<FamilySymbol> IntersectionWallRectangularFamilySymbolCollection;
        ObservableCollection<FamilySymbol> IntersectionWallRoundFamilySymbolCollection;

        ObservableCollection<FamilySymbol> IntersectionFloorRectangularFamilySymbolCollection;
        ObservableCollection<FamilySymbol> IntersectionFloorRoundFamilySymbolCollection;

        public List<RevitLinkInstance> SelectedRevitLinkInstances;

        public FamilySymbol IntersectionWallRectangularFamilySymbol;
        public FamilySymbol IntersectionWallRoundFamilySymbol;
        public FamilySymbol IntersectionFloorRectangularFamilySymbol;
        public FamilySymbol IntersectionFloorRoundFamilySymbol;

        public GloryHoleWPF(List<RevitLinkInstance> revitLinkInstanceList, List<FamilySymbol> intersectionFamilySymbolList)
        {
            IntersectionWallRectangularFamilySymbolCollection = new ObservableCollection<FamilySymbol>(intersectionFamilySymbolList
                .Where(fs => fs.Family.Name == "Пересечение_Стена_Прямоугольное").OrderBy(fs => fs.Name, new AlphanumComparatorFastString()));
            IntersectionWallRoundFamilySymbolCollection = new ObservableCollection<FamilySymbol>(intersectionFamilySymbolList
                .Where(fs => fs.Family.Name == "Пересечение_Стена_Круглое").OrderBy(fs => fs.Name, new AlphanumComparatorFastString()));

            IntersectionFloorRectangularFamilySymbolCollection = new ObservableCollection<FamilySymbol>(intersectionFamilySymbolList
                .Where(fs => fs.Family.Name == "Пересечение_Плита_Прямоугольное").OrderBy(fs => fs.Name, new AlphanumComparatorFastString()));
            IntersectionFloorRoundFamilySymbolCollection = new ObservableCollection<FamilySymbol>(intersectionFamilySymbolList
                .Where(fs => fs.Family.Name == "Пересечение_Плита_Круглое").OrderBy(fs => fs.Name, new AlphanumComparatorFastString()));

            InitializeComponent();

            listBox_RevitLinkInstance.ItemsSource = revitLinkInstanceList;
            listBox_RevitLinkInstance.DisplayMemberPath = "Name";

            comboBox_IntersectionWallRectangularFamilySymbol.ItemsSource = IntersectionWallRectangularFamilySymbolCollection;
            comboBox_IntersectionWallRectangularFamilySymbol.DisplayMemberPath = "Name";
            if(comboBox_IntersectionWallRectangularFamilySymbol.Items.Count != 0)
            {
                comboBox_IntersectionWallRectangularFamilySymbol.SelectedItem = comboBox_IntersectionWallRectangularFamilySymbol.Items.GetItemAt(0);
            }
            

            comboBox_IntersectionWallRoundFamilySymbol.ItemsSource = IntersectionWallRoundFamilySymbolCollection;
            comboBox_IntersectionWallRoundFamilySymbol.DisplayMemberPath = "Name";
            if (comboBox_IntersectionWallRoundFamilySymbol.Items.Count != 0)
            {
                comboBox_IntersectionWallRoundFamilySymbol.SelectedItem = comboBox_IntersectionWallRoundFamilySymbol.Items.GetItemAt(0);
            }

            comboBox_IntersectionFloorRectangularFamilySymbol.ItemsSource= IntersectionFloorRectangularFamilySymbolCollection;
            comboBox_IntersectionFloorRectangularFamilySymbol.DisplayMemberPath = "Name";
            if (comboBox_IntersectionFloorRectangularFamilySymbol.Items.Count != 0)
            {
                comboBox_IntersectionFloorRectangularFamilySymbol.SelectedItem = comboBox_IntersectionFloorRectangularFamilySymbol.Items.GetItemAt(0);
            }

            comboBox_IntersectionFloorRoundFamilySymbol.ItemsSource = IntersectionFloorRoundFamilySymbolCollection;
            comboBox_IntersectionFloorRoundFamilySymbol.DisplayMemberPath = "Name";
            if (comboBox_IntersectionFloorRoundFamilySymbol.Items.Count != 0)
            {
                comboBox_IntersectionFloorRoundFamilySymbol.SelectedItem = comboBox_IntersectionFloorRoundFamilySymbol.Items.GetItemAt(0);
            }
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            this.DialogResult = true;
            this.Close();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                SaveSettings();
                this.DialogResult = true;
                this.Close();
            }

            else if (e.Key == Key.Escape)
            {
                this.DialogResult = false;
                this.Close();
            }
        }
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        private void SaveSettings()
        {
            SelectedRevitLinkInstances = listBox_RevitLinkInstance.SelectedItems.Cast<RevitLinkInstance>().ToList();
            IntersectionWallRectangularFamilySymbol = comboBox_IntersectionWallRectangularFamilySymbol.SelectedItem as FamilySymbol;
            IntersectionWallRoundFamilySymbol = comboBox_IntersectionWallRoundFamilySymbol.SelectedItem as FamilySymbol;
            IntersectionFloorRectangularFamilySymbol = comboBox_IntersectionFloorRectangularFamilySymbol.SelectedItem as FamilySymbol;
            IntersectionFloorRoundFamilySymbol = comboBox_IntersectionFloorRoundFamilySymbol.SelectedItem as FamilySymbol;
        }
    }
}
