CREATE TABLE Character
(
	id_Character SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL,
	Worldview CHARACTER VARYING(50) NOT NULL,
	Sex CHARACTER VARYING(30) NOT NULL,
	Class CHARACTER VARYING(30) NOT NULL,
	Nation CHARACTER VARYING(30) NOT NULL,
	Level INTEGER NOT NULL,
	Health INTEGER,
	Speed INTEGER NOT NULL,
	Experience INTEGER
);

CREATE TABLE Skills
(
	id_Skill SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL,
	Modifier INTEGER 
);

CREATE TABLE Characteristic
(
	id_Characteristic SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL,
	NumericValue Integer,
	Modifier INTEGER 	
);

CREATE TABLE Weapons
(
	id_Weapon SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL,
	AbilityToWear BOOLEAN
);

CREATE TABLE Armor
(
	id_Armor SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL,
	AbilityToWear BOOLEAN
);

CREATE TABLE Attacks
(
	id_Attack SERIAL PRIMARY KEY,
	AttackModifier CHARACTER VARYING(50) NOT NULL
);

CREATE TABLE WeaponItemClass
(
	id_WeaponItemClass SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL
);

ALTER TABLE Weapons
	add AttackId INTEGER REFERENCES Attacks (id_Attack),
	add WeaponItemClassId INTEGER REFERENCES WeaponItemClass (id_WeaponItemClass);
	
CREATE TABLE ArmorItemClass
(
	id_ArmorItemClass SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL
);

CREATE TABLE ArmorClass
(
	id_ArmorClass SERIAL PRIMARY KEY,
	Armorlevel Integer NOT NULL,
	ShieldArmorlevel Integer NOT NULL,
	MagicModifier Integer ,
	Const Integer ,
	Sum  Integer 
);

ALTER TABLE Armor
	ADD ArmorItemClassId INTEGER REFERENCES ArmorItemClass (id_ArmorItemClass),
	ADD ArmorClassId INTEGER REFERENCES ArmorClass (id_ArmorClass);

CREATE TABLE Features
(
	id_Features SERIAL PRIMARY KEY,
	Name CHARACTER VARYING(50) NOT NULL,
	NumericValue Integer
);

CREATE TABLE Character_to_Features
(
	CharacterId INTEGER REFERENCES Character (id_Character),
	FeatureId INTEGER REFERENCES Features (id_Features),
	CONSTRAINT Character_to_Features_pk PRIMARY KEY (CharacterId, FeatureId)
);

CREATE TABLE Character_to_Skills
(
	CharacterId INTEGER REFERENCES Character (id_Character),
	SkillId INTEGER REFERENCES Skills (id_Skill),
	CONSTRAINT Character_to_Skills_pk PRIMARY KEY (CharacterId, SkillId)
);

CREATE TABLE Character_to_Weapon
(
	CharacterId INTEGER REFERENCES Character (id_Character),
	WeaponId INTEGER REFERENCES Weapons (id_Weapon),
	CONSTRAINT Character_to_Weapon_pk PRIMARY KEY (CharacterId, WeaponId)
);

CREATE TABLE Character_to_Armor
(
	CharacterId INTEGER REFERENCES Character (id_Character),
	ArmorId INTEGER REFERENCES Armor (id_Armor),
	CONSTRAINT Character_to_Armor_pk PRIMARY KEY (CharacterId, ArmorId)
);

CREATE TABLE Character_to_Characteristics
(
	CharacterId INTEGER REFERENCES Character (id_Character),
	CharactericticId INTEGER REFERENCES Characteristic (id_Characteristic),
	CONSTRAINT Character_to_Characteristics_pk PRIMARY KEY (CharacterId, CharactericticId)
);