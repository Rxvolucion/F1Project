USE master
GO

--drop database if it exists
IF DB_ID('F1') IS NOT NULL
BEGIN
	ALTER DATABASE F1 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE F1;
END

--CREATE DATABASE F1
CREATE DATABASE F1
GO

USE F1
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	--email varchar(75) UNIQUE NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	favorite_driver varchar(50),
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

--populate default user data
INSERT INTO users (username, password_hash, salt, user_role, favorite_driver) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user',0);
INSERT INTO users (username, password_hash, salt, user_role, favorite_driver) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin',0);

CREATE TABLE f1_drivers (
	driver_id int IDENTITY(3001,1) NOT NULL,
	driver_popularity int,
	driver_name varchar(40) UNIQUE NOT NULL,
	driver_DOB varchar(30) NOT NULL,
	driver_team varchar(100) NOT NULL,
	driver_car_no int NOT NULL,
	driver_nationality varchar(30) NOT NULL,
	driver_worldchampionships int NOT NULL,
	driver_no_of_GP_starts int NOT NULL,
	driver_no_of_GP_podiums int NOT NULL,
	driver_no_of_GP_wins int NOT NULL,
	driver_no_of_DNFs int NOT NULL,
	CONSTRAINT PK_f1_driver PRIMARY KEY (driver_id)
)

--populate default driver data
INSERT INTO f1_drivers (driver_popularity, driver_name, driver_DOB, driver_team, driver_car_no, driver_nationality, driver_worldchampionships, driver_no_of_GP_starts, driver_no_of_GP_podiums, driver_no_of_GP_wins, driver_no_of_DNFs) VALUES (0, 'Lewis Hamilton', 'January 7, 1985', 'Mercedes', 44, 'British', 7,355, 202, 105, 32);
INSERT INTO f1_drivers (driver_popularity, driver_name, driver_DOB, driver_team, driver_car_no, driver_nationality, driver_worldchampionships, driver_no_of_GP_starts, driver_no_of_GP_podiums, driver_no_of_GP_wins, driver_no_of_DNFs) VALUES (0, 'Max Verstappen', 'September 30, 1997', 'Red Bull', 33, 'Dutch', 4, 208, 112, 63, 32);
INSERT INTO f1_drivers (driver_popularity, driver_name, driver_DOB, driver_team, driver_car_no, driver_nationality, driver_worldchampionships, driver_no_of_GP_starts, driver_no_of_GP_podiums, driver_no_of_GP_wins, driver_no_of_DNFs) VALUES (0, 'Fernando Alonso', 'July 29, 1981', 'Aston Martin', 14, 'Spanish', 2, 404, 106, 32, 77)

GO