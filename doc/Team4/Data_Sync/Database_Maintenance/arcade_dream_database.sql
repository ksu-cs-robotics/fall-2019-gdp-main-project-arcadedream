-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 18, 2019 at 03:21 AM
-- Server version: 10.1.37-MariaDB
-- PHP Version: 7.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `arcadedream`
--

-- --------------------------------------------------------

--
-- Table structure for table `playerlist`
--

CREATE TABLE `playerlist` (
  `ID` int(11) NOT NULL,
  `Username` varchar(16) NOT NULL,
  `UserPassword` varchar(255) NOT NULL,
  `Coins` int(11) NOT NULL,
  `Total_Points` int(11) NOT NULL,
  `Ava` int(11) DEFAULT NULL,
  `HatID` int(11) DEFAULT NULL,
  `ShirtID` int(11) DEFAULT NULL,
  `PantsID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `subgame1`
--

CREATE TABLE `subgame1` (
  `GameTime` decimal(16,0) NOT NULL,
  `Highscore` int(11) DEFAULT NULL,
  `Username` varchar(16) DEFAULT NULL,
  `ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `subgame2`
--

CREATE TABLE `subgame2` (
  `GameTime` decimal(16,0) NOT NULL,
  `Highscore` int(11) DEFAULT NULL,
  `Username` varchar(16) DEFAULT NULL,
  `ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `subgame3`
--

CREATE TABLE `subgame3` (
  `GameTime` decimal(16,0) NOT NULL,
  `Highscore` int(11) DEFAULT NULL,
  `Username` varchar(16) DEFAULT NULL,
  `ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `subgame4`
--

CREATE TABLE `subgame4` (
  `GameTime` decimal(16,0) NOT NULL,
  `Highscore` int(11) DEFAULT NULL,
  `Username` varchar(16) DEFAULT NULL,
  `ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `playerlist`
--
ALTER TABLE `playerlist`
  ADD PRIMARY KEY (`ID`,`Username`);

--
-- Indexes for table `subgame1`
--
ALTER TABLE `subgame1`
  ADD PRIMARY KEY (`GameTime`),
  ADD KEY `ID` (`ID`,`Username`);

--
-- Indexes for table `subgame2`
--
ALTER TABLE `subgame2`
  ADD PRIMARY KEY (`GameTime`),
  ADD KEY `ID` (`ID`,`Username`);

--
-- Indexes for table `subgame3`
--
ALTER TABLE `subgame3`
  ADD PRIMARY KEY (`GameTime`),
  ADD KEY `ID` (`ID`,`Username`);

--
-- Indexes for table `subgame4`
--
ALTER TABLE `subgame4`
  ADD PRIMARY KEY (`GameTime`),
  ADD KEY `ID` (`ID`,`Username`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `subgame1`
--
ALTER TABLE `subgame1`
  ADD CONSTRAINT `subgame1_ibfk_1` FOREIGN KEY (`ID`,`Username`) REFERENCES `playerlist` (`ID`, `Username`);

--
-- Constraints for table `subgame2`
--
ALTER TABLE `subgame2`
  ADD CONSTRAINT `subgame2_ibfk_1` FOREIGN KEY (`ID`,`Username`) REFERENCES `playerlist` (`ID`, `Username`);

--
-- Constraints for table `subgame3`
--
ALTER TABLE `subgame3`
  ADD CONSTRAINT `subgame3_ibfk_1` FOREIGN KEY (`ID`,`Username`) REFERENCES `playerlist` (`ID`, `Username`);

--
-- Constraints for table `subgame4`
--
ALTER TABLE `subgame4`
  ADD CONSTRAINT `subgame4_ibfk_1` FOREIGN KEY (`ID`,`Username`) REFERENCES `playerlist` (`ID`, `Username`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
