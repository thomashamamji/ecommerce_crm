-- Run it after initing the database

create table if not exists utilisateur(
    Id_utilisateur int auto_increment,
    pseudo varchar(255),
    password varchar(255),
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

create table if not exists categorie(
    Id_categorie int auto_increment,
    nom varchar(100),
    addedAt datetime,
    Id_utilisateur int,
    foreign key (Id_utilisateur) references utilisateur(Id_utilisateur),
    primary key(Id_categorie)
);

create table if not exists produit(
    Id_produit int auto_increment,
    nom varchar(100),
    description varchar(500),
    addedAt datetime,
    prix float,
    Id_categorie int,
    Id_utilisateur int,
    foreign key (Id_categorie) references categorie(Id_categorie),
    foreign key (Id_utilisateur) references utilisateur(Id_utilisateur),
    primary key(Id_produit)
);

create table if not exists vente(
    Id_vente int auto_increment,
    Id_produit int,
    Id_utilisateur int,
    quantite int,
    foreign key (Id_produit) references produit(Id_produit),
    foreign key (Id_utilisateur) references utilisateur(Id_utilisateur),
    primary key (Id_vente)
)