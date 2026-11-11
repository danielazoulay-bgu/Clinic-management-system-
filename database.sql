---------------------------------------------------------------------------
-- 1. איפוס המבנה (בזהירות!)
----------------------------------------------------------------------------
DROP TABLE IF EXISTS dbo.FilePDF;
DROP TABLE IF EXISTS dbo.Product;
DROP TABLE IF EXISTS dbo.FinancialTransaction;
DROP TABLE IF EXISTS dbo.Appointment;
DROP TABLE IF EXISTS dbo.Treatment;
DROP TABLE IF EXISTS dbo.Supplier;
DROP TABLE IF EXISTS dbo.Customer;
DROP TABLE IF EXISTS dbo.Worker;
DROP TABLE IF EXISTS dbo.ByNoaBranch;

DROP TABLE IF EXISTS dbo.WorkerType;
DROP TABLE IF EXISTS dbo.AppointmentStatusType;
DROP TABLE IF EXISTS dbo.TransactionType;
DROP TABLE IF EXISTS dbo.CustomerStatusType;
DROP TABLE IF EXISTS dbo.EmployeeStatusType;
DROP TABLE IF EXISTS dbo.FileType;
DROP TABLE IF EXISTS dbo.SupplierType;
GO

----------------------------------------------------------------------------
-- 2. יצירת הטבלאות (בלי IDENTITY - ה-ID מגיע מה-C#)
----------------------------------------------------------------------------
CREATE TABLE dbo.WorkerType (WorkerType VARCHAR(20) PRIMARY KEY NOT NULL);
CREATE TABLE dbo.AppointmentStatusType ([Status] VARCHAR(20) PRIMARY KEY NOT NULL);
CREATE TABLE dbo.TransactionType (TransactionType VARCHAR(20) PRIMARY KEY NOT NULL);
CREATE TABLE dbo.CustomerStatusType (CustomerStatus VARCHAR(20) PRIMARY KEY NOT NULL);
CREATE TABLE dbo.EmployeeStatusType (EmployeeStatus VARCHAR(20) PRIMARY KEY NOT NULL);
CREATE TABLE dbo.FileType (FileType VARCHAR(20) PRIMARY KEY NOT NULL);
CREATE TABLE dbo.SupplierType (SupplierType VARCHAR(50) PRIMARY KEY NOT NULL);

CREATE TABLE dbo.ByNoaBranch (
    branchID INT PRIMARY KEY NOT NULL,
    branchName VARCHAR(30) NOT NULL,
    branchPhone VARCHAR(15),
    branchEmail VARCHAR(40),
    [address] VARCHAR(50),  
    openingHour TIME(0),
    closingHour TIME(0)  
);

CREATE TABLE dbo.Worker (
    workerID INT PRIMARY KEY NOT NULL,
    workerName VARCHAR(30) NOT NULL,
    workerBirthDate DATETIME,
    startWorkDate DATETIME,
    workerPhone VARCHAR(15),
    WorkerType VARCHAR(20) NOT NULL,
    EmployeeStatus VARCHAR(20) NOT NULL,
    branchID INT NOT NULL,
    userName VARCHAR(20),
    userPassword VARCHAR(20),
    FOREIGN KEY (WorkerType) REFERENCES dbo.WorkerType(WorkerType),
    FOREIGN KEY (EmployeeStatus) REFERENCES dbo.EmployeeStatusType(EmployeeStatus),
    FOREIGN KEY (branchID) REFERENCES dbo.ByNoaBranch(branchID)
);

CREATE TABLE dbo.Customer (
    customerID INT PRIMARY KEY NOT NULL,
    customerName VARCHAR(30) NOT NULL,
    customerBirthDate DATE,
    customerPhone VARCHAR(15),
    joinDate DATE,          
    CustomerStatus VARCHAR(20) NOT NULL,
    FOREIGN KEY (CustomerStatus) REFERENCES dbo.CustomerStatusType(CustomerStatus)
);

CREATE TABLE dbo.Treatment (
    treatmentName VARCHAR(50) PRIMARY KEY NOT NULL,
    durationMinute INT,
    price MONEY
);

CREATE TABLE dbo.Appointment (
    appointmentID INT PRIMARY KEY NOT NULL,
    appointmentDate DATE,      
    startTime TIME(0),        
    actualStartTime TIME(0),    
    appointmentNotes VARCHAR(255),
    [Status] VARCHAR(20) NOT NULL,
    workerID INT NOT NULL,
    customerID INT NOT NULL,
    treatmentName VARCHAR(50) NOT NULL,
    FOREIGN KEY ([Status]) REFERENCES dbo.AppointmentStatusType([Status]),
    FOREIGN KEY (workerID) REFERENCES dbo.Worker(workerID),
    FOREIGN KEY (customerID) REFERENCES dbo.Customer(customerID),
    FOREIGN KEY (treatmentName) REFERENCES dbo.Treatment(treatmentName)
);

CREATE TABLE dbo.Supplier (
    supplierName VARCHAR(50) PRIMARY KEY NOT NULL,
    SupplierType VARCHAR(50) NOT NULL,
    supplierPhone VARCHAR(15),
    supplierEmail VARCHAR(40),
    contactPerson VARCHAR(30),
    leadTimeInDays INT,
    FOREIGN KEY (SupplierType) REFERENCES dbo.SupplierType(SupplierType)
);

CREATE TABLE dbo.Product (
    productName VARCHAR(50) PRIMARY KEY NOT NULL,
    company VARCHAR(50),
    quantity INT,
    minimumQuantity INT,
    supplierName VARCHAR(50) NOT NULL,
    FOREIGN KEY (supplierName) REFERENCES dbo.Supplier(supplierName)
);

CREATE TABLE dbo.FinancialTransaction (
    transactionID INT PRIMARY KEY NOT NULL,
    amount FLOAT,
    transactionDateTime DATETIME,
    paymentMethod VARCHAR(20),
    notes VARCHAR(255),
    TransactionType VARCHAR(20) NOT NULL,
    customerID INT,
    supplierName VARCHAR(50),
    FOREIGN KEY (TransactionType) REFERENCES dbo.TransactionType(TransactionType),
    FOREIGN KEY (customerID) REFERENCES dbo.Customer(customerID),
    FOREIGN KEY (supplierName) REFERENCES dbo.Supplier(supplierName)
);

CREATE TABLE dbo.FilePDF (
    filePath VARCHAR(255) PRIMARY KEY NOT NULL,
    fileName VARCHAR(50) NOT NULL,
    FileType VARCHAR(20) NOT NULL,
    appointmentID INT,    
    transactionID INT,    
    FOREIGN KEY (FileType) REFERENCES dbo.FileType(FileType),
    FOREIGN KEY (appointmentID) REFERENCES dbo.Appointment(appointmentID),
    FOREIGN KEY (transactionID) REFERENCES dbo.FinancialTransaction(transactionID)
);
GO

----------------------------------------------------------------------------
-- 3. INSERT DATA: החזרת כל המלל והנתונים שלך!
----------------------------------------------------------------------------

-- Lookups
INSERT INTO dbo.WorkerType VALUES ('manager'), ('receptionist'), ('cosmetician');
INSERT INTO dbo.AppointmentStatusType VALUES ('scheduled'), ('cancelled'), ('reminded'), ('archived'), ('medical_review'), ('in_treatment'), ('completed');
INSERT INTO dbo.TransactionType VALUES ('income'), ('expense');
INSERT INTO dbo.CustomerStatusType VALUES ('active'), ('inactive');
INSERT INTO dbo.EmployeeStatusType VALUES ('off_duty'), ('available'), ('busy'), ('on_leave');
INSERT INTO dbo.FileType VALUES ('healthDeclaration'), ('receipt'), ('taxInvoice'), ('invoice');
INSERT INTO dbo.SupplierType VALUES ('productSupplier'), ('utilityCompany'), ('municipality'), ('insuranceCompany'), ('rent'), ('maintenanceService');

-- Branches
INSERT INTO dbo.ByNoaBranch VALUES (1, 'Beer Sheva', '08-6496345', 'bynoa.bs@gmail.com', 'Rager 10, Beer-Sheva, Israel', '09:00:00', '17:00:00');

-- Workers
INSERT INTO dbo.Worker VALUES
(100000001, 'Noa Mahfoda', '1990-05-15', '2020-01-01', '050-7812345', 'manager', 'available', 1, 'Noa', '10000'),
(315884229, 'Shelly Danan', '1988-12-01', '2019-11-15', '053-7264981', 'receptionist', 'available', 1, 'Shelly', '31588'),
(428991365, 'Batsheva Sela', '1999-01-20', '2023-01-01', '050-6498253', 'cosmetician', 'available', 1, 'Batsheva', '32401'),
(509337812, 'Dana Levi', '1995-08-20', '2021-03-10', '052-7841296', 'cosmetician', 'available', 1, 'Dana', '50933'),
(638220147, 'Michal Katz', '1992-04-10', '2022-06-01', '054-6923814', 'cosmetician', 'available', 1, 'Michal', '63822');

-- Customers (כל הרשימה הארוכה שלך)
INSERT INTO dbo.Customer VALUES
(318559027, 'Yael Cohen', '1990-02-10', '050-1234123', '2023-01-01', 'active'),
(427991365, 'Shira Levi', '1988-06-15', '052-2345234', '2023-02-15', 'active'),
(509337812, 'Dana Avraham', '1992-11-30', '053-3456345', '2022-12-10', 'inactive'),
(638220147, 'Orly Peretz', '1989-08-20', '054-4567456', '2023-03-20', 'active'),
(294771503, 'Maya Friedman', '1987-04-05', '050-5678567', '2023-01-20', 'active'),
(351884229, 'Avigail Bitton', '1986-12-25', '052-6789678', '2021-05-05', 'active'),
(402663918, 'Hila Azoulay', '1990-09-09', '053-7890789', '2020-01-01', 'inactive'),
(512559017, 'Inbar Bar', '1991-10-10', '054-8901890', '2023-06-01', 'active'),
(288991462, 'Noa Golan', '1980-01-01', '050-9012901', '2022-08-08', 'active'),
(377220914, 'Rina Gabay', '1988-03-03', '052-0123012', '2023-07-07', 'active'),
(201234567, 'Tamar Shaked', '1985-04-30', '054-1112233', '2024-01-01', 'active'),
(301234567, 'Lior Ben-David', '1985-06-04', '052-2223344', '2024-02-15', 'active'),
(401234567, 'Efrat Nahum', '1983-08-16', '050-3334455', '2024-03-20', 'active'),
(501234567, 'Adi Mizrahi', '2001-04-10', '054-4445566', '2024-05-05', 'active'),
(601234567, 'Keren Ohana', '2001-03-12', '052-5556677', '2024-06-10', 'active'),
(252410277, 'Ruth Barak', '1973-04-05', '052-5499919', '2025-01-01', 'active');

-- Suppliers
INSERT INTO dbo.Supplier VALUES
('Tambour', 'maintenanceService', '09-8654321', 'tambour.pro@gmail.com', 'Ron Hadad', 2),
('BeautyLine Distribution', 'productSupplier', '03-7712244', 'orders@beautyline.co.il', 'Yael Mor', 3),
('CosmoPro Trade Ltd.', 'productSupplier', '03-6895522', 'sales@cosmopro.co.il', 'Daniel Aviv', 5),
('Dermaluxe Import', 'productSupplier', '09-8654432', 'service@dermaluxe.co.il', 'Hadar Shilon', 4),
('NaturaCare Distributors', 'productSupplier', '08-6759988', 'support@naturacare.co.il', 'Lior Shalev', 7),
('Elegance Cosmetics Supply', 'productSupplier', '03-9456678', 'info@elegance-supply.com', 'Miriam Gold', 6),
('PureGlow Wholesale', 'productSupplier', '02-5884411', 'contact@pureglow.co.il', 'Amit Ronen', 2),
('SkinLab International', 'productSupplier', '04-8221133', 'orders@skinlab-int.com', 'Sara Azulay', 5),
('PrimeAesthetics Supply', 'productSupplier', '03-6934411', 'office@primeaesthetics.co.il', 'Omer Levi', 4),
('Beer Sheva Municipality', 'municipality', '08-6463777', 'arnona@beersheva.muni.il', 'Avi Peretz', 0),
('Israel Electric Corporation', 'utilityCompany', '103', 'billing@iec.co.il', 'Rina Shahar', 0),
('Harel Insurance', 'insuranceCompany', '03-7547777', 'claims@harel-ins.co.il', 'Yossi Malka', 0);

-- Treatments
INSERT INTO dbo.Treatment VALUES
('Classic Facial Treatment', 60, 250),
('Anti-Aging Facial Treatment', 90, 450),
('Deep Tissue Massage', 60, 300),
('Manicure Treatment', 60, 120),
('Pedicure Treatment', 60, 150),
('Laser Hair Removal – Legs', 30, 200),
('Laser Hair Removal – Full Body', 120, 800),
('Eyebrow Shaping', 30, 50),
('Acne Facial Treatment', 60, 350),
('Bridal Makeup Session', 120, 600),
('Gel Polish – Hands', 60, 120),
('Gel Polish – Feet', 60, 140);

-- Products
INSERT INTO dbo.Product VALUES
('Ultrasol SPF50', 'Dr Fischer', 50, 10, 'BeautyLine Distribution'),
('Polisher Cream', 'Dr Fischer', 30, 5, 'CosmoPro Trade Ltd.'),
('Exfoliating Scrub', 'Gigi', 20, 5, 'Dermaluxe Import'),
('Body Scrub Patchouli', 'Sabon', 40, 8, 'PrimeAesthetics Supply'),
('Hand Cream Jasmine', 'Sabon', 100, 20, 'PureGlow Wholesale'),
('Face Cleanser', 'Gigi', 60, 10, 'BeautyLine Distribution'),
('Sensitive Skin Cream', 'Dr Fischer', 45, 10, 'SkinLab International'),
('Massage Oil Lavender', 'Sabon', 25, 5, 'NaturaCare Distributors'),
('Nail Polish Remover', 'Gigi', 15, 3, 'Elegance Cosmetics Supply');

-- Appointments (כל ה-20 תורים שלך!)
INSERT INTO dbo.Appointment VALUES
(1, '2025-12-01', '09:00:00', '09:05:00', 'First time client', 'completed', 428991365, 318559027, 'Classic Facial Treatment'),
(2, '2025-12-01', '11:00:00', '11:00:00', 'Regular maintainance', 'completed', 509337812, 427991365, 'Manicure Treatment'),
(3, '2025-12-02', '14:00:00', '14:15:00', 'Late arrival but ok', 'completed', 100000001, 294771503, 'Laser Hair Removal – Legs'),
(4, '2025-12-02', '16:00:00', NULL, 'Client cancelled via SMS', 'cancelled', 638220147, 509337812, 'Anti-Aging Facial Treatment'),
(5, '2025-12-03', '10:00:00', '10:00:00', 'Quick session', 'completed', 509337812, 351884229, 'Eyebrow Shaping'),
(6, '2025-12-03', '12:30:00', '12:35:00', NULL, 'completed', 428991365, 512559017, 'Acne Facial Treatment'),
(7, '2025-12-04', '09:00:00', '09:10:00', 'Sensitive skin noted', 'completed', 638220147, 288991462, 'Deep Tissue Massage'),
(8, '2025-12-04', '15:00:00', '15:00:00', 'Gift for birthday', 'completed', 509337812, 402663918, 'Gel Polish – Hands'),
(9, '2025-12-05', '11:00:00', '11:05:00', NULL, 'completed', 100000001, 377220914, 'Laser Hair Removal – Full Body'),
(10, '2025-12-05', '13:00:00', '13:00:00', 'Pre-wedding prep', 'completed', 638220147, 201234567, 'Bridal Makeup Session'),
(11, '2026-06-01', '10:00:00', NULL, NULL, 'scheduled', 428991365, 301234567, 'Classic Facial Treatment'),
(12, '2026-06-01', '12:00:00', NULL, 'Check allergies', 'scheduled', 509337812, 401234567, 'Pedicure Treatment'),
(13, '2026-06-02', '09:30:00', NULL, NULL, 'scheduled', 638220147, 501234567, 'Anti-Aging Facial Treatment'),
(14, '2026-06-02', '14:00:00', NULL, 'Requested specific polish', 'scheduled', 509337812, 601234567, 'Gel Polish – Feet'),
(15, '2026-06-03', '11:00:00', NULL, NULL, 'scheduled', 100000001, 318559027, 'Laser Hair Removal – Legs'),
(16, '2026-06-03', '15:00:00', NULL, NULL, 'scheduled', 428991365, 427991365, 'Acne Facial Treatment'),
(17, '2026-06-04', '10:30:00', NULL, NULL, 'scheduled', 509337812, 509337812, 'Manicure Treatment'),
(18, '2026-06-04', '13:00:00', NULL, NULL, 'scheduled', 638220147, 638220147, 'Deep Tissue Massage'),
(19, '2026-06-05', '09:00:00', NULL, NULL, 'scheduled', 509337812, 294771503, 'Eyebrow Shaping'),
(20, '2026-06-05', '11:30:00', NULL, NULL, 'scheduled', 100000001, 351884229, 'Laser Hair Removal – Full Body');

-- Financial Transactions
INSERT INTO dbo.FinancialTransaction VALUES
(1, 250, '2025-01-01 11:00:00', 'CreditCard', 'Payment for Facial', 'income', 318559027, NULL),
(2, 120, '2025-01-01 13:00:00', 'Cash', 'Payment for Manicure', 'income', 427991365, NULL),
(3, 5000, '2025-01-02 10:00:00', 'BankTransfer', 'Stock resupply', 'expense', NULL, 'BeautyLine Distribution'),
(4, 200, '2025-01-03 15:00:00', 'Bit', 'Payment for Laser', 'income', 294771503, NULL),
(5, 1500, '2025-01-04 09:00:00', 'BankTransfer', 'Arnona - Jan', 'expense', NULL, 'Beer Sheva Municipality'),
(6, 600, '2025-01-05 10:00:00', 'BankTransfer', 'Electricity - Jan', 'expense', NULL, 'Israel Electric Corporation'),
(7, 300, '2025-01-01 10:00:00', 'Cash', 'Tip included', 'income', 318559027, NULL),
(8, 250, '2025-01-02 12:00:00', 'CreditCard', 'Products purchase', 'expense', NULL, 'Elegance Cosmetics Supply'),
(9, 50, '2025-01-06 10:00:00', 'Cash', 'Eyebrow shaping', 'income', 288991462, NULL),
(10, 50, '2025-01-07 10:00:00', 'Bit', 'Down payment', 'income', 377220914, NULL);

-- FilePDF (החזרתי את כל הנתיבים שלך)
INSERT INTO dbo.FilePDF VALUES
('/docs/2025/jan/1_health.pdf', 'health_yael.pdf', 'healthDeclaration', 1, NULL),
('/docs/2025/jan/1_receipt.pdf', 'receipt_001.pdf', 'receipt', NULL, 1),
('/docs/suppliers/fischer_555.pdf', 'invoice_dr_fischer.pdf', 'invoice', NULL, 3),
('/docs/2025/jan/2_health.pdf', 'health_shira.pdf', 'healthDeclaration', 2, NULL),
('/docs/2025/jan/2_receipt.pdf', 'receipt_002.pdf', 'receipt', NULL, 2),
('/docs/2025/jan/5_health.pdf', 'health_maya.pdf', 'healthDeclaration', 5, NULL),
('/docs/2025/jan/5_consent.pdf', 'consent_laser.pdf', 'healthDeclaration', 5, NULL),
('/docs/expenses/rent_jan.pdf', 'tax_invoice_azrieli.pdf', 'taxInvoice', NULL, 5),
('/docs/2025/jan/5_receipt.pdf', 'receipt_003.pdf', 'receipt', NULL, 4),
('/docs/2025/jan/6_health.pdf', 'health_avigail.pdf', 'healthDeclaration', 6, NULL);
GO

----------------------------------------------------------------------------
-- 5. PROCEDURES (החזרתי הכל, מסודר)
----------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS dbo.Get_all_Customers;
DROP PROCEDURE IF EXISTS dbo.SP_add_customer;
DROP PROCEDURE IF EXISTS dbo.SP_Update_customer;
DROP PROCEDURE IF EXISTS dbo.SP_delete_customer;

DROP PROCEDURE IF EXISTS dbo.Get_all_Appointments;
DROP PROCEDURE IF EXISTS dbo.SP_add_appointment;
DROP PROCEDURE IF EXISTS dbo.SP_Update_appointment;
DROP PROCEDURE IF EXISTS dbo.SP_delete_appointment;

DROP PROCEDURE IF EXISTS dbo.Get_all_Workers;
DROP PROCEDURE IF EXISTS dbo.SP_login_worker;
DROP PROCEDURE IF EXISTS dbo.SP_add_worker;
DROP PROCEDURE IF EXISTS dbo.SP_Update_worker;
DROP PROCEDURE IF EXISTS dbo.SP_delete_worker;

DROP PROCEDURE IF EXISTS dbo.Get_all_Treatments;
DROP PROCEDURE IF EXISTS dbo.SP_add_treatment;
DROP PROCEDURE IF EXISTS dbo.SP_Update_treatment;
DROP PROCEDURE IF EXISTS dbo.SP_delete_treatment;

DROP PROCEDURE IF EXISTS dbo.Get_all_Products;
DROP PROCEDURE IF EXISTS dbo.SP_add_product;
DROP PROCEDURE IF EXISTS dbo.SP_Update_product;
DROP PROCEDURE IF EXISTS dbo.SP_delete_product;

DROP PROCEDURE IF EXISTS dbo.Get_all_Suppliers;
DROP PROCEDURE IF EXISTS dbo.SP_add_supplier;
DROP PROCEDURE IF EXISTS dbo.SP_Update_supplier;
DROP PROCEDURE IF EXISTS dbo.SP_delete_supplier;

DROP PROCEDURE IF EXISTS dbo.Get_all_Transactions;
DROP PROCEDURE IF EXISTS dbo.SP_add_transaction;
DROP PROCEDURE IF EXISTS dbo.SP_delete_transaction;

DROP PROCEDURE IF EXISTS dbo.Get_all_Files;
DROP PROCEDURE IF EXISTS dbo.SP_add_FilePDF;
DROP PROCEDURE IF EXISTS dbo.SP_delete_FilePDF;

DROP PROCEDURE IF EXISTS dbo.Get_all_Branches;


GO
----------------------------------------------------------------------------
-- 1. לקוחות (Customer)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Customers
AS
BEGIN
    SELECT * FROM dbo.Customer;
END
GO

CREATE PROCEDURE dbo.SP_add_customer
    @id INT,
    @name VARCHAR(30),
    @birthDate DATE,
    @phone VARCHAR(15),
    @joinDate DATE,
    @status VARCHAR(20)
AS
BEGIN
    INSERT INTO dbo.Customer (customerID, customerName, customerBirthDate, customerPhone, joinDate, CustomerStatus)
    VALUES (@id, @name, @birthDate, @phone, @joinDate, @status);
END
GO

CREATE PROCEDURE dbo.SP_Update_customer
    @id INT,
    @name VARCHAR(30),
    @phone VARCHAR(15),
    @birthDate DATE,
    @status VARCHAR(20)
AS
BEGIN
    UPDATE dbo.Customer
    SET customerName = @name,
        customerPhone = @phone,
        customerBirthDate = @birthDate,
        CustomerStatus = @status
    WHERE customerID = @id;
END
GO

CREATE PROCEDURE dbo.SP_delete_customer
    @id INT
AS
BEGIN
    DELETE FROM dbo.Customer WHERE customerID = @id;
END
GO

----------------------------------------------------------------------------
-- 2. תורים (Appointment)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Appointments
AS
BEGIN
    SELECT * FROM dbo.Appointment;
END
GO

CREATE PROCEDURE dbo.SP_add_appointment
    @id INT,
    @date DATE,
    @time TIME(0),
    @status VARCHAR(20),
    @workerId INT,
    @customerId INT,
    @treatmentName VARCHAR(50),
    @notes VARCHAR(255)
AS
BEGIN
    INSERT INTO dbo.Appointment (appointmentID, appointmentDate, startTime, [Status], workerID, customerID, treatmentName, appointmentNotes)
    VALUES (@id, @date, @time, @status, @workerId, @customerId, @treatmentName, @notes);
END
GO

CREATE PROCEDURE dbo.SP_Update_appointment
    @id INT,
    @date DATE,
    @startTime TIME(0),
    @actualStart TIME(0),
    @status VARCHAR(20),
    @workerId INT,
    @customerId INT,
    @treatmentName VARCHAR(50),
    @notes VARCHAR(255)
AS
BEGIN
    UPDATE dbo.Appointment
    SET appointmentDate = @date,
        startTime = @startTime,
        actualStartTime = @actualStart,
        [Status] = @status,
        workerID = @workerId,
        customerID = @customerId,
        treatmentName = @treatmentName,
        appointmentNotes = @notes
    WHERE appointmentID = @id;
END
GO

CREATE PROCEDURE dbo.SP_delete_appointment
    @id INT
AS
BEGIN
    DELETE FROM dbo.Appointment WHERE appointmentID = @id;
END
GO

----------------------------------------------------------------------------
-- 3. עובדים ולוגין (Worker)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Workers
AS
BEGIN
    SELECT * FROM dbo.Worker;
END
GO

CREATE PROCEDURE dbo.SP_login_worker
    @user VARCHAR(20),
    @pass VARCHAR(20)
AS
BEGIN
    SELECT * FROM dbo.Worker
    WHERE userName = @user AND userPassword = @pass;
END
GO

CREATE PROCEDURE dbo.SP_add_worker
    @id INT,
    @name VARCHAR(30),
    @birthDate DATETIME,
    @startDate DATETIME,
    @phone VARCHAR(15),
    @type VARCHAR(20),
    @status VARCHAR(20),
    @branchId INT,
    @user VARCHAR(20),
    @pass VARCHAR(20)
AS
BEGIN
    INSERT INTO dbo.Worker (workerID, workerName, workerBirthDate, startWorkDate, workerPhone, WorkerType, EmployeeStatus, branchID, userName, userPassword)
    VALUES (@id, @name, @birthDate, @startDate, @phone, @type, @status, @branchId, @user, @pass);
END
GO

CREATE PROCEDURE dbo.SP_Update_worker
    @id INT,
    @name VARCHAR(30),
    @phone VARCHAR(15),
    @type VARCHAR(20),
    @status VARCHAR(20)
AS
BEGIN
    UPDATE dbo.Worker
    SET workerName = @name,
        workerPhone = @phone,
        WorkerType = @type,
        EmployeeStatus = @status
    WHERE workerID = @id;
END
GO

CREATE PROCEDURE dbo.SP_delete_worker
    @id INT
AS
BEGIN
    DELETE FROM dbo.Worker WHERE workerID = @id;
END
GO

----------------------------------------------------------------------------
-- 4. טיפולים (Treatment)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Treatments
AS
BEGIN
    SELECT * FROM dbo.Treatment;
END
GO

CREATE PROCEDURE dbo.SP_add_treatment
    @name VARCHAR(50),
    @duration INT,
    @price MONEY
AS
BEGIN
    INSERT INTO dbo.Treatment (treatmentName, durationMinute, price)
    VALUES (@name, @duration, @price);
END
GO

CREATE PROCEDURE dbo.SP_Update_treatment
    @name VARCHAR(50),
    @duration INT,
    @price MONEY
AS
BEGIN
    UPDATE dbo.Treatment
    SET durationMinute = @duration,
        price = @price
    WHERE treatmentName = @name;
END
GO

CREATE PROCEDURE dbo.SP_delete_treatment
    @name VARCHAR(50)
AS
BEGIN
    DELETE FROM dbo.Treatment WHERE treatmentName = @name;
END
GO

----------------------------------------------------------------------------
-- 5. מוצרים (Product)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Products
AS
BEGIN
    SELECT * FROM dbo.Product;
END
GO

CREATE PROCEDURE dbo.SP_add_product
    @name VARCHAR(50),
    @company VARCHAR(50),
    @qty INT,
    @minQty INT,
    @supplier VARCHAR(50)
AS
BEGIN
    INSERT INTO dbo.Product (productName, company, quantity, minimumQuantity, supplierName)
    VALUES (@name, @company, @qty, @minQty, @supplier);
END
GO

CREATE PROCEDURE dbo.SP_Update_product
    @name VARCHAR(50),
    @qty INT
AS
BEGIN
    UPDATE dbo.Product
    SET quantity = @qty
    WHERE productName = @name;
END
GO

CREATE PROCEDURE dbo.SP_delete_product
    @name VARCHAR(50)
AS
BEGIN
    DELETE FROM dbo.Product WHERE productName = @name;
END
GO

----------------------------------------------------------------------------
-- 6. ספקים (Supplier)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Suppliers
AS
BEGIN
    SELECT * FROM dbo.Supplier;
END
GO

CREATE PROCEDURE dbo.SP_add_supplier
    @name VARCHAR(50),
    @type VARCHAR(50),
    @phone VARCHAR(15),
    @email VARCHAR(40),
    @contact VARCHAR(30),
    @leadTime INT
AS
BEGIN
    INSERT INTO dbo.Supplier (supplierName, SupplierType, supplierPhone, supplierEmail, contactPerson, leadTimeInDays)
    VALUES (@name, @type, @phone, @email, @contact, @leadTime);
END
GO

CREATE PROCEDURE dbo.SP_Update_supplier
    @name VARCHAR(50),
    @phone VARCHAR(15),
    @contact VARCHAR(30)
AS
BEGIN
    UPDATE dbo.Supplier
    SET supplierPhone = @phone,
        contactPerson = @contact
    WHERE supplierName = @name;
END
GO

CREATE PROCEDURE dbo.SP_delete_supplier
    @name VARCHAR(50)
AS
BEGIN
    DELETE FROM dbo.Supplier WHERE supplierName = @name;
END
GO

----------------------------------------------------------------------------
-- 7. עסקאות (FinancialTransaction)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Transactions
AS
BEGIN
    SELECT * FROM dbo.FinancialTransaction;
END
GO

CREATE PROCEDURE dbo.SP_add_transaction
    @id INT,
    @amount FLOAT,
    @date DATETIME,
    @method VARCHAR(20),
    @notes VARCHAR(255),
    @type VARCHAR(20),
    @custId INT,
    @supName VARCHAR(50)
AS
BEGIN
    INSERT INTO dbo.FinancialTransaction (transactionID, amount, transactionDateTime, paymentMethod, notes, TransactionType, customerID, supplierName)
    VALUES (@id, @amount, @date, @method, @notes, @type, @custId, @supName);
END
GO

CREATE PROCEDURE dbo.SP_delete_transaction
    @id INT
AS
BEGIN
    DELETE FROM dbo.FinancialTransaction WHERE transactionID = @id;
END
GO

----------------------------------------------------------------------------
-- 8. קבצים (FilePDF)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Files
AS
BEGIN
    SELECT * FROM dbo.FilePDF;
END
GO

CREATE PROCEDURE dbo.SP_add_FilePDF
    @path VARCHAR(255),
    @fileName VARCHAR(50),
    @type VARCHAR(20),
    @appId INT,
    @transId INT
AS
BEGIN
    INSERT INTO dbo.FilePDF (filePath, fileName, FileType, appointmentID, transactionID)
    VALUES (@path, @fileName, @type, @appId, @transId);
END
GO

CREATE PROCEDURE dbo.SP_delete_FilePDF
    @path VARCHAR(255)
AS
BEGIN
    DELETE FROM dbo.FilePDF WHERE filePath = @path;
END
GO

----------------------------------------------------------------------------
-- 9. סניפים (ByNoaBranch)
----------------------------------------------------------------------------

CREATE PROCEDURE dbo.Get_all_Branches
AS
BEGIN
    SELECT * FROM dbo.ByNoaBranch;
END
GO


