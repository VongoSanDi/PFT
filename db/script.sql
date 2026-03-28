-- Drop tables
drop table if exists Types;
drop table if exists Categories;
drop table if exists Entries;

create table Types(Id integer primary key autoincrement, Name varchar(10), Description varchar(100));
insert into Types(Name, Description) values('Expense', 'Expense type');
insert into Types(Name, Description) values('Income', 'Income type');

create table Categories(Id integer primary key autoincrement, Name varchar(10), Description varchar(100));
insert into Categories(Name, Description) values('Food', 'Food expenses');
insert into Categories(Name, Description) values('Rent', 'Rent');

create table Entries(Id INTEGER primary key AUTOINCREMENT, Amount real, Date text, Description text, TypeId integer, foreign key(TypeId) references Types(Id));
insert into Entries(Amount, Date, Description, TypeId) values(1, '2023-05-15T07:00:00Z', 'test insertion manuelle', 1);
