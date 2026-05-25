using Loding_Report.Models;
using Loding_Report.Services;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Cryptography;
using System.Windows;

namespace Loding_Report.Views
{
    public partial class ListBillReport : MetroWindow
    {
        private readonly IReportDataService _dataService;

        // Independent data collections bound to the item templates of your dropdowns
        public ObservableCollection<PropertyItem> PropertiesList { get; set; } = new ObservableCollection<PropertyItem>();
        public ObservableCollection<OutletItem> OutletsList { get; set; } = new ObservableCollection<OutletItem>();

        // Parameterless constructor for the Visual Studio XAML Designer
        public ListBillReport()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        // Main Constructor executed at runtime by Dependency Injection
        public ListBillReport(IReportDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            this.DataContext = this;

            LoadFilterData();
        }

        private void LoadFilterData()
        {
            try
            {
                PropertiesList.Clear();
                var properties = _dataService.GetProperties();
                foreach (var prop in properties)
                {
                    PropertiesList.Add(prop);
                }

                OutletsList.Clear();
                var outlets = _dataService.GetOutlets();
                foreach (var outlet in outlets)
                {
                    OutletsList.Add(outlet);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial drop down filters: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            // Clear out dropdown selections back to index zero 
            cmbProperty.SelectedIndex = 0;
            cmbOutlets.SelectedIndex = 0;

            // Reset dates to default historical boundaries
            dpStartDate.SelectedDate = DateTime.Today.AddDays(-30);
            dpEndDate.SelectedDate = DateTime.Today;

            // Wipe layout grid items completely
            dgReport.ItemsSource = null;
        }
        private async void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            Loader.Visibility = Visibility.Visible;

            try
            {
                // Direct UI Parsing: Extract data straight out of the active control element selection states
                long propId = (cmbProperty.SelectedItem as PropertyItem)?.PropertyId ?? -1;
                long outletCd = (cmbOutlets.SelectedItem as OutletItem)?.OutletCode ?? -1L;
                DateTime fromDate = dpStartDate.SelectedDate ?? DateTime.Today.AddDays(-30);
                DateTime toDate = dpEndDate.SelectedDate ?? DateTime.Today;

                // Wrap parameters cleanly into the expected single request object block
                var reportRequest = new RevenueReportRequest
                {
                    PropertyId = propId,
                    OutletCode = outletCd,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                // Forward request across service data pipeline
                var result = await _dataService.GetRevenueReportAsync(reportRequest);

                dgReport.ItemsSource = null;

                if (result != null && result.Status == 1 && !string.IsNullOrEmpty(result.RevenueCollectionDetailsList) && result.RevenueCollectionDetailsList != "No Data")
                {
                    DataTable reportTable = JsonConvert.DeserializeObject<DataTable>(result.RevenueCollectionDetailsList);
                    dgReport.ItemsSource = reportTable?.DefaultView;
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "No records found matching criteria.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error running statement database query: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Loader.Visibility = Visibility.Collapsed;
            }
        }
    }
}