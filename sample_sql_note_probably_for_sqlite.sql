------------------
create database PRN212_project;
go
use PRN212_project;
go
CREATE TABLE Users (
 UserID INT PRIMARY KEY AUTO_INCREMENT,
 FullName VARCHAR(100) NOT NULL,
 Email VARCHAR(100) NOT NULL UNIQUE,
 Password VARCHAR(255) NOT NULL,
 Role ENUM('Citizen', 'AreaLeader', 'Police') NOT NULL,
 Address TEXT NOT NULL
);

go

CREATE TABLE Households (
 HouseholdID INT PRIMARY KEY AUTO_INCREMENT,
 HeadOfHouseholdID INT,
 Address TEXT NOT NULL,
 CreatedDate DATE DEFAULT CURRENT_DATE,
 FOREIGN KEY (HeadOfHouseholdID) REFERENCES Users(UserID)
);
go

CREATE TABLE Registrations (
 RegistrationID INT PRIMARY KEY AUTO_INCREMENT,
 UserID INT,
 RegistrationType ENUM('Permanent', 'Temporary', 'TemporaryStay') NOT
NULL,
 StartDate DATE NOT NULL,
 EndDate DATE NULL,
 Status ENUM('Pending', 'Approved', 'Rejected') DEFAULT 'Pending',
 ApprovedBy INT,
 Comments TEXT NULL,
 FOREIGN KEY (UserID) REFERENCES Users(UserID),
 FOREIGN KEY (ApprovedBy) REFERENCES Users(UserID)
);
go

CREATE TABLE HouseholdMembers (
 MemberID INT PRIMARY KEY AUTO_INCREMENT,
 HouseholdID INT,
 UserID INT,
 Relationship VARCHAR(50) NOT NULL,
 FOREIGN KEY (HouseholdID) REFERENCES Households(HouseholdID),
 FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
go

CREATE TABLE Notifications (
 NotificationID INT PRIMARY KEY AUTO_INCREMENT,
 UserID INT,
 Message TEXT NOT NULL,
 SentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
 IsRead BOOLEAN DEFAULT FALSE,
 FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
go

CREATE TABLE Logs (
 LogID INT PRIMARY KEY AUTO_INCREMENT,
 UserID INT,
 Action VARCHAR(100) NOT NULL,
 Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
 FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
go
