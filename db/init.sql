insert into utilisateur(pseudo, password, prenom, nom, email, naissance, vendeur, acheteur, createdAt, updatedAt) values ('admin', 'abcabcabc', 'Thomas', 'Hamamji', 'thomas.hamamji@gmail.com', '2000-05-29', 1, 0, now(), now());

-- Fill the database with a list of categories and products

-- Categories
insert into categorie(nom) values('Fruit');
insert into categorie(nom) values('Viandes');
insert into categorie(nom) values('Légumes');
insert into categorie(nom) values('Sauces');
insert into categorie(nom) values('Épices');

-- Products
-- Fruits
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Fraise', 'Rouge et sucré (200 g)', now(), 5.00, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Framboise', 'Rose, acide et sucré (125 g)', now(), 15.0, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Mure', 'Violet et acide (125 g)', now(), 20.0, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Pomme verte', 'Vert, acide, sucré et consistant', now(), 2.00, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Poire', 'Vert et sucré', now(), 1.0, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Pomme rouge', 'Rouge et sucré', now(), 1.75, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Cerise', 'Rouge et sucré (250 g)', now(), 2.50, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Citron', 'Jaune et acide', now(), 1.50, 1, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Prune', 'Rouge, acide et sucré', now(), 6.0, 1, 1);

-- Viandes
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Côte de boeuf', 'Riche en protéines, préférable avec une bonne cuisson (300 g)', now(), 13.50, 2, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Pavé de rumsteak', 'Viande de qualité, riche en protéines (300 g)', now(), 34.50, 2, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Escalope de veau', 'Fine tranche de veau (300 g)', now(), 19.50, 2, 1);

-- Légumes

-- Sauces

-- Épices