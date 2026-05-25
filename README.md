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


---

## ⚡ Core Business Logic Flow

1. **Initialization:** On startup, `App.xaml.cs` boots up the application context, registers the infrastructure items to the global service container, and instantiates the main workspace window.
2. **Filter Hydration:** The view layer natively requests list items from the `IReportDataService` to populate the `PropertiesList` and `OutletsList` observables.
3. **Report Execution:** Upon hitting the "Generate Report" action control, the window parses selection inputs straight from the named components (`cmbProperty`, `cmbOutlets`, `dpStartDate`, `dpEndDate`) safely handles numeric conversions (`long`), bundles them into a `RevenueReportRequest` object, and dispatches it over an asynchronous non-blocking UI thread.
4. **Data Generation:** The underlying data layer executes the stored procedure `FXSPA_RevenueCollectionSummaryReport` inside a background worker pool, transforms rows into an encrypted or formatted raw JSON string array, and serializes the array back up to the UI grid for layout rendering.

---

## ⚙️ App.config Layout Specifications

Since this application utilizes native operating system XML configuration mechanisms, the database connection block expects **escaped character tokens**. Raw tokens such as `&` will break the XML schema parser and cause a `ConfigurationErrorsException`. Ensure configuration matches the template below:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <connectionStrings>
        <add name="DefaultConnection" 
             connectionString="Server=YOUR_SERVER_ADDRESS;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;TrustServerCertificate=True;" 
             providerName="Microsoft.Data.SqlClient" />
    </connectionStrings>

    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>
</configuration>



Here is a preview of the clean, enterprise-grade `README.md` file that has been generated for your project repository. It comprehensively covers your architecture, the exact directory layout we settled on, your configuration setup rules, code snippets, and standard troubleshooting strategies for the XML subsystem.

***

```markdown
# Loding Report Application 📊

A robust, enterprise-ready Windows desktop reporting application built using **WPF (.NET 9.0)**, **MahApps.Metro**, and **MaterialDesignInXAML**. This application uses a clean, layered architecture featuring **Dependency Injection (DI)**, decoupled database utilities, and modern asynchronous data processing to pull dynamic reporting streams from a SQL Server back-end.

---

## 🛠️ Technology Stack & Ecosystem

- **UI Framework:** WPF (.NET 9.0 Windows Desktop SDK)
- **Styling UI Components:** [MahApps.Metro](https://mahapps.com/) & [MaterialDesignInXAML](http://materialdesigninxaml.net/)
- **Data Access:** `Microsoft.Data.SqlClient` (Asynchronous executing pipeline)
- **Serialization:** `Newtonsoft.Json` (Dynamic schema-less `DataTable` mapping)
- **DI Engine:** `Microsoft.Extensions.DependencyInjection`

---

## 🏗️ Architectural Topology

The application enforces strict structural separation of concerns to remain loosely coupled, easily testable, and highly maintainable:
