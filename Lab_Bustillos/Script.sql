create database Lab_Bustillos

use Lab_Bustillos;

create table Productos(Codigo varchar(35),Descripcion varchar(50),Precio int, Existencias int);

create table Nomina(Rol varchar(35) primary key,Salario varchar(35));

create table Personal(Nombre varchar(35),Edad int,Direccion varchar(35),Telefono varchar(35),Usuario varchar(35),Contraseña varchar(35),Rol varchar(35) foreign key references Nomina(Rol));

create table Ventas(Codigo varchar(35),Descripcion varchar(50),Precio int,Cantidad int, Total int,Fecha varchar(35));

create table RespaldoU(Nombre varchar(35),Edad int,Direccion varchar(35),Telefono varchar(35),Usuario varchar(35),Contraseña varchar(35),Rol varchar(35),Estado varchar(35));

create table RespaldoP(Codigo varchar(35),Descripcion varchar(50),Precio int, Existencias int,Estado varchar(35));

create table Clicks(Nombre varchar(50),Clicks int);

drop table Clicks

insert into Nomina values('Administrador',1000),('Empleado',800);

select * from Personal


create proc AgregarU
@nom varchar(35),@eda int,@dir varchar(35),@tel varchar(35),@usu varchar(35),@contra varchar(35),@Rol varchar(35)
as
insert into Personal values(@nom,@eda,@dir,@tel,@usu,@contra,@Rol);
insert into RespaldoU values(@nom,@eda,@dir,@tel,@usu,@contra,@Rol,'Insertado');
insert into Clicks values(@nom,0);
go

create proc EliminarU
@nom varchar(35),@eda int,@dir varchar(35),@tel varchar(35),@usu varchar(35),@contra varchar(35),@Rol varchar(35)
as
delete from Personal where Nombre=@nom;
insert into RespaldoU values(@nom,@eda,@dir,@tel,@usu,@contra,@Rol,'Eliminado');
go

create proc ModificarU
@nom varchar(35),@eda int,@dir varchar(35),@tel varchar(35),@usu varchar(35),@contra varchar(35),@Rol varchar(35),
@nom1 varchar(35),@eda1 int,@dir1 varchar(35),@tel1 varchar(35),@usu1 varchar(35),@contra1 varchar(35),@Rol1 varchar(35)
as
update Personal set Nombre=@nom,Edad=@eda,Direccion=@dir,Telefono=@tel,Usuario=@usu,Contraseña=@contra,Rol=@Rol where Nombre=@nom;
insert into RespaldoU values(@nom1,@eda1,@dir1,@tel1,@usu1,@contra1,@Rol1,'Modificado');
go



create proc AgregarP
@codi varchar(35),@desc varchar(35),@pre int,@exit int
as
insert into Productos values(@codi,@desc,@pre,@exit);
insert into RespaldoP values(@codi,@desc,@pre,@exit,'Insertado');
go

create proc EliminarP
@codi varchar(35),@desc varchar(35),@pre int,@exit int
as
delete from Productos where Codigo=@codi;
insert into RespaldoP values(@codi,@desc,@pre,@exit,'Eliminado');
go

create proc ModificarP
@codi varchar(35),@desc varchar(35),@pre int,@exit int,
@codi1 varchar(35),@desc1 varchar(35),@pre1 int,@exit1 int
as
update Productos set Descripcion=@desc,Precio=@pre,Existencias=@exit where Codigo=@codi;
insert into RespaldoP values(@codi1,@desc1,@pre1,@exit1,'Modificado');
go

select * from RespaldoP;


select * from Productos



select * from Personal


insert into Personal values('Mario',19,'Tulancingo','7711917153','Mbros','12345','Administrador');

select * from Nomina


delete from Personal

exec AgregarU 'Mario',19,'Tulancingo','7711917153','Mbros','12345','Administrador'

select Personal.Nombre,Personal.Rol, Clicks.Clicks from Personal inner join Clicks on Personal.Nombre=Clicks.Nombre where Personal.Usuario='Mbros' and Personal.Contraseña='12345'