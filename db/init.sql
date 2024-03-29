insert into utilisateur(pseudo, password, prenom, nom, email, naissance, vendeur, acheteur, createdAt, updatedAt) values ('admin', 'abcabcabc', 'Thomas', 'Hamamji', 'thomas.hamamji@gmail.com', '2000-05-29', 1, 1, now(), now());

-- Fill the database with a list of categories and products

-- Categories
insert into categorie(nom, Id_utilisateur, addedAt) values('Fruit', 1, now());
insert into categorie(nom, Id_utilisateur, addedAt) values('Viande', 1, now());
insert into categorie(nom, Id_utilisateur, addedAt) values('Légume', 1, now());
insert into categorie(nom, Id_utilisateur, addedAt) values('Sauce', 1, now());
insert into categorie(nom, Id_utilisateur, addedAt) values('Épice', 1, now());

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
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Salade', '300 g', now(), 1.0, 3, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Aubergines', '1 kg', now(), 2.0, 3, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Salade', '300 g', now(), 1.0, 3, 1);

-- Sauces
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Ketchup', 'Sauce tomate', now(), 3.50, 4, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Mayonnaise', 'Sauce blanche', now(), 2.40, 4, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Barbecue', 'Sauce pour viandes', now(), 2.15, 4, 1);

-- Épices
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Piment', 'Très piquant (100 g)', now(), 1.7, 5, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Safran', 'Épice rare de Iran (100 g)', now(), 60, 5, 1);
insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values ('Poivre', 'Récipient de 40 g', now(), 1, 5, 1);

-- Sells
insert into vente(Id_produit, Id_utilisateur, quantite) values (2, 1, 4); -- 4 framboises achetées par l'admin 