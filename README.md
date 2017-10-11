# ABPCore
ABPCore Travel
This project base on https://aspnetboilerplate.com/

Getting Started
1.Config ABPCore.EntityFrameworkCore /App.config ConnectionString , Register your dbcontext in EFModule
2.Config ABPCore.Web / appsettings.json ConnectionString

1. Without (DDD)Id Primary Key DBContext
2. T4 generate template support  EF Core 2.0 
3. Multiple DBContext
4. I'm trying to supprt mutiple DBContext access in DomainService ,now still throw error
(The specified transaction is not associated with the current connection. Only transactions associated with the current connection may be used.)


