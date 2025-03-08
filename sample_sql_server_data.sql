-- Insert sample data into Users table
INSERT INTO Users (FullName, Email, Password, Role, Address) VALUES
('John Doe', 'john.doe@example.com', CONVERT(VARCHAR(255),HASHBYTES('SHA1', 'Password1'),2), 'Citizen', '123 Main Street'),
('Jane Smith', 'jane.smith@example.com', CONVERT(VARCHAR(255),HASHBYTES('SHA1', 'Password1'),2), 'AreaLeader', '456 Maple Avenue'),
('Michael Brown', 'michael.brown@example.com', CONVERT(VARCHAR(255),HASHBYTES('SHA1', 'Password1'),2), 'Police', '789 Oak Lane'),
('Emily Davis', 'emily.davis@example.com', CONVERT(VARCHAR(255),HASHBYTES('SHA1', 'Password1'),2), 'Citizen', '321 Pine Street'),
('Robert Johnson', 'robert.johnson@example.com', CONVERT(VARCHAR(255),HASHBYTES('SHA1', 'Password1'),2), 'Citizen', '654 Cedar Road');
GO

-- Insert sample data into Households table
-- Here, we assume that Users with UserID 1, 2 and 4 are heads of households.
INSERT INTO Households (HeadOfHouseholdID, Address) VALUES
(1, '123 Main Street'),
(2, '456 Maple Avenue'),
(4, '321 Pine Street');
GO

-- Insert sample data into Registrations table
-- Example: User 1 gets a Permanent registration approved by Police (User 3),
-- User 4 submits a Temporary registration (still pending),
-- and User 2 had a TemporaryStay registration rejected.
INSERT INTO Registrations (UserID, RegistrationType, StartDate, EndDate, Status, ApprovedBy, Comments) VALUES
(1, 'Permanent', '2023-01-01', NULL, 'Approved', 3, 'Registration approved by Police.'),
(4, 'Temporary', '2023-05-01', '2023-12-31', 'Pending', NULL, 'Awaiting review.'),
(2, 'TemporaryStay', '2023-06-15', '2023-07-15', 'Rejected', 3, 'Incomplete documents.');
GO

-- Insert sample data into HouseholdMembers table
-- Assigning members to households (HouseholdID as inserted above)
INSERT INTO HouseholdMembers (HouseholdID, UserID, Relationship) VALUES
(1, 1, 'Head'),
(2, 5, 'Child'),
(2, 2, 'Head'),
(3, 4, 'Head');
GO

-- Insert sample data into Notifications table
INSERT INTO Notifications (UserID, Message) VALUES
(1, 'Your registration has been approved.'),
(4, 'Please update your household details.'),
(2, 'New alert: System maintenance scheduled for tonight.');
GO

-- Insert sample data into Logs table
INSERT INTO Logs (UserID, Action) VALUES
(1, 'Logged in.'),
(3, 'Approved registration for user 1.'),
(2, 'Updated profile.');
GO
