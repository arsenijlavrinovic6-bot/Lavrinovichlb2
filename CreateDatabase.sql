/* Лабораторная работа №2. Выполнить в SQL Server Management Studio. */
IF DB_ID(N'MotorbikeDB') IS NULL CREATE DATABASE MotorbikeDB;
GO
USE MotorbikeDB;
GO
IF OBJECT_ID(N'dbo.Table_Motorbike', N'U') IS NOT NULL DROP TABLE dbo.Table_Motorbike;
CREATE TABLE dbo.Table_Motorbike (
    ID INT NOT NULL PRIMARY KEY,
    Brand NVARCHAR(100) NOT NULL,
    Model NVARCHAR(150) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Power INT NOT NULL,
    Mileage INT NOT NULL,
    Image NVARCHAR(50) NULL
);
GO
INSERT INTO dbo.Table_Motorbike (ID, Brand, Model, Price, Power, Mileage, Image) VALUES
(1, N'Ducati', N'Multistrada 950', 1200000, 113, 6300, N'1.png'),
(2, N'Harley-Davidson', N'FXDR 114', 4500000, 90, 12, N'2.png'),
(3, N'BMW', N'R 1200 GS', 775000, 105, 50454, N'3.png'),
(4, N'Honda', N'Africa Twin CRF 1000L/1100L', 1095000, 95, 37620, N'4.png'),
(5, N'BMW', N'R 1200 R', 725000, 110, 28000, N'5.png'),
(6, N'Aprilia', N'SL 750 Shiver', 357000, 95, 357000, N'6.png'),
(7, N'KTM', N'390 Adventure', 679900, 44, 15, N'7.png'),
(8, N'Yamaha', N'XVS1300', 620000, 76, 60000, N'8.png'),
(9, N'Honda', N'VFR 1200', 750000, 111, 38800, N'9.png'),
(10, N'Triumph', N'Street Triple', 1015000, 116, 4700, N'10.png'),
(11, N'Ducati', N'Monster', 950000, 109, 20334, N'11.png'),
(12, N'Yamaha', N'XV950 Bolt', 630000, 52, 39729, N'12.png'),
(13, N'Ducati', N'XDiavel', 2020000, 152, 7429, N'13.png'),
(14, N'Honda', N'XLR', 200000, 33, 31000, N'14.png'),
(15, N'Honda', N'VT 750', 470000, 45, 16400, N'15.png'),
(16, N'BMW', N'R 1200 R', 840000, 110, 16200, N'16.png'),
(17, N'BMW', N'R 1250 GS Adventure', 3200000, 136, 5, N'17.png'),
(18, N'Harley-Davidson', N'Sportster S', 1930000, 122, 352, N'18.png'),
(19, N'BMW', N'F 900 XR', 1050000, 105, 3100, N'19.png'),
(20, N'Minsk', N'X 250', 130000, 16, 284, N'20.png');
GO
SELECT * FROM dbo.Table_Motorbike ORDER BY ID;
