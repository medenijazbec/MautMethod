using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MAUTmethod;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MautMetoda
{
    public partial class Form1 : Form
    {
        private Dictionary<string, UtilityFunction> utilityFunctions;
        public Form1()
        {
            InitializeComponent();
            utilityFunctions = new Dictionary<string, UtilityFunction>();
            InitializeAlternativeGrid();
            dataGridViewNumerical.Visible = false;
        }
        private void InitializeAlternativeGrid()
        {
            // dodaanje stolpca za ime alternative dgv
            this.dataGridViewAlternatives.Columns.Add("AlternativeName", "Alternative Name");

            // dodaanje stolpcev za parametre in njihove utility vrednosti v mrežo
            AddParameterColumns(treeViewParameters.Nodes, dataGridViewAlternatives);

            // dodaanje stolpca za ime alternative v dgv
            this.dataGridViewNumerical.Columns.Add("AlternativeName", "Alternative Name");
            AddParameterColumns(treeViewParameters.Nodes, dataGridViewNumerical);
        }

        private void AddParameterColumns(TreeNodeCollection nodes, DataGridView gridView)
        {
            foreach (TreeNode node in nodes)
            {
                //pridobi informacije o parametru iz oznake node
                ParameterInfo parameterInfo = node.Tag as ParameterInfo;
                if (parameterInfo != null && parameterInfo.Type == "Basic")
                {
                    string parameterName = node.Text.Split(' ')[0];
                    gridView.Columns.Add(parameterName, parameterName);
                    gridView.Columns.Add($"{parameterName}_Utility", $"{parameterName} Utility");
                }
                //rekurzivno kliče metodo za podnode trenutnega noda
                AddParameterColumns(node.Nodes, gridView);
            }
        }



        private void buttonAddAlternative_Click(object sender, EventArgs e)
        {
            AlternativeForm alternativeForm = new AlternativeForm(GetParameterNames());
            //preveri ce je uporabnik potrdil vnos nove alternative
            if (alternativeForm.ShowDialog() == DialogResult.OK)
            {
                //ustvari novo vrstico za alternativne vrednosti v dataGridViewAlternatives
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridViewAlternatives);
                row.Cells[0].Value = alternativeForm.AlternativeName;
                //izpolni celice vrstice z vrednostmi parametrov iz alternativeForm
                for (int i = 0; i < alternativeForm.RawParameterValues.Count; i++)
                {
                    row.Cells[2 * i + 1].Value = alternativeForm.RawParameterValues[i];
                }

                //doda novo vrstico
                this.dataGridViewAlternatives.Rows.Add(row);

                //preveri in doda manjkajoče stolpce iz dgvALT v dgvNum
                while (this.dataGridViewNumerical.Columns.Count < this.dataGridViewAlternatives.Columns.Count)
                {
                    this.dataGridViewNumerical.Columns.Add(this.dataGridViewAlternatives.Columns[this.dataGridViewNumerical.Columns.Count].Name, this.dataGridViewAlternatives.Columns[this.dataGridViewNumerical.Columns.Count].HeaderText);
                }

                //ustvari novo vrstico za numerične vrednosti v dgvNum
                DataGridViewRow numericalRow = new DataGridViewRow();
                numericalRow.CreateCells(this.dataGridViewNumerical);
                numericalRow.Cells[0].Value = alternativeForm.AlternativeName;
                for (int i = 0; i < alternativeForm.NumericParameterValues.Count; i++)
                {
                    numericalRow.Cells[2 * i + 1].Value = alternativeForm.NumericParameterValues[i];
                }
                //doda novo vrstico v dataGridViewNumerical
                this.dataGridViewNumerical.Rows.Add(numericalRow);
            }
        }

        private void buttonEditAlternative_Click(object sender, EventArgs e)
        {

            if (this.dataGridViewAlternatives.SelectedRows.Count > 0)
            {
                //pridobi izbrano vrstico
                DataGridViewRow selectedRow = this.dataGridViewAlternatives.SelectedRows[0];
                //pridobi ime alternative iz prve celice vrstice
                string alternativeName = selectedRow.Cells[0].Value.ToString();
                //ustvari seznam za surove vrednosti parametrov
                List<string> rawParameterValues = new List<string>();

                //celice izbrane vrstice in pridobi vrednosti parametrov
                for (int i = 1; i < selectedRow.Cells.Count; i += 2)
                {
                    rawParameterValues.Add(selectedRow.Cells[i].Value.ToString());
                }


                AlternativeForm alternativeForm = new AlternativeForm(GetParameterNames(), alternativeName, rawParameterValues);
                if (alternativeForm.ShowDialog() == DialogResult.OK)
                {
                    //posodobi ime alternative v prvi celici vrstice
                    selectedRow.Cells[0].Value = alternativeForm.AlternativeName;

                    //posodobi surove vrednosti parametrov v izbrani vrstici
                    for (int i = 0; i < alternativeForm.RawParameterValues.Count; i++)
                    {
                        selectedRow.Cells[2 * i + 1].Value = alternativeForm.RawParameterValues[i];
                    }

                    //pridobi ustrezno vrstico v dataGridViewNumerical
                    DataGridViewRow numericalRow = this.dataGridViewNumerical.Rows[selectedRow.Index];
                    //posodobi ime alternative v prvi celici numerične vrstice
                    numericalRow.Cells[0].Value = alternativeForm.AlternativeName;

                    //posodobi numerične vrednosti parametrov v numerični vrstici
                    for (int i = 0; i < alternativeForm.NumericParameterValues.Count; i++)
                    {
                        numericalRow.Cells[2 * i + 1].Value = alternativeForm.NumericParameterValues[i];
                    }
                }
            }
            else
            {

                MessageBox.Show("Please select an alternative to edit.");
            }
        }

        private List<string> GetParameterNames()
        {
            List<string> parameterNames = new List<string>();

            //iterira skozi vse stolpce v dataGridViewAlternatives
            foreach (DataGridViewColumn column in this.dataGridViewAlternatives.Columns)
            {

                if (column.Index == 0 || column.HeaderText.EndsWith("Utility")) continue; //preskoči prvi stolpec in stolpce, katerih ime se konča z "Utility"
                parameterNames.Add(column.HeaderText); //ime stolpca v seznam imen parametrov
            }
            return parameterNames;
        }
        private void buttonAddParameter_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewParameters.SelectedNode;
            //pridobi trenutno izbrani node v treeViewParameters

            if (selectedNode == null && treeViewParameters.Nodes.Count == 0) //če ni izbran noben node in treeViewParameters nima nobenih vozlov
            {
                selectedNode = new TreeNode("Root");//nardi root
                treeViewParameters.Nodes.Add(selectedNode);
            }

            if (selectedNode != null)//preveri ali je node izbran (ali novo ustvarjen)
            {
                AddParameterForm addParameterForm = new AddParameterForm();
                if (addParameterForm.ShowDialog() == DialogResult.OK)
                {
                    string parameterName = addParameterForm.ParameterName;
                    string parameterType = addParameterForm.ParameterType;
                    int parameterWeight = addParameterForm.ParameterWeight;
                    //validacija
                    if (!string.IsNullOrEmpty(parameterName) && ValidateWeight(selectedNode, parameterWeight))
                    {//ustvari nov node za parameter z imenom in težo
                        TreeNode newNode = new TreeNode($"{parameterName} ({parameterWeight})");

                        newNode.Tag = new ParameterInfo { Type = parameterType, Weight = parameterWeight };
                        selectedNode.Nodes.Add(newNode);//doda nov node kot podnode izbranega vozla
                        selectedNode.Expand();  //razširi izbrani node, da prikaže novega podvozla

                        //če je tip parametra "Basic", do stolpce za parameter in utility v dataGridViewAlternatives
                        if (parameterType == "Basic")
                        {
                            this.dataGridViewAlternatives.Columns.Add(parameterName, parameterName);
                            this.dataGridViewAlternatives.Columns.Add($"{parameterName}_Utility", $"{parameterName} Utility");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid parameter name or weight.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a parent node to add a parameter.");
            }
        }
        private bool ValidateWeight(TreeNode parentNode, int newWeight)
        {
            int totalWeight = newWeight; //začetna teža je nastavljena na težo novega parametra
            foreach (TreeNode childNode in parentNode.Nodes)//iterira skozi vse podnode starševskega vozla
            {
                if (childNode != treeViewParameters.SelectedNode)//preveri, če trenutni node ni izbrani node (k nocs spet dodat negove teže)
                {
                    ParameterInfo parameterInfo = childNode.Tag as ParameterInfo;//pridobi informacije o parametru iz oznake vozla
                    totalWeight += parameterInfo.Weight; //doda težo trenutnega parametra k skupni teži
                }
            }
            return totalWeight <= 100;   //preveri, če skupna teža ne presega 100
        }

        private void buttonEditParameter_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewParameters.SelectedNode; //pridobi trenutno izbrani node v treeViewParameters
                                                                     //preveri, če je izbran node
            if (selectedNode != null)
            {

                if (selectedNode.Text == "Root") //preveri, če je izbrani node korenski node
                {
                    MessageBox.Show("Root node cannot be edited.");
                    return;
                }
                //pridobi informacije o parametru iz oznake vozla
                ParameterInfo parameterInfo = selectedNode.Tag as ParameterInfo;
                EditParameterForm editParameterForm = new EditParameterForm(selectedNode.Text, parameterInfo.Type, parameterInfo.Weight);
                if (editParameterForm.ShowDialog() == DialogResult.OK) //preveri, če je uporabnik potrdil urejanje parametra
                { //preveri, če je nova teža parametra veljavna
                    if (ValidateWeight(selectedNode.Parent, editParameterForm.ParameterWeight - parameterInfo.Weight))
                    {
                        selectedNode.Text = $"{editParameterForm.ParameterName} ({editParameterForm.ParameterWeight})";
                        selectedNode.Tag = new ParameterInfo { Type = editParameterForm.ParameterType, Weight = editParameterForm.ParameterWeight };
                    }
                    else
                    {
                        MessageBox.Show("Invalid parameter weight.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a parameter to edit.");
            }
        }

        private void buttonDeleteParameter_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewParameters.SelectedNode;
            if (selectedNode != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this parameter?", "Delete Parameter", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    treeViewParameters.Nodes.Remove(selectedNode);
                }
            }
            else
            {
                MessageBox.Show("Please select a parameter to delete.");
            }
        }
        private void RecalculateUtilitiesForParameter(string parameterName, DataGridView gridView)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (row.IsNewRow) continue; //preskoči novo vrstico - dodaanje novih

                //preveri, če celica za podani parameter obstaja in ima vrednost
                if (row.Cells[parameterName] != null && row.Cells[parameterName].Value != null)
                {
                    string cellValue = row.Cells[parameterName].Value.ToString(); //pridobi vrednost celice kot niz
                    string numericString = new string(cellValue.Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());//filtrira niz, da obdrži samo številke, decimalne pike in minuse

                    if (double.TryParse(numericString, out double value))  //poskusi pretvoriti filtrirani niz v double vrednost
                    {
                        double utility = utilityFunctions[parameterName].CalculateUtility(value); //nastavi izračunano utility vrednost v ustrezno celico
                        row.Cells[$"{parameterName}_Utility"].Value = utility;
                    }
                }
            }
        }
        private (double min, double max) GetMinMaxValues(string parameterName)
        {
            double minValue = double.MaxValue; //nastavi začetne vrednosti za minimum in maksimum
            double maxValue = double.MinValue;

            foreach (DataGridViewRow row in dataGridViewAlternatives.Rows) //iterira skozi vse vrstice v dataGridViewAlternatives
            {
                if (row.IsNewRow) continue;
                //pridobi celico za podani parameter v trenutni vrstici
                DataGridViewCell cell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.OwningColumn.HeaderText == parameterName);
                if (cell != null && cell.Value != null) //preveri, če celica obstaja in ima vrednost
                {
                    string cellValue = cell.Value.ToString();  //pridobi vrednost celice kot niz
                    string numericString = new string(cellValue.Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());//filtrira niz, da obdrži samo številke, decimalne pike in minuse
                    //poskusi pretvoriti filtrirani niz v double vrednost
                    if (double.TryParse(numericString, out double value))
                    {//posodobi minimum in maksimum glede na trenutno vrednost
                        if (value < minValue)
                        {
                            minValue = value;
                        }
                        if (value > maxValue)
                        {
                            maxValue = value;
                        }
                    }
                }
            }


            if (minValue == double.MaxValue) minValue = 0;
            if (maxValue == double.MinValue) maxValue = 0;

            return (minValue, maxValue); //vrne minimum in maksimum kot tuple
        }
        private void buttonSetUtilityFunction_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewParameters.SelectedNode; //pridobi trenutno izbrani node v treeViewParameters
            if (selectedNode != null) //preveri, če je izbran node
            {
                ParameterInfo parameterInfo = selectedNode.Tag as ParameterInfo; //pridobi informacije o parametru iz oznake vozla
                if (parameterInfo != null && parameterInfo.Type == "Basic") //preveri, če informacije o parametru niso null in ali je tip parametra "Basic"
                {
                    string parameterName = selectedNode.Text.Split(' ')[0]; //pridobi ime parametra iz besedila vozla (prvi del)
                    var minMaxValues = GetMinMaxValues(parameterName); //pridobi minimalne in maksimalne vrednosti za ta parameter
                    UtilityFunctionForm utilityFunctionForm = new UtilityFunctionForm(minMaxValues.min, minMaxValues.max);
                    //preveri, če je uporabnik potrdil nastavljanje utility funkcije
                    if (utilityFunctionForm.ShowDialog() == DialogResult.OK)
                    {  //pridobi tip funkcije, minimalno in maksimalno vrednost iz obrazca
                        string functionType = utilityFunctionForm.FunctionType;
                        double minValue = utilityFunctionForm.MinValue;
                        double maxValue = utilityFunctionForm.MaxValue;
                        //ustvari novo utility funkcijo in jo doda v slovar utilityFunctions
                        utilityFunctions[parameterName] = new UtilityFunction
                        {
                            FunctionType = functionType,
                            MinValue = minValue,
                            MaxValue = maxValue
                        };
                        //posodobi besedilo izbranega vozla z novimi podatki o utility funkciji
                        selectedNode.Text = $"{parameterName} ({parameterInfo.Weight}) [Utility: {functionType}]";

                        //utility vrednosti za ta parameter v dataGridViewNumerical
                        RecalculateUtilitiesForParameter(parameterName, dataGridViewNumerical);
                    }
                }
                else
                {
                    MessageBox.Show("Utility functions can only be set for basic parameters.");
                }
            }
            else
            {
                MessageBox.Show("Please select a parameter to set a utility function.");
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            //ustvari slovar za shranjevanje utility vrednosti za vsako alternativo
            var alternativeUtilities = new Dictionary<string, Dictionary<string, double>>();
            //iterira skozi vse vrstice v dataGridViewNumerical
            foreach (DataGridViewRow row in dataGridViewNumerical.Rows)
            {
                if (row.IsNewRow) continue;

                string alternativeName = row.Cells[0].Value.ToString();//pridobi ime alternative iz prve celice vrstice
                var utilities = new Dictionary<string, double>(); //ustvari nov slovar za shranjevanje utility vrednosti za trenutni parameter

                for (int i = 1; i < row.Cells.Count; i += 2) //iterira skozi celice vrstice, preskoči prvo celico, in obravnava vsak drugi stolpec (utility vrednosti)
                {   //pridobi ime parametra iz naslova stolpca
                    string parameterName = dataGridViewNumerical.Columns[i].HeaderText;
                    //pretvori vrednost celice v double in jo shrani v slovar utility vrednosti
                    double utilityValue = Convert.ToDouble(row.Cells[i + 1].Value);
                    utilities[parameterName] = utilityValue;
                }
                //doda slovar utility vrednosti za trenutno alternativo v glavni slovar
                alternativeUtilities[alternativeName] = utilities;
            }
            // ustvari nov obrazec za prikaz rezultatov in mu posreduje izračunane utility vrednosti in drevo parametrov
            ResultForm resultForm = new ResultForm(alternativeUtilities, treeViewParameters);  //prikaži obrazec za rezultate
            resultForm.Show();
        }
        public class UtilityFunction
        {
            public string FunctionType { get; set; }
            public double MinValue { get; set; }
            public double MaxValue { get; set; }

            public double CalculateUtility(double value)
            {
                switch (FunctionType)
                {
                    case "Linear":
                        //funkcija se povečuje linearno glede na vrednost
                        //Formula: (value - MinValue) / (MaxValue - MinValue)
                        //ko je med vrednostjo in utility funkcijo neposredna proporcionalna povezava
                        return (value - MinValue) / (MaxValue - MinValue);

                    case "Exponential":
                        //funkcija se povečuje eksponentno glede na vrednost.
                        //Formula: Math.Exp(value - MinValue) / Math.Exp(MaxValue - MinValue)
                        //ko je vpliv višjih vrednosti na utility veliko večji od vpliva nižjih vrednosti
                        return Math.Exp(value - MinValue) / Math.Exp(MaxValue - MinValue);

                    case "Logarithmic":
                        //funkcija se povečuje logaritemsko glede na vrednost.
                        //Formula: Math.Log(value - MinValue + 1) / Math.Log(MaxValue - MinValue + 1)
                        //Uporaba: Kadar imajo začetne spremembe večji vpliv, kasnejše pa manjši vpliv
                        return Math.Log(value - MinValue + 1) / Math.Log(MaxValue - MinValue + 1);

                    case "Concave":
                        //funkcija sledi konkavni (kvadratni) zvezi, kar predstavlja padajoče donose
                        //Formula: Math.Pow((value - MinValue) / (MaxValue - MinValue), 2)
                        //ko povečevanje vrednosti prinaša vedno manjše izboljšanje v utility
                        return Math.Pow((value - MinValue) / (MaxValue - MinValue), 2);

                    case "Convex":
                        //funkcija sledi konveksni (kvadratni koren) zvezi, kar predstavlja naraščajoče donose
                        //Formula: Math.Sqrt((value - MinValue) / (MaxValue - MinValue))
                        //Uporaba: Kadar začetne spremembe prinašajo manjše izboljšave, kasnejše pa večje
                        return Math.Sqrt((value - MinValue) / (MaxValue - MinValue));

                    case "InverseLinear":
                        //funkcija se zmanjšuje linearno glede na vrednost
                        //Formula: 1 - ((value - MinValue) / (MaxValue - MinValue))
                        //ko je obratno sorazmerje med vrednostjo in utility, torej so nižje vrednosti boljše
                        return 1 - ((value - MinValue) / (MaxValue - MinValue));

                    default:
                        return 0;
                }
            }



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dataGridViewNumerical.Visible = true;
            }
            else
            {
                dataGridViewNumerical.Visible = false;
            }
        }

    }
}
