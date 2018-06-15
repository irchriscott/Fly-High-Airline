--CREATION DATABASE

create database FlyHighAirline
GO

--USE DATABASE

use FlyHighAirline
GO

--CREATION OF TABLES

create table [dbo].[AircraftsSchedule](
	AircraftId int PRIMARY KEY,
	AircraftName varchar(100),
	TotalSits int,
	VIP int,
	BusinessClass int,
	EconomyClass int,
	StartedDate date,
	TravelDay varchar(50),
	PlaceFrom varchar(100),
	PlaceTo varchar(100),
	MoveTime time,
	DayNum int
)

create table [dbo].[Aircrafts](
	AircraftId int PRIMARY KEY,
	AircraftName varchar(100),
	TotalSits int,
	VIP int,
	BusinessClass int,
	EconomyClass int,
	StartedDate date,
)

create table [dbo].[ClassesSits](
	AircraftId int FOREIGN KEY REFERENCES [dbo].[Aircrafts](AircraftId),
	TotalSits int,
	VIP int,
	BusinessClass int,
	EconomyClass int
)

create table [dbo].[Administrators](
	AdminID int PRIMARY KEY,
	FirstName varchar(100),
	LastName varchar(100),
	UserName varchar(100),
	PassCode varchar(100),
	AdminAvatar varchar(200),
	PhoneNumber varchar(100),
	Addresse text,
	RoleName varchar(200),
	StartDate date, 
	EndDate date
)

create table [dbo].[Stewardess](
	StewardessId int PRIMARY KEY,
	FirstName varchar(100),
	LastName varchar(100),
	PhoneNumber varchar(100),
	StewardessAvatar varchar(200),
	Addresse text,
	Aircraft int FOREIGN KEY REFERENCES [dbo].[Aircrafts](AircraftId),
	DateStart date,
	DateEnd date
)

create table [dbo].[Pilots](
	PilotId int PRIMARY KEY,
	FirstName varchar(100),
	LastName varchar(100),
	PhoneNumber varchar(100),
	PilotAvatar varchar(200),
	Addresse text,
	Aircraft int FOREIGN KEY REFERENCES [dbo].[Aircrafts](AircraftId),
	DateStart date,
	DateEnd date
)

create table [dbo].[Services](
	ServiceId int PRIMARY KEY,
	ServiceName varchar(100)
)

create table [dbo].[Passengers](
	FirstName varchar(100),
	LastName varchar(100),
	NickName varchar(100),
	DocumentId varchar(100) PRIMARY KEY,
	DateOdBirth date,
	PlaceOfBirth varchar(100),
	EmailAddress varchar(100),
	Nationality varchar(100),
	Gender varchar(100),
	CustomerAvatar varchar(200),
	PassCode varchar(100) NULL,
	ServiceId int FOREIGN KEY REFERENCES [dbo].[Services](ServiceId),
	TimeRegist date
)

create table [dbo].[TravelService](
	DocumentId varchar(100) FOREIGN KEY REFERENCES [dbo].[Passengers](DocumentId),
	Reason text,
	VisaPossesstion varchar(100),
	Kid1 text,
	Kid2 text,
	Kid3 text,
	Kid4 text,
	Kid5 text,
	Addresse text,
	PhoneNumber varchar(100),
	Confirmation varchar(100),
	AmountPaid int,
	Currency varchar(100)
)

create table [dbo].[TourService](
	DocumentId varchar(100) FOREIGN KEY REFERENCES [dbo].[Passengers](DocumentId),
	StartDate date,
	EndDate date,
	Residence text,
	PlaceVisite text,
	Kid1 text,
	Kid2 text,
	Kid3 text,
	Kid4 text,
	Kid5 text,
	Confirmation varchar(100),
	AmountPaid int,
	Currency varchar(100)
)

create table [dbo].[TransfertService](
	DocumentId varchar(100) FOREIGN KEY REFERENCES [dbo].[Passengers](DocumentId),
	SendFirstName varchar(100),
	SendLastName varchar(100),
	SendEmail varchar(100),
	SendAddress text,
	SendPhoneNumber varchar(100),
	AmountSent int,
	Currency varchar(100),
	ArrivalDate date,
	Confirmation varchar(100),
	AmountPaid int
)

create table [dbo].[BusinessService](
	DocumentId varchar(100) FOREIGN KEY REFERENCES [dbo].[Passengers](DocumentId),
	StartDate date,
	EndDate date,
	Products text,
	ReasonBusiness text,
	Quantity varchar(100),
	Budget int,
	Currency varchar(100),
	Confirmation varchar(100),
	AmountPaid int,
)

create table [dbo].[PassengerAir](
	DocumentId varchar(100) FOREIGN KEY REFERENCES [dbo].[Passengers](DocumentId),
	Confirmation varchar(50),
	SitNumber int,
	UseDate date,
	ServiceId int FOREIGN KEY REFERENCES [dbo].[Services](ServiceId),
)

create table [dbo].[Reservation](
	FirstName varchar(100) NULL,
	LastName varchar(100) NULL,
	NickName varchar(100) NULL,
	DocumentID varchar(100) NULL,
	Email varchar(200) NULL,
	PlaceOfBirth varchar(100) NULL,
	DateOfBirth date NULL,
	Gender varchar(50) NULL,
	Nationality varchar(100) NULL,
	ServiceID int FOREIGN KEY REFERENCES [dbo].[Services](ServiceId)
)

