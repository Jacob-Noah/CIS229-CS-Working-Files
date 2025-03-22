CREATE DATABASE IF NOT EXISTS Inventory;
USE Inventory;

CREATE TABLE IF NOT EXISTS accounts (
    id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
    username varchar(32) NOT NULL UNIQUE,
    password varchar(32) NOT NULL,
    email varchar(32) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS products (
    id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name varchar(32) NOT NULL,
    quantity int NOT NULL,
    customer_id int NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES accounts(id)
);

delimiter //

-- ACCOUNTS PROCEDURES
CREATE procedure IF NOT EXISTS does_account_exist(
    username varchar(32),
    password varchar(32)
)
BEGIN
    SELECT COUNT(*) FROM accounts WHERE accounts.username = username AND accounts.password = password;
END;
//

CREATE procedure IF NOT EXISTS read_all_accounts()
BEGIN
    SELECT username, password FROM accounts;
END;

-- PRODUCTS PROCEDURES
CREATE procedure IF NOT EXISTS create_product(
    name varchar(32),
    quantity int,
    customer_id int
)
BEGIN
    INSERT INTO products (name, quantity, customer_id) VALUES (name, quantity, customer_id);
END;
//

CREATE procedure IF NOT EXISTS read_all_products()
BEGIN
    SELECT * FROM products;
END;
//

CREATE procedure IF NOT EXISTS update_product(
    id int,
    name varchar(32),
    quantity int
)
BEGIN
    UPDATE products SET products.name = name, products.quantity = quantity WHERE products.id = id;
END;
//

CREATE procedure IF NOT EXISTS delete_product(
    id int
)
BEGIN
    DELETE FROM products WHERE products.id = id;
END;
//

delimiter ;