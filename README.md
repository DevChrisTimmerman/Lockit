# 🔐 Lockit

**Lockit** is a school locker management web app built with Blazor Server. It simplifies the entire locker administration process—from initial distribution to end-of-year (EOY) key retrieval—replacing manual spreadsheets and paperwork with a smart, clean interface.

---

## 🛠️ Tech Stack

- **Frontend & Backend**: Blazor Server (.NET 9)
- **Database**: Entity Framework Core + SQL Server
- **Language**: C#
- **Hosting**: Local server or Azure App Service

---

## ✨ Features

### 👩‍🏫 Admin Panel
- Track key return status
- Auto or manual locker assignment
- Real-time graphs & status: available, occupied, maintenance
- Visual dashboard with student-locker mapping
- Edit locker details and reassign if needed
- Export locker status reports (PDF/Excel)

### 📥 Import & Export
- Bulk student upload via CSV
- Export reports (Excel/PDF)
- Generate locker assignment summaries

### 🔍 Lookup Tool
- Search by locker number, student name, or class
- Mobile-friendly for hallway lookups (planned)

### 📦 End-of-Year Returns
- Email reminders for returns (planned)
- Track locker key returns
- Mark late or missing returns
- Summary report generation

---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- SQLite or SQL Server (default is SQL Server for local dev)

### Run Locally

```bash
git clone https://github.com/yourusername/Lockit.git
cd Lockit
dotnet run --project Lockit.Server
```


## 📸 Screenshots (coming soon)

## 👩‍💼 Use Case

Originally built to help a high school administrators manage locker assignments with ease, reduce manual work with spreadsheets, and simplify end-of-year key collection.

## 🧭 Roadmap

- [x] Base admin panel
- [ ] Admin login (linked with SmartSchool API)
- [ ] CSV Import
- [ ] Locker reassignment
- [ ] QR Code integration + scanning
- [ ] Report export (Excel/PDF)
- [ ] Interactive floor/locker map
- [ ] Email reminders for returns
- [ ] .Net Maui frontend for mobile/tablet use

## 📛 Branding

**Name:** Lockit
**Style:** Clean, modern, mobile-aware
**Domain ideas:** lockit.app, getlockit.com, lockitadmin.com

## 🤝 License

**MIT**

## 🧑‍💻 Author

**Chris Timmerman**
[LinkedIn](https://www.linkedin.com/in/chris-timmerman/) | [Email](mailto:Chris.Timmerman@proximus.be)
