CREATE DATABASE  IF NOT EXISTS `kpadb_ptxt` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `kpadb_ptxt`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: kpadb_ptxt
-- ------------------------------------------------------
-- Server version	5.6.38-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `db_company_account`
--

DROP TABLE IF EXISTS `db_company_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `db_company_account` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `company_guid` varchar(100) DEFAULT NULL,
  `company_name` varchar(100) DEFAULT NULL,
  `company_type` varchar(100) DEFAULT NULL,
  `company_email` varchar(50) DEFAULT NULL,
  `company_mobile` varchar(20) DEFAULT NULL,
  `company_status` tinyint(4) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `db_company_account`
--

LOCK TABLES `db_company_account` WRITE;
/*!40000 ALTER TABLE `db_company_account` DISABLE KEYS */;
INSERT INTO `db_company_account` VALUES (1,'f4469ab9-8fdd-49ff-8e49-313ddf1abf86','PollyStore','Local Business','pollystore.a@gmail.com','09995233848',2,'2018-02-01 20:26:27','2018-02-01 20:26:27'),(2,'df2d4cde-b03d-4a82-82ff-20349877f1e0','PollyLoad','Product/Service','pollystore.a@gmail.com','09177715380',2,'2018-02-01 20:26:27','2018-02-01 20:26:27'),(3,'e53a1c37-71d6-45b7-8ba9-c8b731f63385','LoadStop','Product/Service','','09177715380',2,'2018-02-01 20:26:27','2018-02-01 20:26:27');
/*!40000 ALTER TABLE `db_company_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ptxt_cache`
--

DROP TABLE IF EXISTS `ptxt_cache`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ptxt_cache` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `company_uid_` int(11) DEFAULT NULL,
  `from_` varchar(45) DEFAULT NULL,
  `message_` text,
  `status_` tinyint(4) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ptxt_cache`
--

LOCK TABLES `ptxt_cache` WRITE;
/*!40000 ALTER TABLE `ptxt_cache` DISABLE KEYS */;
/*!40000 ALTER TABLE `ptxt_cache` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ptxt_cache_data`
--

DROP TABLE IF EXISTS `ptxt_cache_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ptxt_cache_data` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `company_uid_` int(11) DEFAULT NULL,
  `from_` varchar(45) DEFAULT NULL,
  `message_` text,
  `status_` tinyint(4) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ptxt_cache_data`
--

LOCK TABLES `ptxt_cache_data` WRITE;
/*!40000 ALTER TABLE `ptxt_cache_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `ptxt_cache_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ptxt_queued`
--

DROP TABLE IF EXISTS `ptxt_queued`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ptxt_queued` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Company_uid` int(11) DEFAULT NULL,
  `UserId` varchar(11) DEFAULT NULL,
  `UserIp` varchar(20) DEFAULT NULL,
  `ToNumber` varchar(11) DEFAULT NULL,
  `ToMessage` longtext,
  `Status` tinyint(4) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ptxt_queued`
--

LOCK TABLES `ptxt_queued` WRITE;
/*!40000 ALTER TABLE `ptxt_queued` DISABLE KEYS */;
/*!40000 ALTER TABLE `ptxt_queued` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ptxt_requestmpin`
--

DROP TABLE IF EXISTS `ptxt_requestmpin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ptxt_requestmpin` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `from_` varchar(11) DEFAULT NULL,
  `reference_no` varchar(50) DEFAULT NULL,
  `amount` decimal(18,2) DEFAULT NULL,
  `status_` tinyint(4) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ptxt_requestmpin`
--

LOCK TABLES `ptxt_requestmpin` WRITE;
/*!40000 ALTER TABLE `ptxt_requestmpin` DISABLE KEYS */;
/*!40000 ALTER TABLE `ptxt_requestmpin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ptxt_sentitems`
--

DROP TABLE IF EXISTS `ptxt_sentitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ptxt_sentitems` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Company_uid` int(11) DEFAULT NULL,
  `UserId` varchar(11) DEFAULT NULL,
  `UserIp` varchar(20) DEFAULT NULL,
  `ToNumber` varchar(11) DEFAULT NULL,
  `ToMessage` longtext,
  `Status` tinyint(4) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ptxt_sentitems`
--

LOCK TABLES `ptxt_sentitems` WRITE;
/*!40000 ALTER TABLE `ptxt_sentitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `ptxt_sentitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ptxt_to_be_queued`
--

DROP TABLE IF EXISTS `ptxt_to_be_queued`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ptxt_to_be_queued` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Company_uid` int(11) DEFAULT NULL,
  `UserId` varchar(11) DEFAULT NULL,
  `UserIp` varchar(20) DEFAULT NULL,
  `ToNumber` varchar(11) DEFAULT NULL,
  `ToMessage` longtext,
  `Status` tinyint(4) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ptxt_to_be_queued`
--

LOCK TABLES `ptxt_to_be_queued` WRITE;
/*!40000 ALTER TABLE `ptxt_to_be_queued` DISABLE KEYS */;
/*!40000 ALTER TABLE `ptxt_to_be_queued` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-03-08 23:14:19
