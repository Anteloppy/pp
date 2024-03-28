-- Создание базы данных
CREATE DATABASE IF NOT EXISTS employees_database;
USE employees_database;

-- Таблица "Персональные карточки сотрудников"
create table if not exists personal_cards (
	int_person int auto_increment primary key,
    last_name varchar(55),
    name varchar(55),
    surname varchar(55),
    birthdate date,
    region_id int,
    district_id int,
    settlement_id int,
    street_id int,
    house_number varchar(20),
    flat_number varchar(10),
    bank_id int,
    bank_account varchar(20),
    INN varchar(12),
    SNILS varchar(14),
    employment_date date,
    dismissal_date date,
    work_schedule_id int
);

-- Таблица "Структурные подразделения"
create table if not exists departments (
    id_department int auto_increment primary key,
    department_name varchar(255)
);

-- Таблица "Должности"
create table if not exists positions (
    id_position int auto_increment primary key,
    position_name varchar(255)
);

-- Таблица "Структура"
create table if not exists structure (
    ## id_department int auto_increment primary key,
    id_position int,
    salary decimal(10,2),
    bonus decimal(10,2)
);

-- Таблица "Справочник графиков работы"
create table if not exists work_schedules (
    id_work_schedule int auto_increment primary key,
    schedule_name varchar(255),
    working_days int,
    days_off int,
    working_hours_per_day int
);

-- Таблица "Контактная информация сотрудников"
create table if not exists contact_info (
    ## id_person int auto_increment primary key,
    id_department int,
    id_position int,
    mobile_phone varchar(20),
    landline_phone varchar(20),
    email varchar(255),
    id_supervisor int
);

-- Таблица "Справочник регионов"
create table if not exists regions (
    id_region int auto_increment primary key,
    region_name varchar(255)
);

-- Таблица "Справочник городов"
create table if not exists cities (
    id_city int auto_increment primary key,
    city_name varchar(255),
    id_region int
);

-- Таблица "Справочник улиц"
create table if not exists streets (
    id_street int auto_increment primary key,
    street_name varchar(255)
);

-- Таблица "Справочник курсов повышения квалификации"
create table if not exists qualification_courses (
    id_course int auto_increment primary key,
    course_name varchar(255)
);

-- Таблица "Журнал прохождения курсов повышения квалификации"
create table if not exists qualification_course_journal (
    ## id_course int auto_increment primary key,
    id_person int,
    start_date date,
    end_date date,
    result varchar(20),
    status varchar(20),
    notes varchar(255),
    certificate_number varchar(20),
    certificate_date date
);

-- Таблица "Кадровый резерв"
create table if not exists personnel_reserve (
    ## id_person int auto_increment primary key,
    id_position int,
    id_department int,
    reserve_entry_date date,
    reserve_exit_date date,
    reserve_status varchar(20),
    new_position int
);

-- Таблица "Справочник банков"
create table if not exists banks (
    id_bank int auto_increment primary key,
    bank_name varchar(255)
);



-- Добавление данных в таблицу "Employee Personal Cards"
insert into personal_cards (id_person, LastName, FirstName, SecondName, birthdate, id_region, id_district, id_settlement, id_street, house_number, flat_number, bank_id, bank_account, INN, SNILS, employment_date, dismissal_date, id_work_schedule) 
values (1, 'Smith', 'John', 'Adam', '1990-05-15', 1, 2, 3, 4, '123', '10', 1, '1234567890123456', '123456789012', '12345678901234', '2020-01-01', null, 1);

-- Добавление данных в таблицу "Departments"
insert into departments (id_department, department_name) values (1, 'Human Resources');

-- Добавление данных в таблицу "Positions"
insert into positions (id_position, position_name) values (1, 'HR Manager');

-- Добавление данных в таблицу "Structure"
insert into structure (id_department, id_position, salary, bonus) values (1, 1, 50000.00, 2000.00);

-- Добавление данных в таблицу "Work Schedules Dictionary"
insert into work_schedules (id_work_schedule, schedule_name, working_days, days_off, working_hours_per_day) values (1, 'Standard', 5, 2, 8);

-- Добавление данных в таблицу "Employee Contact Information"
insert into contact_info (id_person, id_department, id_position, mobile_phone, landline_phone, email, id_supervisor)
values (1, 1, 1, '1234567890', '9876543210', 'john.smith@example.com', null);

-- Добавление данных в таблицу "Regions Dictionary"
insert into regions (id_region, region_name) values (1, 'North Region');

-- Добавление данных в таблицу "Cities Dictionary"
insert into cities (id_city, city_name, id_region) values (1, 'Cityville', 1);

-- Добавление данных в таблицу "Streets Dictionary"
insert into streets (id_street, street_name) values (1, 'Main Street');

-- Добавление данных в таблицу "Qualification Courses Dictionary"
insert into qualification_courses (id_course, course_name) values (1, 'Leadership Training');

-- Добавление данных в таблицу "Qualification Courses Completion Journal"
insert into qualification_course_journal (id_course, id_person, start_date, end_date, result, status, notes, certificate_number, certificate_date)
values (1, 1, '2020-03-01', '2020-03-15', 'Passed', 'Completed', 'Good performance', '12345', '2020-04-01');

-- Добавление данных в таблицу "Personnel Reserve"
insert into personnel_reserve (id_person, id_position, id_department, reserve_entry_date, reserve_exit_date, reserve_status, new_position)
values (1, 1, 1, '2021-01-01', null, 'Active', null);

-- Добавление данных в таблицу "Banks Dictionary"
insert into banks (id_bank, bank_name) values (1, 'ABC Bank');





