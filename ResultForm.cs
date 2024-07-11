using System.Text;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MautMetoda
{
    public partial class ResultForm : Form
    {
        private TreeView treeViewParameters;

        private PlotView plotView;




        public ResultForm(Dictionary<string, Dictionary<string, double>> alternativeUtilities, TreeView treeViewParameters)
        {
            this.treeViewParameters = treeViewParameters;
            InitializeComponent();
            this.Text = "Results";


       



            resultDataGridView.Columns.Add("Parameter", "Parameter");
            resultDataGridView.Columns.Add("Weight", "Weight");

            foreach (var alt in alternativeUtilities.Keys)
            {
                resultDataGridView.Columns.Add(alt, alt);
            }

            var allParameters = GetAllParameters(treeViewParameters.Nodes);
            var weights = GetWeights(treeViewParameters.Nodes);

            //calc util for each param
            foreach (var param in allParameters)
            {
                //ustvari novo vrstico za DataGridView
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(resultDataGridView);
                row.Cells[0].Value = param;

                //nastavi težo, če obstaja v slovarju
                if (weights.ContainsKey(param))
                {
                    row.Cells[1].Value = param == "Root" ? 1.0 : weights[param] / 100.0;
                }
                else
                {
                    row.Cells[1].Value = "N/A";
                }

                //začetni indeks za stolpce
                int colIndex = 2; // Adjusted for weight column
                                  //iterira skozi alternative
                foreach (var alt in alternativeUtilities.Keys)
                {
                    //izračuna utility vrednost za parameter in alternativo
                    double utility = CalculateUtility(param, alt, alternativeUtilities, weights);
                    //nastavi utility vrednost v ustrezno celico
                    row.Cells[colIndex].Value = utility == -1 ? "N/A" : utility.ToString();
                    colIndex++;
                }
                //doda vrstico v DataGridView
                resultDataGridView.Rows.Add(row);
            }


            this.Controls.Add(resultDataGridView);

           
            logListView.Columns.Add("Log", -2, System.Windows.Forms.HorizontalAlignment.Left);
            this.Controls.Add(logListView);

            //calculate and update the utility for the root node
            UpdateRootUtilities(resultDataGridView, alternativeUtilities, weights);

        
            TreeView resultTreeView = new TreeView
            {
                Left = 620,
                Top = 10,
                Width = 560,
                Height = 250
            };
            CloneTreeNodes(treeViewParameters.Nodes, resultTreeView.Nodes);
            this.Controls.Add(resultTreeView);


            plotView = new PlotView
            {
                Left = 377,
                Top = 337,
                Width = this.ClientSize.Width - 377 - 20, 
                Height = 440 
            };
            plotView.Model = CreatePlotModel(resultDataGridView);
            this.Controls.Add(plotView);

        }

     
        private void SaveReport(string reportHtml)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, reportHtml);
                    MessageBox.Show("Report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private string SaveGraphAsImage(PlotModel plotModel)
        {
            var filePath = Path.Combine(Path.GetTempPath(), "graph.png");

            using (var stream = File.Create(filePath))
            {
                var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
                pngExporter.Export(plotModel, stream);
            }

            return filePath;
        }

        private string GenerateReport(DataGridView resultDataGridView, PlotModel plotModel)
        {
            var sb = new StringBuilder();
            sb.Append("<html><head>");
            sb.Append("<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css'>");
            sb.Append("</head><body class='container'>");

            sb.Append("<h1 class='my-4'>Results Report</h1>");

            //Add dgv data
            sb.Append("<h2 class='my-4'>Data Grid</h2>");
            sb.Append("<table class='table table-bordered'>");
            sb.Append("<thead class='thead-light'><tr>");
            foreach (DataGridViewColumn column in resultDataGridView.Columns)
            {
                sb.AppendFormat("<th>{0}</th>", column.HeaderText);
            }
            sb.Append("</tr></thead><tbody>");
            foreach (DataGridViewRow row in resultDataGridView.Rows)
            {
                sb.Append("<tr>");
                foreach (DataGridViewCell cell in row.Cells)
                {
                    sb.AppendFormat("<td>{0}</td>", cell.Value?.ToString() ?? string.Empty);
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");

            //add tv data
            sb.Append("<h2 class='my-4'>Parameters</h2>");
            sb.Append(GetTreeViewHtml(treeViewParameters.Nodes));

            //add Log data
            sb.Append("<h2 class='my-4'>Log</h2>");
            sb.Append("<ul class='list-group'>");
            foreach (ListViewItem item in logListView.Items)
            {
                sb.AppendFormat("<li class='list-group-item'>{0}</li>", item.Text);
            }
            sb.Append("</ul>");

            //add Graph
            sb.Append("<h2 class='my-4'>Graph</h2>");
            string graphPath = SaveGraphAsImage(plotModel);
            sb.AppendFormat("<img src='file:///{0}' class='img-fluid' alt='Graph'>", graphPath);

            sb.Append("</body></html>");
            return sb.ToString();
        }

        private string GetTreeViewHtml(TreeNodeCollection nodes)
        {
            var sb = new StringBuilder();
            sb.Append("<ul class='list-group'>");
            foreach (TreeNode node in nodes)
            {
                sb.AppendFormat("<li class='list-group-item'>{0}</li>", node.Text);
                if (node.Nodes.Count > 0)
                {
                    sb.Append(GetTreeViewHtml(node.Nodes));
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }




        private PlotModel CreatePlotModel(DataGridView resultDataGridView)
        {
            //ustvari nov plot model z naslovom
            var plotModel = new PlotModel { Title = "Winning Alternatives" };
            //ustvari kategorno os na levi strani
            var categoryAxis = new OxyPlot.Axes.CategoryAxis { Position = OxyPlot.Axes.AxisPosition.Left };
            //ustvari linearno os na spodnji strani z mejami od 0 do 1
            var valueAxis = new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Minimum = 0, Maximum = 1 };
            //ustvari bar serijo z nastavitvami za oznake
            var barSeries = new BarSeries { LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0:.00}" };

            //inicializira rootRow
            DataGridViewRow rootRow = null;
            //iterira skozi vrstice v resultDataGridView
            foreach (DataGridViewRow row in resultDataGridView.Rows)
            {
                //preveri prvo celico za "Root"
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "Root")
                {
                    //nastavi rootRow
                    rootRow = row;
                    break;
                }
            }

            //preveri ali je rootRow null
            if (rootRow != null)
            {
                //začetni indeks za stolpce
                int colIndex = 2;
                //iterira skozi stolpce v resultDataGridView
                foreach (DataGridViewColumn column in resultDataGridView.Columns)
                {
                    //preveri pogoje za obdelavo stolpcev
                    if (colIndex < rootRow.Cells.Count && column.HeaderText != "Parameter" && column.HeaderText != "Weight")
                    {
                        //doda oznako kategorije in vrednost bar serije
                        categoryAxis.Labels.Add(column.HeaderText);
                        barSeries.Items.Add(new BarItem { Value = double.Parse(rootRow.Cells[colIndex].Value.ToString()) });
                        colIndex++;
                    }
                }
            }

            //doda osi in bar serijo v plot model
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);
            plotModel.Series.Add(barSeries);

            //vrne plot model
            return plotModel;
        }





        private void UpdateRootUtilities(DataGridView resultDataGridView, Dictionary<string, Dictionary<string, double>> alternativeUtilities, Dictionary<string, double> weights)
        {
            //inicializira rootRow
            DataGridViewRow rootRow = null;

            //iterira skozi vrstice v resultDataGridView
            foreach (DataGridViewRow row in resultDataGridView.Rows)
            {
                //preveri prvo celico za "Root"
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "Root")
                {
                    //nastavi rootRow
                    rootRow = row;
                    break;
                }
            }

            //preveri ali je rootRow null
            if (rootRow == null) return;

            //nastavi težo korenskega vozla na 1.0
            rootRow.Cells[1].Value = 1.0;

            //inicializira debugInfo
            StringBuilder debugInfo = new StringBuilder();
            debugInfo.AppendLine("Root Node Children:");

            //začetni indeks za stolpce
            int rootColIndex = 2;

            //iterira skozi alternative
            foreach (var alt in alternativeUtilities.Keys)
            {
                //izračuna rootUtility
                double rootUtility = CalculateRootUtility(resultDataGridView, alternativeUtilities, weights, alt, debugInfo);

                //nastavi utility vrednost
                rootRow.Cells[rootColIndex].Value = rootUtility == -1 ? "N/A" : rootUtility.ToString();
                rootColIndex++;
            }

            //počisti logListView in doda informacije
            logListView.Items.Clear();
            foreach (var line in debugInfo.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                logListView.Items.Add(new ListViewItem(line));
            }
        }



        private double CalculateRootUtility(DataGridView resultDataGridView, Dictionary<string, Dictionary<string, double>> alternativeUtilities, Dictionary<string, double> weights, string alt, StringBuilder debugInfo)
        {
            //pridobi korenski vozel (prvi vozel v treeViewParameters)
            TreeNode rootNode = treeViewParameters.Nodes[0]; //root is 1st
                                                             //inicializiraj začetno vrednost utility
            double utility = 0.0;

            //dodaj informacije o korenskem vozlu v debugInfo
            debugInfo.AppendLine($"Root Node: {rootNode.Text}");

            //iterira skozi vse podvozle korenskega vozla
            foreach (TreeNode childNode in rootNode.Nodes)
            {
                //pridobi ime parametra iz besedila vozla (prvi del besedila)
                string childParam = childNode.Text.Split(' ')[0];

                //pridobi utility vrednost za trenutni parameter in alternativo iz resultDataGridView
                double childUtility = GetUtilityFromDataGridView(resultDataGridView, childParam, alt);
                //pridobi težo za trenutni parameter iz resultDataGridView
                double childWeight = GetWeightFromDataGridView(resultDataGridView, childParam);

                //preveri, če sta utility vrednost in teža veljavni (nista -1)
                if (childUtility != -1 && childWeight != -1)
                {
                    //izračunaj skupno utility vrednost za trenutni parameter in jo dodaj k skupni utility vrednosti
                    utility += childUtility * childWeight;

                    //dodaj informacije o trenutnem parametru, utility vrednosti in teži v debugInfo
                    debugInfo.AppendLine($"Child: {childParam}, Utility: {childUtility}, Weight: {childWeight}");
                }
                else
                {
                    //dodaj informacije o manjkajoči utility vrednosti ali teži za trenutni parameter v debugInfo
                    debugInfo.AppendLine($"Child: {childParam}, Missing Utility or Weight");
                }
            }

           
            return utility;
        }


        private double GetUtilityFromDataGridView(DataGridView dgv, string param, string alt)
        {
            //iterira skozi vse vrstice v podani DataGridView
            foreach (DataGridViewRow row in dgv.Rows)
            {
                //preveri, če prva celica vrstice ni null in vsebuje ime parametra
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == param)
                {
                    //iterira skozi vse celice v trenutni vrstici
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //preveri, če je ime stolpca celice enako imenu alternative
                        if (dgv.Columns[cell.ColumnIndex].HeaderText == alt)
                        {
                            //poskusi pretvoriti vrednost celice v double
                            if (double.TryParse(cell.Value.ToString(), out double utility))
                            {
                                //utility vrenost
                                return utility;
                            }
                        }
                    }
                }
            }
            
            return -1;
        }


        private double GetWeightFromDataGridView(DataGridView dgv, string param)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {//preveri, če prva celica vrstice ni null in vsebuje ime parametra
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == param)
                {//poskusi pretvoriti vrednost v drugi celici v double
                    if (double.TryParse(row.Cells[1].Value.ToString(), out double weight))
                    {
                        return weight;//vrne težo
                    }
                }
            }
            return -1;
        }

        private double CalculateUtility(string param, string alt, Dictionary<string, Dictionary<string, double>> alternativeUtilities, Dictionary<string, double> weights)
        {
            //if a basic parameter
            if (alternativeUtilities[alt].ContainsKey(param))
            {
                return alternativeUtilities[alt][param];
            }

            //else a composite parameter
            TreeNode node = FindNodeByParameter(treeViewParameters.Nodes, param);
            if (node == null || node.Nodes.Count == 0)
                return -1;

            double utility = 0.0;
            foreach (TreeNode childNode in node.Nodes)
            {
                string childParam = childNode.Text.Split(' ')[0];
                if (alternativeUtilities[alt].ContainsKey(childParam) && weights.ContainsKey(childParam))
                {
                    utility += alternativeUtilities[alt][childParam] * (weights[childParam] / 100.0);
                }
            }
            return utility;
        }

        private TreeNode FindNodeByParameter(TreeNodeCollection nodes, string param)
        {
            //iterira skozi vozle v nodes
            foreach (TreeNode node in nodes)
            {
                //preveri, če vozel začne z iskanim parametrom
                if (node.Text.StartsWith(param + " "))
                    return node; //vrne vozel, če je najden

                //preveri, če ima vozel podvozle
                if (node.Nodes.Count > 0)
                {
                    //rekurzivno išče vozel v podvozlih
                    TreeNode foundNode = FindNodeByParameter(node.Nodes, param);
                    if (foundNode != null)
                        return foundNode; //vrne vozel, če je najden v podvozlih
                }
            }
            //vrne null, če vozel ni najden
            return null;
        }



        private Dictionary<string, double> GetWeights(TreeNodeCollection nodes)
        {
            Dictionary<string, double> weights = new Dictionary<string, double>();//slovar za shranjevanje tež parametrov
            //iterira skozi vse vozle v podani kolekciji vozlov
            foreach (TreeNode node in nodes)
            {
                string parameterName = node.Text.Split(' ')[0]; //pridobi ime parametra iz besedila vozla (prvi del besedila)
                double weight = 1.0;//privzeta teža parametra je 1.0
                 //preveri, ali besedilo vozla vsebuje težo (med oklepaji)
                if (node.Text.Contains('('))
                { //pridobi težo kot niz (med oklepaji) in jo pretvori v double
                    string weightStr = node.Text.Split('(')[1].Split(')')[0];
                    weight = double.Parse(weightStr);
                }

                weights[parameterName] = weight; //ime parametra in njegovo težo v slovar

                if (node.Nodes.Count > 0) //preveri, ali ima vozel podvozle
                { //rekurzivno pridobi teže podvozlov
                    Dictionary<string, double> childWeights = GetWeights(node.Nodes);//dodaj teže podvozlov v glavni slovar
                    foreach (var kvp in childWeights)
                    {
                        weights[kvp.Key] = kvp.Value;
                    }
                }
            }

            return weights; //slovar tež parametrov
        }

        private List<string> GetAllParameters(TreeNodeCollection nodes)
        {
            List<string> parameters = new List<string>();
            foreach (TreeNode node in nodes)
            {
                if (node.Nodes.Count == 0)
                {
                    //Basic parameter
                    parameters.Add(node.Text.Split(' ')[0]); //extract parameter name without weight or utility info
                }
                else
                {
                    //Composite parameter
                    parameters.Add(node.Text.Split(' ')[0]);
                    parameters.AddRange(GetAllParameters(node.Nodes));
                }
            }
            return parameters;
        }

        private void CloneTreeNodes(TreeNodeCollection sourceNodes, TreeNodeCollection targetNodes)
        {
            foreach (TreeNode node in sourceNodes)
            {
                TreeNode newNode = new TreeNode(node.Text)
                {//kopira oznako iz izvornega vozla v nov vozel
                    Tag = node.Tag
                };
                targetNodes.Add(newNode);
                CloneTreeNodes(node.Nodes, newNode.Nodes);//rekurzivno kliče metodo za kopiranje podvozlov iz izvornega v novega vozla
            }
        }

        private void generateReportButton_Click_1(object sender, EventArgs e)
        {
            var reportHtml = GenerateReport(resultDataGridView, plotView.Model);
            SaveReport(reportHtml);
        }
    }
}
