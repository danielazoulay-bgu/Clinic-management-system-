# Clinic Management System

Information Systems Analysis & Design course project, Ben-Gurion University, 2026. Team project, C# (WinForms) and SQL Server.

A desktop management system for a beauty clinic, from requirements analysis through database design to a working application.

## Features
- Appointment scheduling with statuses (scheduled, reminded, in treatment, completed, cancelled)
- Customer cards and treatment history
- Staff management and staff schedule
- Supplier and product inventory management
- Income and expense tracking
- Document management (health declarations, receipts, invoices) linked to customers

## Structure
- `database.sql`: full schema (tables, keys, lookup tables) with sample data. All sample data is fictional.
- `ByNoa/`: the C# WinForms application (Visual Studio project).

## How to run
1. Run `database.sql` on a SQL Server instance.
2. Update the connection string in `ByNoa/App.config` and `ByNoa/SQL_CON.cs`.
3. Open `ByNoa/ByNoa.csproj` in Visual Studio and run.
