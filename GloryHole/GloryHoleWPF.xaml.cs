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

        public string HoleShapeButtonName;
        public string RoundHolesPositionButtonName;
        public double RoundHoleSizesUpIncrement;
        public double RoundHolePositionIncrement;

        public double PipeSideClearance;
        public double PipeTopBottomClearance;

        public double DuctSideClearance;
        public double DuctTopBottomClearance;

        public double CableTraySideClearance;
        public double CableTrayTopBottomClearance;

        GloryHoleSettings GloryHoleSettingsItem;


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

            GloryHoleSettingsItem = new GloryHoleSettings().GetSettings();

            InitializeComponent();

            listBox_RevitLinkInstance.ItemsSource = revitLinkInstanceList;
            listBox_RevitLinkInstance.DisplayMemberPath = "Name";

            comboBox_IntersectionWallRectangularFamilySymbol.ItemsSource = IntersectionWallRectangularFamilySymbolCollection;
            comboBox_IntersectionWallRectangularFamilySymbol.DisplayMemberPath = "Name";

            comboBox_IntersectionWallRoundFamilySymbol.ItemsSource = IntersectionWallRoundFamilySymbolCollection;
            comboBox_IntersectionWallRoundFamilySymbol.DisplayMemberPath = "Name";

            comboBox_IntersectionFloorRectangularFamilySymbol.ItemsSource= IntersectionFloorRectangularFamilySymbolCollection;
            comboBox_IntersectionFloorRectangularFamilySymbol.DisplayMemberPath = "Name";

            comboBox_IntersectionFloorRoundFamilySymbol.ItemsSource = IntersectionFloorRoundFamilySymbolCollection;
            comboBox_IntersectionFloorRoundFamilySymbol.DisplayMemberPath = "Name";

            SetSavedSettingsValueToForm();
        }
        private void radioButton_HoleShape_Checked(object sender, RoutedEventArgs e)
        {
            HoleShapeButtonName = (this.groupBox_HoleShape.Content as System.Windows.Controls.Grid)
                .Children.OfType<RadioButton>()
                .FirstOrDefault(rb => rb.IsChecked.Value == true)
                .Name;
        }

        private void radioButton_RoundHolesPosition_Checked(object sender, RoutedEventArgs e)
        {
            RoundHolesPositionButtonName = (this.groupBox_RoundHolesPosition.Content as System.Windows.Controls.Grid)
                .Children.OfType<RadioButton>()
                .FirstOrDefault(rb => rb.IsChecked.Value == true)
                .Name;
            if (RoundHolesPositionButtonName == "radioButton_RoundHolesPositionYes")
            {
                label_RoundHolePosition.IsEnabled = true;
                textBox_RoundHolePositionIncrement.IsEnabled = true;
                label_RoundHolePositionMM.IsEnabled = true;
            }
            else if (RoundHolesPositionButtonName == "radioButton_RoundHolesPositionNo")
            {
                label_RoundHolePosition.IsEnabled = false;
                textBox_RoundHolePositionIncrement.IsEnabled = false;
                label_RoundHolePositionMM.IsEnabled = false;
            }
        }
        private void SaveSettings()
        {
            GloryHoleSettingsItem = new GloryHoleSettings();
            SelectedRevitLinkInstances = listBox_RevitLinkInstance.SelectedItems.Cast<RevitLinkInstance>().ToList();

            IntersectionWallRectangularFamilySymbol = comboBox_IntersectionWallRectangularFamilySymbol.SelectedItem as FamilySymbol;
            if(IntersectionWallRectangularFamilySymbol != null)
            {
                GloryHoleSettingsItem.IntersectionWallRectangularFamilySymbolName = IntersectionWallRectangularFamilySymbol.Name;
            }

            IntersectionWallRoundFamilySymbol = comboBox_IntersectionWallRoundFamilySymbol.SelectedItem as FamilySymbol;
            if (IntersectionWallRoundFamilySymbol != null)
            {
                GloryHoleSettingsItem.IntersectionWallRoundFamilySymbolName = IntersectionWallRoundFamilySymbol.Name;
            }

            IntersectionFloorRectangularFamilySymbol = comboBox_IntersectionFloorRectangularFamilySymbol.SelectedItem as FamilySymbol;
            if (IntersectionFloorRectangularFamilySymbol != null)
            {
                GloryHoleSettingsItem.IntersectionFloorRectangularFamilySymbolName = IntersectionFloorRectangularFamilySymbol.Name;
            }

            IntersectionFloorRoundFamilySymbol = comboBox_IntersectionFloorRoundFamilySymbol.SelectedItem as FamilySymbol;
            if (IntersectionFloorRoundFamilySymbol != null)
            {
                GloryHoleSettingsItem.IntersectionFloorRoundFamilySymbolName = IntersectionFloorRoundFamilySymbol.Name;
            }

            GloryHoleSettingsItem.HoleShapeButtonName = HoleShapeButtonName;
            GloryHoleSettingsItem.RoundHolesPositionButtonName = RoundHolesPositionButtonName;

            double.TryParse(textBox_RoundHoleSizesUpIncrement.Text, out RoundHoleSizesUpIncrement);
            GloryHoleSettingsItem.RoundHoleSizesUpIncrementValue = textBox_RoundHoleSizesUpIncrement.Text;

            double.TryParse(textBox_RoundHolePositionIncrement.Text, out RoundHolePositionIncrement);
            GloryHoleSettingsItem.RoundHolePositionIncrementValue = textBox_RoundHolePositionIncrement.Text;

            double.TryParse(textBox_PipeSideClearance.Text, out PipeSideClearance);
            GloryHoleSettingsItem.PipeSideClearanceValue = textBox_PipeSideClearance.Text;

            double.TryParse(textBox_PipeTopBottomClearance.Text, out PipeTopBottomClearance);
            GloryHoleSettingsItem.PipeTopBottomClearanceValue = textBox_PipeTopBottomClearance.Text;

            double.TryParse(textBox_DuctSideClearance.Text, out DuctSideClearance);
            GloryHoleSettingsItem.DuctSideClearanceValue = textBox_DuctSideClearance.Text;

            double.TryParse(textBox_DuctTopBottomClearance.Text, out DuctTopBottomClearance);
            GloryHoleSettingsItem.DuctTopBottomClearanceValue = textBox_DuctTopBottomClearance.Text;

            double.TryParse(textBox_CableTraySideClearance.Text, out CableTraySideClearance);
            GloryHoleSettingsItem.CableTraySideClearanceValue = textBox_CableTraySideClearance.Text;

            double.TryParse(textBox_CableTrayTopBottomClearance.Text, out CableTrayTopBottomClearance);
            GloryHoleSettingsItem.CableTrayTopBottomClearanceValue = textBox_CableTrayTopBottomClearance.Text;
            GloryHoleSettingsItem.SaveSettings();
        }

        private void SetSavedSettingsValueToForm()
        {
            if(IntersectionWallRectangularFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionWallRectangularFamilySymbolName) != null)
            {
                comboBox_IntersectionWallRectangularFamilySymbol.SelectedItem = IntersectionWallRectangularFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionWallRectangularFamilySymbolName);
            }
            else
            {
                if(comboBox_IntersectionWallRectangularFamilySymbol.Items.Count != 0)
                {
                    comboBox_IntersectionWallRectangularFamilySymbol.SelectedItem = comboBox_IntersectionWallRectangularFamilySymbol.Items.GetItemAt(0);
                }
            }

            if (IntersectionWallRoundFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionWallRoundFamilySymbolName) != null)
            {
                comboBox_IntersectionWallRoundFamilySymbol.SelectedItem = IntersectionWallRoundFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionWallRoundFamilySymbolName);
            }
            else
            {
                if (comboBox_IntersectionWallRoundFamilySymbol.Items.Count != 0)
                {
                    comboBox_IntersectionWallRoundFamilySymbol.SelectedItem = comboBox_IntersectionWallRoundFamilySymbol.Items.GetItemAt(0);
                }
            }

            if (IntersectionFloorRectangularFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionFloorRectangularFamilySymbolName) != null)
            {
                comboBox_IntersectionFloorRectangularFamilySymbol.SelectedItem = IntersectionFloorRectangularFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionFloorRectangularFamilySymbolName);
            }
            else
            {
                if (comboBox_IntersectionFloorRectangularFamilySymbol.Items.Count != 0)
                {
                    comboBox_IntersectionFloorRectangularFamilySymbol.SelectedItem = comboBox_IntersectionFloorRectangularFamilySymbol.Items.GetItemAt(0);
                }
            }

            if (IntersectionFloorRoundFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionFloorRoundFamilySymbolName) != null)
            {
                comboBox_IntersectionFloorRoundFamilySymbol.SelectedItem = IntersectionFloorRoundFamilySymbolCollection.FirstOrDefault(fs => fs.Name == GloryHoleSettingsItem.IntersectionFloorRoundFamilySymbolName);
            }
            else
            {
                if (comboBox_IntersectionFloorRoundFamilySymbol.Items.Count != 0)
                {
                    comboBox_IntersectionFloorRoundFamilySymbol.SelectedItem = comboBox_IntersectionFloorRoundFamilySymbol.Items.GetItemAt(0);
                }
            }

            if (GloryHoleSettingsItem.HoleShapeButtonName != null)
            {
                if (GloryHoleSettingsItem.HoleShapeButtonName == "radioButton_HoleShapeRectangular")
                {
                    radioButton_HoleShapeRectangular.IsChecked = true;
                }
                else
                {
                    radioButton_HoleShapeRound.IsChecked = true;
                }
            }

            if (GloryHoleSettingsItem.RoundHolesPositionButtonName != null)
            {
                if (GloryHoleSettingsItem.RoundHolesPositionButtonName == "radioButton_RoundHolesPositionYes")
                {
                    radioButton_RoundHolesPositionYes.IsChecked = true;
                }
                else
                {
                    radioButton_RoundHolesPositionNo.IsChecked = true;
                }
            }

            if (GloryHoleSettingsItem.RoundHoleSizesUpIncrementValue != null)
            {
                textBox_RoundHoleSizesUpIncrement.Text = GloryHoleSettingsItem.RoundHoleSizesUpIncrementValue;
            }
            else
            {
                textBox_RoundHoleSizesUpIncrement.Text = "50";
            }

            if (GloryHoleSettingsItem.RoundHolePositionIncrementValue != null)
            {
                textBox_RoundHolePositionIncrement.Text = GloryHoleSettingsItem.RoundHolePositionIncrementValue;
            }
            else
            {
                textBox_RoundHolePositionIncrement.Text = "10";
            }

            if (GloryHoleSettingsItem.PipeSideClearanceValue != null)
            {
                textBox_PipeSideClearance.Text = GloryHoleSettingsItem.PipeSideClearanceValue;
            }
            else
            {
                textBox_PipeSideClearance.Text = "50";
            }

            if (GloryHoleSettingsItem.PipeTopBottomClearanceValue != null)
            {
                textBox_PipeTopBottomClearance.Text = GloryHoleSettingsItem.PipeTopBottomClearanceValue;
            }
            else
            {
                textBox_PipeTopBottomClearance.Text = "50";
            }

            if (GloryHoleSettingsItem.DuctSideClearanceValue != null)
            {
                textBox_DuctSideClearance.Text = GloryHoleSettingsItem.DuctSideClearanceValue;
            }
            else
            {
                textBox_DuctSideClearance.Text = "75";
            }

            if (GloryHoleSettingsItem.DuctTopBottomClearanceValue != null)
            {
                textBox_DuctTopBottomClearance.Text = GloryHoleSettingsItem.DuctTopBottomClearanceValue;
            }
            else
            {
                textBox_DuctTopBottomClearance.Text = "75";
            }

            if (GloryHoleSettingsItem.CableTraySideClearanceValue != null)
            {
                textBox_CableTraySideClearance.Text = GloryHoleSettingsItem.CableTraySideClearanceValue;
            }
            else
            {
                textBox_CableTraySideClearance.Text = "50";
            }

            if (GloryHoleSettingsItem.CableTrayTopBottomClearanceValue != null)
            {
                textBox_CableTrayTopBottomClearance.Text = GloryHoleSettingsItem.CableTrayTopBottomClearanceValue;
            }
            else
            {
                textBox_CableTrayTopBottomClearance.Text = "50";
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
    }
}
