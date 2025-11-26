using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DllFinance.SQL;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DllFinance.DLLFinance.FinansOdeme
{
    public partial class CekVeSenet : UserControl
    {
        private const string ViewName = "CF_ceksenetView";

        public CekVeSenet()
        {
            InitializeComponent();
            LoadCekSenetData();
        }

        private void LoadCekSenetData()
        {
            try
            {
                DataTable cekSenetData = SqlHelper.Instance.GetDataTable(ViewName);

                if (cekSenetData == null || cekSenetData.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "CF_ceksenetView verisi bulunamadı. Lütfen veritabanı bağlantısını kontrol edin.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                gridControlAll.DataSource = cekSenetData;
                ConfigureDetailGrid(gridViewAll);

                DataColumn dueDateColumn = GetDueDateColumn(cekSenetData);
                DataTable groupedTable = cekSenetData.Copy();
                AddYearAndMonthColumns(groupedTable, dueDateColumn);

                gridControlGrouped.DataSource = groupedTable;
                ConfigureGroupedGrid(gridViewGrouped, dueDateColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static DataColumn GetDueDateColumn(DataTable table)
        {
            return table?.Columns
                .Cast<DataColumn>()
                .FirstOrDefault(c => c.DataType == typeof(DateTime) && c.ColumnName.ToLower().Contains("vade"));
        }

        private static void AddYearAndMonthColumns(DataTable table, DataColumn dueDateColumn)
        {
            if (table == null)
                return;

            if (!table.Columns.Contains("YIL"))
            {
                table.Columns.Add("YIL", typeof(int));
            }

            if (!table.Columns.Contains("AY"))
            {
                table.Columns.Add("AY", typeof(int));
            }

            if (dueDateColumn == null)
                return;

            foreach (DataRow row in table.Rows)
            {
                if (row[dueDateColumn] == DBNull.Value)
                    continue;

                DateTime dueDate = Convert.ToDateTime(row[dueDateColumn]);
                row["YIL"] = dueDate.Year;
                row["AY"] = dueDate.Month;
            }
        }

        private static void ConfigureDetailGrid(GridView view)
        {
            view.PopulateColumns();
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowAutoFilterRow = true;
            view.OptionsView.ShowFooter = true;

            foreach (GridColumn column in view.Columns)
            {
                if (column.ColumnType == typeof(DateTime))
                {
                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    column.DisplayFormat.FormatString = "dd.MM.yyyy";
                }
                else if (column.ColumnType == typeof(decimal)
                    || column.ColumnType == typeof(double)
                    || column.ColumnType == typeof(float))
                {
                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    column.DisplayFormat.FormatString = "n2";
                }
            }

            GridColumn totalColumn = view.Columns
                .Cast<GridColumn>()
                .FirstOrDefault(c => c.FieldName.Equals("cstutar_tl", StringComparison.OrdinalIgnoreCase));

            if (totalColumn != null)
            {
                totalColumn.Summary.Clear();
                totalColumn.Summary.Add(new GridColumnSummaryItem(SummaryItemType.Sum, totalColumn.FieldName, "{0:n2}"));
            }
        }

        private static void ConfigureGroupedGrid(GridView view, DataColumn dueDateColumn)
        {
            view.PopulateColumns();
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowAutoFilterRow = true;
            view.OptionsBehavior.AutoExpandAllGroups = true;
            view.OptionsView.ShowFooter = true;

            GridColumn yearColumn = view.Columns["YIL"];
            GridColumn monthColumn = view.Columns["AY"];

            if (yearColumn != null)
            {
                yearColumn.Caption = "Yıl";
                yearColumn.GroupIndex = 0;
                yearColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            }

            if (monthColumn != null)
            {
                monthColumn.Caption = "Ay";
                monthColumn.GroupIndex = 1;
                monthColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            }

            if (dueDateColumn != null)
            {
                GridColumn dueColumn = view.Columns[dueDateColumn.ColumnName];
                if (dueColumn != null)
                {
                    dueColumn.Caption = "Vade Tarihi";
                    dueColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dueColumn.DisplayFormat.FormatString = "dd.MM.yyyy";
                }
            }

            GridColumn totalColumn = view.Columns
                .Cast<GridColumn>()
                .FirstOrDefault(c => c.FieldName.Equals("cstutar_tl", StringComparison.OrdinalIgnoreCase));

            if (totalColumn != null)
            {
                totalColumn.Caption = "Tutar (TL)";
                totalColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                totalColumn.DisplayFormat.FormatString = "n2";

                view.GroupSummary.Clear();
                view.GroupSummary.Add(new GridGroupSummaryItem(
                    SummaryItemType.Sum,
                    totalColumn.FieldName,
                    totalColumn,
                    "{0:n2}"));
                totalColumn.Summary.Clear();
                totalColumn.Summary.Add(new GridColumnSummaryItem(SummaryItemType.Sum, totalColumn.FieldName, "{0:n2}"));
            }
        }
    }
}
