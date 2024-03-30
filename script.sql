-- Создание базы данных
create database if not exists employees_database;
use employees_database;

#drop database employees_database; commit;
#contact_info, personnel_reserve, personal_cards, addreses, cities, structure, raions

-- Справочники:
-- Таблица "Справочник структурных подразделений"
create table if not exists departments (
    id_department int auto_increment primary key,
    department_name varchar(255)
);

-- Таблица "Справочник должностей"
create table if not exists positions (
    id_position int auto_increment primary key,
    position_name varchar(255)
);

-- Таблица "Справочник графиков работы"
create table if not exists work_schedules (
    id_work_schedule int auto_increment primary key,
    schedule_name varchar(255),
    working_days int,
    days_off int,
    working_hours_per_day int
);

-- Таблица "Справочник регионов"
create table if not exists regions (
    id_region int auto_increment primary key,
    region_name varchar(255)
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

-- Таблица "Справочник банков"
create table if not exists banks (
    id_bank int auto_increment primary key,
    bank_name varchar(255)
);

-- Таблицы с внешними ключами:
#FK \/
-- Таблица "Районы"
create table if not exists districts (
    id_district int auto_increment primary key,
    district_name varchar(255),
    region_id int,
    foreign key (region_id) references regions(id_region)
);

#FK \/
-- Таблица "Города"
create table if not exists cities (
    id_city int auto_increment primary key,
    city_name varchar(255),
    district_id int,
    foreign key (district_id) references districts(id_district)
);

#FK \/
-- Таблица "Адреса"
create table if not exists addresses (
	id_address int auto_increment primary key,
    city_id int,
    street_id int,
    house_number varchar(5),
    flat_number int,
    foreign key (city_id) references cities(id_city),
    foreign key (street_id) references streets(id_street)
);

#FK \/
-- Таблица "Персональные карточки сотрудников"
create table if not exists personal_cards (
	id_person int auto_increment primary key,
    last_name varchar(55),
    name varchar(55),
    surname varchar(55),
    birth_date date,
    address_id int,
    bank_id int,
    bank_account varchar(20),
    INN varchar(12),
    SNILS varchar(14),
    employment_date date,
    dismissal_date date,
    foreign key (address_id) references addresses(id_address),
    foreign key (bank_id) references banks(id_bank)
);

#FK \/
-- Таблица "Структура"
create table if not exists structure (
    id_structure int auto_increment primary key,
    department_id int,
    position_id int,
    salary decimal(10,2),
    bonus decimal(10,2),
    foreign key (department_id) references departments(id_department),
    foreign key (position_id) references positions(id_position)
);

#FK \/
-- Таблица "Контактная информация сотрудников"
create table if not exists contact_info (
	id_contact_info int auto_increment primary key,
    person_id int,
    position_id int,
    department_id int,
    mobile_phone varchar(20),
    landline_phone varchar(20),
    email varchar(255),
    supervisor_id int,
    foreign key (position_id) references positions(id_position),
    foreign key (department_id) references departments(id_department)
);

#FK \/
-- Таблица "Журнал прохождения курсов повышения квалификации"
create table if not exists qualification_course_journal (
	id_course_journal int auto_increment primary key,
    course_id int,
    person_id int,
    start_date date,
    end_date date,
    result varchar(20),
    status varchar(20),
    notes varchar(255),
    certificate_number varchar(20),
    certificate_date date,
    foreign key (course_id) references qualification_courses(id_course),
    foreign key (person_id) references personal_cards(id_person)
);

#FK \/
-- Таблица "Кадровый резерв"
create table if not exists personnel_reserve (
    id_personnel_reserve int auto_increment primary key,
    person_id int,
    structure_id int,
    reserve_entry_date date,
    reserve_exit_date date,
    reserve_status varchar(20),
    new_structure int,
    work_schedule_id int,
    foreign key (person_id) references personal_cards(id_person),
    foreign key (structure_id) references structure(id_structure),
    foreign key (new_structure) references structure(id_structure),
    foreign key (work_schedule_id) references work_schedules(id_work_schedule)
);


-- Добавление в справочники:
-- Добавление данных в таблицу "Departments"
insert into departments (department_name) values ('Маркетинговый отдел'), ('Отдел кадров');

-- Добавление данных в таблицу "Positions"
insert into positions (position_name) values ('Пиар-менеджер'), ('Директор'), ('Бухгалтер');


-- Добавление данных в таблицу "Work Schedules"
insert into work_schedules (schedule_name, working_days, days_off, working_hours_per_day) values ('Стандартный', 5, 2, 8), ('Пол-ставки', 5, 2, 4);

-- Добавление данных в таблицу "Regions"
insert into regions (region_name) values ('Республика Хакасия'), ('Новосибирская область'), ('Красноярский край');

-- Добавление данных в таблицу "Streets"
insert into streets (street_name) values ('ул. Пушкина'), ('ул. Советсвкая'), ('ул. Гоголя');

-- Добавление данных в таблицу "Qualification Courses"
insert into qualification_courses (course_name) values ('Тибилдинговый курс'), ('Профессиональный маркетинг');

-- Добавление данных в таблицу "Banks"
insert into banks (bank_name) values ('СберБанк'), ('ВТБ банк');


-- Добавление в таблицы с внешними ключами:
-- Добавление данных в таблицу "Districts"
insert into districts (district_name, region_id) values ('Северный район', 1), ('Восточный район', 1);

-- Добавление данных в таблицу "Cities"
insert into cities (city_name, district_id) values ('Абакан', 1), ('Новосибирск', 2), ('Красноярск', 2);

-- Добавление данных в таблицу "Addresses"
insert into addresses (city_id, street_id, house_number, flat_number) values (1, 1, '30', 3);

-- Добавление данных в таблицу "Personal Cards"
insert into personal_cards (last_name, name, surname, birth_date, address_id, bank_id, bank_account, INN, SNILS, employment_date, dismissal_date) values ('Ефремов', 'Ефрем', 'Георгьевич', '1990-05-15', 1, 1, '1234567890123456', '123456789012', '12345678901234', '2020-01-01', null), ('Терентьева', 'Зинаида', 'Авксентьевна', '1980-02-12', 1, 1, '1234567890123456', '123456789012', '12345678901234', '2020-01-01', null);

-- Добавление данных в таблицу "Structure"
insert into structure (department_id, position_id, salary, bonus) values (1, 1, 35000.00, 2000.00), (2, 1, 50000.00, 5000.00);

-- Добавление данных в таблицу "Contact Info"
insert into contact_info (person_id, department_id, position_id, mobile_phone, landline_phone, email, supervisor_id) values (1, 1, 1, '1234567890', '9876543210', 'Yefremich@example.com', 2), (2, 2, 2, '1234567890', '9876543210', 'ZinaidaTA@example.com', null);

-- Добавление данных в таблицу "Qualification Courses Completion Journal"
insert into qualification_course_journal (course_id, person_id, start_date, end_date, result, status, notes, certificate_number, certificate_date) values (2, 1, '2020-03-01', '2020-03-15', 'Пройдено', 'Завершено', 'Хорошая успеваемость', '12345', '2020-04-01'), (1, 2, '2020-10-01', '2020-10-15', 'Пройдено', 'Завершено', 'Хорошая успеваемость', '12345', '2020-10-15');

-- Добавление данных в таблицу "Personnel Reserve"
insert into personnel_reserve (person_id, structure_id, reserve_entry_date, reserve_exit_date, reserve_status, new_structure, work_schedule_id) values (1, 1, '2021-01-01', null, 'В резерве', null, 1), (2, 2, null, null, 'Не в резерве', null, 1);