--ALTER TABLES

--alter table [dbo].[Administrators] add Avatar varchar(100)
--alter table [dbo].[Stewardess] add Avatar varchar(100)
--alter table [dbo].[Pilots] add Avatar varchar(100)
--alter table [dbo].[PassengerAir] add Confirmation varchar(50)

--INSERT USEFULL DATA

insert into Administrators (AdminId, FirstName, LastName, UserName, PassCode, PhoneNumber, Addresse, Avatar)
values ('1', 'Christian', 'Lurhakwa', 'Chriscott', 'chriscons123', '0756891594', 'Kampala Nana Hostel, Makerere', 'chris.jpg')

insert into AircraftsSchedule(AircraftId, AircraftName, BusinessClass, EconomyClass, VIP, TotalSits, StartedDate, PlaceFrom, PlaceTo, TravelDay, MoveTime)
values 
('1', 'BOING 325', '40', '80', '0', '120', '2016-06-26', 'Kampala', 'Dubai - Quatar', 'Mondays', '17:00:00' ), 
('2', 'AIRBUS A350', '30', '120', '20', '170', '2016-10-15', 'Kampala', 'Paris - France', 'Thusdays', '10:00:00' ), 
('3', 'AIRBUS A380', '50', '120', '30', '200', '2016-11-05', 'Kampala', 'Johannesbourg - SA', 'Tuesdays', '18:00:00' ),
('4', 'BOING 325', '40', '80', '0', '120', '2016-06-26', 'Kampala', 'London - UK', 'Wednesdays', '01:00:00' ),
('5', 'AIRBUS A350', '30', '120', '20', '170', '2016-10-15', 'Kampala', 'Rio de Janeiro - Brazil', 'Saturdays', '21:00:00'), 
('6', 'AIRBUS A380', '50', '120', '30', '200', '2016-11-05', 'Kampala', 'New York - USA', 'Sundays', '19:00:00')


SELECT AircraftsSchedule.AircraftId, AircraftsSchedule.AircraftName, AircraftsSchedule.TotalSits, AircraftsSchedule.TravelDay, AircraftsSchedule.PlaceFrom, AircraftsSchedule.PlaceTo FROM AircraftsSchedule

SELECT * FROM AircraftsSchedule

--CREATE PROCEDURES

create proc InsertAdmin
	@AdminId int,
	@FirstName varchar(100),
	@LastName varchar(100),
	@Username varchar(100),
	@Password varchar(100),
	@PhoneNumber varchar(100),
	@Address text,
	@Function varchar(100),
	@AdminAvatar varbinary(max),
	@StartDate date,
	@EndDate date
as
Begin
	Insert into Administrators (AdminID, FirstName, LastName, UserName, PassCode, PhoneNumber, Addresse, RoleName, AdminAvatar, StartDate, EndDate)
	values
	(@AdminId, @FirstName, @LastName, @Username, @Password, @PhoneNumber, @Address, @Function, @AdminAvatar, @StartDate, @EndDate)
End

create proc InsertPilot
	@PilotId int,
	@FirstName varchar(100),
	@LastName varchar(100),
	@PhoneNumber varchar(100),
	@Address text,
	@Aircraft int,
	@StartDate date,
	@EndDate date,
	@PilotAvatar varbinary(max)
as
Begin
	Insert into Pilots(PilotId, FirstName, LastName, PhoneNumber, Addresse, Aircraft, DateStart, DateEnd, PilotAvatar)
	values
	(@PilotId, @FirstName, @LastName, @PhoneNumber, @Address, @Aircraft, @StartDate, @EndDate, @PilotAvatar)
End

select * from Stewardess

create proc InsertStewardess
	@StewardessId int,
	@FirstName varchar(100),
	@LastName varchar(100),
	@PhoneNumber varchar(100),
	@Address text,
	@Aircraft int,
	@StartDate date,
	@EndDate date,
	@StewardessAvatar varbinary(max)
as
Begin
	Insert into Stewardess(StewardessId, FirstName, LastName, PhoneNumber, Addresse, Aircraft, DateStart, DateEnd, StewardessAvatar)
	values
	(@StewardessId, @FirstName, @LastName, @PhoneNumber, @Address, @Aircraft, @StartDate, @EndDate, @StewardessAvatar)
End

create proc InsertNewPassenger
	@firstname varchar(200),
	@lastname varchar(200),
	@nickname varchar(200),
	@documentid varchar(200),
	@dateofbirth date,
	@placeofbirth varchar(200),
	@email varchar(200),
	@nationality varchar(200),
	@gender varchar(50),
	@service int,
	@registration date,
	@customeravatar varbinary(max)
as
Begin
	Insert into Passengers(FirstName, LastName, NickName, DocumentId, DateOdBirth, PlaceOfBirth, EmailAddress, Nationality, Gender, CustomerAvatar, ServiceId, TimeRegist)
	values
	(@firstname, @lastname, @nickname, @documentid, @dateofbirth, @placeofbirth, @email, @nationality, @gender, @customeravatar, @service, @registration)
End