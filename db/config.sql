-- Run it as root to init the database

create user ecommerce_admin_app_dbuser identified by 'app12345$$$$$';
create database ecommerce_admin_app_db;
grant all privileges on ecommerce_admin_app_db.* to 'ecommerce_admin_app_dbuser'@'%';
flush privileges;