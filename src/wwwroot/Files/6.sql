/*
SQLyog Ultimate v11.11 (64 bit)
MySQL - 5.5.5-10.4.24-MariaDB : Database - flatmate_app
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`flatmate_app` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `flatmate_app`;

/*Table structure for table `areas` */

DROP TABLE IF EXISTS `areas`;

CREATE TABLE `areas` (
  `AID` int(11) NOT NULL AUTO_INCREMENT,
  `AreaName` varchar(30) DEFAULT NULL,
  `City` varchar(30) DEFAULT 'Lucknow',
  PRIMARY KEY (`AID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Data for the table `areas` */

insert  into `areas`(`AID`,`AreaName`,`City`) values (1,'Alambagh','Lucknow'),(2,'Hazratgunj','Lucknow'),(3,'Charbagh','Lucknow'),(4,'LDA','Lucknow'),(5,'Ashiayana','Lucknow'),(6,'Chinhat','Lucknow'),(7,'Gomtinagar','Lucknow'),(8,'Chowk','Lucknow');

/*Table structure for table `bookings` */

DROP TABLE IF EXISTS `bookings`;

CREATE TABLE `bookings` (
  `BID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT 'Unknown',
  `Mobile` varchar(10) DEFAULT NULL,
  `RoomsForBooking` int(11) DEFAULT 0,
  `HID` int(11) DEFAULT NULL,
  `UID` int(11) DEFAULT 1,
  `BookingDate` datetime DEFAULT NULL,
  PRIMARY KEY (`BID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

/*Data for the table `bookings` */

insert  into `bookings`(`BID`,`Name`,`Mobile`,`RoomsForBooking`,`HID`,`UID`,`BookingDate`) values (11,'new','9451712232',2,31,1,'2019-03-11 18:19:02');

/*Table structure for table `cities` */

DROP TABLE IF EXISTS `cities`;

CREATE TABLE `cities` (
  `CID` int(11) NOT NULL AUTO_INCREMENT,
  `CityName` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`CID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `cities` */

insert  into `cities`(`CID`,`CityName`) values (1,'Lucknow'),(2,'Kanpur'),(3,'Delhi'),(4,'Mumbai'),(5,'Jaipur');

/*Table structure for table `feedbacks` */

DROP TABLE IF EXISTS `feedbacks`;

CREATE TABLE `feedbacks` (
  `FID` int(11) NOT NULL AUTO_INCREMENT,
  `Feedback` varchar(2000) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Mobile` varchar(15) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`FID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `feedbacks` */

insert  into `feedbacks`(`FID`,`Feedback`,`Name`,`Mobile`,`Email`,`CreatedOn`) values (1,'asdasdasdasdasd','hello',NULL,'hello@gmail.com','2018-03-26 22:44:26'),(2,'asd asd asd asd ','hello',NULL,'mohit.xxx@gmail.com','2018-03-26 22:55:27');

/*Table structure for table `interests` */

DROP TABLE IF EXISTS `interests`;

CREATE TABLE `interests` (
  `IID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UID` bigint(20) DEFAULT 1,
  `PGID` bigint(20) DEFAULT 1,
  `AddedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`IID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `interests` */

insert  into `interests`(`IID`,`UID`,`PGID`,`AddedOn`) values (1,1,2,'2024-05-04 15:54:21');

/*Table structure for table `locations` */

DROP TABLE IF EXISTS `locations`;

CREATE TABLE `locations` (
  `LID` int(11) NOT NULL AUTO_INCREMENT,
  `LocationName` varchar(50) DEFAULT NULL,
  `Rent` int(11) DEFAULT NULL,
  `Rating` int(11) DEFAULT NULL,
  `Area` varchar(30) DEFAULT NULL,
  `City` varchar(30) DEFAULT NULL,
  `Country` varchar(30) DEFAULT NULL,
  `Type` varchar(20) DEFAULT 'hotel',
  `Contact` varchar(30) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`LID`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

/*Data for the table `locations` */

insert  into `locations`(`LID`,`LocationName`,`Rent`,`Rating`,`Area`,`City`,`Country`,`Type`,`Contact`,`Address`,`Description`) values (1,'Hotel1',1000,3,'Alambagh','Lucknow','India','hotel',NULL,'NAka CrossRoad, Near Railway Station, Charbagh',NULL),(2,'Hotel2',1500,4,'Ashiyana','New York','USA','home',NULL,' Address usa  Address usa  Address usa  Address usa  Address usa  Address usa  Address usa ',NULL),(3,'CMS',1200,3,'LDA','Beijing','China','School',NULL,'Address China Address China Address China Address China ',NULL),(4,'Charbagh Railway Station',0,2,'Charbagh','Lucknow','Intia','RailwayStation',NULL,'Lucknow CharbaghLucknow CharbaghLucknow CharbaghLucknow Charbagh',NULL),(5,'Airport',0,0,'Amousi','Lucknow','india','Airport',NULL,'lucknow Amousi lucknow Amousi lucknow Amousi lucknow Amousi lucknow Amousi ',NULL),(6,'Imambada',0,0,'Chowk','Lucknow','India','Monument',NULL,'Lucknow ChowkLucknow ChowkLucknow ChowkLucknow ChowkLucknow Chowk',NULL),(7,'Tunday',0,0,'Chowk','Lucknow','India','Restaurant',NULL,'Lucknow Aminabad Lucknow Aminabad Lucknow Aminabad Lucknow Aminabad ',NULL),(8,'Sahara Hospital',999,9,'Gomtinagar','Lucknow','india','Hospital',NULL,'Lucknow Gomtinagar Lucknow Gomtinagar Lucknow Gomtinagar Lucknow Gomtinagar ',NULL),(9,'Taz Hotel',NULL,NULL,'Gomtinagar',NULL,NULL,'Hotel','9988776262','Gomti nagar near 1090 choraha',NULL),(16,'La Martinier',NULL,NULL,'Gomtinagar',NULL,NULL,'School','hello','Near 1090 Choraha',NULL),(17,'',NULL,NULL,'',NULL,NULL,'3 Star','8877445566','samar vihar',NULL),(18,'',NULL,NULL,'',NULL,NULL,'5 Star','9988776655','Gomtinagar',NULL),(19,'',NULL,NULL,'',NULL,NULL,'3 Star','9988776655','hello',NULL);

/*Table structure for table `messages` */

DROP TABLE IF EXISTS `messages`;

CREATE TABLE `messages` (
  `MID` bigint(20) NOT NULL AUTO_INCREMENT,
  `SenderUID` bigint(20) DEFAULT 1,
  `RecieverUID` bigint(20) DEFAULT 1,
  `MessageText` varchar(2000) DEFAULT 'Message',
  `MessageDateTime` datetime DEFAULT NULL,
  `Seen` tinyint(1) DEFAULT 0,
  `SeenAt` datetime DEFAULT NULL,
  `Status` varchar(10) DEFAULT 'UnRead',
  PRIMARY KEY (`MID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `messages` */

insert  into `messages`(`MID`,`SenderUID`,`RecieverUID`,`MessageText`,`MessageDateTime`,`Seen`,`SeenAt`,`Status`) values (1,1,2,'asdasd','2024-05-04 15:39:23',0,NULL,'UnRead');

/*Table structure for table `pgrooms` */

DROP TABLE IF EXISTS `pgrooms`;

CREATE TABLE `pgrooms` (
  `PGID` int(11) NOT NULL AUTO_INCREMENT,
  `Capacity` tinyint(4) DEFAULT NULL,
  `Type` varchar(20) DEFAULT 'Normal',
  `Charges` int(11) DEFAULT 1000,
  `Description` varchar(100) DEFAULT 'In Line Short Description',
  `Services` varchar(1000) DEFAULT NULL,
  `UID` int(11) DEFAULT 1,
  `City` varchar(20) DEFAULT 'Delhi',
  `Address` varchar(200) DEFAULT 'No Address',
  `Status` varchar(20) DEFAULT 'Vacant',
  PRIMARY KEY (`PGID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `pgrooms` */

insert  into `pgrooms`(`PGID`,`Capacity`,`Type`,`Charges`,`Description`,`Services`,`UID`,`City`,`Address`,`Status`) values (1,5,'Normal',5000,'Desc Desc Desc Desc Desc Desc Desc Desc ','Cloth Washing,Room Cleaner,Geyser,AC',1,'Delhi','De lhiDel hiDelh iDelhiDelhiD elhi','Vacant'),(2,2,'Delux',2500,'In Line Short Description','Food,AC',2,'Delhi','No Address','Vacant');

/*Table structure for table `photos` */

DROP TABLE IF EXISTS `photos`;

CREATE TABLE `photos` (
  `PID` bigint(20) NOT NULL AUTO_INCREMENT,
  `FileName` varchar(25) DEFAULT NULL,
  `HID` bigint(20) DEFAULT 1,
  PRIMARY KEY (`PID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `photos` */

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `UID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `PWD` varchar(20) DEFAULT NULL,
  `Mobile` varchar(15) DEFAULT NULL,
  `Address` varchar(200) DEFAULT NULL,
  `Areas` varchar(30) DEFAULT 'No Area',
  `Status` varchar(10) DEFAULT 'Pending',
  `UserType` varchar(10) DEFAULT 'User',
  `DestinationCity` varchar(20) DEFAULT 'No City',
  `Gender` varchar(6) DEFAULT 'M',
  `ContributionAmount` int(11) DEFAULT 0,
  `Hobbies` varchar(100) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `DOB` date DEFAULT '2000-01-01',
  `MaritialStatus` varchar(10) DEFAULT 'Single',
  `ArrivalDate` date DEFAULT '2024-01-01',
  PRIMARY KEY (`UID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`UID`,`UserName`,`Email`,`PWD`,`Mobile`,`Address`,`Areas`,`Status`,`UserType`,`DestinationCity`,`Gender`,`ContributionAmount`,`Hobbies`,`CreatedOn`,`DOB`,`MaritialStatus`,`ArrivalDate`) values (1,'Mohit Sharma','admin@gmail.com','aa','9999999999','Address Address Address Address Address Address 226005','andheri','Active','User','Delhi','M',5000,'Music Lover,Movies,Party Animal,Smoking,Vegan Only','2012-05-07 21:53:48','2000-01-01','Single','2024-01-01'),(2,'Rishi Sharma','user@gmail.com','aa','7007502987',NULL,NULL,'Pending','HotelOwner','Delhi','F',10000,'NightOwl,Wanderer,PetLover,Smoking','2012-05-07 21:53:48','2000-01-01','Single','2024-01-01');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
