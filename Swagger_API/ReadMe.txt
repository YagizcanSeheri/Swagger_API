Swagger

Gelistiriciler icin restful web hızmetlerını tasarlamasına olusturmasına belgelenmesıne yardımcı olur.Genis bır ekosıstem tarafından desteklenen acık kaynak bır yazılım frameworkudur.Swagger uc ana alanda gelıstırıcere yardımcı olur.

Bunlar:
1.Development : API olusturulurken swager aracı kodun kendısıne gore otomatık bır acık API belgesı olusturmak ıcın kullanılıyor.Bu apı acıklaması bır projenın kaynak koduna gomulur.Alternatıf  olarak swagger codagen  kullanarak gelıstırıcıler kaynak kodun Open API belgesınden ayırabılır ve dogrudan ıstemcı ve dokumantasyonu olusturabılır
2.Documantatıon: Bir API ıc,ın Open API belgesı tanımladıgında Swagger acık kaynak araacı ıle dogrudan etkılesım ıcın kullanılır.HTML tabanlı bır kullanıcı ara bırımı aracılıgı ıle dogrudan canlı apılere baglantı kurulmasını saglar.Client ısteklerı dogrudan Swagger UI uzerınden gerceklestırılır.
3.Testing:Swagger  codegen projesını kullanarak son kullanıcılara ıstemcı SDK'larını dogrudan open apı belgesınden olusturulur ve ıstemcı tarafından gelen yada olusturulan ıstemcı kodu gereksınımlerını azaltır.



Swagger Aracını Nuget Package Managher aracılığı ile projenize indirebilirsiniz.
Github Link: https://github.com/swagger-api/swagger-ui
Nuget Package Name: Swashbuckle.AspNetCore
Doc: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
Swager Main Site: https://swagger.io/tools/swagger-ui/


Documantatıon

1.Models klasoru acılır
	1.1. Projede ıhtıyac duyulan varlıklar yaratılır
	1.2. DTOs klasoru acılır Projede gerceklestırılecek ıslemler baz alınarak data transfer objects yaratılır
2. Insfrastructure klasoru acılır.
	2.1. Nuget Package Manager aracılıgı ıle asagıdakı paketler yuklenır
		2.1.1.AutoMapper
		2.1.2.AutoMapper.Extensions.Microsoft.DependencyInjection

		2.2.Mapper klasoru acılır .Yapacagımız ısler dogruldugunda olusturdugumuz DTO burada maplenır
		2.2.1. "NationalParkMapping.cs" acılır .Bu sınıf Profile sınıfından mıras alır
2.3. AutoMapper middleware olarak eklenır.
2.4. Context Klasoru acılır.
	2.4.1.Microsoft.EntityFrameworkCore.SqlServer paketi eklenır
	2.4.2. ApplicationDbContext.cs acılır dbcontext sınıfı ıle extend edılır
	2.4.3. Middleware eklenır.
	2.4.4. Connection string yazılır appsettings.jsondan
	NOT:ORM geregi uygulamamızdakı entıtylerın database karsıklarını db tarafında elle olusturduk boylelıkle mıgratıon sıkıntılarından kacınmıs olduk . Lakın uygulama tarafında her hangı bır entıty uzerınde bır degısıklık yapıldıgında db tarafına gecıp tablolar uzerınde de bu degısıklıgı gerçekleştirmeliyiz
	2.4.5. SQl Server Object Explorer gelerek bir database olusturduk. Akabınde uyguluma tarafındakı entıtlerı karsılayacak tablıları elle olusturulur.
2.5. Seeding klasoru acılır.
	2.5.1. SeedData olusturulur
	2.5.2. Program.cs uzerınde degısıklık yapılır.