-- drop database PRN212_project;
create database PRN212_project;
go
use PRN212_project;
go
create table Users (
    UserID int identity(1, 1) primary key,
    FullName varchar(100) not null,
    Email varchar(100) not null unique,
    Password varchar(255) not null,
    Role varchar(255) not null check (Role IN('Citizen', 'AreaLeader', 'Police')),
    Address text not null
)

go

create table Households (
    HouseholdID int identity(1, 1) primary key,
    HeadOfHouseholdID int references Users(UserID),
    Address text not null,
    CreatedDate date default GETDATE()
)

go

create table Registrations (
    RegistrationID int identity(1, 1) primary key,
    UserID int references Users(UserID),
    RegistrationType varchar(255) not null check (RegistrationType IN('Permanent', 'Temporary', 'TemporaryStay')),
    StartDate date not null,
    EndDate date,
    Status varchar(255) not null check (Status IN('Pending', 'Approved', 'Rejected')) default 'Pending',
    ApprovedBy int references Users(UserID),
    Comments text
)

go

create table HouseholdMembers (
    MemberID int identity(1, 1) primary key,
    HouseholdID int references Households(HouseholdID),
    UserID int references Users(UserID),
    Relationship varchar(50) not null
)

go

create table Notifications (
    NotificationID int identity(1, 1) primary key,
    UserID int references Users(UserID),
    Message text not null,
    SentDate datetime default CURRENT_TIMESTAMP,
    IsRead bit default 0
)

go

create table Logs (
    LogID int identity(1, 1) primary key,
    UserID int references Users(UserID),
    Action varchar(100) not null,
    Timestamp datetime default CURRENT_TIMESTAMP
)
