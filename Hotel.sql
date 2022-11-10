drop database Hotel;
create database if not exists Hotel;
use Hotel;


CREATE TABLE IF NOT EXISTS RoomType (
  TypeID INT UNSIGNED NOT NULL,
  RoomType TEXT(100) NOT NULL,
  Price INT NOT NULL,
  PRIMARY KEY (TypeID)
  )
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS HotelStaff (
  StaffID INT UNSIGNED NOT NULL,
  FIO VARCHAR(150) NOT NULL,
  Post VARCHAR(80) NOT NULL,
  Salary INT UNSIGNED NOT NULL,
  WorkSchedule VARCHAR(5) NOT NULL,
  PRIMARY KEY (StaffID)
  )
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS Rooms (
  RoomNum INT UNSIGNED NOT NULL,
  Places INT UNSIGNED NOT NULL,
  RoomFeatures MEDIUMTEXT NULL,
  Floor INT UNSIGNED NOT NULL,
  TypeID INT UNSIGNED NOT NULL,
  StaffID INT UNSIGNED NULL,
  RoomStatus VARCHAR(45) NOT NULL,
  PRIMARY KEY (RoomNum),
  CONSTRAINT `fk_Rooms_RoomType1`
    FOREIGN KEY (TypeID)
    REFERENCES RoomType (TypeID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT fk_Rooms_HotelStaff1
    FOREIGN KEY (StaffID)
    REFERENCES HotelStaff (StaffID)
    ON DELETE CASCADE
    ON UPDATE CASCADE
    )
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS Guests (
  PassportNum INT UNSIGNED NOT NULL,
  FIO VARCHAR(150) NOT NULL,
  Citizenship VARCHAR(45) NOT NULL,
  TypeGuest VARCHAR(45) NOT NULL,
  Discount INT NULL,
  PRIMARY KEY (PassportNum)
  )
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS Placement (
  RoomNum INT UNSIGNED NOT NULL,
  PassportNum INT UNSIGNED NOT NULL,
  SetDate DATETIME NOT NULL,
  DepartureDate DATETIME NULL,
  PRIMARY KEY (RoomNum, PassportNum),
  CONSTRAINT `fk_SettlingRoom_Rooms`
    FOREIGN KEY (RoomNum)
    REFERENCES Rooms (RoomNum)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_SettlingRoom_Guests1`
    FOREIGN KEY (PassportNum)
    REFERENCES Guests (PassportNum)
    ON DELETE CASCADE
    ON UPDATE CASCADE
    )
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS DailyAccounting (
  RoomNum INT UNSIGNED NOT NULL,
  ServiceDate DATETIME NOT NULL,
  ConditionRoom VARCHAR(45) NOT NULL, #Состояние комнаты
  Complaints MEDIUMTEXT NULL, #Жалобы
  ServicesRendered MEDIUMTEXT NOT NULL, #Оказанные услуги
  PRIMARY KEY (RoomNum),
  CONSTRAINT `fk_DailyAccounting_Rooms1`
    FOREIGN KEY (RoomNum)
    REFERENCES Rooms (RoomNum)
    ON DELETE CASCADE
    ON UPDATE CASCADE
    )
ENGINE = InnoDB;

insert RoomType (TypeID, RoomType, Price)
values
(1100, 'Однокомнатный', 4400),
(2200, 'Двухкомнатный', 5700),
(5601, 'Люкс', 6600),
(6301, 'Апартамент', 7400),
(2802, 'Семейный', 9800),
(3000, 'Сюит', 11200);

insert HotelStaff (StaffID, FIO, Post, Salary, WorkSchedule)
values
(1346, 'Фролова Милана Максимовна', 'Горничная', 25000, '5/2'),
(3401, 'Соболев Илья Максимович', 'Электрик', 40000, '5/2'),
(2678, 'Баранова Ева Ивановна', 'Горничная', 23000, '2/2'),
(7118, 'Коновалов Иван Миронович', 'Сантехник', 43000, '5/2'),
(3256, 'Козина Анна Романовна', 'Горничная', 27000, '3/1'),
(8693, 'Щербаков Тимур Иванович', 'Сантехник', 41000, '2/2'),
(5947, 'Родин Илья Александрович', 'Электрик', 45000, '3/1'),
(6891, 'Мальцев Михаил Артёмович', 'Электрик', 38000, '2/2'),
(9600, 'Артемов Александр Игоревич', 'Сантехник', 47000, '3/1');

insert Rooms (RoomNum, Places, RoomFeatures, Floor, TypeID, StaffID, RoomStatus)
values
(145, 2, null, 1, 1100, null, 'свободно'),
(201, 3, null, 2, 2200, null, 'свободно'),
(426, 2, 'Кабинет, телевизор', 4, 5601, null, 'занято'),
(351, 4, 'Телефизор, зона для игр', 3, 2802, 5947, 'обслуживается'),
(457, 3, 'Кухня, кабинет, телевизор', 4, 6301, null, 'занято'),
(505, 6, 'Гостинная, кабинет, телевизор, проводной интернет', 5, 3000, null, 'свободно'),
(137, 4, null, 1, 2200, 2678, 'обслуживается'),
(309, 2, 'Кабинет, телевизор', 4, 5601, null, 'свободно'),
(346, 4, 'Телефизор, зона для игр', 3, 2802, null, 'свободно'),
(161, 2, null, 1, 1100, 9600, 'обслуживается');

insert Guests (PassportNum, FIO, Citizenship, TypeGuest, Discount)
values
(866743, 'Максимова Агата Никитична', 'Россия', 'Обычный', null),
(491364, 'Семенов Макар Дмитриевич', 'Норвегия', 'Обычный', null),
(310846, 'Черкасов Захар Матвеевич', 'Индий', 'Постоянный', 800),
(738952, 'Лукьянова Стефания Дмитриевна', 'Греция', ' VIP', null),
(169003, 'Иванова Ева Львовна', 'Македония', 'Постоянный, VIP', 1300);

insert Placement (RoomNum, PassportNum, SetDate, DepartureDate)
values
(201, 866743, '2022-01-27 16:23:56', null),
(145, 491364, '2022-03-13 10:01:34', '2022-03-14 09:14:08'),
(505, 169003, '2022-02-08 13:22:11', null),
(346, 738952, '2022-03-17 17:11:29', '2022-03-20 17:00:21'),
(309, 310846, '2022-01-04 06:45:57', null);

insert DailyAccounting (RoomNum, ServiceDate, ConditionRoom, Complaints, ServicesRendered)
values
(351, '2022-03-18 18:13:41', 'обслуживается', 'Грязно, протекает труба в ванной', 'Убрано, починка'),
(137, '2022-03-21 20:43;12', 'обслуживается', 'Не работает кондиционер', 'Починка'),
(161, '2022-03-19 09:03:22', 'обслуживается', 'Грязно', 'Уборка');



DELIMITER //
CREATE TRIGGER insertStatusNumber AFTER INSERT ON DailyAccounting
FOR EACH ROW 
BEGIN
	if NEW.ConditionRoom = 'обслуживается' then
		UPDATE Rooms Set RoomStatus = 'обслуживается' where RoomNum = NEW.RoomNum;
	END IF;
END 
// DELIMITER ;


DELIMITER //
CREATE TRIGGER updateStatusNumber AFTER UPDATE ON DailyAccounting
FOR EACH ROW 
BEGIN
	if NEW.ConditionRoom = 'обслужено' then
		UPDATE Rooms Set RoomStatus = 'свободно' where RoomNum = NEW.RoomNum;
	END IF;
END 
// DELIMITER ;


DELIMITER //
CREATE TRIGGER deleteStatusNumber BEFORE DELETE ON DailyAccounting
FOR EACH ROW 
BEGIN
	UPDATE Rooms Set RoomStatus = 'свободно' where RoomNum = OLD.RoomNum;
END 
// DELIMITER ;

#UPDATE DailyAccounting SET ConditionRoom = 'обслужено' where RoomNum = 161;

-- SELECT * FROM Placement where (RoomNum between 100 and 310) and 
--                        (PassportNum between 0 and 2000000) and (SetDate between '2022-01-01' and '2122-01-01');

-- Select * from Rooms where (RoomNum between 100 and 400) 
-- and (TypeID between 1000 and 3000) and Places = '4'
-- Order by TypeID;

#select* from Placement where SetDate LIKE '2022-01-27%';

#DELETE FROM HotelStaff where StaffID = 5947;
#INSERT INTO Placement values (161, 310846, '2022-05-27', null);

select* from RoomType;
select* from HotelStaff;
select* from Rooms;
select* from Guests;
select* from Placement;
select* from DailyAccounting;