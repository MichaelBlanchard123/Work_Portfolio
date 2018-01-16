-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 11, 2018 at 09:26 PM
-- Server version: 5.7.16-log
-- PHP Version: 7.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `money_owing_database`
--

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE `transactions` (
  `transaction_id` int(11) NOT NULL,
  `transaction_desc` varchar(70) NOT NULL,
  `transaction_amount` decimal(13,2) NOT NULL,
  `transaction_type` varchar(10) NOT NULL,
  `user_user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `transactions`
--

INSERT INTO `transactions` (`transaction_id`, `transaction_desc`, `transaction_amount`, `transaction_type`, `user_user_id`) VALUES
(1, 'Car Repair Bill', '200.50', 'Payable', 7),
(2, 'Loaned money to Kevin', '100.00', 'Receivable', 18),
(3, 'Bought Monitors', '700.99', 'Payable', 7),
(4, 'Owe money to Ryan', '350.00', 'Payable', 7),
(5, 'Sued by losers', '7000000.00', 'Payable', 18),
(6, 'Won contest', '200.69', 'Receivable', 7),
(7, 'Tax Return', '576.23', 'Receivable', 18),
(8, 'Went Shopping', '450.00', 'Payable', 7),
(9, 'purus sapien, gravida', '370.30', 'Resolved', 2),
(10, 'mauris blandit mattis.', '672.08', 'Resolved', 2),
(11, 'mauris a nunc.', '991.55', 'Payable', 2),
(12, 'euismod et, commodo', '601.72', 'Payable', 5),
(13, 'tempus eu, ligula.', '831.52', 'Receivable', 1),
(14, 'Class aptent taciti', '36.37', 'Payable', 3),
(15, 'quis massa. Mauris', '145.14', 'Resolved', 1),
(16, 'vestibulum lorem, sit', '124.60', 'Receivable', 5),
(17, 'Phasellus libero mauris,', '195.59', 'Payable', 3),
(18, 'Donec tempus, lorem', '777.23', 'Payable', 8),
(19, 'pede ac urna.', '856.01', 'Resolved', 2),
(20, 'sapien. Cras dolor', '60.77', 'Receivable', 10),
(21, 'molestie in, tempus', '902.92', 'Resolved', 3),
(22, 'cursus et, eros.', '280.87', 'Payable', 10),
(23, 'eget ipsum. Suspendisse', '740.96', 'Receivable', 6),
(24, 'fringilla est. Mauris', '237.94', 'Resolved', 1),
(25, 'urna, nec luctus', '926.44', 'Receivable', 4),
(26, 'imperdiet non, vestibulum', '397.83', 'Resolved', 8),
(27, 'enim. Etiam imperdiet', '358.41', 'Payable', 3),
(28, 'hendrerit consectetuer, cursus', '189.97', 'Payable', 1),
(29, 'vel, venenatis vel,', '465.97', 'Receivable', 7),
(30, 'ac risus. Morbi', '14.58', 'Receivable', 9),
(31, 'vel lectus. Cum', '961.84', 'Receivable', 10),
(32, 'mauris. Integer sem', '984.28', 'Resolved', 1),
(33, 'turpis egestas. Fusce', '252.54', 'Payable', 1),
(34, 'habitant morbi tristique', '984.03', 'Payable', 4),
(35, 'enim. Etiam gravida', '334.69', 'Resolved', 9),
(36, 'mollis vitae, posuere', '412.61', 'Receivable', 5),
(37, 'vitae mauris sit', '927.22', 'Receivable', 6),
(38, 'ornare egestas ligula.', '491.18', 'Payable', 4),
(39, 'neque. Morbi quis', '654.58', 'Resolved', 8),
(40, 'est, congue a,', '942.53', 'Payable', 2),
(41, 'eget mollis lectus', '526.35', 'Receivable', 1),
(42, 'vehicula. Pellentesque tincidunt', '849.39', 'Payable', 1),
(43, 'ac mattis semper,', '123.39', 'Resolved', 7),
(44, 'In nec orci.', '886.00', 'Payable', 10),
(45, 'ornare, lectus ante', '500.65', 'Resolved', 1),
(46, 'pede blandit congue.', '454.51', 'Payable', 8),
(47, 'tellus sem mollis', '337.11', 'Resolved', 2),
(48, 'Aenean eget magna.', '85.19', 'Receivable', 10),
(49, 'Proin eget odio.', '105.18', 'Receivable', 5),
(50, 'tempor diam dictum', '483.79', 'Payable', 2),
(51, 'Morbi sit amet', '268.69', 'Resolved', 8),
(52, 'dolor vitae dolor.', '216.64', 'Payable', 6),
(53, 'neque tellus, imperdiet', '470.38', 'Resolved', 4),
(54, 'tristique senectus et', '886.80', 'Payable', 5),
(55, 'varius et, euismod', '517.05', 'Payable', 3),
(56, 'ligula consectetuer rhoncus.', '546.15', 'Resolved', 2),
(57, 'vulputate dui, nec', '648.21', 'Resolved', 4),
(58, 'luctus aliquet odio.', '674.68', 'Resolved', 3),
(59, 'Sed diam lorem,', '319.28', 'Resolved', 7),
(60, 'sollicitudin commodo ipsum.', '454.04', 'Resolved', 1),
(61, 'vestibulum lorem, sit', '701.94', 'Receivable', 7),
(62, 'gravida non, sollicitudin', '896.53', 'Receivable', 3),
(63, 'auctor quis, tristique', '889.01', 'Resolved', 4),
(64, 'dui, nec tempus', '988.59', 'Receivable', 10),
(65, 'fringilla. Donec feugiat', '326.55', 'Payable', 2),
(66, 'ipsum. Donec sollicitudin', '47.16', 'Receivable', 3),
(67, 'enim. Etiam gravida', '171.12', 'Resolved', 6),
(68, 'dictum sapien. Aenean', '597.04', 'Resolved', 9),
(69, 'Vivamus sit amet', '496.68', 'Payable', 8),
(70, 'euismod ac, fermentum', '351.50', 'Receivable', 4),
(71, 'at augue id', '560.90', 'Receivable', 3),
(72, 'sociis natoque penatibus', '700.40', 'Payable', 4),
(73, 'porttitor eros nec', '568.40', 'Payable', 9),
(74, 'Nulla dignissim. Maecenas', '688.04', 'Resolved', 3),
(75, 'ut cursus luctus,', '620.92', 'Resolved', 5),
(76, 'eu odio tristique', '66.11', 'Resolved', 7),
(77, 'sit amet ultricies', '959.00', 'Receivable', 10),
(78, 'eu metus. In', '634.47', 'Payable', 3),
(79, 'orci luctus et', '795.40', 'Payable', 6),
(80, 'purus, accumsan interdum', '333.01', 'Resolved', 3),
(81, 'mi felis, adipiscing', '784.00', 'Payable', 3),
(82, 'dolor. Nulla semper', '655.36', 'Receivable', 1),
(83, 'Cras eget nisi', '134.27', 'Receivable', 3),
(84, 'ante. Nunc mauris', '500.69', 'Receivable', 10),
(85, 'est ac mattis', '109.67', 'Payable', 7),
(86, 'Curabitur vel lectus.', '317.16', 'Resolved', 2),
(87, 'nec, diam. Duis', '360.74', 'Payable', 7),
(88, 'eget lacus. Mauris', '196.87', 'Payable', 7),
(89, 'vitae odio sagittis', '367.38', 'Resolved', 10),
(90, 'vel turpis. Aliquam', '545.95', 'Resolved', 9),
(91, 'lectus. Cum sociis', '875.83', 'Payable', 1),
(92, 'eget tincidunt dui', '163.29', 'Resolved', 1),
(93, 'Suspendisse eleifend. Cras', '792.83', 'Resolved', 5),
(94, 'dictum. Proin eget', '364.70', 'Receivable', 1),
(95, 'Duis sit amet', '80.57', 'Receivable', 9),
(96, 'Quisque nonummy ipsum', '157.66', 'Receivable', 10),
(97, 'pharetra, felis eget', '999.64', 'Resolved', 10),
(98, 'mollis non, cursus', '323.14', 'Payable', 5),
(99, 'ornare lectus justo', '263.47', 'Receivable', 4),
(100, 'pretium et, rutrum', '903.63', 'Receivable', 6),
(101, 'tellus non magna.', '735.78', 'Resolved', 6),
(102, 'Mauris non dui', '84.55', 'Receivable', 1),
(102, 'vitae, posuere at,', '524.53', 'Payable', 7),
(103, 'sed, sapien. Nunc', '417.54', 'Receivable', 4),
(103, 'consectetuer adipiscing elit.', '574.32', 'Resolved', 7),
(104, 'Vivamus nibh dolor,', '308.45', 'Payable', 5),
(104, 'nulla. Integer urna.', '133.35', 'Resolved', 8),
(105, 'rutrum urna, nec', '911.38', 'Payable', 4),
(105, 'eu, eleifend nec,', '472.18', 'Payable', 5),
(106, 'justo eu arcu.', '881.30', 'Resolved', 7),
(106, 'Sed dictum. Proin', '56.24', 'Payable', 8),
(107, 'ultricies sem magna', '350.13', 'Receivable', 5),
(107, 'Cras interdum. Nunc', '964.90', 'Payable', 10),
(108, 'vel arcu eu', '174.68', 'Resolved', 3),
(108, 'Lorem ipsum dolor', '766.64', 'Payable', 5),
(109, 'odio. Etiam ligula', '866.38', 'Receivable', 3),
(110, 'ac libero nec', '833.29', 'Resolved', 2),
(111, 'Aenean gravida nunc', '899.71', 'Payable', 2),
(112, 'eu, ligula. Aenean', '859.36', 'Resolved', 10),
(113, 'neque sed sem', '825.15', 'Payable', 10),
(114, 'dui. Fusce diam', '212.35', 'Resolved', 1),
(115, 'per inceptos hymenaeos.', '605.71', 'Payable', 7),
(116, 'Nulla eget metus', '851.88', 'Receivable', 10),
(117, 'lacinia mattis. Integer', '348.55', 'Payable', 4),
(118, 'dolor egestas rhoncus.', '418.38', 'Resolved', 4),
(119, 'faucibus ut, nulla.', '162.50', 'Resolved', 7),
(120, 'semper rutrum. Fusce', '709.09', 'Payable', 5),
(121, 'in faucibus orci', '434.43', 'Resolved', 9),
(122, 'felis. Donec tempor,', '412.76', 'Resolved', 7),
(123, 'pharetra, felis eget', '671.41', 'Payable', 1),
(124, 'eu tellus eu', '167.33', 'Payable', 7),
(125, 'dui, semper et,', '301.75', 'Payable', 4),
(126, 'nisl arcu iaculis', '848.55', 'Payable', 10),
(127, 'rhoncus. Proin nisl', '516.46', 'Resolved', 2),
(128, 'orci. Ut semper', '393.02', 'Receivable', 1),
(129, 'lobortis, nisi nibh', '267.11', 'Resolved', 1),
(130, 'libero est, congue', '409.67', 'Receivable', 8),
(131, 'non, luctus sit', '494.81', 'Payable', 8),
(132, 'neque sed dictum', '970.45', 'Payable', 1),
(133, 'egestas rhoncus. Proin', '530.80', 'Payable', 4),
(134, 'aliquam eros turpis', '850.81', 'Payable', 3),
(135, 'Ut semper pretium', '35.30', 'Receivable', 4),
(136, 'ac turpis egestas.', '278.61', 'Payable', 6),
(137, 'diam nunc, ullamcorper', '437.40', 'Receivable', 3),
(138, 'gravida. Aliquam tincidunt,', '138.13', 'Resolved', 5),
(139, 'Cum sociis natoque', '325.63', 'Resolved', 2),
(140, 'Nunc ullamcorper, velit', '516.96', 'Resolved', 3),
(141, 'commodo auctor velit.', '56.81', 'Receivable', 10),
(142, 'mi, ac mattis', '707.46', 'Payable', 1),
(143, 'Praesent luctus. Curabitur', '612.37', 'Receivable', 5),
(144, 'non justo. Proin', '172.11', 'Payable', 9),
(145, 'vel, faucibus id,', '554.42', 'Resolved', 10),
(146, 'ut eros non', '182.02', 'Receivable', 5),
(147, 'Vivamus nisi. Mauris', '556.88', 'Resolved', 4),
(148, 'est ac mattis', '226.88', 'Payable', 1),
(149, 'diam. Proin dolor.', '360.26', 'Payable', 1),
(150, 'tristique senectus et', '936.47', 'Resolved', 5),
(151, 'tincidunt tempus risus.', '780.79', 'Payable', 5),
(152, 'laoreet ipsum. Curabitur', '581.21', 'Payable', 10),
(153, 'porttitor interdum. Sed', '258.00', 'Resolved', 5),
(154, 'dolor dolor, tempus', '111.30', 'Resolved', 6),
(155, 'libero. Proin mi.', '537.20', 'Resolved', 9),
(156, 'cursus non, egestas', '528.62', 'Resolved', 8),
(157, 'augue ut lacus.', '213.44', 'Resolved', 10),
(158, 'lorem, luctus ut,', '977.53', 'Payable', 10),
(159, 'at pretium aliquet,', '191.70', 'Payable', 1),
(160, 'natoque penatibus et', '832.32', 'Payable', 8),
(161, 'ante dictum cursus.', '544.24', 'Receivable', 5),
(162, 'cubilia Curae; Donec', '219.40', 'Payable', 5),
(163, 'nonummy ipsum non', '768.85', 'Receivable', 2),
(164, 'Nullam suscipit, est', '645.56', 'Receivable', 4),
(165, 'sapien molestie orci', '753.36', 'Resolved', 8),
(166, 'vel, vulputate eu,', '63.90', 'Resolved', 9),
(167, 'felis. Nulla tempor', '455.81', 'Receivable', 7),
(168, 'dui. Cras pellentesque.', '702.73', 'Payable', 1),
(169, 'ante blandit viverra.', '749.21', 'Payable', 7),
(170, 'nostra, per inceptos', '181.87', 'Payable', 4),
(171, 'ac metus vitae', '191.58', 'Receivable', 8),
(172, 'amet orci. Ut', '630.27', 'Payable', 7),
(173, 'fermentum convallis ligula.', '620.42', 'Resolved', 10),
(174, 'egestas. Aliquam nec', '840.60', 'Receivable', 1),
(175, 'cursus, diam at', '434.49', 'Payable', 9),
(176, 'varius. Nam porttitor', '677.74', 'Receivable', 4),
(177, 'dolor egestas rhoncus.', '205.27', 'Resolved', 3),
(178, 'pharetra sed, hendrerit', '191.91', 'Receivable', 7),
(179, 'quis arcu vel', '946.16', 'Resolved', 6),
(180, 'mi enim, condimentum', '563.63', 'Receivable', 10),
(181, 'Donec tincidunt. Donec', '850.75', 'Receivable', 9),
(182, 'Integer vulputate, risus', '101.69', 'Receivable', 9),
(183, 'condimentum eget, volutpat', '408.81', 'Resolved', 7),
(184, 'ullamcorper, nisl arcu', '80.82', 'Resolved', 5),
(185, 'eget laoreet posuere,', '499.43', 'Resolved', 7),
(186, 'risus. Quisque libero', '947.13', 'Payable', 4),
(187, 'enim nisl elementum', '34.01', 'Payable', 8),
(188, 'purus. Maecenas libero', '527.69', 'Payable', 7),
(189, 'amet lorem semper', '364.04', 'Resolved', 9),
(190, 'neque sed dictum', '463.68', 'Resolved', 6),
(191, 'amet lorem semper', '127.71', 'Receivable', 6),
(192, 'tortor at risus.', '801.38', 'Receivable', 9),
(193, 'ligula. Aenean gravida', '444.28', 'Payable', 7),
(194, 'dignissim tempor arcu.', '566.36', 'Receivable', 4),
(195, 'gravida mauris ut', '389.74', 'Payable', 8),
(196, 'pellentesque massa lobortis', '980.79', 'Payable', 4),
(197, 'neque non quam.', '509.77', 'Resolved', 3),
(198, 'Aliquam nec enim.', '573.44', 'Payable', 4),
(199, 'enim non nisi.', '110.54', 'Payable', 8),
(200, 'senectus et netus', '133.01', 'Payable', 2);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `user_first_name` varchar(40) NOT NULL,
  `user_last_name` varchar(40) NOT NULL,
  `user_email` varchar(75) NOT NULL,
  `user_password` varchar(32) NOT NULL,
  `user_date_of_birth` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`user_id`, `user_first_name`, `user_last_name`, `user_email`, `user_password`, `user_date_of_birth`) VALUES
(0, 'Admin', 'Admin', 'admin@system.com', '36f75dba5279680ab964f3d89eeb084c', '1970-01-01'),
(1, 'Goerge', 'Dummast', 'Goaehge.Habust@gmail.com', '36f75dba5279680ab964f3d89eeb084c', '2016-03-15'),
(2, 'Benedict', 'Greer', 'netus@Aenean.edu', '73aa709db15d00c4341fc9bf618dd7d1', '2018-06-26'),
(3, 'Dylan', 'Barr', 'Ut.nec.urna@nonduinec.org', '73aa709db15d00c4341fc9bf618dd7d1', '2017-09-09'),
(4, 'Caleb', 'Mcguire', 'convallis@estMauris.net', '73aa709db15d00c4341fc9bf618dd7d1', '2017-07-27'),
(5, 'Joshua', 'Galloway', 'lacinia.mattis.Integer@eueros.com', '73aa709db15d00c4341fc9bf618dd7d1', '2018-01-14'),
(6, 'Blake', 'Martin', 'malesuada@velnisl.org', '73aa709db15d00c4341fc9bf618dd7d1', '2017-06-23'),
(7, 'John', 'Italien', 'Johnny@gmail.com', '73aa709db15d00c4341fc9bf618dd7d1', '2017-12-26'),
(8, 'Uriah', 'Casey', 'semper.egestas@magnisdisparturient.net', '73aa709db15d00c4341fc9bf618dd7d1', '2018-05-22'),
(9, 'Lucas', 'Gilbert', 'libero@dapibusid.ca', '36f75dba5279680ab964f3d89eeb084c', '2018-03-28'),
(10, 'Matthew', 'Sullivan', 'justo@egetvolutpat.co.uk', '73aa709db15d00c4341fc9bf618dd7d1', '2018-02-12'),
(11, 'Marsden', 'Cummings', 'dignissim.pharetra.Nam@nec.edu', '73aa709db15d00c4341fc9bf618dd7d1', '2017-08-11'),
(18, 'Michael', 'Blanchard', 'Michaelblanchard123@gmail.com', '73aa709db15d00c4341fc9bf618dd7d1', '1995-01-04'),
(20, 'Bruno', 'Elliott', 'Donec.vitae@scelerisqueduiSuspendisse.edu', '73aa709db15d00c4341fc9bf618dd7d1', '2017-08-26');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`transaction_id`,`user_user_id`),
  ADD KEY `fk_transactions_user_idx` (`user_user_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `transactions`
--
ALTER TABLE `transactions`
  MODIFY `transaction_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=201;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `transactions`
--
ALTER TABLE `transactions`
  ADD CONSTRAINT `fk_transactions_user` FOREIGN KEY (`user_user_id`) REFERENCES `user` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
