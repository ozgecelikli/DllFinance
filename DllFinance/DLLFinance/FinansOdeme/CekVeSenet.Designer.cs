using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;

namespace DllFinance.DLLFinance.FinansOdeme
{
    partial class CekVeSenet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.gridControlGrouped = new DevExpress.XtraGrid.GridControl();
            this.gridViewGrouped = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlAll = new DevExpress.XtraGrid.GridControl();
            this.gridViewAll = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGrouped)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGrouped)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAll)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControlTitle
            // 
            this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControlTitle.Appearance.Options.UseFont = true;
            this.labelControlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlTitle.Location = new System.Drawing.Point(0, 0);
            this.labelControlTitle.Margin = new System.Windows.Forms.Padding(4);
            this.labelControlTitle.Name = "labelControlTitle";
            this.labelControlTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.labelControlTitle.Size = new System.Drawing.Size(199, 30);
            this.labelControlTitle.TabIndex = 0;
            this.labelControlTitle.Text = "Çek ve Senetler";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.gridControlGrouped, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.gridControlAll, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1067, 685);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // gridControlGrouped
            // 
            this.gridControlGrouped.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlGrouped.Location = new System.Drawing.Point(4, 4);
            this.gridControlGrouped.MainView = this.gridViewGrouped;
            this.gridControlGrouped.Margin = new System.Windows.Forms.Padding(4);
            this.gridControlGrouped.Name = "gridControlGrouped";
            this.gridControlGrouped.Size = new System.Drawing.Size(1059, 303);
            this.gridControlGrouped.TabIndex = 0;
            this.gridControlGrouped.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGrouped});
            // 
            // gridViewGrouped
            // 
            this.gridViewGrouped.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridViewGrouped.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewGrouped.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridViewGrouped.Appearance.Row.Options.UseFont = true;
            this.gridViewGrouped.GridControl = this.gridControlGrouped;
            this.gridViewGrouped.Name = "gridViewGrouped";
            this.gridViewGrouped.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewGrouped.OptionsView.ShowAutoFilterRow = true;
            this.gridViewGrouped.OptionsView.ShowFooter = true;
            this.gridViewGrouped.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlAll
            // 
            this.gridControlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlAll.Location = new System.Drawing.Point(4, 315);
            this.gridControlAll.MainView = this.gridViewAll;
            this.gridControlAll.Margin = new System.Windows.Forms.Padding(4);
            this.gridControlAll.Name = "gridControlAll";
            this.gridControlAll.Size = new System.Drawing.Size(1059, 366);
            this.gridControlAll.TabIndex = 1;
            this.gridControlAll.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAll});
            // 
            // gridViewAll
            // 
            this.gridViewAll.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridViewAll.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewAll.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridViewAll.Appearance.Row.Options.UseFont = true;
            this.gridViewAll.GridControl = this.gridControlAll;
            this.gridViewAll.Name = "gridViewAll";
            this.gridViewAll.OptionsView.ShowAutoFilterRow = true;
            this.gridViewAll.OptionsView.ShowFooter = true;
            this.gridViewAll.OptionsView.ShowGroupPanel = false;
            // 
            // CekVeSenet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.labelControlTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CekVeSenet";
            this.Size = new System.Drawing.Size(1067, 715);
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGrouped)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGrouped)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControlTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private DevExpress.XtraGrid.GridControl gridControlGrouped;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGrouped;
        private DevExpress.XtraGrid.GridControl gridControlAll;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAll;
    }
}
