USE GD2C2019;

GO


/*Se crea schema y se eliminan procedures*/

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'JARDCOUD')
  		BEGIN
    		EXEC ('CREATE SCHEMA [JARDCOUD] AUTHORIZATION gdCupon2019');
  		END;

IF OBJECT_ID('JARDCOUD.limpiar_fk')IS NOT NULL
	DROP PROCEDURE JARDCOUD.limpiar_fk;

IF OBJECT_ID('JARDCOUD.limpiar_tablas')IS NOT NULL
	DROP PROCEDURE JARDCOUD.limpiar_tablas;

IF OBJECT_ID('JARDCOUD.creacion_tablas')IS NOT NULL
	DROP PROCEDURE JARDCOUD.creacion_tablas;

IF OBJECT_ID('JARDCOUD.seeder')IS NOT NULL
	DROP PROCEDURE JARDCOUD.seeder;

IF OBJECT_ID('JARDCOUD.migracion')IS NOT NULL
	DROP PROCEDURE JARDCOUD.migracion;

IF OBJECT_ID('JARDCOUD.top_proveedores_mayor_descuento')IS NOT NULL
	DROP PROCEDURE JARDCOUD.top_proveedores_mayor_descuento;

IF OBJECT_ID('JARDCOUD.listar_cupones_de_proveedor')IS NOT NULL
	DROP PROCEDURE JARDCOUD.listar_cupones_de_proveedor;

IF OBJECT_ID('JARDCOUD.listar_ofertas_de_proveedor')IS NOT NULL
	DROP PROCEDURE JARDCOUD.listar_ofertas_de_proveedor;

IF OBJECT_ID('JARDCOUD.top_proveedores_mayor_facturacion')IS NOT NULL
	DROP PROCEDURE JARDCOUD.top_proveedores_mayor_facturacion;

IF OBJECT_ID('JARDCOUD.agregar_usuario_cliente')IS NOT NULL
	DROP PROCEDURE JARDCOUD.agregar_usuario_cliente;

IF OBJECT_ID('JARDCOUD.crear_usuarios_cliente')IS NOT NULL
	DROP PROCEDURE JARDCOUD.crear_usuarios_cliente;

IF OBJECT_ID('JARDCOUD.agregar_usuario_proveedor')IS NOT NULL
	DROP PROCEDURE JARDCOUD.agregar_usuario_proveedor;

IF OBJECT_ID('JARDCOUD.crear_usuarios_proveedor')IS NOT NULL
	DROP PROCEDURE JARDCOUD.crear_usuarios_proveedor;

IF OBJECT_ID('JARDCOUD.crear_usuarios_proveedor')IS NOT NULL
	DROP PROCEDURE JARDCOUD.crear_usuarios_proveedor;


GO

/*Se eliminan las Foreign Keys*/
CREATE PROCEDURE JARDCOUD.limpiar_fk
AS
BEGIN

	DECLARE @SQL varchar(4000)=''
	SELECT @SQL = 
	@SQL + 'ALTER TABLE ' + S.name +'.'+ T.name + ' DROP CONSTRAINT [' + RTRIM(F.name) +'];' + CHAR(13)
	FROM sys.Tables T
	INNER JOIN sys.foreign_keys F ON F.parent_object_id = T.object_id
	INNER JOIN sys.schemas S ON S.schema_id = F.schema_id
	EXEC (@SQL)
END
GO

