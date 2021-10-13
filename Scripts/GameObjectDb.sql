create table Equipment
(
	Id int primary key identity not null,
	Names nvarchar(25) not null,
	Cost int not null
);

create table Weapon_Type
(
	Id int primary key identity not null,
	Special bit,
	Simpl bit,
	Throwing bit
);

create table Weapons
(
	EquipmentId int references Equipment (Id),
	Weapon_TypeId int references Weapon_Type (Id),
	constraint PK_weapons primary key (EquipmentId),
	Distance_Of_Attack nvarchar(20) not null,
	WeaponGrip nvarchar(20) not null,
	Ttype nvarchar(20) not null,
	Damage nvarchar(20) not null,
	Critical_Hit nvarchar(20) not null
);

create table Snaryad
(
	Weapon_TypeId int references Weapon_Type (Id),
	constraint PK_Snaryad primary key (Weapon_TypeId),
	Names nvarchar(20) not null,
	Cost int,
	Quantity int
);

create table Consumables
(
	EquipmentId int references Equipment (Id),
	constraint PK_consumables primary key (EquipmentId),
	Desctiption nvarchar(69) not null,
	Value int
);

create table Type_of_armor
(
	id int primary key identity not null,
	Title nvarchar(20)
);

create table Armor
(
	EquipmentId int references Equipment (Id),
	Type_of_armorId int references Type_of_armor (Id),
	constraint PK_Armor primary key (EquipmentId),
	ArmorLevel int
);

create table Enemy
(
	id int primary key identity not null,
	Name nvarchar(30) not null,
	Initiative int not null,
	Health int not null,
	Armor int,
	TypeAttack nvarchar(20) not null,
	Damage nvarchar(20) not null,
	Modifier_name nvarchar(20),
	Modifier_value int,
	Experience int not null
);

create table Traps
(
	id int primary key identity not null,
	Type_of_trap nvarchar(20) not null,
	Trigger_condition nvarchar(20) not null,
	Cooldown nvarchar(20) not null,
	Actions nvarchar(100) not null
);

create table Game_events
(
	id int primary key identity not null,
	Names nvarchar(20),
	Descriptions nvarchar(100),
	Reward nvarchar(20)
);