-- Run it after initing the database

create table if not exists categorie(
    Id_categorie varchar(255),
    nom varchar(100),
    primary key(Id_categorie)
);

create table if not exists utilisateur(
    Id_utilisateur varchar(255),
    pseudo varchar(255),
    prenom varchar(30),
    nom varchar(30),
    email varchar(130),
    naissance date,
    vendeur int,
    acheteur int,
    createdAt datetime,
    updatedAt datetime,
    primary key(Id_utilisateur)
);

create table if not exists produit(
    Id_produit varchar(255),
    nom varchar(100),
    description varchar(500),
    addedAt datetime,
    prix float,
    Id_categorie varchar(255),
    Id_utilisateur varchar(255),
    foreign key (Id_categorie) references categorie(Id_categorie),
    foreign key (Id_utilisateur) references utilisateur(Id_utilisateur),
    primary key(Id_produit)
);