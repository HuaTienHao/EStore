INSERT INTO Category VALUES ('Headphone')
INSERT INTO Category VALUES ('Mouse')
INSERT INTO Category VALUES ('Keyboard')
INSERT INTO Category VALUES ('Laptop')

INSERT INTO Product VALUES (N'Tai nghe Gaming Logitech Pro X 2',5499000,'logitech-tainghe.jpg',1,'LOGITECH')
INSERT INTO Product VALUES (N'Tai nghe HyperX CLOUD FLIGHT WIRELESS',2499000,'hyperx-tainghe',1,'HyperX')
INSERT INTO Product VALUES (N'Tai nghe không dây Gaming ZIDLI AH1',1099000,'zidli-tainghe',1,'ZIDLI')
INSERT INTO Product VALUES (N'Chuột gaming ASUS ROG Gladius II',1850000,'asus-chuot',2,'ASUS')
INSERT INTO Product VALUES (N'Chuột gaming ZOWIE eSport EC2-C',1678000,'zomie-chuot',2,'ZOWIE')
INSERT INTO Product VALUES (N'Chuột máy tính XIAOMI Lite/Black',239000,'xiaomi-chuot',2,' XIAOMI')
INSERT INTO Product VALUES (N'Bàn phím Apple Magic Keyboard',7790000,'apple-kb',3,'Apple')
INSERT INTO Product VALUES (N'Bàn phím cơ gaming Corsair K70 PRO',3699000,'corsair-kb',3,'CORSAIR')
INSERT INTO Product VALUES (N'Bàn phím không dây Logitech MX Keys S',2549000,'logitech-kb',3,'LOGITECH')
INSERT INTO Product VALUES (N'Laptop Acer Nitro 16 Phoenix ',25990000,'acer-lap',4,'Acer')
INSERT INTO Product VALUES (N'Apple MacBook Pro 16 M2 Max',94690000,'apple-lap',4,'Apple')
INSERT INTO Product VALUES (N'Laptop Dell Inspiron 3530 ',18590000,'dell-lap',4,'Dell')

INSERT INTO OrderStatus VALUES ('Pending',1)
INSERT INTO OrderStatus VALUES ('Shipped',2)
INSERT INTO OrderStatus VALUES ('Delivered',3)
INSERT INTO OrderStatus VALUES ('Cancelled',4)
INSERT INTO OrderStatus VALUES ('Returned',6)
INSERT INTO OrderStatus VALUES ('Refund',6)

INSERT INTO Discount VALUES ('DISCOUNT5',5)
INSERT INTO Discount VALUES ('DISCOUNT10',10)
INSERT INTO Discount VALUES ('DISCOUNT20',20)

delete from Product where CategoryId=4
select * from Category
select * from Product