/*Se eliminan los objetos que ya existen*/
CREATE PROCEDURE JARDCOUD.limpiar_tablas
AS
BEGIN


	IF OBJECT_ID('JARDCOUD.Funcionalidad','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Funcionalidad;

	IF OBJECT_ID('JARDCOUD.Rol','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Rol;

	IF OBJECT_ID('JARDCOUD.Rol_Funcionalidad','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Rol_Funcionalidad;

	IF OBJECT_ID('JARDCOUD.Usuario','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Usuario;

	IF OBJECT_ID('JARDCOUD.Usuario_Rol','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Usuario_Rol;

	IF OBJECT_ID('JARDCOUD.Cliente','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Cliente;    

	IF OBJECT_ID('JARDCOUD.TipoPago','U') IS NOT NULL
	    DROP TABLE JARDCOUD.TipoPago;

	IF OBJECT_ID('JARDCOUD.CargaCredito','U') IS NOT NULL
	    DROP TABLE JARDCOUD.CargaCredito;    

	IF OBJECT_ID('JARDCOUD.Rubro','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Rubro;        

	IF OBJECT_ID('JARDCOUD.Proveedor','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Proveedor;

	IF OBJECT_ID('JARDCOUD.Oferta','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Oferta;

	IF OBJECT_ID('JARDCOUD.Factura','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Factura;

	IF OBJECT_ID('JARDCOUD.Item_Factura','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Item_Factura;   

	IF OBJECT_ID('JARDCOUD.Cupon','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Cupon;  
		
	IF OBJECT_ID('JARDCOUD.Compra','U') IS NOT NULL
	    DROP TABLE JARDCOUD.Compra;  
		              
END	

GO


/*Script para crear las tablas necesarias de nuestra base de datos*/

CREATE PROCEDURE JARDCOUD.creacion_tablas
AS
BEGIN
	CREATE TABLE JARDCOUD.Funcionalidad (
		id INT IDENTITY(1,1) PRIMARY KEY,
		nombre VARCHAR (50) NOT NULL
	);

	CREATE TABLE JARDCOUD.Rol (
		id INT IDENTITY(1,1) PRIMARY KEY,
		nombre VARCHAR (50) NOT NULL,
		habilitado BIT NOT NULL
	);

	CREATE TABLE JARDCOUD.Rol_Funcionalidad (
		rol_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Rol(id),
		funcionalidad_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Funcionalidad(id)
	);

	CREATE TABLE JARDCOUD.Usuario (
		id INT IDENTITY(1,1) PRIMARY KEY,
		username VARCHAR (20) NOT NULL UNIQUE,
		password VARCHAR (255) NOT NULL,
		login_intentos INT,
		habilitado BIT NOT NULL
	);

	CREATE TABLE JARDCOUD.Usuario_Rol (
		usuario_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Usuario(id),
		rol_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Rol(id)
	);

	CREATE TABLE JARDCOUD.Cliente (
		id INT IDENTITY(1,1) PRIMARY KEY,
		nombre NVARCHAR (255) NOT NULL,
		apellido NVARCHAR (255) NOT NULL,
		dni NUMERIC(18,0) NOT NULL UNIQUE,
		mail NVARCHAR (255) NOT NULL, /*UNIQUE*/
		telefono NUMERIC(18,0),
		direccion NVARCHAR (255) NOT NULL,
		codigo_postal VARCHAR (10),
		fecha_nacimiento DATETIME NOT NULL,
		credito NUMERIC(18,2),
		usuario_id INT FOREIGN KEY REFERENCES JARDCOUD.Usuario(id),
		habilitado BIT NOT NULL
	);

	CREATE TABLE JARDCOUD.TipoPago (
		id INT IDENTITY(1,1) PRIMARY KEY,
		descripcion VARCHAR (20) NOT NULL
	);

	CREATE TABLE JARDCOUD.CargaCredito (
		id INT IDENTITY(1,1) PRIMARY KEY,
		fecha DATETIME NOT NULL,
		cliente_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Cliente(id),
		tipo_pago_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.TipoPago(id),
		monto NUMERIC(18,2) NOT NULL,
		numero_tarjeta VARCHAR (20),
		fecha_vencimiento VARCHAR (5),
		cod_seguridad VARCHAR (3),
	);

	CREATE TABLE JARDCOUD.Rubro (
		id INT IDENTITY(1,1) PRIMARY KEY,
		descripcion NVARCHAR (100)
	);

	CREATE TABLE JARDCOUD.Proveedor (
		id INT IDENTITY(1,1) PRIMARY KEY,
		razon_social NVARCHAR (100) NOT NULL UNIQUE,
		mail NVARCHAR (255),
		telefono NUMERIC(18,2),
		direccion NVARCHAR (100) NOT NULL,
		ciudad NVARCHAR (255) NOT NULL,
		cuit NVARCHAR (20) NOT NULL UNIQUE,
		rubro_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Rubro(id),
		nombre_contacto NVARCHAR(255),
		usuario_id INT FOREIGN KEY REFERENCES JARDCOUD.Usuario(id),
		habilitado BIT NOT NULL
	);

	CREATE TABLE JARDCOUD.Oferta (
		id INT IDENTITY(1,1) PRIMARY KEY,
		proveedor_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Proveedor(id),
		codigo NVARCHAR (50) NOT NULL UNIQUE,
		descripcion NVARCHAR (255),
		fecha_publicacion DATETIME NOT NULL,
		fecha_vencimiento DATETIME NOT NULL,
		precio_oferta NUMERIC(18,2) NOT NULL,
		precio_lista NUMERIC(18,2) NOT NULL,
		descuento NUMERIC(18,2),
		cantidad_disponible NUMERIC(18,0) NOT NULL,
		cantidad_max_cliente NUMERIC(18,0) 
	);

	CREATE TABLE JARDCOUD.Factura (
		id INT IDENTITY(1,1) PRIMARY KEY,
		proveedor_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Proveedor(id),
		numero NUMERIC(18,0) NOT NULL UNIQUE,
		total NUMERIC(18,2) NOT NULL,
		fecha DATETIME NOT NULL
	);

	CREATE TABLE JARDCOUD.Compra (
		id INT IDENTITY(1,1) PRIMARY KEY,
		cliente_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Cliente(id),
		oferta_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Oferta(id),
		fecha DATETIME NOT NULL,
		cantidad NUMERIC(18,0) 
	);

	CREATE TABLE JARDCOUD.Item_Factura (
		id INT IDENTITY(1,1) PRIMARY KEY,
		oferta_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Oferta(id),
		factura_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Factura(id),
		importe NUMERIC(18,2) NOT NULL,
		cantidad NUMERIC(18,0) 
	);

	CREATE TABLE JARDCOUD.Cupon (
		id INT IDENTITY(1,1) PRIMARY KEY,
		compra_id INT NOT NULL FOREIGN KEY REFERENCES JARDCOUD.Compra(id),
		cliente_id INT FOREIGN KEY REFERENCES JARDCOUD.Cliente(id),
		codigo NVARCHAR (50) NOT NULL UNIQUE,
		fecha_vencimiento DATETIME NOT NULL,
		fecha_de_consumo DATETIME,
		canjeado BIT NOT NULL
	);

	
END

GO

/*Agregamos datos propios del sistema*/
CREATE PROCEDURE JARDCOUD.seeder
AS
BEGIN

	--Funcionalidades
	insert into JARDCOUD.Funcionalidad values ('Abm Cliente');
	insert into JARDCOUD.Funcionalidad values ('Abm Proveedor');
	insert into JARDCOUD.Funcionalidad values ('Abm Rol');
	insert into JARDCOUD.Funcionalidad values ('Comprar Oferta');
	insert into JARDCOUD.Funcionalidad values ('Carga Credito');
	insert into JARDCOUD.Funcionalidad values ('Crear Oferta');
	insert into JARDCOUD.Funcionalidad values ('Facturar');
	insert into JARDCOUD.Funcionalidad values ('Listado Estadistico');
	insert into JARDCOUD.Funcionalidad values ('Abm Usuario');
	insert into JARDCOUD.Funcionalidad values ('Entrega Consumo de oferta');

	--Roles
	insert into JARDCOUD.Rol values ('Administrativo',1);
	insert into JARDCOUD.Rol values ('Cliente',1);
	insert into JARDCOUD.Rol values ('Proveedor',1);

	--Rol_Funcionalidad
	insert into JARDCOUD.Rol_Funcionalidad values (1,1);
	insert into JARDCOUD.Rol_Funcionalidad values (1,2);
	insert into JARDCOUD.Rol_Funcionalidad values (1,3);
	insert into JARDCOUD.Rol_Funcionalidad values (1,4);
	insert into JARDCOUD.Rol_Funcionalidad values (1,5);
	insert into JARDCOUD.Rol_Funcionalidad values (1,6);
	insert into JARDCOUD.Rol_Funcionalidad values (1,7);
	insert into JARDCOUD.Rol_Funcionalidad values (1,8);
	insert into JARDCOUD.Rol_Funcionalidad values (1,9);
	insert into JARDCOUD.Rol_Funcionalidad values (1,10);

	insert into JARDCOUD.Rol_Funcionalidad values (2,4);
	insert into JARDCOUD.Rol_Funcionalidad values (2,5);

	insert into JARDCOUD.Rol_Funcionalidad values (3,6);
	insert into JARDCOUD.Rol_Funcionalidad values (3,10);

	--Usuarios
	insert into JARDCOUD.Usuario values ('admin',HASHBYTES('SHA2_256', 'w23e'),0,1);

	--Usuario_Rol
	insert into JARDCOUD.Usuario_Rol values (1,1);
END

GO 

/*Migramos todas las columnas de la tabla maestra para hacerla compatible con nuestro diseño*/


CREATE PROCEDURE JARDCOUD.migracion
AS
BEGIN

INSERT INTO JARDCOUD.Cliente(
	    nombre,
	    apellido,
	    dni,
	    mail,
	    telefono,
		direccion,
		codigo_postal,
	    fecha_nacimiento,
		credito,
		habilitado
		) 
		SELECT DISTINCT
	    Cli_Nombre,
		Cli_Apellido,
	    Cli_Dni,
	    Cli_Mail,
	    Cli_Telefono,
		CLI_Direccion,
		0,
	    Cli_Fecha_Nac,
		200,
		1
	 
	FROM gd_esquema.Maestra

INSERT INTO JARDCOUD.Rubro 
		SELECT distinct Provee_Rubro 
	FROM gd_esquema.Maestra WHERE Provee_Rubro IS NOT NULL

INSERT INTO JARDCOUD.Proveedor(
	    razon_social,
	    telefono,
	    direccion,
	    ciudad,
	    cuit,
	    rubro_id,
		habilitado
		)
		SELECT DISTINCT 
		M.Provee_RS,
	    M.Provee_Telefono,
		M.Provee_Dom,
	    M.Provee_Ciudad,
	    M.Provee_CUIT,
	    R.id,
		1
	    
	FROM gd_esquema.Maestra M
	JOIN JARDCOUD.Rubro R ON (R.descripcion = M.Provee_Rubro);
	
INSERT INTO JARDCOUD.Oferta(
	proveedor_id,
	codigo,
	descripcion,
	fecha_publicacion,
	fecha_vencimiento,
	precio_oferta,
	precio_lista,
	descuento,
	cantidad_disponible,
	cantidad_max_cliente
	) 
	SELECT DISTINCT 
	P.id,
	M.Oferta_Codigo, 
	M.Oferta_Descripcion,
	M.Oferta_Fecha,
	M.Oferta_Fecha_Venc, 
	M.Oferta_Precio,
	M.Oferta_Precio_Ficticio, 
	((M.Oferta_Precio_Ficticio - M.Oferta_Precio)) / M.Oferta_Precio_Ficticio, 
	M.Oferta_Cantidad,
	1
	FROM gd_esquema.Maestra M
	JOIN JARDCOUD.Proveedor P ON (P.razon_social = M.Provee_RS AND  P.cuit = M.Provee_CUIT);

INSERT INTO JARDCOUD.TipoPago(
	descripcion
	)
	VALUES ('Crédito'),('Débito'), ('Efectivo');

INSERT INTO JARDCOUD.CargaCredito(
	fecha,
	cliente_id,
	tipo_pago_id,
	monto
	) 
	SELECT 
	M.Carga_Fecha, 
	C.id,
	(SELECT id FROM JARDCOUD.TipoPago WHERE M.Tipo_Pago_Desc = descripcion),
	M.Carga_Credito
	FROM gd_esquema.Maestra M
	JOIN JARDCOUD.Cliente C ON (M.Cli_Dni = C.dni)
	WHERE  M.Carga_Fecha IS NOT NULL AND M.Carga_Credito IS NOT NULL and M.Cli_Dni IS NOT NULL AND M.Tipo_Pago_Desc IS NOT NULL

INSERT INTO JARDCOUD.Factura(
	proveedor_id,
	numero,
	fecha,
	total
	) 
	SELECT P.id,
	M.Factura_Nro, 
	M.Factura_Fecha, 
	SUM(M.Oferta_Precio)
	FROM gd_esquema.Maestra M
	INNER JOIN JARDCOUD.Proveedor P ON M.Provee_CUIT = P.cuit
	WHERE M.Factura_Nro IS NOT NULL
	GROUP BY P.id, M.Factura_Nro, M.Factura_Fecha

INSERT INTO JARDCOUD.Compra(
	cliente_id, 
	oferta_id, 
	fecha,
	cantidad
	)
	SELECT
	C.id,
	O.id,
	M.Oferta_Fecha_Compra,
	COUNT(*)
	FROM gd_esquema.Maestra M
	JOIN JARDCOUD.Cliente C ON C.dni = M.Cli_Dni
	JOIN JARDCOUD.Oferta O ON O.codigo = M.Oferta_Codigo
	WHERE M.Oferta_Fecha_Compra IS NOT NULL
	GROUP BY M.Oferta_Fecha_Compra, C.id, O.id

INSERT INTO JARDCOUD.Item_Factura(
	factura_id,
	oferta_id,
	importe,
	cantidad)
	SELECT DISTINCT 
	F.id, 
	O.id, 
	SUM(ISNULL(M.Oferta_Precio,0)),
	COUNT(*)
	FROM gd_esquema.Maestra M
	JOIN JARDCOUD.Factura F ON F.numero = M.Factura_Nro
	JOIN JARDCOUD.Oferta O ON O.codigo = M.Oferta_Codigo AND O.proveedor_id = F.proveedor_id
	JOIN JARDCOUD.Cliente C ON C.dni = M.Cli_Dni
	JOIN JARDCOUD.Compra Com ON Com.oferta_id = O.id AND Com.cliente_id = C.id
	WHERE M.Factura_Nro IS NOT NULL
	AND M.Oferta_Codigo IS NOT NULL
	AND M.Oferta_Fecha IS NOT NULL
	GROUP BY M.Factura_Nro,M.Oferta_Codigo,F.id,O.id


INSERT INTO JARDCOUD.Cupon(
		compra_id,
		cliente_id,
		codigo,
		fecha_vencimiento,
		fecha_de_consumo,
		canjeado
		)
		SELECT
		C.id,
		NULL,
		CONCAT('CODIGO_',C.id),
		(SELECT DATEADD(day, 15, O.fecha_vencimiento)),
		NULL,
		0 
		FROM JARDCOUD.Oferta O
		JOIN JARDCOUD.Compra C ON C.oferta_id = O.id
	
	
	

END

GO






/* Procedimientos usados */

/*Creacion de usuarios para los clientes */

CREATE PROCEDURE JARDCOUD.agregar_usuario_cliente( @ID_CLIENTE int, @USUARIO_NEW varchar(20), @PASSWORD_NEW varchar(255) )
AS
BEGIN

INSERT INTO JARDCOUD.Usuario VALUES (@USUARIO_NEW,HASHBYTES('SHA2_256',@PASSWORD_NEW),0,2)
INSERT INTO JARDCOUD.Usuario_Rol VALUES(IDENT_CURRENT('JARDCOUD.Usuario'),2)

UPDATE JARDCOUD.Cliente 
SET usuario_id = (SELECT id FROM JARDCOUD.Usuario WHERE username = @USUARIO_NEW) WHERE id = @ID_CLIENTE

END

GO

/* Asignacion de usuarios a todos los clientes de la migración*/

CREATE PROCEDURE JARDCOUD.crear_usuarios_cliente
AS
BEGIN

	DECLARE @ID_CLIENTE int;
	DECLARE @DNI NVARCHAR (255);

	DECLARE C_CLIENTE CURSOR FOR
	SELECT
	id,
	dni
	FROM JARDCOUD.Cliente
	GROUP BY id,dni
	
	OPEN C_CLIENTE
	
	FETCH NEXT FROM C_CLIENTE INTO @ID_CLIENTE, @DNI

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		EXEC JARDCOUD.agregar_usuario_cliente @ID_CLIENTE, @DNI, @DNI
		FETCH NEXT FROM C_CLIENTE INTO @ID_CLIENTE, @DNI
			
	END
	
	CLOSE C_CLIENTE
	DEALLOCATE C_CLIENTE
END
GO

/* Creacion de usuarios para los proveedores */

CREATE PROCEDURE JARDCOUD.agregar_usuario_proveedor( @ID_PROVEEDOR int, @USUARIO_NEW varchar(20), @PASSWORD_NEW varchar(255) )
AS
BEGIN

INSERT INTO JARDCOUD.Usuario VALUES (@USUARIO_NEW,HASHBYTES('SHA2_256',@PASSWORD_NEW),0,1)
INSERT INTO JARDCOUD.Usuario_Rol VALUES(IDENT_CURRENT('JARDCOUD.Usuario'),3)

UPDATE JARDCOUD.Proveedor
SET usuario_id  = (SELECT id FROM JARDCOUD.Usuario WHERE username = @USUARIO_NEW) WHERE id = @ID_PROVEEDOR

END

GO

/* Asignacion de usuarios a todos los proveedores de la migración*/

CREATE PROCEDURE JARDCOUD.crear_usuarios_proveedor
AS
BEGIN

	DECLARE @ID_PROVEEDOR int;
	DECLARE @RAZON_SOCIAL NVARCHAR (255);
	

	DECLARE C_PROVEEDOR CURSOR FOR
	SELECT
	id,
	razon_social
	FROM JARDCOUD.Proveedor
	GROUP BY id,razon_social
	
	OPEN C_PROVEEDOR

	FETCH NEXT FROM C_PROVEEDOR INTO @ID_PROVEEDOR, @RAZON_SOCIAL

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		EXEC JARDCOUD.agregar_usuario_PROVEEDOR @ID_PROVEEDOR, @RAZON_SOCIAL, @RAZON_SOCIAL
		FETCH NEXT FROM C_PROVEEDOR INTO @ID_PROVEEDOR, @RAZON_SOCIAL
			
	END
	
	CLOSE C_PROVEEDOR
	DEALLOCATE C_PROVEEDOR
END
GO

/* Lista los cupones vigentes de un proveedor*/

CREATE PROCEDURE JARDCOUD.listar_cupones_de_proveedor
@proveedor_id INT,
@codigo varchar(50) = null,
@fecha datetime
AS
BEGIN
	SELECT DISTINCT Cu.id,O.descripcion,Co.cantidad,Cu.codigo,Cu.fecha_vencimiento
	FROM JARDCOUD.Proveedor P
	INNER JOIN JARDCOUD.Oferta O ON P.id = O.proveedor_id
	INNER JOIN JARDCOUD.Compra Co ON O.id = Co.oferta_id
	INNER JOIN JARDCOUD.Cupon Cu ON Co.id = Cu.compra_id
	WHERE Cu.canjeado = 0 
	AND P.id = @proveedor_id AND Cu.fecha_vencimiento >= @fecha
	AND 
	(
		@codigo is null 
		or Cu.codigo = @codigo
	);
END

GO

/* Lista las ofertas vigentes de un proveedor*/

CREATE PROCEDURE JARDCOUD.listar_ofertas_de_proveedor
@proveedor_id INT,
@fecha_desde datetime,
@fecha_hasta datetime 
AS
BEGIN
	SELECT O.id,O.codigo,O.descripcion,O.precio_oferta,SUM(C.cantidad) AS 'cantidad',SUM(O.precio_oferta * C.cantidad) AS 'total'
	FROM JARDCOUD.Proveedor P
	INNER JOIN JARDCOUD.Oferta O ON P.id = O.proveedor_id
	INNER JOIN JARDCOUD.Compra C ON O.id = C.oferta_id
	WHERE P.id = @proveedor_id AND C.fecha >= @fecha_desde AND C.fecha <= @fecha_hasta
	GROUP BY O.id,O.codigo,O.descripcion,O.precio_oferta
END

GO


/* Listado estadistico */

 /* Proveedores con mayor porcentaje de descuento ofrecido en sus ofertas*/
 CREATE PROCEDURE JARDCOUD.top_proveedores_mayor_descuento
 	@anio int,
 	@semestre int
 AS
 BEGIN
 	DECLARE @desde DATETIME2 = DATEFROMPARTS(@anio, 1, 1)
 	DECLARE @hasta DATETIME2 = DATEFROMPARTS(@anio, 1, 1)
 	IF @semestre = 1
 	BEGIN
 		SET @hasta = DATEFROMPARTS(@anio, 6, 30);
 	END
 	ELSE IF @semestre = 2
 	BEGIN
 		SET @desde = DATEFROMPARTS(@anio, 6, 30);
 		SET @hasta = DATEFROMPARTS(@anio, 12, 31);
 	END

 	SELECT DISTINCT TOP 5
	P.id,
	P.razon_social,
	P.cuit,
	Cast(Cast((AVG(o.descuento))*100 as decimal(18,2)) as varchar(5)) + ' %' as 'Promedio Descuentos'
	FROM JARDCOUD.Proveedor P 
	JOIN JARDCOUD.Oferta O ON P.id = O.proveedor_id
	WHERE YEAR(O.fecha_publicacion) = @anio
 		AND O.fecha_publicacion BETWEEN @desde AND @hasta
    GROUP BY P.id, p.razon_social, p.cuit
 	ORDER BY 'Promedio Descuentos' DESC
 END
 GO


/* Proveedores con mayor facturacion*/

CREATE PROCEDURE JARDCOUD.top_proveedores_mayor_facturacion
 	@anio int,
 	@semestre int
 AS
 BEGIN
 	DECLARE @desde DATETIME2 = DATEFROMPARTS(@anio, 1, 1)
 	DECLARE @hasta DATETIME2 = DATEFROMPARTS(@anio, 1, 1)
 	IF @semestre = 1
 	BEGIN
 		SET @hasta = DATEFROMPARTS(@anio, 6, 30);
 	END
 	ELSE IF @semestre = 2
 	BEGIN
 		SET @desde = DATEFROMPARTS(@anio, 6, 30);
 		SET @hasta = DATEFROMPARTS(@anio, 12, 31);
 	END

	SELECT TOP 5 
	P.id,
	P.razon_social,
	P.cuit,
	MAX(F.total) as 'Facturacion Total' 
	FROM JARDCOUD.Proveedor P
	JOIN JARDCOUD.Factura F ON P.id = F.proveedor_id
	JOIN JARDCOUD.Item_Factura I ON F.id = I.factura_id
	JOIN JARDCOUD.Oferta O ON O.id = I.oferta_id
	JOIN JARDCOUD.Compra C ON C.oferta_id = O.id	
	
	WHERE C.fecha between @Desde and @Hasta
    GROUP BY P.id, p.razon_social, p.cuit
 	ORDER BY MAX(F.total) DESC 

 END
 GO

 
-- Ahora si ejecutamos el script

BEGIN TRY
	BEGIN TRANSACTION
		EXEC JARDCOUD.limpiar_fk
		EXEC JARDCOUD.limpiar_tablas
		EXEC JARDCOUD.creacion_tablas
		EXEC JARDCOUD.seeder
		EXEC JARDCOUD.migracion
		EXEC JARDCOUD.crear_usuarios_proveedor
		EXEC JARDCOUD.crear_usuarios_cliente

		
	COMMIT
END TRY
BEGIN CATCH	
	ROLLBACK
END CATCH

/*final*/

