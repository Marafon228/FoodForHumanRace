using FoodForHumanRaceManagerDesktop.Entity;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace FoodForHumanRaceManagerDesktop.Pages.View
{
    /// <summary>
    /// Interaction logic for ViewReporting.xaml
    /// </summary>
    public partial class ViewReporting : Page
    {
        public ViewReporting()
        {


            InitializeComponent();
            ChartOrders.ChartAreas.Add(new ChartArea("Main"));

            ComboTypeChars.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
            ListUser.ItemsSource = ADO.Instance.User.ToList().Where(u=> u.Role.Name == "Клиент");
            var currentSeries = new Series("Order")
            {
                IsValueShownAsLabel = true
            };
            ChartOrders.Series.Add(currentSeries);
            
            
        }

        private void Update_Chart(object sender, SelectionChangedEventArgs e)
        {
            if (ListUser.SelectedItem is User currentUser && ComboTypeChars.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartOrders.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                /*var productsList = ADO.Instance.Product.ToList();
                foreach (var product in productsList)
                {
                        currentSeries.Points.AddXY(product.Name, ADO.Instance.Order
                            .ToList()
                            .Where(o => o.User == currentUser && o.OrderAndProduct.FirstOrDefault().Product == product)
                            .Sum(o => o.OrderAndProduct.FirstOrDefault().Product.Price * o.OrderAndProduct.Count()));
                }*/

                var statusList = ADO.Instance.Status.ToList();
                foreach (var status in statusList)
                {
                    currentSeries.Points.AddXY(status.Name, ADO.Instance.Order.ToList().Where(o => o.User == currentUser
                    && o.Status == status).Sum(o=> o.OverPrice * o.Count));
                }
            
            }

            


        }

        private void BtnExportExel_Click(object sender, RoutedEventArgs e)
        {
            var alluser = ADO.Instance.User.Where(u=> u.Role.Name =="Клиент").ToList().OrderBy(u => u.Login).ToList();

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = alluser.Count();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);


            for (int i = 0; i < alluser.Count(); i++)
            {
                int startRowIndex = 1;

                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name = alluser[i].Login;

                worksheet.Cells[1][startRowIndex] = "Дата заказа";
                worksheet.Cells[2][startRowIndex] = "Название";
                worksheet.Cells[3][startRowIndex] = "Стоимость";
                worksheet.Cells[4][startRowIndex] = "Количество";
                worksheet.Cells[5][startRowIndex] = "Сумма";

                startRowIndex++;

                var usersStatus = alluser[i].Order.OrderBy(o => o.Date).GroupBy(o => o.Status).OrderBy(o => o.Key.Name);

                foreach (var groupStatus in usersStatus)
                {
                    Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[5][startRowIndex]];
                    headerRange.Merge();
                    headerRange.Value = groupStatus.Key.Name;
                    headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerRange.Font.Italic = true;

                    startRowIndex++;

                    foreach (var order in groupStatus)
                    {
                        worksheet.Cells[1][startRowIndex] = order.Date.ToString("dd.MM.yyyy HH:mm");
                        worksheet.Cells[2][startRowIndex] = order.Name;
                        worksheet.Cells[3][startRowIndex] = order.OverPrice;
                        worksheet.Cells[4][startRowIndex] = order.Count;

                        worksheet.Cells[5][startRowIndex].Formula = $"=C{startRowIndex}*D{startRowIndex}";

                        /*worksheet.Cells[3][startRowIndex].NumberFormat =
                            worksheet.Cells[3][startRowIndex].NumberFormat = "#,###.00";*/

                        startRowIndex++;
                    }

                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "ИТОГО:";
                    sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    worksheet.Cells[5][startRowIndex].Formula = $"=SUM(E{startRowIndex - groupStatus.Count()}:" +
                        $"E{startRowIndex - 1})";
                    
                    sumRange.Font.Bold = worksheet.Cells[5][startRowIndex].Font.Bold = true;
                    //worksheet.Cells[5][startRowIndex].NumberFormat = "#,###.00";

                    startRowIndex++;

                    Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex - 1]];
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                    worksheet.Columns.AutoFit();
                }
            }
            application.Visible = true;

        }

        private void BtnExportWord_Click(object sender, RoutedEventArgs e)
        {
            var allUsers = ADO.Instance.User.Where(u => u.Role.Name == "Клиент").ToList();
            var allStatus = ADO.Instance.Status.ToList();

            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            foreach (var user in allUsers)
            {
                Word.Paragraph userParagraph = document.Paragraphs.Add();
                Word.Range userRange = userParagraph.Range;
                userRange.Text = user.Login;
                //userParagraph.set_Style("Title");
                userRange.InsertParagraphAfter();
                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table paymentsTable = document.Tables.Add(tableRange, allStatus.Count() + 1, 3);
                paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                = Word.WdLineStyle.wdLineStyleSingle;
                paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                /*cellRange = paymentsTable.Cell(1, 1).Range;
                cellRange.Text = "Иконка";*/
                cellRange = paymentsTable.Cell(1, 1).Range;
                cellRange.Text = "Статус";
                cellRange = paymentsTable.Cell(1, 2).Range;
                cellRange.Text = "Сумма расходов";


                paymentsTable.Rows[1].Range.Bold = 1;
                paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < allStatus.Count(); i++)
                {
                    var currentStatus = allStatus[i];

                    /*cellRange = paymentsTable.Cell(i + 2, 1).Range;
                    Word.InlineShape imageShape = cellRange.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory +
                        "..\\..\\" + currentStatus.Name);
                    imageShape.Width = imageShape.Height = 40;*/
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = paymentsTable.Cell(i + 2, 1).Range;
                    cellRange.Text = currentStatus.Name;
                    cellRange = paymentsTable.Cell(i + 2, 2).Range;
                    cellRange.Text = user.Order.ToList()
                    .Where(p => p.Status == currentStatus).Sum(p => p.Count * p.OverPrice).ToString("N2") + "руб.";

                }

                Order maxOrder = user.Order.OrderByDescending(p => p.OverPrice * p.Count).FirstOrDefault();
                if (maxOrder != null)
                {


                    Word.Paragraph maxPaymentParagraph = document.Paragraphs.Add();
                    Word.Range maxPaymentRange = maxPaymentParagraph.Range;
                    maxPaymentRange.Text = $"Самый дорогостоящий заказ - {maxOrder.Name} за {(maxOrder.OverPrice * maxOrder.Count).ToString("N2")} " +
                        $"руб. от {maxOrder.Date.ToString("dd.MM.yyyу HH:mm")}";
                    //maxPaymentParagraph.set_Style("Intense Quote");
                    maxPaymentRange.Font.Color = Word.WdColor.wdColorDarkRed;
                    maxPaymentRange.InsertParagraphAfter();
                }
                Order minOrder = user.Order
                    .OrderBy(p => p.OverPrice * p.Count).FirstOrDefault();

                if (minOrder != null)
                {
                    Word.Paragraph minPaymentParagraph = document.Paragraphs.Add();
                    Word.Range minPaymentRange = minPaymentParagraph.Range;                    
                    minPaymentRange.Text = $"Самый дешевый заказ - {minOrder.Name} за {(minOrder.OverPrice * minOrder.Count).ToString("N2")} " +
                        $"руб. от {minOrder.Date.ToString("dd.MM.yyyy HH:mm")}";
                    //minPaymentParagraph.set_Style("Intense Quote");
                    minPaymentRange.Font.Color = Word.WdColor.wdColorDarkGreen;
                }

                if (user != allUsers.LastOrDefault())
                {
                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                }
            }

            application.Visible = true;

            //document.SaveAs2(@"E:\WSR\Test1.dacx");
            //document.SaveAs2(@"E:\WSR\Test1.pdf", Word.WdExportFormat.wdExportFormatPDF);

        }
    }
}
