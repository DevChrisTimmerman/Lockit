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


## 📸 Screenshots

Lockit Dashboard <br/>
<img width="900" height="361" alt="Lockit-dashboard" src="https://github.com/user-attachments/assets/c24716c1-b2bc-46d2-bf6c-80266b32a319" />
<br/><br/>CSV import with header detections<br/>
<img width="900" height="423" alt="Lockit-import" src="https://github.com/user-attachments/assets/2e005414-3317-4269-b99a-88efb8ae5f94" />
<br/><br/>Locker overview<br/>
<img width="900" height="423" alt="Lockit-StudentList" src="https://github.com/user-attachments/assets/99e7c9cd-9c93-404e-9dcf-eb7068b95463" />
<br/><br/>Location overview<br/>
<img width="900" height="490" alt="Lockit-LocationList" src="https://github.com/user-attachments/assets/776241f3-663c-4796-94c7-3e97310d2dd3" />

## 👩‍💼 Use Case

Originally built to help a high school administrators manage locker assignments with ease, reduce manual work with spreadsheets, and simplify end-of-year key collection.

## 🧭 Roadmap

- [x] Base admin panel
- [x] CSV Import
- [x] Report export (Excel/PDF)
- [ ] Admin login (linked with SmartSchool API)
- [ ] Locker reassignment
- [ ] QR Code integration + scanning
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
