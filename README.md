# Loding Report Application 📊

A Windows desktop reporting workbench built using **WPF (.NET 9.0)**, styled with **MahApps.Metro** and **Material Design**, using a clean layered architecture with Dependency Injection.

---

## 🏗️ Folder Structure

```text
Loding_Report/
│
├── App.config                      # Application Connection Strings Database settings
├── App.xaml / App.xaml.cs          # Composition Root & DI Service Registration
│
├── Database/
│   └── SqlDatabaseUtility.cs       # Stored Procedure processor & ADO.NET wrapper
│
├── Services/
│   └── ReportDataService.cs        # Maps requests & processes raw JSON datasets
│
├── Models/
│   ├── RevenueReportRequest.cs     # Request parameters context payload
│   └── RevenueCollectionSummaryResp.cs
│
└── Views/
    ├── ListBillReport.xaml         # UI Layout elements with specific x:Names
    └── ListBillReport.xaml.cs      # Button event handler & control data reading
